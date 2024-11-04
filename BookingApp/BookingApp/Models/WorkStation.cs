//clone method, constructot to private 
namespace BookingApp.Models;

public class WorkStation : ModelBase<WorkStation>
{
    
    private int _id;
    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    private StationCategory _category;
    public StationCategory Category
    {
        get => _category;
        set => _category = value;
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

    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1; 
    }

    protected override WorkStation Clone()
    {
        return new WorkStation(this);
    }


    public WorkStation(StationCategory category, decimal price)
    {
        try
        {
            AssignId();
            Category = category;
            Price = price;
            Add(new WorkStation(this));
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    
    private WorkStation(WorkStation other)
    {
        _id = other.Id;
        _category = other.Category;
        _price = other.Price;
    }
    
    
}
