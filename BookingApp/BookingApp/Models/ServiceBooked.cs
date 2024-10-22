namespace BookingApp.Models;

public class ServiceBooked
{
    public int IdServiceBooked { get; set; }
    public int IdService { get; set; }
    public int IdBooking { get; set; }
    public DateTime ServiceTime { get; set; }
}