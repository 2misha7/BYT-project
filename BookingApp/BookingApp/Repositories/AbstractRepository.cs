namespace BookingApp.Repositories;

public abstract class AbstractRepository
{
    protected static string FilePath { get; set; }

    protected AbstractRepository(string filePath)
    {
        FilePath = filePath;
    }
    
}