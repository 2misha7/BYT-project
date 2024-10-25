
namespace ConsoleApp1.Models;

public class Service(int id, string name, StationCategory serviceCategory, string description, decimal price) : ModelBase<Service>
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public StationCategory ServiceCategory { get; set; } = serviceCategory;
    public string Description { get; set; } = description;
    public decimal Price { get; set; } = price;
    public ICollection<DateTime> AvailableTimeSlots = new HashSet<DateTime>();
}