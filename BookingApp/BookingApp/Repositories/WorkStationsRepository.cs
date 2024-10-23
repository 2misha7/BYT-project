using BookingApp.Models;

namespace BookingApp.Repositories;

public class WorkStationsRepository() : AbstractRepository<WorkStation>(_filePath)                   
{   
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\workStation.json");
}