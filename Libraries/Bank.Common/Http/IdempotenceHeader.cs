using Bank.Common.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Common.Http;

public static class IdempotenceHeader
{
    public const string Header = "Idempotence-Key";

    public static Guid GetIdempotenceKey(this HttpContext context)
    {
        var val = context.Request.Headers[Header];
        Validation.Check(Header, !string.IsNullOrEmpty(val), $"'{Header}' header is missing.");
        Validation.Check(
            Header,
            Guid.TryParse(val, out var key),
            $"'{Header}' header is in invalid format."
        );
        return key;
    }
}
