namespace ConsoleApp1.Models;

public class Notification(int id, string text) : ModelBase<Notification>
{
    public int Id { get; set; } = id;
    public string Text = text;
}