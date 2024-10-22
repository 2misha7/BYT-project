using BookingApp.Models;

namespace BookingApp.Repositories;

public class WorkStationsRepository() : AbstractRepository<WorkStation>(_filePath)                   
{   
    private static readonly string _filePath = "workStation.json";
}