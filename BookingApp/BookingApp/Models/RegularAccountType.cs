namespace BookingApp.Models;

public class RegularAccountType : IAccountType
{
    public bool IsSubscriptionActive() => false;
}