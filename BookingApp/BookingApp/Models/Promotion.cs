namespace ConsoleApp1.Models;

public class Promotion(int id, int totalDiscountPercentage, string name, string discount)  : ModelBase<Promotion>
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Discount { get; set; } = discount;
    public int TotalDiscountPercentage { get; set; } = totalDiscountPercentage;
    public static int MinDiscountPercentage { get; set; } = 5;
    public static int MaxDiscountPercentage { get; set; } = 40;
    
}