namespace BookingApp.Models;

public class PortfolioPage : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ICollection<int> PostsIndexes = new HashSet<int>();
    
}