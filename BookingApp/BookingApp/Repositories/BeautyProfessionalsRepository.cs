using BookingApp.Models;

namespace BookingApp.Repositories;

public class BeautyProfessionalsRepository() : AbstractRepository<BeautyProfessional>(_filePath)
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\beautyProf.json");
}

