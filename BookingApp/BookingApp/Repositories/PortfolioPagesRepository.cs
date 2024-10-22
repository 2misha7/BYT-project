using BookingApp.Models;

namespace BookingApp.Repositories;

public class PortfolioPagesRepository() : AbstractRepository<PortfolioPage>(_filepath)
{
    private static readonly string _filepath = "portfolioPage.json";
}