using BookingApp;
using BookingApp.Models;

namespace BookingAppTests;

//A unit test that checks for extent presestiency (are the files stored correctly and are they
//retrieved correctly after each new run) 
public class ExtentPersistencyTests
{
    private const string TestFilePath = @"..\..\..\testData.json";
    public ExtentPersistencyTests()
    {
        FileOperations.FilePath = TestFilePath;
    }

    [Test]
    public void File()
    {
        Repository.GetAllFromFile();
        var n = new Notification("asfsgdhf");
        var w = new WorkStation(StationCategory.Body, 12);
 
        Repository.WriteAllToFile();
    }
}