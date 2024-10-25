namespace ConsoleApp1.Models;

public class ServiceBooked(int id, DateTime serviceTime) : ModelBase<ServiceBooked>
{
    public int Id { get; set; } = id;
    public DateTime ServiceTime { get; set; } = serviceTime;
}