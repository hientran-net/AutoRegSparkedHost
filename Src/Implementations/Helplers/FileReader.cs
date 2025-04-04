using Src.Interface.Helplers;

namespace Src.Implementations.Helplers;

public class FileReader : IFileReader 
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
}