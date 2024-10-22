using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class BeautyProfessional(
    string firstName,
    string lastName,
    string email,
    string phoneNumber,
    string login,
    string password,
    string address,
    string city,
    decimal walletBalance,
    List<string> specializations,
    string experience,
    IAccountType accountType)
    : Person(firstName, lastName, email, phoneNumber, login, password, address, city, walletBalance)
{
    
    public string Experience { get; set; } = experience;
    public List<string> Specializations { get; set; } = specializations;
    public IAccountType AccountType { get; set; } = accountType;
    
    public void UpgradeToPremium(DateTime startOfSubscription, SubscriptionDuration duration)
    {
        AccountType = new PremiumAccountType(startOfSubscription, duration);
    }

    public void DowngradeToRegular() {
        AccountType = new RegularAccountType();
    }
}