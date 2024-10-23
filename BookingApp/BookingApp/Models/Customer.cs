using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class Customer(
    string firstName,
    string lastName,
    string email,
    string phoneNumber,
    string login,
    string password,
    string address,
    string city,
    decimal walletBalance,
    IAccountType accountType)
    : Person(firstName, lastName, email, phoneNumber, login, password, address, city, walletBalance)
{
    public IAccountType AccountType { get; set; } = accountType;
    public ICollection<Guid> CustomersInvited { get; set; } = new HashSet<Guid>();
    public ICollection<Guid> Bookings { get; set; } = new HashSet<Guid>();
    public ICollection<Guid> Coupons { get; set; } = new HashSet<Guid>();

    public void UpgradeToPremium(DateTime startOfSubscription, SubscriptionDuration duration)
    {
        AccountType = new PremiumAccountType(startOfSubscription, duration);
    }

    public void DowngradeToRegular() {
        AccountType = new RegularAccountType();
    }
}