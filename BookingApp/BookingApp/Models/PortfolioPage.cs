namespace BookingApp.Models;

public class PortfolioPage() : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ICollection<Guid> PostsId = new HashSet<Guid>();
    //create portfolioPage while creating BeautyProf and then assign values 
    public Guid BeautyProfId { get; set; }
}