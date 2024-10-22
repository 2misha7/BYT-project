using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class WorkStation(StationCategory category, decimal price, int coworkingSpaceId)
{
    public int IdWorkStation;
    public StationCategory Category { get; set; } = category;
    public decimal Price { get; set; } = price;
    public int CoworkingSpaceId { get; set; } = coworkingSpaceId;
}