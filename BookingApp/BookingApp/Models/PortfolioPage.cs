namespace BookingApp.Models;

public class PortfolioPage
{
    public int IdPortfolioPage;
    public ICollection<int> PostsIndexes = new HashSet<int>();
}