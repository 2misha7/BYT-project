namespace BookingApp.Models;

public class ServiceBooked : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int IdService { get; set; }
    public int IdBooking { get; set; }
    public DateTime ServiceTime { get; set; }
}