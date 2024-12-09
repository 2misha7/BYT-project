using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class PortfolioPageBeautyProfessionalTests
{
    
    [Test]
    public void Test_AddBeautyProToPortfolioPage_Success()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var portfolioPage = new PortfolioPage("new page");

        portfolioPage.AddBeautyProToPortfolioPage(beautyProfessional);

        Assert.AreEqual(beautyProfessional, portfolioPage.BeautyProfessional);
        Assert.AreEqual(portfolioPage, beautyProfessional.PortfolioPage);
    }

    [Test]
    public void Test_AddBeautyProToPortfolioPage_ExceptionHandling()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var portfolioPage1 = new PortfolioPage("new page");
        var portfolioPage2 = new PortfolioPage("new page2");

        var ex1 = Assert.Throws<ArgumentNullException>(() => portfolioPage1.AddBeautyProToPortfolioPage(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'beautyProfessional')", ex1.Message);

        portfolioPage1.AddBeautyProToPortfolioPage(beautyProfessional);

        var ex2 = Assert.Throws<InvalidOperationException>(() => portfolioPage2.AddBeautyProToPortfolioPage(beautyProfessional));
        Assert.AreEqual("This Beauty Pro already has a PortfolioPage.", ex2.Message);
    }

    [Test]
    public void Test_RemoveBeautyProFromPortfolioPage_Success()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var portfolioPage = new PortfolioPage("new page");

        portfolioPage.AddBeautyProToPortfolioPage(beautyProfessional);
        portfolioPage.RemoveBeautyProFromPortfolioPage();

        Assert.IsNull(portfolioPage.BeautyProfessional);
        Assert.IsNull(beautyProfessional.PortfolioPage);
    }

    [Test]
    public void Test_RemoveBeautyProFromPortfolioPage_ExceptionHandling()
    {
        var portfolioPage = new PortfolioPage("new page");

        var ex = Assert.Throws<InvalidOperationException>(() => portfolioPage.RemoveBeautyProFromPortfolioPage());
        Assert.AreEqual("This PortfolioPage is not assigned to a BeautyPro", ex.Message);
    }

    [Test]
    public void Test_ChangeBeautyProForPortfolioPage_Success()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional1 = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var beautyProfessional2 = new BeautyProfessional("Jack", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", new List<string> { "Hair" },
            new RegularAccountType());
        var portfolioPage = new PortfolioPage("new page");

        portfolioPage.AddBeautyProToPortfolioPage(beautyProfessional1);
        portfolioPage.ChangeBeautyProForPortfolioPage(beautyProfessional2);

        Assert.AreEqual(beautyProfessional2, portfolioPage.BeautyProfessional);
        Assert.AreEqual(portfolioPage, beautyProfessional2.PortfolioPage);
        Assert.IsNull(beautyProfessional1.PortfolioPage);
    }

    [Test]
    public void Test_ChangeBeautyProForPortfolioPage_ExceptionHandling()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional1 = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var beautyProfessional2 = new BeautyProfessional("Jack", "Doe", "jaack.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var portfolioPage = new PortfolioPage("new page");

        var ex1 = Assert.Throws<ArgumentNullException>(() => portfolioPage.ChangeBeautyProForPortfolioPage(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newBeautyPro')", ex1.Message);

        portfolioPage.AddBeautyProToPortfolioPage(beautyProfessional1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => portfolioPage.ChangeBeautyProForPortfolioPage(beautyProfessional1));
        Assert.AreEqual("This BeautyPro is already assigned to this PortfolioPage", ex2.Message);

        portfolioPage.RemoveBeautyProFromPortfolioPage();

        var ex3 = Assert.Throws<InvalidOperationException>(() => portfolioPage.ChangeBeautyProForPortfolioPage(beautyProfessional2));
        Assert.AreEqual("It is not possible to assign a new BeautyPro to this PortfolioPage, because it does not have any", ex3.Message);
    }

    [Test]
    public void Test_AddPortfolioPageToBeautyPro_Success()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var portfolioPage = new PortfolioPage("new page");

        beautyProfessional.AddPortfolioPageToBeautyPro(portfolioPage);

        Assert.AreEqual(portfolioPage, beautyProfessional.PortfolioPage);
        Assert.AreEqual(beautyProfessional, portfolioPage.BeautyProfessional);
    }

    [Test]
    public void Test_AddPortfolioPageToBeautyPro_ExceptionHandling()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var portfolioPage1 = new PortfolioPage("new page");
        var portfolioPage2 =new PortfolioPage("new page");

        var ex1 = Assert.Throws<ArgumentNullException>(() => beautyProfessional.AddPortfolioPageToBeautyPro(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'portfolioPage')", ex1.Message);

        beautyProfessional.AddPortfolioPageToBeautyPro(portfolioPage1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => beautyProfessional.AddPortfolioPageToBeautyPro(portfolioPage2));
        Assert.AreEqual("This Beauty Pro already has a PortfolioPage.", ex2.Message);
    }

    [Test]
    public void Test_RemovePortfolioPageFromBeautyPro_Success()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var portfolioPage = new PortfolioPage("new page");

        beautyProfessional.AddPortfolioPageToBeautyPro(portfolioPage);
        beautyProfessional.RemovePortfolioPageFromBeautyPro();

        Assert.IsNull(beautyProfessional.PortfolioPage);
        Assert.IsNull(portfolioPage.BeautyProfessional);
    }

    [Test]
    public void Test_RemovePortfolioPageFromBeautyPro_ExceptionHandling()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());

        var ex = Assert.Throws<InvalidOperationException>(() => beautyProfessional.RemovePortfolioPageFromBeautyPro());
        Assert.AreEqual("This BeautyPro does not have a PortfolioPage", ex.Message);
    }

    [Test]
    public void Test_ChangePortfolioPageForBeautyPro_Success()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var portfolioPage1 = new PortfolioPage("new page");
        var portfolioPage2 = new PortfolioPage("new page");

        beautyProfessional.AddPortfolioPageToBeautyPro(portfolioPage1);
        beautyProfessional.ChangePortfolioPageForBeautyPro(portfolioPage2);

        Assert.AreEqual(portfolioPage2, beautyProfessional.PortfolioPage);
        Assert.AreEqual(beautyProfessional, portfolioPage2.BeautyProfessional);
        Assert.IsNull(portfolioPage1.BeautyProfessional);
    }

    [Test]
    public void Test_ChangePortfolioPageForBeautyPro_ExceptionHandling()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var portfolioPage1 = new PortfolioPage("new page");
        var portfolioPage2 = new PortfolioPage("new page2");

        var ex1 = Assert.Throws<ArgumentNullException>(() => beautyProfessional.ChangePortfolioPageForBeautyPro(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newPortfolioPage')", ex1.Message);

        beautyProfessional.AddPortfolioPageToBeautyPro(portfolioPage1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => beautyProfessional.ChangePortfolioPageForBeautyPro(portfolioPage1));
        Assert.AreEqual("This PortfolioPage is already assigned to this BeautyPro", ex2.Message);

        beautyProfessional.RemovePortfolioPageFromBeautyPro();

        var ex3 = Assert.Throws<InvalidOperationException>(() => beautyProfessional.ChangePortfolioPageForBeautyPro(portfolioPage2));
        Assert.AreEqual("It is not possible to assign a new PortfolioPage to this BeautyPro, because it does not have any", ex3.Message);
    }
}