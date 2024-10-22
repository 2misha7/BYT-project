namespace BookingApp.Models;

public class Booking : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal TotalPrice;
    public ICollection<int> ServiceBookedId = new HashSet<int>();
    
}