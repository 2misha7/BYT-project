namespace BookingApp.Models;

public class CoworkingSpace(string address, string city, string contactNumber) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Address { get; set; } = address;
    public string City { get; set; } = city;
    public string ContactNumber { get; set; } = contactNumber;
    public ICollection<Guid> WorkStationsId { get; set; } = new HashSet<Guid>();
    
}