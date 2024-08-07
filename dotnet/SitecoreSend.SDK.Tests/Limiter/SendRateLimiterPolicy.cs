﻿using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.RateLimit;

namespace SitecoreSend.SDK.Tests.Limiter;

public static class SendRateLimits
{
    public static readonly AsyncPolicy<SendResponse<IList<Subscriber>>?> AddMultipleSubscribers =
        new SendRateLimiterPolicy<SendResponse<IList<Subscriber>>?>(2, TimeSpan.FromSeconds(10)).Instance;

    public static readonly AsyncPolicy<SendResponse<Subscriber>?> AddSubscriber =
        new SendRateLimiterPolicy<SendResponse<Subscriber>?>(10, TimeSpan.FromSeconds(10)).Instance;

    public static readonly AsyncPolicy<SendResponse?> UnsubscribeFromAllLists =
        new SendRateLimiterPolicy<SendResponse?>(10, TimeSpan.FromSeconds(20)).Instance;

    public static readonly AsyncPolicy<SendResponse?> UnsubscribeFromList =
        new SendRateLimiterPolicy<SendResponse?>(10, TimeSpan.FromSeconds(20)).Instance;

    public static readonly AsyncPolicy<SendResponse?> UnsubscribeFromListAndCampaign =
        new SendRateLimiterPolicy<SendResponse?>(10, TimeSpan.FromSeconds(20)).Instance;
}

public class SendRateLimiterPolicy<T> where T : SendResponse?
{
    public AsyncPolicy<T> Instance { get; private set; }

    public SendRateLimiterPolicy(int numberOfExecutions,
        TimeSpan perTimeSpan)
    {
        Instance = Policy.WrapAsync(PoliciesHelper.RetryRateLimit<T>(),
            PoliciesHelper.RetryRateLimitException<T>(numberOfExecutions, perTimeSpan),
            PoliciesHelper.ConfigureRateLimit<T>(numberOfExecutions, perTimeSpan));
    }
}

public static class PoliciesHelper
{
    public static AsyncPolicy<T> ConfigureRateLimit<T>(int numberOfExecutions,
        TimeSpan perTimeSpan)
    {
        return Policy
                .RateLimitAsync<T>(numberOfExecutions, perTimeSpan)
            ;
    }

    public static AsyncPolicy<T> RetryRateLimitException<T>(int numberOfExecutions, TimeSpan perTimeSpan)
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: perTimeSpan.Divide(numberOfExecutions) * 1.5,
            retryCount: 5);
        return Policy<T>.Handle<RateLimitRejectedException>()
            .WaitAndRetryAsync(delay);
    }

    public static AsyncPolicy<T> RetryRateLimit<T>() where T : SendResponse?
    {
        return Policy
            .HandleResult<T>((response) => response?.RateLimitDetails != null)
            .WaitAndRetryAsync(5, (i, result, _) =>
            {
                if (result.Result?.RateLimitDetails?.Expires != null)
                {
                    var utcNow = DateTimeOffset.UtcNow;
                    return result.Result.RateLimitDetails.Expires.Value.Subtract(utcNow) +
                           TimeSpan.FromMilliseconds(i * 50);
                }

                return TimeSpan.FromMilliseconds(i * 50);
            }, (_, _, _, _) => Task.CompletedTask);
    }
}