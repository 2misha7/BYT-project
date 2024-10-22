namespace BookingApp.Models;

public class Booking
{
    public int IdBooking;
    public decimal TotalPrice;
    public ICollection<int> ServiceBookedId = new HashSet<int>();
}