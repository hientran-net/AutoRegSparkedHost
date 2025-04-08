using System.Data.SQLite;
using System.Globalization;
using System.Text;
using System.Xml;
using OpenQA.Selenium.DevTools.V134.Runtime;
using Src.Implementations;
using Src.Implementations.Helplers;
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
            Console.Write("Chọn: ");
            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {   
                case 1:
                    int gameOption;
                    do
                    {
                        ConsoleUI.Display_GameMenu();
                        Console.WriteLine();
                        Console.Write("Chọn: ");
                        gameOption = Convert.ToInt32(Console.ReadLine());
                        switch (gameOption)
                        {
                            case 1:
                                int palworldOption;
                                do
                                {
                                    ConsoleUI.Display_PalworldMenu();
                                    Console.WriteLine();
                                    Console.WriteLine("Chọn: ");
                                    palworldOption = Convert.ToInt32(Console.ReadLine());
                                    switch (palworldOption)
                                    {
                                        case 1:
                                            PalworldTask.RegisterAccount();
                                            break;
                                        case 2:
                                            PalworldTask.LoginForTheFirstTime();
                                            break;
                                        case 3:
                                            PalworldTask.LoginAccount();
                                            break;
                                        default:
                                            ConsoleUI.Display_ErrorMessage();
                                            Thread.Sleep(3000);
                                            break;
                                    }
                                }while(palworldOption != 0);
                                break;
                            default:
                                ConsoleUI.Display_ErrorMessage();
                                Thread.Sleep(3000);
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
                    Console.ReadLine();
                    break;
            }
        }while (option != 0);
        ConsoleUI.Display_ThanksMessage();
    }
    
    
    
    

    

    
}