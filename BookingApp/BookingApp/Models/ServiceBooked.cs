namespace BookingApp.Models;

public class ServiceBooked : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdService { get; set; }
    public Guid IdBooking { get; set; }
    public DateTime ServiceTime { get; set; }
}