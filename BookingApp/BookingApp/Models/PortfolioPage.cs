namespace BookingApp.Models;

public class PortfolioPage(Guid beatyProfId) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ICollection<Guid> PostsId = new HashSet<Guid>();

    public Guid BeatyProfId { get; set; } = beatyProfId;
}