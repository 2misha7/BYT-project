namespace BookingApp.Models;

public class RegularAccountType : IAccountType
{
    public bool IsSubscriptionActive() => false;
    private static int _maxNumberOfBookingsPerMonth = 5;
}