namespace ConsoleApp1.Models;

public class Review(int id, ReviewRating rating, string comment, DateTime date) : ModelBase<Review>
{
    public int Id { get; set; } = id;
    public ReviewRating Rating = rating;
    public string Comment = comment;
    public DateTime Date = date;
}

public enum ReviewRating
{
    Awful = 0,
    Bad = 1,
    Medium = 2,
    Good = 3,
    VeryGood = 4,
    Perfect = 5
}