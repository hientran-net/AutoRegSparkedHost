using Src.Interface;
using System;
using System.Text;
using System.Threading.Channels;

namespace Src.Implementations;

public class ConsoleUI : IConsoleUI
{
    // Các phương thức hiển thị.
    
    // Hiển thị menu Tool.
    public static void Display_ToolsMenu()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("===== VUI LÒNG CHỌN TOOL ĐỂ HOST =====");
        Console.WriteLine("1. Web Hosting.");
        Console.WriteLine("2. Bot Hosting.");
        Console.WriteLine("0. Quay lại.");
    }
    
    // Hiển thị thông báo lỗi.
    public static void Display_ErrorMessage()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("!!! KHÔNG TỒN TẠI CHỨC NĂNG NÀY !!!");
    }
    
    // Hiển thị menu game.
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
        Console.WriteLine("0. Quay lại.");
    }
    
    // Hiển thị menu chính.
    public static void Display_MainMenu()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("===== CHÀO MỪNG TỚI TOOL REG HOST =====");
        Console.WriteLine("1. Host Games.");
        Console.WriteLine("2. Host Tools.");
        Console.WriteLine("0. Thoát.");
    }
    
    // Hiển thị thng báo cảm ơn.
    public static void Display_ThanksMessage()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("!!! CẢM ƠN ĐÃ SỬ DỤNG CHƯƠNG TRÌNH !!!");
    }

    public static void Display_PalworldMenu()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("MENU PALWORLD");
        Console.WriteLine("1. Đăng ký tài khoản.");
        Console.WriteLine("2. Đăng nhập lần đầu (nghĩa là vừa tạo tài khoản - chưa đăng nhập ần nào).");
        Console.WriteLine("3. Đăng nhập.");
        Console.WriteLine("0. Thoát.");
    }
}