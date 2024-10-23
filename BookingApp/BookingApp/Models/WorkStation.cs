using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class WorkStation(StationCategory category, decimal price, Guid coworkingSpaceId) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public StationCategory Category { get; set; } = category;
    public decimal Price { get; set; } = price;
    public Guid CoworkingSpaceId { get; set; } = coworkingSpaceId;
    public IDictionary<DateTime, Guid> ScheduleOfReservations = new Dictionary<DateTime, Guid>();
   
}