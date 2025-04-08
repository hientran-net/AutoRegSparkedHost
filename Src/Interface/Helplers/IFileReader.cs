namespace Src.Interface.Helplers;

public interface IFileReader
{
    public static string ReadLink(string filePath){ return File.ReadAllText(filePath); }
    public static string GetEmailToLogin(string emailFilePath){ return GetEmailToLogin(emailFilePath); }
    public static string GetPasswordToLogin(string passwordFilePath){ return GetPasswordToLogin(passwordFilePath); }
}