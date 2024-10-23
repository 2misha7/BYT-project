namespace BookingApp.Models;

public class Promotion(int totalDiscountPercentage, string name, string discount) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public string Discount { get; set; } = discount;
    public int TotalDiscountPercentage { get; set; } = totalDiscountPercentage;
    public static int MinDiscountPercentage { get; set; } = 5;
    public static int MaxDiscountPercentage { get; set; } = 40;
    public ICollection<Guid> ServicesId { get; set; } = new HashSet<Guid>();
}