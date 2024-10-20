using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class PremiumAccountType : IAccountType
{
    private DateTime StartOfSubscription { get; set; }
    private DateTime EndOfSubscription { get; set; }

    public PremiumAccountType(DateTime startOfSubscription, SubscriptionDuration duration)
    {
        StartOfSubscription = startOfSubscription;
        EndOfSubscription = StartOfSubscription.AddMonths((int)duration);
    }
    
    public bool IsSubscriptionActive()
    {
        return DateTime.Now <= EndOfSubscription;
    }

    public void ContinueSubscription(int months)
    {
        EndOfSubscription = EndOfSubscription.AddMonths(months);
    }
        
}


