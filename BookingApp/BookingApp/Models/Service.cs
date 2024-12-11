
using System.Drawing;

namespace BookingApp.Models;

public class Service: ModelBase<Service>
{
    private int _id;
    public int Id
    {
        get => _id;
        private set => _id = value;
    }
    private string _name = null!;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name cannot be empty");
            }
            _name = value;
        }
    }

    private StationCategory _serviceCategory; 

    public StationCategory ServiceCategory
    {
        get => _serviceCategory;
        set => _serviceCategory = value;
    }
    private string _description = null!;

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    private decimal _price;

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0) 
            {
                throw new ArgumentException("Price cannot be negative");
            }
            _price = value;
        }
    }
    
   
    private ICollection<DateTime> _availableTimeSlots = new HashSet<DateTime>();

    public IReadOnlyCollection<DateTime> AvailableTimeSlots => (IReadOnlyCollection<DateTime>)_availableTimeSlots; 
    
    public Service(string name, StationCategory serviceCategory, string description, decimal price)
    {
        try
        {
            Name = name;
            ServiceCategory = serviceCategory;
            Description = description;
            Price = price;
            Add(this);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
  
    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1; 
    }
    
    //Service - Review one-to-many, composition
    private readonly List<Review> _reviews = new();
    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();
    private bool _isUpdating = false;

    public void AddReviewToService(Review review)
    {
        if (review == null)
            throw new ArgumentNullException(nameof(review));
        if (_isUpdating)
        {
            return; 
        }
        if (_reviews.Contains(review))
            throw new InvalidOperationException("This Service already has this Review.");
        _isUpdating = true;
        _reviews.Add(review);
        review.AssignServiceToReview(this);
        _isUpdating = false;
    }
    public void RemoveReviewFromService(Review review)
    {
        if (review == null)
            throw new ArgumentNullException(nameof(review));

        if (_isUpdating) return; 
        if (!_reviews.Contains(review)) throw new InvalidOperationException("This Service does not have this Review."); 

        _isUpdating = true;
        _reviews.Remove(review);
        review.RemoveServiceFromReview();
        _isUpdating = false;
    }
    public void SubstituteReview(Review oldReview, Review newReview)
    {
        if (oldReview == null)
            throw new ArgumentNullException(nameof(oldReview));
        if (newReview == null)
            throw new ArgumentNullException(nameof(newReview));
        if (!_reviews.Contains(oldReview))
        {
            throw new Exception("This Service does not have this Review");
        }

        if (_reviews.Contains(newReview))
        {
            throw new Exception("This Service already has this Review");
        }
        
        if (newReview.Service != null)
        {
            throw new Exception("It is not possible to add this Review to a Service, as it is already assigned to a Service in the system");
        }
        
        RemoveReviewFromService(oldReview); 
        AddReviewToService(newReview);
    }
    //After deletion of the service,reviews will be deleted
    public void DeleteService()
    {
        if(_reviews.Count > 0)
        {
            foreach(var r in _reviews.ToList()){
                r.DeleteReview(); 
            }
        }
        Delete(this);
    }
}