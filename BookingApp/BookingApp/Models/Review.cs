namespace BookingApp.Models;

public class Review: ModelBase<Review>
{
    
    private int _id;
    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    private ReviewRating _rating;

    public ReviewRating Rating
    {
        get => _rating;
        set => _rating = value;
    }

    private string _comment = null!;
    public string Comment
    {
        get => _comment;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Comment cannot be empty");
            }
            _comment = value;
        }
    }


    private DateTime _date;

    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }
    
    public Review(ReviewRating rating, string comment, DateTime date)
    {
        try
        {
            Rating = rating; 
            Comment = comment;
            Date = date;
            Add(this); 
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    
    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1; 
    }
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