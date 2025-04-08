using System.Globalization;
using System.Text;
using System.Xml;
using OpenQA.Selenium.DevTools.V134.Runtime;
using Src.Implementations;
using Src.Implementations.TaskGame;

namespace Src;

class Program
{
    static void Main(string[] args)
    {
        // Đường dẫn đến database
        
        int option;
        do
        {
            ConsoleUI.Display_MainMenu();
            Console.WriteLine();
            Console.Write("Chọn chức năng: ");
            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {   
                case 1:
                    int gameOption;
                    do
                    {
                        ConsoleUI.Display_GameMenu();
                        Console.WriteLine();
                        Console.Write("Chọn chức năng: ");
                        gameOption = Convert.ToInt32(Console.ReadLine());
                        switch (gameOption)
                        {
                            case 1:
                                PalworldTask.RegisterAccount();
                                break;
                            default:
                                ConsoleUI.Display_ErrorMessage();
                                break;
                        }
                    }while (gameOption != 0);
                    break;
                case 2:
                    int toolsOption;
                    do
                    {
                        ConsoleUI.Display_ToolsMenu();
                        Console.WriteLine();
                        toolsOption = Convert.ToInt32(Console.ReadLine());
                        switch (toolsOption)
                        {
                            default:
                                ConsoleUI.Display_ErrorMessage();
                                break;
                        }
                    }while (toolsOption != 0);
                    break;
                case 10:
                    string fileName = "Links/RegisterPalworld.txt"; // Hoặc "Links\\RegisterPalworld.txt" nếu dùng trên Windows
                    string basePath = AppContext.BaseDirectory;
                    string fullPath = Path.Combine(basePath, fileName);
                    Console.WriteLine(fullPath);
                    Console.ReadLine();
                    break;
            }
        }while (option != 0);
        ConsoleUI.Display_ThanksMessage();
    }
    
    
    
    

    

    
}