using System.Text.Json;

namespace BookingApp;

public abstract class ModelBase<T> where T : ModelBase<T>
{
    private static readonly ICollection<T> Entities = new List<T>();
    
    private bool _isDeserializing = false;
    protected abstract void AssignId();

    
    //protected abstract T Clone();
    public static IReadOnlyCollection<T> GetAll()
    {
        //return Entities.Select(entity => entity.Clone()).ToList().AsReadOnly();
        return Entities.ToList().AsReadOnly();
    }

    protected static void Add(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentException($"{nameof(entity)} cannot be null");
        }
        if (!entity._isDeserializing)
        {
            entity.AssignId();
        }
        
        Entities.Add(entity);
    }
    
    

    public static void LoadClassExtentFromFile()
    {
        
        if (!FileOperations.FileExists())
        {
            Entities.Clear();
            return;
        }
        
        try
        {
            var json = FileOperations.ReadTextFromFile();
            if (string.IsNullOrWhiteSpace(json))
            {
                Entities.Clear();
                return;
            }
            var options = new JsonSerializerOptions
            {
                Converters = { new AccountTypeConverter() },
            };
            var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
            if (data != null && data.TryGetValue(typeof(T).Name, out JsonElement entityDataElement))
            {
                var list = JsonSerializer.Deserialize<List<T>>(entityDataElement.GetRawText(), options);
                if (list != null)
                {
                    Entities.Clear();
                    foreach (var entity in list)
                    {
                        entity._isDeserializing = true;
                        Add(entity);
                        entity._isDeserializing = false;
                    }
                }
            }
        }
        
        catch (IOException ex)
        {
            Console.WriteLine($"I/O error: {ex.Message}");
        }
    }
}


