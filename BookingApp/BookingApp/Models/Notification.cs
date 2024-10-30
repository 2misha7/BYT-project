namespace BookingApp.Models;

public class Notification: ModelBase<Notification>
{
    private int _id;
    

    public int Id
    {
        get => _id;
        private set => _id = value;
    }
    private string _text = null!;
    public string Text
    {
        get => _text;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Text cannot be empty");
            }
            _text = value;
        }
    }
    
    public Notification(string text)
    {
        try
        {
            Text = text;
            Add(this);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    
    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1;
    }
}