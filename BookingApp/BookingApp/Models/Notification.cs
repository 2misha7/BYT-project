namespace BookingApp.Models;

public class Notification(string text) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text = text;
    
}