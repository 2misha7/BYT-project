using System.Runtime.InteropServices.JavaScript;
using BookingApp.Models.Enums;
namespace BookingApp.Models;

public class Service(string name, StationCategory serviceCategory, string description, decimal price, Guid workStationId) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public StationCategory ServiceCategory { get; set; } = serviceCategory;
    public string Description { get; set; } = description;
    public decimal Price { get; set; } = price;
    public ICollection<DateTime> AvailableTimeSlots = new HashSet<DateTime>();
    public ICollection<Guid> ServiceBookedId = new HashSet<Guid>();
    public ICollection<Guid> PromotionsId = new HashSet<Guid>();
    public ICollection<Guid> ReviewsId = new HashSet<Guid>();
    public Guid WorkStationId { get; set; } = workStationId;
}