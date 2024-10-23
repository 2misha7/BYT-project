using System.Runtime.InteropServices.JavaScript;
using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class PremiumAccountType : IAccountType
{
    public DateTime StartOfSubscription { get; set; }
    public DateTime EndOfSubscription { get; set; }
    public SubscriptionDuration Duration { get; set; }
    public PremiumAccountType(DateTime startOfSubscription, SubscriptionDuration duration)
    {
        StartOfSubscription = startOfSubscription;
        EndOfSubscription = StartOfSubscription.AddMonths((int)duration);
        Duration = duration;
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


