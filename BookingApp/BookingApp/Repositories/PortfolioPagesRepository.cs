using BookingApp.Models;

namespace BookingApp.Repositories;

public class PortfolioPagesRepository() : AbstractRepository<PortfolioPage>(_filepath)
{
    private static readonly string _filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\portfolioPage.json");
}