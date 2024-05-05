using Microsoft.AspNetCore.Http;

namespace Bank.Idempotency.Middlewares
{
    public class IdempotencyEnforcingMiddleware
    {
        private readonly RequestDelegate _next;

        public IdempotencyEnforcingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IActionService actionService)
        {
            // since other http methods are idempotent by definition
            if (
                context.Request.Method != HttpMethod.Post.Method
                && context.Request.Method != HttpMethod.Patch.Method
            )
            {
                await _next(context);
                return;
            }

            await HandleNonIdempotentRequest(context, actionService);
        }

        private async Task HandleNonIdempotentRequest(HttpContext context, IActionService actionService)
        {
            Guid idempotencyKey = RetrieveIdempotencyKey(context)
                ?? throw new InvalidDataException("Idempotency key is either absent or malformed");

            var actionDescriptor = new ActionDescriptor() { Name = context.Request.Path.Value ?? "", IdempotencyKey = idempotencyKey };

            var result = await actionService.WasActionExecuted(actionDescriptor);
            if (result)
            {
                context.Response.StatusCode = 200;
                return;
            }

            await _next(context);

            if (!IsSuccessStatusCode(context.Response)) return;

            await actionService.StoreAction(actionDescriptor);
        }

        private static Guid? RetrieveIdempotencyKey(HttpContext context)
        {
            bool headerResult = context.Request.Headers.TryGetValue(IdempotencyConstants.Header, out var headerValue);
            if (headerResult == false) return null;

            if (string.IsNullOrEmpty(headerValue.First())) return null;

            bool parseResult = Guid.TryParse(headerValue.First(), out Guid idempotencyKey);
            if (parseResult == false) return null;

            return idempotencyKey;
        }

        private static bool IsSuccessStatusCode(HttpResponse response)
            => response.StatusCode >= 200 && response.StatusCode <= 299;
    }
}
