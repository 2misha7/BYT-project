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
    public WorkStation(StationCategory category, decimal price)
    {
        try
        {
            Category = category;
            Price = price;
            Add(this);
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    
}
