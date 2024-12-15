namespace BookingAppTests;

using BookingApp;
using BookingApp.Models;


public class IdInitializationTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
        Repository.GetAllFromFile();
    }
    
    [Test]
    public void WorkStation_IdIsAssignedCorrectly()
    {
        var category = StationCategory.Body;
        var price = 100.00m;

        var firstWorkStation = new WorkStation(category, price);
        var secondWorkStation = new WorkStation(category, 150.00m);

        Assert.AreEqual(1, firstWorkStation.Id);
        Assert.AreEqual(2, secondWorkStation.Id);
    }
    
    [Test]
    public void BeautyProfessional_IdIsAssignedCorrectly()
    {
        // Arrange test parameters for BeautyProfessional instances
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var phoneNumber = "+1234567890";
        var login = "johndoe";
        var password = "securepassword";
        var address = "123 Beauty Street";
        var city = "Beautytown";
        var walletBalance = 100.00m;
        var experience = "5 years";
        var specializations = new List<string> { "Hair Styling", "Makeup" };
        var accountType = new RegularAccountType(); // Assuming StandardAccountType is a concrete implementation of IAccountType

        // Act - Create two BeautyProfessional instances
        var firstBeautyProfessional = new BeautyProfessional(firstName, lastName, email, phoneNumber, login, password, address, city, walletBalance, experience, specializations, accountType);
        var secondBeautyProfessional = new BeautyProfessional(firstName, lastName, email, phoneNumber, login, password, address, city, walletBalance, experience, specializations, accountType);

        // Assert - Check if IDs are assigned incrementally
        Assert.AreEqual(1, firstBeautyProfessional.Id);
        Assert.AreEqual(2, secondBeautyProfessional.Id);
    }
    
    //Coupon
    // Test for ID initialization
    [Test]
    public void Coupon_IdIsAssignedCorrectly()
    {
        // Arrange - create two Coupon instances
        var firstCoupon = new Coupon("CODE1", "First coupon", 10, DateTime.Now, DateTime.Now.AddDays(10));
        var secondCoupon = new Coupon("CODE2", "Second coupon", 20, DateTime.Now, DateTime.Now.AddDays(10));

        // Act & Assert - verify IDs are assigned incrementally
        Assert.AreEqual(1, firstCoupon.Id);
        Assert.AreEqual(2, secondCoupon.Id);
    }
    
    [Test]
    public void CoworkingSpace_IdIsAssignedCorrectly()
    {
        var address = "123 Main St";
        var city = "New York";
        var contactNumber = "+1234567890";

        var firstCoworkingSpace = new CoworkingSpace(address, city, contactNumber);
        var secondCoworkingSpace = new CoworkingSpace(address, city, contactNumber);

        Assert.AreEqual(1, firstCoworkingSpace.Id);
        Assert.AreEqual(2, secondCoworkingSpace.Id);
    }
    [Test]
    public void Customer_IdIsAssignedCorrectly()
    {
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var phoneNumber = "+1234567890";
        var login = "johndoe";
        var password = "securePass123";
        var address = "123 Main St";
        var city = "Cityville";
        var walletBalance = 100.0m;
        var accountType = new RegularAccountType();

        // Create first customer
        var firstCustomer = new Customer(firstName, lastName, email, phoneNumber, login, password, address, city, walletBalance, accountType);

        // Create second customer
        var secondCustomer = new Customer(firstName, lastName, email, phoneNumber, login, password, address, city, walletBalance, accountType);

        // Assert that the IDs are correctly assigned
        Assert.AreEqual(1, firstCustomer.Id);
        Assert.AreEqual(2, secondCustomer.Id);
    }
    
    [Test]
    public void Notification_IdIsAssignedCorrectly()
    {
        string firstText = "First notification";
        string secondText = "Second notification";

        var firstNotification = new Notification(firstText);
        var secondNotification = new Notification(secondText);

        // Check if the IDs are correctly assigned starting from 1 for the first instance
        Assert.AreEqual(1, firstNotification.Id);
        Assert.AreEqual(2, secondNotification.Id);
    }

    [Test]
    public void Payment_IdIsAssignedCorrectly()
    {
        decimal finalAmount = 100.0m;
        string? couponCode = "DISCOUNT10";

        var firstPayment = new Payment(finalAmount, couponCode);
        var secondPayment = new Payment(finalAmount, couponCode);

        // Verify the ID is assigned correctly
        Assert.AreEqual(1, firstPayment.Id);
        Assert.AreEqual(2, secondPayment.Id);
    }
    [Test]
    public void PortfolioPage_IdIsAssignedCorrectly()
    {
        // Arrange
        var description1 = "First portfolio page";
        var description2 = "Second portfolio page";

        var firstPortfolioPage = new PortfolioPage(description1);
        var secondPortfolioPage = new PortfolioPage(description2);

        // Act & Assert
        Assert.AreEqual(1, firstPortfolioPage.Id);
        Assert.AreEqual(2, secondPortfolioPage.Id);
    }
    [Test]
    public void Post_IdIsAssignedCorrectly()
    {
        // Arrange
        var imageLink1 = "https://example.com/image1.jpg";
        var text1 = "First post";
        var imageLink2 = "https://example.com/image2.jpg";
        var text2 = "Second post";

        var firstPost = new Post(imageLink1, text1);
        var secondPost = new Post(imageLink2, text2);

        // Act & Assert
        Assert.AreEqual(1, firstPost.Id);
        Assert.AreEqual(2, secondPost.Id);
    }
    [Test]
    public void Promotion_IdIsAssignedCorrectly()
    {
        // Arrange
        var name1 = "Black Friday Sale";
        var discountDescription1 = "20% off on all items";
        var totalDiscountPercentage1 = 20;
        var name2 = "Cyber Monday Sale";
        var discountDescription2 = "30% off on selected items";
        var totalDiscountPercentage2 = 30;

        var firstPromotion = new Promotion(name1, discountDescription1, totalDiscountPercentage1);
        var secondPromotion = new Promotion(name2, discountDescription2, totalDiscountPercentage2);

        // Act & Assert
        Assert.AreEqual(1, firstPromotion.Id);
        Assert.AreEqual(2, secondPromotion.Id);
    }

    [Test]
    public void Review_IdIsAssignedCorrectly()
    {
        // Arrange
        var rating1 = ReviewRating.VeryGood;
        var comment1 = "Excellent!";
        var date1 = new DateTime(2024, 11, 06);

        var rating2 = ReviewRating.VeryGood;
        var comment2 = "It was okay.";
        var date2 = new DateTime(2024, 11, 07);

        var firstReview = new Review(rating1, comment1, date1);
        var secondReview = new Review(rating2, comment2, date2);

        // Act & Assert
        Assert.AreEqual(1, firstReview.Id);
        Assert.AreEqual(2, secondReview.Id);
    }
    [Test]
    public void Service_IdIsAssignedCorrectly()
    {
        // Arrange
        var service1 = new Service("Haircut", StationCategory.Body, "A simple haircut", 25.0m);
        var service2 = new Service("Facial",StationCategory.Body, "A rejuvenating facial", 50.0m);

        // Act & Assert
        Assert.AreEqual(1, service1.Id);
        Assert.AreEqual(2, service2.Id);
    }
    
    
    [OneTimeTearDown]
    public void TearDown()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
    }
}