namespace Src.Interface.Helplers;

public interface IFileReader
{
    public static string ReadLink(string filePath){ return File.ReadAllText(filePath); }
}