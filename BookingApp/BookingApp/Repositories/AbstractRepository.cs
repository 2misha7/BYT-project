using System.Text.Json;
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
        
        if (typeof(Person).IsAssignableFrom(typeof(T)))
        {
            options.Converters.Add(new AccountTypeConverter());
        }

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

        var options = new JsonSerializerOptions();
        if (typeof(Person).IsAssignableFrom(typeof(T)))
        {
            options.Converters.Add(new AccountTypeConverter());
        }

        return JsonSerializer.Deserialize<List<T>>(json, options);
    }
    
    public void Add(T newItem)
    {
        List<T> items = Load();
        items.Add(newItem);
        Save(items);
    }

    public void Delete(Guid id)
    {
        var items = Load();
        var itemToRemove = items.SingleOrDefault(r => r.Id == id);
        if (itemToRemove != null) items.Remove(itemToRemove);
        Save(items);
    }
    
    public T? Find(Guid id)
    {
        var items = Load();
        var itFound = items.SingleOrDefault(r => r.Id == id);
        return itFound;
    }
    
    public List<T> FindManyByIds(ICollection<Guid> ids)
    {
        var items = Load();
        return items.Where(item => ids.Contains(item.Id)).ToList();
    }

}