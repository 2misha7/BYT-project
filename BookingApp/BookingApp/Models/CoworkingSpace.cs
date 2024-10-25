namespace ConsoleApp1.Models;

public class CoworkingSpace(int id, string address, string city, string contactNumber) : ModelBase<CoworkingSpace>
{
    public int Id { get; set; } = id;
    public string Address { get; set; } = address;
    public string City { get; set; } = city;
    public string ContactNumber { get; set; } = contactNumber;
}