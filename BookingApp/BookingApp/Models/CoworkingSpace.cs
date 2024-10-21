namespace BookingApp.Models;

public class CoworkingSpace(string adress, string city, string contactNumber)
{
    public string Adress { get; set; } = adress;
    public string City { get; set; } = city;
    public string ContactNumber { get; set; } = contactNumber;
    public ICollection<int> WorkStationsId { get; set; } = new HashSet<int>();
}