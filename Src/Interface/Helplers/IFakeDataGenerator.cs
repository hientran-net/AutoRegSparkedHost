namespace Src.Interface.Helplers;

public interface IFakeDataGenerator
{
    public static HashSet<string> LoadExistingEmails(string filePath){return LoadExistingEmails(filePath); }
    public static void SaveEmailToFile(string filePath, string email){}
    public static string GenerateEmail(HashSet<string> existingEmails, int maxAttempts = 10000){return GenerateEmail(existingEmails, maxAttempts);}
    public static void SavePasswordToFile(string filePath, string value){}
    public static HashSet<string> LoadExistingPhones(string filePath){return LoadExistingPhones(filePath);}
    public static void SavePhoneToFile(string filePath, string phone){}
    public static string GenerateVietnamPhone(HashSet<string> existingPhones){return GenerateVietnamPhone(existingPhones);}
    
}