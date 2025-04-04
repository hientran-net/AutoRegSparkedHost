namespace Src.Interface.Task;

public interface IAutomationTask
{
    public static void RegisterAccount(){ Console.WriteLine("RegisterAccount"); }
    public static void LoginAccount(){ Console.WriteLine("LoginAccount"); }
}