namespace BookingApp.Models;

public class Notification(string text, Guid bookingId) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text = text;
    public Guid BookingId { get; set; } = bookingId;
}