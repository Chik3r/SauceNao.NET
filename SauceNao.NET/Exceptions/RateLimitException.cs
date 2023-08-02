namespace SauceNao.NET.Exceptions; 

public class RateLimitException : Exception {
    public RateLimitReached LimitReached { get; }

    public RateLimitException(RateLimitReached limitReached) : base(GetReason(limitReached)) {
        LimitReached = limitReached;
    }

    private static string GetReason(RateLimitReached limitReached) =>
        limitReached switch {
            RateLimitReached.ShortLimit => "30 second limit reached",
            RateLimitReached.LongLimit => "24 hour limit reached",
            _ => throw new ArgumentOutOfRangeException(nameof(limitReached), limitReached, null)
        };
}