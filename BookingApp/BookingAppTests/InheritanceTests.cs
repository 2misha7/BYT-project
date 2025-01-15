using BookingApp.Models;

namespace BookingAppTests;
//inheritance
public class InheritanceTests
{
    [Test]
    public void BeautyProfessional_ShouldImplementIPerson()
    {
        var beautyPro = new BeautyProfessional(
            "John", "Doe", "john.doe@example.com", "+123456789",
            "johndoe", "password123", "123 Main St", "City",
            100m, "5 years", new List<string> { "Hair Styling" }, new RegularAccountType()
        );

        Assert.IsInstanceOf<IPerson>(beautyPro);
    }

    [Test]
    public void Customer_ShouldImplementIPerson()
    {
        var customer = new Customer(
            "Jane", "Smith", "jane.smith@example.com", "+987654321",
            "janesmith", "securepassword", "456 Elm St", "Town",
            50m, new PremiumAccountType(DateTime.Now.AddMonths(-1), SubscriptionDuration.Month)
        );

        Assert.IsInstanceOf<IPerson>(customer);
    }
    
    [Test]
    public void PremiumAccountType_ShouldImplementIAccountType()
    {
        var premiumAccount = new PremiumAccountType(DateTime.Now, SubscriptionDuration.Year);
        Assert.IsInstanceOf<IAccountType>(premiumAccount);
    }

    [Test]
    public void RegularAccountType_ShouldImplementIAccountType()
    {
        var regularAccount = new RegularAccountType();
        Assert.IsInstanceOf<IAccountType>(regularAccount);
    }
    
    [Test]
    public void PremiumAccountType_ShouldCalculateSubscriptionDatesCorrectly()
    {
        var startDate = new DateTime(2025, 1, 1);
        var premiumAccount = new PremiumAccountType(startDate, SubscriptionDuration.HalfYear);

        Assert.AreEqual(startDate, premiumAccount.StartOfSubscription);
        Assert.AreEqual(startDate.AddMonths(6), premiumAccount.EndOfSubscription);
        Assert.IsTrue(premiumAccount.IsSubscriptionActive());
    }
    [Test]
    public void CannotSwitchToRegularAccount_WhenPremiumSubscriptionIsActive()
    {
        var customer = new Customer(
            "Alex", "Taylor", "alex.taylor@example.com", "+111222333",
            "alextaylor", "mypassword", "789 Pine St", "Village",
            75m, new PremiumAccountType(DateTime.Now.AddMonths(-1), SubscriptionDuration.Year)
        );

        // Expecting an InvalidOperationException if the account type change is attempted
        Assert.Throws<InvalidOperationException>(() => customer.SetAccountType(new RegularAccountType()));
    }
    [Test]
    public void CanSwitchToRegularAccount_WhenPremiumSubscriptionHasEnded()
    {
        var customer = new Customer(
            "Sam", "Wilson", "sam.wilson@example.com", "+444555666",
            "samwilson", "securepass", "321 Oak St", "City",
            85m, new PremiumAccountType(DateTime.Now.AddYears(-1), SubscriptionDuration.Month)
        );

        // Attempting to change to regular account after the subscription has ended
        Assert.DoesNotThrow(() => customer.SetAccountType(new RegularAccountType()));
    }
}