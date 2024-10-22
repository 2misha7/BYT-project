using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class WorkStation(StationCategory category, decimal price, int coworkingSpaceId) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public StationCategory Category { get; set; } = category;
    public decimal Price { get; set; } = price;
    public int CoworkingSpaceId { get; set; } = coworkingSpaceId;
    public IDictionary<DateTime, int> ScheduleOfReservations = new Dictionary<DateTime, int>();
   
}