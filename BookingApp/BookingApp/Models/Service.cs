using BookingApp.Models.Enums;
namespace BookingApp.Models;

public class Service(string name, StationCategory serviceCategory, string description, decimal price)
{
    public int IdService;
    public string Name { get; set; } = name;
    public StationCategory ServiceCategory { get; set; } = serviceCategory;
    public string Description { get; set; } = description;
    public decimal Price { get; set; } = price;
}