using Src.Interface.Helplers;

namespace Src.Implementations.Helplers;

public class FileInteract : IFileInteract
{
    public static string ReadLink(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);    
        }

        Console.WriteLine("File không tồn tại.");
        return null;
    }

    public static string GetEmailToLogin(string filePath)
    {
        try
        {
            // Đọc tất cả các dòng từ file
            string[] lines = File.ReadAllLines(filePath);
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

    public static string GetPasswordToLogin(string selectedEmail, string filePath)
    {
        try
        {
            // Đọc tất cả các dòng từ file password
            string[] passwordLines = File.ReadAllLines(filePath);
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

    public static void SaveContentToFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content + Environment.NewLine);
    }
}