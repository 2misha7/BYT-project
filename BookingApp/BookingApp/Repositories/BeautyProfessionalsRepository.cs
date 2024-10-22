using BookingApp.Models;

namespace BookingApp.Repositories;

public class BeautyProfessionalsRepository() : AbstractRepository<BeautyProfessional>(_filePath)
{
    private static readonly string _filePath = "beautyProf.json";
}

