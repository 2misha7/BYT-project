namespace BookingApp.Models;

public class Promotion : ModelBase<Promotion>
{
    private static int _maxDiscountPercentage = 35;
    private static int _minDiscountPercentage = 5;
    public static int MaxDiscountPercentage
    {
        get => _maxDiscountPercentage;
        set => _maxDiscountPercentage = value;
    }
    
    public static int MinDiscountPercentage
    {
        get => _minDiscountPercentage;
        set => _minDiscountPercentage = value;
    }
    
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

    private string _discountDescription = null!;
    public string DiscountDescription
    {
        get => _discountDescription;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Description cannot be empty");
            }
            _discountDescription = value;
        }
    }

    private int _totalDiscountPercentage;
    public int TotalDiscountPercentage
    {
        get => _totalDiscountPercentage;
        set
        {
            if (value < MinDiscountPercentage || value > MaxDiscountPercentage)
            {
                throw new ArgumentException($"Total discount percentage must be between {MinDiscountPercentage} and {MaxDiscountPercentage}.");
            }
            _totalDiscountPercentage = value;
        }
    }
    
    public Promotion(string name, string discountDescription, int totalDiscountPercentage)
    {
        try
        {
            Name = name;
            DiscountDescription = discountDescription;
            TotalDiscountPercentage = totalDiscountPercentage;
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
    //Promotion - Service (many-to-many)
    private bool _isUpdating = false;
    //private readonly List<Service> _services = new();
    //public IReadOnlyList<Service> Services => _services.AsReadOnly();
    //public void AddServiceToPromotion(Service service)
    //{
    //    if (service == null)
    //        throw new ArgumentNullException(nameof(service));
    //    if (_isUpdating)
    //    {
    //        return; 
    //    }
    //    if (_services.Contains(service))
    //        throw new InvalidOperationException("This Promotion is already assigned to this Service.");
    //    _isUpdating = true;
    //    _services.Add(service);
    //    service.AddPromotionToService(this);
    //    _isUpdating = false;
    //}
    //
    //public void RemoveServiceFromPromotion(Service service)
    //{
    //    if (service == null)
    //        throw new ArgumentNullException(nameof(service));
//
    //    if (_isUpdating) return; 
    //    if (!_services.Contains(service)) throw new InvalidOperationException("This Promotion is not assigned to this Promotion."); 
//
    //    _isUpdating = true;
    //    _services.Remove(service);
    //    service.RemovePromotionFromService(this);
    //    _isUpdating = false;
    //}
    //
    //public void SubstituteService(Service oldS, Service newS)
    //{
    //    if (oldS == null)
    //        throw new ArgumentNullException(nameof(oldS));
    //    if (newS == null)
    //        throw new ArgumentNullException(nameof(newS));
    //    if (!_services.Contains(oldS))
    //    {
    //        throw new Exception("This Promotion does not have this Service");
    //    }
//
    //    if (_services.Contains(newS))
    //    {
    //        throw new Exception("This Promotion already has this Service");
    //    }
    //    
    //    RemoveServiceFromPromotion(oldS); 
    //    AddServiceToPromotion(newS);
    //}
    
    //Promotion-ServicePromoted-Service
    private readonly List<ServicePromoted> _servicePromotions = new();
    public IReadOnlyList<ServicePromoted> ServicePromotions => _servicePromotions.AsReadOnly(); 
    public void AddService(Service service, DateTime startDate, DateTime endDate, ServicePromoted? servicePromoted)
    {
        if (_isUpdating)
        {
            return;
        }
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if (_servicePromotions.Any(sp => sp.Service == service && sp.Promotion == this))
        {
            throw new InvalidOperationException("This Service is already promoted by this Promotion.");
        }
        if (servicePromoted == null)
        {
            servicePromoted = new ServicePromoted(startDate, endDate, this, service);
        }
        

        _isUpdating = true;
        _servicePromotions.Add(servicePromoted);
        service.AddPromotion(this, startDate, endDate, servicePromoted);
        _isUpdating = false;
    }
    public void RemoveService(Service service)
    {
        if (_isUpdating)
        {
            return;
        }
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        var servicePromoted = _servicePromotions.FirstOrDefault(sp => sp.Service == service && sp.Promotion == this);
        if (servicePromoted == null)
        {
            throw new InvalidOperationException("No matching ServicePromotion found.");
            
        }
        _isUpdating = true;
        _servicePromotions.Remove(servicePromoted);
        service.RemovePromotion(this);
        var spToDelete = ServicePromoted.GetAll().FirstOrDefault(sp => sp.Service == service && sp.Promotion == this);
        if (spToDelete != null)
        {
            ServicePromoted.Delete(servicePromoted);
        }
        _isUpdating = false;
    }
    
    public void SubstituteService(Service oldS, Service newS)
    {
        if (oldS == null)
            throw new ArgumentNullException(nameof(oldS));
        if (newS == null)
            throw new ArgumentNullException(nameof(newS));
        if (!_servicePromotions.Any(sp => sp.Service == oldS && sp.Promotion == this))
        {
            throw new Exception("This old Service has not been assigned to this Promotion");
        }

        if (_servicePromotions.Any(sp => sp.Service == newS && sp.Promotion == this))
        {
            throw new Exception("This new Service is already assigned to this Promotion");
        }

        var spToDelete = _servicePromotions.First(sp => sp.Service == oldS && sp.Promotion == this);
        RemoveService(oldS); 
        AddService(newS, spToDelete.StartDate, spToDelete.EndDate, null);
    }
    
    
}