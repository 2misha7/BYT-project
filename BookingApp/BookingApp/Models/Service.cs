
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

}