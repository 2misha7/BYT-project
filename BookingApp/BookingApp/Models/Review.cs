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
    
    //Review - Service many-to-one, composition
    private Service? _service; 
    public Service? Service => _service;
    private bool _isUpdating = false;
    
    public void AssignServiceToReview(Service service)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if (_isUpdating)
        {
            return;
        }
        if (_service != null)
        {
            throw new InvalidOperationException("This Review is already assigned to a Service in the system.");
        }
        _isUpdating = true;
        _service = service;
        service.AddReviewToService(this);
        _isUpdating = false;
    }
    
    public void RemoveServiceFromReview()
    {
        if (_isUpdating) return;
        if (_service == null) 
            throw new InvalidOperationException("This Review is not assigned to a Service");
        _isUpdating = true;
        var previousS = _service;
        _service = null;
        previousS.RemoveReviewFromService(this); 
        _isUpdating = false;
        
    }
    public void ChangeServiceAssigned(Service newService)
    {
        if (newService == null)
            throw new ArgumentNullException(nameof(newService));
        if (_service == newService)
        {
            throw new InvalidOperationException("This Service is already assigned to exactly this Review");
        }

        if (_service == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign the review to another service, because it is not assigned to any");
        }
        RemoveServiceFromReview(); 
        AssignServiceToReview(newService); 
    }
    public void DeleteReview()
    {
        if (_service != null)
        {
            RemoveServiceFromReview();    
        }
        Delete(this);
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