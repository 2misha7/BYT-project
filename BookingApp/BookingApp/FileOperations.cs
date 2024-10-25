using System.Text.Json;
using ConsoleApp1.Models;

namespace ConsoleApp1;

public class FileOperations
{
    private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\data.json");

    public static void SaveAll()
    {
        var data = new Dictionary<string, object>
        {
            {nameof(BeautyProfessional), BeautyProfessional.GetAll()},
            {nameof(Booking), Booking.GetAll()}, 
            {nameof(Coupon), Coupon.GetAll()}, 
            {nameof(CoworkingSpace), CoworkingSpace.GetAll()}, 
            {nameof(Customer), Customer.GetAll()}, 
            {nameof(Notification), Notification.GetAll()}, 
            {nameof(Payment), Payment.GetAll()}, 
            {nameof(PortfolioPage), PortfolioPage.GetAll()}, 
            {nameof(Post), Post.GetAll()}, 
            {nameof(Promotion), Promotion.GetAll()}, 
            {nameof(Review), Review.GetAll()}, 
            {nameof(Service), Service.GetAll()},
            {nameof(ServiceBooked), ServiceBooked.GetAll()}, 
            {nameof(WorkStation), WorkStation.GetAll()}, 
        };
        var options = new JsonSerializerOptions
        {
            Converters = { new AccountTypeConverter() },
            WriteIndented = true 
        };
        
        File.WriteAllText(FilePath, JsonSerializer.Serialize(data, options));
    }


    public static void LoadAll()
    {
        BeautyProfessional.LoadFromFile();
        Booking.LoadFromFile();
        Coupon.LoadFromFile();
        CoworkingSpace.LoadFromFile();
        Customer.LoadFromFile();
        Notification.LoadFromFile();
        Payment.LoadFromFile();
        PortfolioPage.LoadFromFile();
        Post.LoadFromFile();
        Promotion.LoadFromFile();
        Review.LoadFromFile(); 
        Service.LoadFromFile();
        ServiceBooked.LoadFromFile();
        WorkStation.LoadFromFile();
    }
    
}