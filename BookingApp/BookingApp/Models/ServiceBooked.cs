namespace BookingApp.Models;

public class ServiceBooked(Guid idService, DateTime serviceTime) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdService { get; set; } = idService;

    //check ServiceTime by the Service later, is it possible at all to make a booking
    //assign IdBooking after passing List of ServiceBooked to Booking
    //public Guid IdBooking { get; set; }
    public DateTime ServiceTime { get; set; } = serviceTime;
}