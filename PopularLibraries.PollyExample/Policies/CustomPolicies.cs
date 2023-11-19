using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Retry;

namespace PopularLibraries.PollyExample.Policies;

public static class CustomPolicies
{
    public static AsyncRetryPolicy<HttpResponseMessage> RetryPolicy(int retryCount, int delayInMilliseconds)
    {
        IEnumerable<TimeSpan> delays = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromMilliseconds(delayInMilliseconds),  retryCount);
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(delays);
    }
}