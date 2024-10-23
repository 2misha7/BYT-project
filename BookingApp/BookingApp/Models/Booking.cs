namespace BookingApp.Models;

public class Booking(Guid customerId, ICollection<Guid> serviceBookedId) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    //will be calculated while saving a booking to the system 
    public decimal? TotalPrice { get; set; }
    public Guid CustomerId { get; set; } = customerId;
    public ICollection<Guid> ServiceBookedId { get; set; } = serviceBookedId;
    
}