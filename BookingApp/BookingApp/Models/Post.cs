namespace BookingApp.Models;

public class Post(string imageLink, Guid portfolioPageId, string text) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ImageLink { get; set; } = imageLink;
    public Guid PortfolioPageID { get; set; } = portfolioPageId;
    public string Text { get; set; } = text;
    public int Likes { get; set; } = 0;
    public int Dislikes { get; set; } = 0;
    public ICollection<string> Comments { get; set; } = new HashSet<string>();
    
}