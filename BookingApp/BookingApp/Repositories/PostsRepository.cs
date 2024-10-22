namespace BookingApp.Repositories;

public class PostsRepository() : AbstractRepository(_filePath)
{
    private static readonly string _filePath = "post.json";
}