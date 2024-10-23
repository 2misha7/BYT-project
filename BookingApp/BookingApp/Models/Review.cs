using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class Review(ReviewRating rating, string comment, DateTime date, Guid serviceId) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ReviewRating Rating = rating;
    public string Comment = comment;
    public DateTime Date = date;
    public Guid ServiceId { get; set; } = serviceId;
}