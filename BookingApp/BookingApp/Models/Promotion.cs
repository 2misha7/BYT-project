namespace BookingApp.Models;

public class Promotion: ModelBase<Promotion>
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
    public static int MinDiscountPercentage { get; set; } = 5;
    public static int MaxDiscountPercentage { get; set; } = 40;
    
    public Promotion(string name, string discountDescription, int totalDiscountPercentage)
    {
        try
        {
            Name = name;
            DiscountDescription = discountDescription;
            TotalDiscountPercentage = totalDiscountPercentage;
            Add(new Promotion(this)); 
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    
    private Promotion(Promotion original)
    {
        _id = original._id;
        _name = original._name;
        _discountDescription = original._discountDescription;
        _totalDiscountPercentage = original._totalDiscountPercentage;
    }
    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1; 
    }

    protected override Promotion Clone()
    {
        return new Promotion(this);
    }
}