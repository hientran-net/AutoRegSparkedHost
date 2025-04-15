using Src.Interface.Helplers;

namespace Src.Implementations.Helplers;

public class StringInteract : IStringInteract
{
    public static string GetServerIdFromUrl(string url)
    {
        // Tách chuỗi theo dấu '/'
        string[] parts = url.Split('/');
    
        // Tìm phần chứa "server" và lấy phần tiếp theo là ID
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] == "server" && i + 1 < parts.Length)
            {
                return parts[i + 1]; // Phần tiếp theo là ID
            }
        }
    
        return string.Empty; // Trả về chuỗi rỗng nếu không tìm thấy
    }
}