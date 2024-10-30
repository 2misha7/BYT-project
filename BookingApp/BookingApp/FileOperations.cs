using System.Text.Json;
using BookingApp.Models;

namespace BookingApp;

public static class FileOperations
{
    private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data.json");

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
        
        try
        { 
            File.WriteAllText(FilePath, JsonSerializer.Serialize(data, options));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }


    public static void LoadAll()
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading all entities: {ex.Message}");
        }
    }
    
}