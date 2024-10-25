
namespace ConsoleApp1.Models;

public class WorkStation(int id, StationCategory category, decimal price) : ModelBase<WorkStation>
{
    public int Id { get; set; } = id;
    public StationCategory Category { get; set; } = category;
    public decimal Price { get; set; } = price;
}