using Microsoft.Extensions.DependencyInjection;

namespace Bank.Common.Extensions;

public static class ResilienceExtensions
{
    public static IHttpClientBuilder AddResilience(this IHttpClientBuilder clientBuilder)
    {
        clientBuilder.AddStandardResilienceHandler(c =>
        {
            c.Retry.BackoffType = Polly.DelayBackoffType.Exponential;
            c.Retry.Delay = TimeSpan.FromSeconds(1);

            c.CircuitBreaker.FailureRatio = 0.7;
            c.CircuitBreaker.BreakDurationGenerator = args =>
                ValueTask.FromResult(TimeSpan.FromSeconds(args.FailureRate * 10));
        });

        return clientBuilder;
    }
}
