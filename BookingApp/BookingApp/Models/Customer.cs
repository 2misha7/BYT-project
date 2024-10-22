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
    IAccountType accountType,
    List<Customer> invitedCustomers)
    : Person(firstName, lastName, email, phoneNumber, login, password, address, city, walletBalance)
{
    public int IdCustomer;
    public IAccountType AccountType { get; set; } = accountType;
    public List<Customer> InvitedCustomers { get; set; } = [];

    public void UpgradeToPremium(DateTime startOfSubscription, SubscriptionDuration duration)
    {
        AccountType = new PremiumAccountType(startOfSubscription, duration);
    }

    public void DowngradeToRegular() {
        AccountType = new RegularAccountType();
    }
}