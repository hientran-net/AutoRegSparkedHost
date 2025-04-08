using Src.Interface.Helplers;

namespace Src.Implementations.Helplers;

public class FileReader : IFileReader
{
    private static string emailFilePath = @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\emails.txt";
    private static string passwordFilePath = @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\passwords.txt";
    public static string ReadLink(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);    
        }

        Console.WriteLine("File không tồn tại.");
        return null;
    }

    public static string GetEmailToLogin()
    {
        try
        {
            // Đọc tất cả các dòng từ file
            string[] lines = File.ReadAllLines(emailFilePath);
            if (lines.Length == 0)
            {
                Console.WriteLine("File email trống!");
                return null;
            }

            // Lấy dòng cuối cùng
            string lastLine = lines[lines.Length - 1];
            string email = lastLine.Split(new[] { "\t\t" }, StringSplitOptions.None)[0];
        
            Console.WriteLine($"Email mới nhất từ file: {email}");
            return email;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi đọc file email: {ex.GetType().Name} - {ex.Message}");
            return null;
        }
    }

    public static string GetPasswordToLogin(string selectedEmail)
    {
        try
        {
            // Đọc tất cả các dòng từ file password
            string[] passwordLines = File.ReadAllLines(passwordFilePath);
            if (passwordLines.Length == 0)
            {
                Console.WriteLine("File password trống!");
                return null;
            }

            // Lấy dòng cuối cùng
            string lastLine = passwordLines[passwordLines.Length - 1];
            string password = lastLine.Split(new[] { "\t\t" }, StringSplitOptions.None)[0];
        
            Console.WriteLine($"Password tương ứng: {password}");
            return password;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi đọc file password: {ex.GetType().Name} - {ex.Message}");
            return null;
        }
    }
}