using BookingApp;
using BookingApp.Models;
namespace BookingAppTests;

//tests for all getter and setter and exception
// tests for all class extents is everything saved and stored correctly

public class AttributeCorrectnessTests
{
    //Check if every attribute in your diagram was implemented correctly :
    // ○ One test to check if gets the correct information back
    // ○ A few tests (depending on the amount of exceptions) to showcase if the exceptions are being
    //     thrown correctly 

    [OneTimeSetUp]
    public void SetUp()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
        Repository.GetAllFromFile();
    }

    private ICollection<string> validSpecializations = new List<string> { "Hair", "Makeup" };
    private IAccountType accountType = new RegularAccountType();

    //WorkStation 
    //one test to check if we get correct info back
    [Test]
    public void WorkstationCreate_GetCorrectInfoBack()
    {
        var category = StationCategory.Hair;
        decimal price = 50;

        var workStation = new WorkStation(category, price);

        Assert.That(workStation.Category, Is.EqualTo(category));
        Assert.That(workStation.Price, Is.EqualTo(price));
    }

    //check all exceptions, not possible to create an object without passing validation
    [Test]
    public void Workstation_ThrowsExceptionWithNegativePrice()
    {
        StationCategory category = StationCategory.Makeup;
        decimal negativePrice = (decimal)-10.0;

        var ex = Assert.Throws<ArgumentException>(() => new WorkStation(category, negativePrice));
        Assert.AreEqual("Price cannot be negative", ex.Message);
    }

    

    //BeautyProfessional
    //one test to check if we get correct info back
    [Test]
    public void BeautyProfessional_Create_ValidAttributes_ReturnsCorrectInfo()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());

        Assert.AreEqual("Jane", beautyProfessional.FirstName);
        Assert.AreEqual("Doe", beautyProfessional.LastName);
        Assert.AreEqual("jane.doe@example.com", beautyProfessional.Email);
        Assert.AreEqual("+1234567890", beautyProfessional.PhoneNumber);
        Assert.AreEqual("janedoe", beautyProfessional.Login);
        Assert.AreEqual("securePass123", beautyProfessional.Password);
        Assert.AreEqual("123 Main St", beautyProfessional.Address);
        Assert.AreEqual("Cityville", beautyProfessional.City);
        Assert.AreEqual(100.0m, beautyProfessional.WalletBalance);
        Assert.AreEqual("5 years", beautyProfessional.Experience);
        Assert.AreEqual(specializations, beautyProfessional.Specializations);
    }

    [Test]
    //check all exceptions, not possible to create an object without passing validation
    public void BeautyProfessional_ThrowsExceptionOnEmptyFirstName()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", new List<string> { "Hair" },
                new RegularAccountType()));
        Assert.AreEqual("First name cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnInvalidEmail()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "invalid-email", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", new List<string> { "Hair" },
                new RegularAccountType()));
        Assert.AreEqual("Invalid email format", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnEmptyEmail()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", new List<string> { "Hair" },
                new RegularAccountType()));
        Assert.AreEqual("Email cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnEmptyLastName()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", new List<string> { "Hair" },
                new RegularAccountType()));
        Assert.AreEqual("Last name cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnEmptyPhoneNumber()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", validSpecializations,
                accountType));
        Assert.AreEqual("Phone number cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnInvalidPhoneNumberFormat()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "0123",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", validSpecializations,
                accountType));
        Assert.AreEqual("Invalid phone number format", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnEmptyLogin()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", validSpecializations, accountType));
        Assert.AreEqual("Login cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnEmptyPassword()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "", "123 Main St", "Cityville", 100.0m, "5 years", validSpecializations, accountType));
        Assert.AreEqual("Password cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnEmptyAddress()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "", "Cityville", 100.0m, "5 years", validSpecializations, accountType));
        Assert.AreEqual("Address cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnEmptyCity()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "", 100.0m, "5 years", validSpecializations, accountType));
        Assert.AreEqual("City cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnNegativeWalletBalance()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", -10.0m, "5 years", validSpecializations,
                accountType));
        Assert.AreEqual("Wallet balance cannot be negative", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnEmptyExperience()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "", validSpecializations, accountType));
        Assert.AreEqual("Experience cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnEmptySpecializations()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", new List<string>(),
                accountType));
        Assert.AreEqual("Specializations cannot be empty", ex.Message);
    }

    [Test]
    public void BeautyProfessional_ThrowsExceptionOnNullAccountType()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", validSpecializations, null));
        Assert.AreEqual("Account type cannot be null", ex.Message);
    }
    
   
    

    //Booking 
    //only id, because no setters, no exceptions
    [Test]
    public void Booking_IdIsAssignedCorrectly()
    {
        // Arrange - create two Booking instances
        var firstBooking = new Booking();
        var secondBooking = new Booking();

        // Act & Assert - verify IDs are assigned incrementally
        Assert.AreEqual(1, firstBooking.Id);
        Assert.AreEqual(2, secondBooking.Id);
    }

    //Coupon
    
    // Test for DiscountPercentage out of bounds (negative value)
    [Test]
    public void Coupon_ThrowsException_WhenDiscountPercentageIsNegative()
    {
        // Arrange
        var couponCode = "DISCOUNT20";
        var description = "20% off";
        var validFrom = DateTime.Now;
        var validTo = DateTime.Now.AddDays(10);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new Coupon(couponCode, description, -5, validFrom, validTo));
        Assert.AreEqual("Discount percentage must be between 0 and 100", ex.Message);
    }
    
    [Test]
    public void Coupon_ThrowsException_CouponCodeEmpty()
    {
        // Arrange
        var couponCode = "";
        var description = "20% off";
        var validFrom = DateTime.Now;
        var validTo = DateTime.Now.AddDays(10);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new Coupon(couponCode, description, 5, validFrom, validTo));
        Assert.AreEqual("Coupon code cannot be empty", ex.Message);
    }
    
    [Test]
    public void Coupon_ThrowsException_DescriptionEmpty()
    {
        // Arrange
        var couponCode = "123";
        var description = "";
        var validFrom = DateTime.Now;
        var validTo = DateTime.Now.AddDays(10);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new Coupon(couponCode, description, 5, validFrom, validTo));
        Assert.AreEqual("Description cannot be empty", ex.Message);
    }

    // Test for DiscountPercentage out of bounds (greater than 100)
    [Test]
    public void Coupon_ThrowsException_WhenDiscountPercentageIsGreaterThan100()
    {
        // Arrange
        var couponCode = "DISCOUNT50";
        var description = "50% off";
        var validFrom = DateTime.Now;
        var validTo = DateTime.Now.AddDays(10);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new Coupon(couponCode, description, 150, validFrom, validTo));
        Assert.AreEqual("Discount percentage must be between 0 and 100", ex.Message);
    }

    // Test for ValidTo date being earlier than ValidFrom date
    [Test]
    public void Coupon_ThrowsException_WhenValidToIsEarlierThanValidFrom()
    {
        // Arrange
        var couponCode = "DISCOUNT10";
        var description = "10% off";
        var discountPercentage = 10;
        var validFrom = DateTime.Now;
        var validTo = DateTime.Now.AddDays(-5); // earlier than validFrom

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new Coupon(couponCode, description, discountPercentage, validFrom, validTo));
        Assert.AreEqual("ValidTo date cannot be earlier than ValidFrom date", ex.Message);
    }

    [Test]
    public void Coupon_AttributesAssignedCorrectly()
    {
        // Arrange
        var couponCode = "DISCOUNT30";
        var description = "30% off";
        var discountPercentage = 30;
        var validFrom = DateTime.Now;
        var validTo = DateTime.Now.AddDays(15);

        // Act
        var coupon = new Coupon(couponCode, description, discountPercentage, validFrom, validTo);

        // Assert
        Assert.AreEqual(couponCode, coupon.CouponCode);
        Assert.AreEqual(description, coupon.Description);
        Assert.AreEqual(discountPercentage, coupon.DiscountPercentage);
        Assert.AreEqual(validFrom, coupon.ValidFrom);
        Assert.AreEqual(validTo, coupon.ValidTo);
    }

    //CoworkingSpace
    [Test]
    public void CoworkingSpace_ThrowsException_WhenAddressIsEmpty()
    {
        // Arrange
        var address = "";
        var city = "New York";
        var contactNumber = "+1234567890";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new CoworkingSpace(address, city, contactNumber));
        Assert.AreEqual("Address cannot be empty", ex.Message);
    }
    [Test]
    public void CoworkingSpace_ThrowsException_WhenCityIsEmpty()
    {
        // Arrange
        var address = "123 Main St";
        var city = "";
        var contactNumber = "+1234567890";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new CoworkingSpace(address, city, contactNumber));
        Assert.AreEqual("City cannot be empty", ex.Message);
    }
    [Test]
    public void CoworkingSpace_ThrowsException_WhenContactNumberIsEmpty()
    {
        // Arrange
        var address = "123 Main St";
        var city = "New York";
        var contactNumber = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new CoworkingSpace(address, city, contactNumber));
        Assert.AreEqual("Contact number cannot be empty", ex.Message);
    }
    [Test]
    public void CoworkingSpace_ThrowsException_WhenContactNumberHasInvalidFormat()
    {
        // Arrange
        var address = "123 Main St";
        var city = "New York";
        var contactNumber = "012345"; // Invalid format

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new CoworkingSpace(address, city, contactNumber));
        Assert.AreEqual("Invalid contact number format", ex.Message);
    }
    
    [Test]
    public void CoworkingSpace_AttributesAssignedCorrectly()
    {
        // Arrange
        var address = "123 Main St";
        var city = "New York";
        var contactNumber = "+1234567890";

        // Act
        var coworkingSpace = new CoworkingSpace(address, city, contactNumber);

        // Assert
        Assert.AreEqual(address, coworkingSpace.Address);
        Assert.AreEqual(city, coworkingSpace.City);
        Assert.AreEqual(contactNumber, coworkingSpace.ContactNumber);
    }

    //Customer
    [Test]
    public void Customer_AttributesAssignedCorrectly()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var phoneNumber = "+1234567890";
        var login = "johndoe";
        var password = "Password123";
        var address = "123 Main St";
        var city = "New York";
        var walletBalance = 100.00m;
        IAccountType accountType = new RegularAccountType(); // Assuming RegularAccount is an implementation of IAccountType

        // Act
        var customer = new Customer(firstName, lastName, email, phoneNumber, login, password, address, city, walletBalance, accountType);

        // Assert
        Assert.AreEqual(firstName, customer.FirstName);
        Assert.AreEqual(lastName, customer.LastName);
        Assert.AreEqual(email, customer.Email);
        Assert.AreEqual(phoneNumber, customer.PhoneNumber);
        Assert.AreEqual(login, customer.Login);
        Assert.AreEqual(password, customer.Password);
        Assert.AreEqual(address, customer.Address);
        Assert.AreEqual(city, customer.City);
        Assert.AreEqual(walletBalance, customer.WalletBalance);
        Assert.AreEqual(accountType, customer.AccountType);
    }
    [Test]
        public void Customer_ThrowsException_WhenFirstNameIsEmpty()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "securePass123", "123 Main St", "Cityville", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("First name cannot be empty", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenLastNameIsEmpty()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "", "john.doe@example.com", "+1234567890", "johndoe", "securePass123", "123 Main St", "Cityville", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("Last name cannot be empty", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenEmailIsEmpty()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "", "+1234567890", "johndoe", "securePass123", "123 Main St", "Cityville", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("Email cannot be empty", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenInvalidEmailFormat()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "invalid-email", "+1234567890", "johndoe", "securePass123", "123 Main St", "Cityville", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("Invalid email format", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenPhoneNumberIsEmpty()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "john.doe@example.com", "", "johndoe", "securePass123", "123 Main St", "Cityville", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("Phone number cannot be empty", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenInvalidPhoneNumberFormat()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "john.doe@example.com", "012345", "johndoe", "securePass123", "123 Main St", "Cityville", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("Invalid phone number format", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenLoginIsEmpty()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "", "securePass123", "123 Main St", "Cityville", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("Login cannot be empty", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenPasswordIsEmpty()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "", "123 Main St", "Cityville", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("Password cannot be empty", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenAddressIsEmpty()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "securePass123", "", "Cityville", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("Address cannot be empty", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenCityIsEmpty()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "securePass123", "123 Main St", "", 100.0m, new RegularAccountType())
            );
            Assert.AreEqual("City cannot be empty", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenWalletBalanceIsNegative()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "securePass123", "123 Main St", "Cityville", -50.0m, new RegularAccountType())
            );
            Assert.AreEqual("Wallet balance cannot be negative", ex.Message);
        }

        [Test]
        public void Customer_ThrowsException_WhenAccountTypeIsNull()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "securePass123", "123 Main St", "Cityville", 100.0m, null)
            );
            Assert.AreEqual("Account type cannot be null", ex.Message);
        }
    


    //Notification
    [Test]
    public void NotificationCreate_GetCorrectInfoBack()
    {
        string text = "Test Notification";

        var notification = new Notification(text);

        Assert.That(notification.Text, Is.EqualTo(text));
    }

    
    [Test]
    public void Notification_ThrowsExceptionWhenTextIsEmpty()
    {
        string emptyText = "";

        var ex = Assert.Throws<ArgumentException>(() => new Notification(emptyText));
        Assert.AreEqual("Text cannot be empty", ex.Message);
    }

    //Payment
    [Test]
    public void Payment_ThrowsExceptionWithNegativeFinalAmount()
    {
        string? couponCode = "DISCOUNT10";
        decimal negativeAmount = -50.0m;

        var ex = Assert.Throws<ArgumentException>(() => new Payment(negativeAmount, couponCode));
        Assert.AreEqual("Final amount cannot be negative", ex.Message);
    }

    
    // Test to check if exception is thrown for invalid CouponCode (empty or whitespace)
    [Test]
    public void Payment_ThrowsExceptionWithEmptyCouponCode()
    {
        decimal finalAmount = 100.0m;
        string? emptyCouponCode = "  ";

        var ex = Assert.Throws<ArgumentException>(() => new Payment(finalAmount, emptyCouponCode));
        Assert.AreEqual("Coupon code cannot be empty or whitespace.", ex.Message);
    }

    [Test]
    public void Payment_CreatedCorrectlyWithoutCouponCode()
    {
        decimal finalAmount = 100.0m;
        string? couponCode = null;

        var payment = new Payment(finalAmount, couponCode);

        Assert.AreEqual(finalAmount, payment.FinalAmount);
        Assert.AreEqual(couponCode, payment.CouponCode);
        Assert.AreEqual(PaymentStatus.Pending, payment.Status);
    }
    [Test]
    public void Payment_AttributesAssignedCorrectly()
    {
        // Arrange
        decimal finalAmount = 100.0m;
        string? couponCode = "DISCOUNT10";
        PaymentStatus status = PaymentStatus.Pending;

        // Act
        var payment = new Payment(finalAmount, couponCode);

        // Assert
        Assert.AreEqual(finalAmount, payment.FinalAmount);
        Assert.AreEqual(couponCode, payment.CouponCode);
        Assert.AreEqual(status, payment.Status);
    }
    //PortfolioPage
    [Test]
    public void PortfolioPage_AttributesAssignedCorrectly()
    {
        // Arrange
        var description = "This is a portfolio page description";

        // Act
        var portfolioPage = new PortfolioPage(description);

        // Assert
        Assert.AreEqual(description, portfolioPage.Description);
    }
    [Test]
    public void PortfolioPage_ThrowsExceptionWithEmptyDescription()
    {
        // Arrange
        var emptyDescription = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PortfolioPage(emptyDescription));
        Assert.AreEqual("Description cannot be empty", ex.Message);
    }


    //Post
    [Test]
    public void Post_AttributesAssignedCorrectly()
    {
        // Arrange
        var imageLink = "https://example.com/image.jpg";
        var text = "This is a post text";

        // Act
        var post = new Post(imageLink, text);

        // Assert
        Assert.AreEqual(imageLink, post.ImageLink);
        Assert.AreEqual(text, post.Text);
        Assert.AreEqual(0, post.Likes);
        Assert.AreEqual(0, post.Dislikes);
        Assert.AreEqual(0, post.Comments.Count);
    }
    [Test]
    public void Post_ThrowsExceptionWithEmptyImageLink()
    {
        // Arrange
        var emptyImageLink = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Post(emptyImageLink, "Some text"));
        Assert.AreEqual("Image link cannot be empty", ex.Message);
    }
    [Test]
    public void Post_ThrowsExceptionWithEmptyText()
    {
        // Arrange
        var emptyText = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Post("https://example.com/image.jpg", emptyText));
        Assert.AreEqual("Text cannot be empty", ex.Message);
    }
    
    //Promotion
    [Test]
    public void Promotion_AttributesAssignedCorrectly()
    {
        // Arrange
        var name = "Black Friday Sale";
        var discountDescription = "20% off on all items";
        var totalDiscountPercentage = 20;

        // Act
        var promotion = new Promotion(name, discountDescription, totalDiscountPercentage);

        // Assert
        Assert.AreEqual(name, promotion.Name);
        Assert.AreEqual(discountDescription, promotion.DiscountDescription);
        Assert.AreEqual(totalDiscountPercentage, promotion.TotalDiscountPercentage);
        
    }
    [Test]
    public void Promotion_ThrowsExceptionWithEmptyName()
    {
        // Arrange
        var emptyName = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Promotion(emptyName, "Discount description", 10));
        Assert.AreEqual("Name cannot be empty", ex.Message);
    }
    [Test]
    public void Promotion_ThrowsExceptionWithEmptyDiscountDescription()
    {
        // Arrange
        var emptyDescription = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Promotion("Sale", emptyDescription, 10));
        Assert.AreEqual("Description cannot be empty", ex.Message);
    }

    [Test]
    public void Promotion_ThrowsExceptionWithDiscountBelowMin()
    {
        // Arrange
        var lowDiscountPercentage = 4;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Promotion("Sale", "Discount description", lowDiscountPercentage));
        Assert.AreEqual("Total discount percentage must be between 5 and 35.", ex.Message);
    }
    [Test]
    public void Promotion_ThrowsExceptionWithDiscountAboveMax()
    {
        // Arrange
        var highDiscountPercentage = 41;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Promotion("Sale", "Discount description", highDiscountPercentage));
        Assert.AreEqual("Total discount percentage must be between 5 and 35.", ex.Message);
    }

    //review
    [Test]
    public void Review_AttributesAssignedCorrectly()
    {
        // Arrange
        var rating = ReviewRating.VeryGood; // Assuming ReviewRating is an enum
        var comment = "Great service!";
        var date = new DateTime(2024, 11, 06);

        // Act
        var review = new Review(rating, comment, date);

        // Assert
        Assert.AreEqual(rating, review.Rating);
        Assert.AreEqual(comment, review.Comment);
        Assert.AreEqual(date, review.Date);
    }

    // Test to check if an exception is thrown when the comment is empty
    [Test]
    public void Review_ThrowsExceptionWithEmptyComment()
    {
        // Arrange
        var emptyComment = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Review(ReviewRating.VeryGood, emptyComment, DateTime.Now));
        Assert.AreEqual("Comment cannot be empty", ex.Message);
    }

    //Service
    [Test]
    public void Service_AttributesAssignedCorrectly()
    {
        // Arrange
        var name = "Facial";
        var serviceCategory = StationCategory.Body; 
        var description = "A rejuvenating facial treatment.";
        var price = 50.0m;

        // Act
        var service = new Service(name, serviceCategory, description, price);

        // Assert
        Assert.AreEqual(name, service.Name);
        Assert.AreEqual(serviceCategory, service.ServiceCategory);
        Assert.AreEqual(description, service.Description);
        Assert.AreEqual(price, service.Price);
    }

    [Test]
    public void Service_ThrowsExceptionWithEmptyName()
    {
        // Arrange
        var emptyName = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Service(emptyName, StationCategory.Body, "A description", 50.0m));
        Assert.AreEqual("Name cannot be empty", ex.Message);
    }

    // Test to check if an exception is thrown when the price is negative
    [Test]
    public void Service_ThrowsExceptionWithNegativePrice()
    {
        // Arrange
        var negativePrice = -1.0m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Service("Massage", StationCategory.Body, "A relaxing massage.", negativePrice));
        Assert.AreEqual("Price cannot be negative", ex.Message);
    }
    //ServiceBooked

    [Test]
    public void ServiceBooked_AttributesAssignedCorrectly()
    {
        var serviceTime = DateTime.Now.AddHours(1); 
        var serviceBooked = new ServiceBooked(serviceTime);
        Assert.AreEqual(serviceTime, serviceBooked.ServiceTime);
    }

    [Test]
    public void ServiceBooked_ThrowsExceptionWithPastServiceTime()
    {
        var pastServiceTime = DateTime.Now.AddHours(-1); // Past time
        var ex = Assert.Throws<ArgumentException>(() => new ServiceBooked(pastServiceTime));
        Assert.AreEqual("Service time cannot be in the past", ex.Message);
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
    }
}