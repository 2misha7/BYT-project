namespace ConsoleApp1.Models;

public class RegularAccountType : IAccountType
{
    public bool IsSubscriptionActive() => false;
}