namespace ConsoleApp1.Models;

public class Booking : ModelBase<Booking>
{
    public Booking(int id, decimal totalPrice)
    {
        Id = id;
        TotalPrice = totalPrice;
        Add(this);
    }

    public int Id { get; set; }     
    public decimal TotalPrice { get; set; }
}