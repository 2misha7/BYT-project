namespace BookingApp.Models;
public class PremiumAccountType(DateTime startOfSubscription, SubscriptionDuration duration)
    : IAccountType
{
    public DateTime StartOfSubscription { get; set; } = startOfSubscription;
    public DateTime EndOfSubscription => StartOfSubscription.AddMonths((int)Duration);
    public SubscriptionDuration Duration { get; set; } = duration;
    
    public bool IsSubscriptionActive()
    {
        return DateTime.Now <= EndOfSubscription;
    }
}

public enum SubscriptionDuration
{
    Month = 1,
    HalfYear = 6,
    Year = 12
}
