using System.Text.Json;
using BookingApp.Models;

namespace BookingApp;

public static class FileOperations
{
    private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data.json");

    public static string FilePath
    {
        get => _filePath;
        set => _filePath = value;
    }

    public static void WriteToFile(Dictionary<string,object> data)
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new AccountTypeConverter() },
            WriteIndented = true 
        };
        
        try
        { 
            File.WriteAllText(_filePath, JsonSerializer.Serialize(data, options));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }

    public static bool FileExists()
    {
        if (!File.Exists(_filePath))
        {
            using (File.Create(_filePath)) { }
            return false;
        }

        return true;
    }

    public static string ReadTextFromFile()
    {
        return File.ReadAllText(_filePath);
    }


    
    
}