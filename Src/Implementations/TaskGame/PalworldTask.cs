using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Src.Implementations.Helplers;
using Src.Interface.Task;
using Src.Interface.Helplers;

namespace Src.Implementations.TaskGame;


public class PalworldTask : IAutomationTask
{
    private static IWebDriver driver; 
        
    public static void RegisterAccount()
    {
        if (driver == null)
        {
            driver = new ChromeDriver();
        }
        // khởi tạo instace Element (phần tử)
        IWebElement webElement;
        
        // khởi tạo instance Wait Element (đợi phần tử)
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        
        // khởi tạo instace Javascript
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        
        // vào link.
        driver.Navigate().GoToUrl(FileReader.ReadLink("D:\\University\\Own_Project\\2025\\AutoRegSparkedHost\\Src\\Resources\\RegisterPalworld.txt"));
        
        // tìm - tick chọn server location.
        try
        {
            webElement = wait.Until(d =>
                d.FindElement(By.XPath("//*[@id=\"productConfigurableOptions\"]/div/div[2]/div/div[7]")));
            if (webElement.Displayed)
            {
                var location = webElement.Location;
                int x = location.X;
                int y = location.Y;

                js.ExecuteScript($"window.scrollTo({x}, {y});");
                if (webElement.Enabled)
                {
                    webElement.Click();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
        
        // tìm - click continue
        try
        {
            webElement = wait.Until(d => d.FindElement(By.XPath("//*[@id=\"orderSummary\"]/div[2]")));
            if (webElement.Displayed)
            {
                var location = webElement.Location;
                int x = location.X;
                int y = location.Y;
                js.ExecuteScript($"window.scrollTo({x}, {y});");
                if (webElement.Enabled)
                {
                    webElement.Click();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
        
        
        EnterToContinue:
        Console.WriteLine("Nhấn Enter để tiếp tục...");
        Console.ReadLine();
    }
}