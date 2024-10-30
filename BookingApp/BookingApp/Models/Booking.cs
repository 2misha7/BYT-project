namespace BookingApp.Models;
//catch exception in constructor 
public class Booking : ModelBase<Booking>
{
    
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    private decimal _totalPrice;
    public decimal TotalPrice
    {
        get => _totalPrice;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Total price cannot be negative");
            }
            _totalPrice = value;
        }
    }
    
    public Booking(decimal totalPrice)
    {
        try
        {
            TotalPrice = totalPrice;
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