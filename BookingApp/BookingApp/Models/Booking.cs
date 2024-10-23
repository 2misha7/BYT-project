namespace BookingApp.Models;

public class Booking(Guid customerId, ICollection<Guid> serviceBookedId) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal TotalPrice { get; set; } = 0;
    public Guid CusomerId { get; set; } = customerId;
    public ICollection<Guid> ServiceBookedId { get; set; } = serviceBookedId;

    //find total price 
}