using BookingApp.Models;

namespace BookingApp.Repositories;

public class ReviewRepository() : AbstractRepository<Review>(_filePath)
{
    private static readonly string _filePath = "review.json";
}