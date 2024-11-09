namespace BookingApp.Models;

public class Booking : ModelBase<Booking>
{
    
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    //do after implementing relationships, no setter, getter must return totalPrice
    private decimal _totalPrice = 0;
    public decimal TotalPrice
    {
        get => _totalPrice;
    }
    
    public Booking()
    {
        try
        {
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