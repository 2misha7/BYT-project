
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using NUnit.Framework;

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
    
    
    public Service(string name, StationCategory serviceCategory, string description, decimal price)
    {
        try
        {
            var spec = new List<string>();
            spec.Add("Hair");
            _beautyProfessional = new BeautyProfessional("fake", "fake", "fake@gmail.com", "+1111111", "fake",
                "QWwersdf1234$$", "fake", "fake", 0, "no", spec, new RegularAccountType());
            _assignedDateTime = new DateTime();
            _assignedWorkStation = new WorkStation(StationCategory.Hair, 0);
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
    
    public Service(string name, StationCategory serviceCategory, string description, decimal price, BeautyProfessional beautyProfessional)
    {
        try
        {
            _beautyProfessional = beautyProfessional;
            _assignedDateTime = new DateTime();
            _assignedWorkStation = new WorkStation(StationCategory.Hair, 0);
            Name = name;
            ServiceCategory = serviceCategory;
            Description = description;
            Price = price;
            AddBeautyProToService(beautyProfessional);
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
        
        if (newReview.Service._name != "Fake")
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
        if (_booking != null)
        {
            RemoveFromBooking();
        }
        Delete(this);
    }
    
    //Service - Workstation 
    private WorkStation _assignedWorkStation;

    public WorkStation AssignedWorkStation
    {
        get => _assignedWorkStation;
        set => _assignedWorkStation = value;
    }

    private DateTime _assignedDateTime;

    public DateTime AssignedDateTime
    {
        get => _assignedDateTime;
        set => _assignedDateTime = value;
    }
    public void AssignWorkStationAndTime(WorkStation workStation, DateTime dateTime)
    {
        if (workStation == null)
            throw new ArgumentNullException(nameof(workStation));
        if (_isUpdating)
            return;
        if (ServiceCategory != workStation.Category)
        {
            throw new InvalidOperationException("Category of workstation and service must be the same");
        }
        if (_assignedWorkStation.Price != 0)
            throw new InvalidOperationException("This Service is already assigned to a WorkStation.");

        _isUpdating = true;
        _assignedDateTime = dateTime;
        _assignedWorkStation = workStation;
        workStation.AddServiceAtTime(this, dateTime);
        _isUpdating = false;
    }

   public void RemoveWorkStationAndTime()
   {
       if (_isUpdating)
           return;
       if (AssignedWorkStation.Price == 0)
           throw new InvalidOperationException("This Service is not assigned to a WorkStation.");
      
       _isUpdating = true;
       var previousWorkStation = _assignedWorkStation;
       _assignedWorkStation = new WorkStation(StationCategory.Hair, 0);
       _assignedDateTime = new DateTime();
       previousWorkStation.RemoveServiceAtTime(this);
       _isUpdating = false;
   }
   
   public void ChangeWorkStation(WorkStation newWorkStation)
   {
       if (newWorkStation == null)
           throw new ArgumentNullException(nameof(newWorkStation));
       if (newWorkStation == _assignedWorkStation)
           throw new InvalidOperationException("This Service is already assigned to the specified WorkStation.");
       if (ServiceCategory != newWorkStation.Category)
           throw new InvalidOperationException("The WorkStation's category does not match the Service's category.");
       if (_assignedWorkStation.Price == 0)
       {
           throw new InvalidOperationException("It is impossible to assign new Workstation to this Service, as there is no Workstation assigned before");
       }

       var dateTime =  _assignedWorkStation.ServicesByTime.FirstOrDefault(x => x.Value == this).Key;
       RemoveWorkStationAndTime();
       AssignWorkStationAndTime(newWorkStation, dateTime);
       
   }
    
   
   
   
   //Service-ServicePromoted-Promotion
   private readonly List<ServicePromoted> _servicePromotions = new();
   public IReadOnlyList<ServicePromoted> ServicePromotions => _servicePromotions.AsReadOnly(); 
   
   public void AddPromotion(Promotion promotion, DateTime startDate, DateTime endDate, ServicePromoted? servicePromoted)
   {
       if (_isUpdating)
       {
           return;
       }
       if (promotion == null)
           throw new ArgumentNullException(nameof(promotion));
       
       if (_servicePromotions.Any(sp => sp.Service == this && sp.Promotion == promotion))
       {
           throw new InvalidOperationException("This Service is already promoted by this Promotion.");
       }
        
       if (servicePromoted == null)
       {
           servicePromoted = new ServicePromoted(startDate, endDate, promotion, this);
       }
       _isUpdating = true;
       _servicePromotions.Add(servicePromoted);
       promotion.AddService(this, startDate, endDate, servicePromoted);
       _isUpdating = false;
   }
   
   public void RemovePromotion(Promotion promotion)
   {
       if (_isUpdating)
       {
           return;
       }
       if (promotion == null)
           throw new ArgumentNullException(nameof(promotion));
       var servicePromoted = _servicePromotions.FirstOrDefault(sp => sp.Service == this && sp.Promotion == promotion);
       if (servicePromoted == null)
       {
           throw new InvalidOperationException("No matching ServicePromotion found.");
            
       }
       _isUpdating = true;
       _servicePromotions.Remove(servicePromoted);
       promotion.RemoveService(this);
       var spToDelete = ServicePromoted.GetAll().FirstOrDefault(sp => sp.Service == this && sp.Promotion == promotion);
       if (spToDelete != null)
       {
           ServicePromoted.Delete(servicePromoted);
       }
       _isUpdating = false;
   }
   
   public void SubstitutePromotion(Promotion oldP, Promotion newP)
   {
       if (oldP == null)
           throw new ArgumentNullException(nameof(oldP));
       if (newP == null)
           throw new ArgumentNullException(nameof(newP));
       if (!_servicePromotions.Any(sp => sp.Service == this && sp.Promotion == oldP))
       {
           throw new Exception("This old Promotion has not been assigned to this Service");
       }

       if (_servicePromotions.Any(sp => sp.Service == this && sp.Promotion == newP))
       {
           throw new Exception("This new Promotion is already assigned to this Service");
       }

       var spToDelete = _servicePromotions.First(sp => sp.Service == this && sp.Promotion == oldP);
       RemovePromotion(oldP); 
       AddPromotion(newP, spToDelete.StartDate, spToDelete.EndDate, null);
   }
   
   //Service-BeautyProfessional (one-to-many)
   private BeautyProfessional _beautyProfessional;

   public BeautyProfessional BeautyProfessional
   {
       get => _beautyProfessional;
       set => _beautyProfessional = value;
   }
   
   public void AddBeautyProToService(BeautyProfessional beautyProfessional)
   {
       if (beautyProfessional == null)
           throw new ArgumentNullException(nameof(beautyProfessional));

       if (_isUpdating)
       {
           return;
       }

       if (_beautyProfessional != null && _beautyProfessional.FirstName != "fake")
       {
           throw new InvalidOperationException("This Service already has a Beauty Professional.");
       }

       _isUpdating = true;
       _beautyProfessional = beautyProfessional;
       beautyProfessional.AddServiceToBeautyProfessional(this);
       _isUpdating = false;
   }
   
   public void RemoveBeautyFromService()
   {
       if (_isUpdating) return;
       if (_beautyProfessional.FirstName == "fake") 
           throw new InvalidOperationException("This Service does not have a BeautyPro");
       _isUpdating = true;
       var previousBP = _beautyProfessional;
       _beautyProfessional = null;
       previousBP.RemoveServiceFromBeautyPro(this); 
       _isUpdating = false;
        
   }
   public void ChangeBeautyPro(BeautyProfessional newBP)
   {
       if (newBP == null)
           throw new ArgumentNullException(nameof(newBP));
       if (_beautyProfessional == newBP)
       {
           throw new InvalidOperationException("This Service is already assigned to this BeautyPro");
       }
       
       RemoveBeautyFromService(); 
       AddBeautyProToService(newBP); 
   }
   //Service-Booking (many-to-one)
   private Booking? _booking; 
   public Booking? Booking => _booking;
   public void AssignToBooking(Booking booking)
   {
       if (booking == null)
           throw new ArgumentNullException(nameof(booking));
       if (_isUpdating)
       {
           return;
       }
       if (_booking != null)
       {
           throw new InvalidOperationException("This Service is already in the Booking.");
       }
       _isUpdating = true;
       _booking = booking;
       booking.AddService(this);
       _isUpdating = false;
   }
   public void RemoveFromBooking()
   {
       if (_isUpdating) return;
       if (_booking == null) 
           throw new InvalidOperationException("This service is not assigned to a Booking");
       _isUpdating = true;
       var previuosBooking = _booking;
       _booking = null;
       previuosBooking.RemoveService(this); 
       _isUpdating = false;
        
   }
   public void ChangeBookingAssignedToACoupon(Booking newB)
   {
       if (newB == null)
           throw new ArgumentNullException(nameof(newB));
       if (_booking == newB)
       {
           throw new InvalidOperationException("This Booking is already assigned to exactly this Service");
       }

       if (_booking == null)
       {
           throw new InvalidOperationException(
               "It is not possible to assign this Service to a new Booking, because it is not assigned to any");
       }
       RemoveFromBooking(); 
       AssignToBooking(newB); 
   }
   
   public void RemoveConnectionWhileDeletingBooking()
   {
       _booking = null;
   }
   
}