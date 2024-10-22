using System.Text.Json;
using System.Xml;
using BookingApp.Models;

namespace BookingApp.Repositories;



public abstract class AbstractRepository<T> where T : IEntity
{
    protected static string FilePath { get; set; }

    protected AbstractRepository(string filePath)
    {
        FilePath = filePath;
    }
    
    public void Save(List<T> items)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(items, options);
        File.WriteAllText(FilePath, json);
    }
    
    public List<T> Load()
    {
        if (!File.Exists(FilePath))
        {
            return new List<T>();
        }
        var json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<T>(); 
        }
        return JsonSerializer.Deserialize<List<T>>(json);
    }
    
    public void Add(T newItem)
    {
        List<T> items = Load();
        items.Add(newItem);
        
        Save(items);
    }
    
    
}