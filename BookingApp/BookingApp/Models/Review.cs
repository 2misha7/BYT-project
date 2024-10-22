using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class Review(ReviewRating rating, string comment, DateTime date)
{
    public int IdReview;
    public ReviewRating Rating = rating;
    public string Comment = comment;
    public DateTime Date = date;
    
}