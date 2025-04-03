using System.Globalization;
using System.Text;
using System.Xml;
using OpenQA.Selenium.DevTools.V134.Runtime;

namespace Src;

class Program
{
    private readonly string dbPath = "AutoRegHost.db";

    static void Main(string[] args)
    {
        // đường dẫn đến database
        
        int option;
        do
        {
            Display_MainMenu();
            
            Console.WriteLine();
            Console.Write("Chọn chức năng: ");
            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {   
                case 1:
                    int gameOption;
                    do
                    {
                        Display_GameMenu();
                        Console.WriteLine();
                        gameOption = Convert.ToInt32(Console.ReadLine());
                        switch (gameOption)
                        {
                            case 1:
                            default:
                                Display_ErrorMessage();
                                break;
                        }
                    }while (gameOption != 0);
                    break;
                case 2:
                    int toolsOption;
                    do
                    {
                        Display_ToolsMenu();
                        Console.WriteLine();
                        toolsOption = Convert.ToInt32(Console.ReadLine());
                        switch (toolsOption)
                        {
                            default:
                                Display_ErrorMessage();
                                break;
                        }
                    }while (toolsOption != 0);
                    break;
                default:
                    Display_ErrorMessage();
                    break;
            }
        }while (option != 0);
    }
    
    public static void Display_MainMenu()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("===== CHÀO MỪNG TỚI TOOL REG HOST =====");
        Console.WriteLine("1. Host Games.");
        Console.WriteLine("2. Host Tools.");
        Console.WriteLine("0. Exit.");
    }
    
    public static void Display_GameMenu()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("===== VUI LÒNG CHỌN GAME ĐỂ HOST =====");
        Console.WriteLine("1. Palworld.");
        Console.WriteLine("2. Minecraft.");
        Console.WriteLine("3. Rust.");
        Console.WriteLine("4. Satisfactory.");
        Console.WriteLine("5. Ark Survival Envolved.");
        Console.WriteLine("6. The forest.");
        Console.WriteLine("7. Son of The Forest.");
        Console.WriteLine("8. Conan Exiles.");
        Console.WriteLine("0. Exit");
    }

    public static void Display_ToolsMenu()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("===== VUI LÒNG CHỌN TOOL ĐỂ HOST =====");
        Console.WriteLine("1. Web Hosting.");
        Console.WriteLine("2. Bot Hosting.");
        Console.WriteLine("0. Exit.");
    }

    public static void Display_ErrorMessage()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("!!! KHÔNG TỒN TẠI CHỨC NĂNG NÀY !!!");
    }
}