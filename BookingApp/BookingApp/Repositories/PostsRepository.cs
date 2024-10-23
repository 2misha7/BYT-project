using BookingApp.Models;

namespace BookingApp.Repositories;

public class PostsRepository() : AbstractRepository<Post>(_filePath)
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\post.json");
}