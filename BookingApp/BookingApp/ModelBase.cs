using System.Text.Json;

namespace ConsoleApp1.Models;

public abstract class ModelBase<T> where T : ModelBase<T>
{
    public static string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data.json");
    private static readonly ICollection<T> Entities = new List<T>();

    public static IReadOnlyCollection<T> GetAll()
    {
        return Entities.ToList().AsReadOnly();
    }

    protected static void Add(T entity)
    {
        Entities.Add(entity);
    }

    public static void LoadFromFile()
    {
        var json = File.ReadAllText(FilePath);
        var options = new JsonSerializerOptions
        {
            Converters = { new AccountTypeConverter() },
        };
        
        
        var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
        if (!data.TryGetValue(typeof(T).Name, out JsonElement entityDataElement)) return;
        var list = JsonSerializer.Deserialize<List<T>>(entityDataElement.GetRawText(), options);
        if (list == null) return;
        Entities.Clear();
        foreach (var entity in list)
        {
            Entities.Add(entity);
        }
    }
}