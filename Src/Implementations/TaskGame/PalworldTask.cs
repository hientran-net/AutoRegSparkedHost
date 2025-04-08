using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Src.Implementations.Helplers;
using SeleniumExtras.WaitHelpers;
using Src.Interface.Task;
using Src.Interface.Helplers;

namespace Src.Implementations.TaskGame;

public class PalworldTask : IAutomationTask
{
    private static IWebDriver driver = null;

    public static void RegisterAccount()
    {
        try
        {
            // khởi tạo driver:
            try
            {
                driver = new ChromeDriver();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi khởi tạo ChromeDriver: {ex.GetType().Name} - {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return; // thoát phương thức nếu không khởi tạo được driver 
            }

            // Khởi tạo instance Element.
            IWebElement webElement;

            // khởi tạo instance WaitElement.
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            // Khởi tạo instance Javascript.
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            try
            {
                string url =
                    FileReader.ReadLink(
                        "D:\\University\\Own_Project\\2025\\AutoRegSparkedHost\\Src\\Resources\\RegisterPalworld.txt");
                driver.Navigate().GoToUrl(url);
                Console.WriteLine($"Đã điều hướng đến URL: {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi điều hướng đến URL: {ex.GetType().Name} - {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return; // Thoát phương thức nếu không điều hướng đến url được.
            }

            // Tìm và chọn server location
            int option;
            bool isSingaporeSelected = false;

            Console.Clear();
            Console.WriteLine("DANH SÁCH SERVER LOCATION:");
            Console.WriteLine("1. BUFFALO, NEW YORK.");
            Console.WriteLine("2. DALLAS, TEXAS.");
            Console.WriteLine("3. MIAMI, FLORIDA.");
            Console.WriteLine("4. SALT LAKE CITY, UTAH (WEST COAST).");
            Console.WriteLine("5. PARIS, FRANCE.");
            Console.WriteLine("6. SYDNEY, AUSTRALIA.");
            Console.WriteLine("7. SINGAPORE.");
            Console.WriteLine("8. MUMBAI, INDIA.");
            Console.WriteLine("0. THOÁT CHƯƠNG TRÌNH.");
            Console.WriteLine();
            Console.WriteLine("Chọn server location: ");
            option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 0:
                    ConsoleUI.Display_ThanksMessage();
                    return;
                case 7:
                    try
                    {
                        webElement = wait.Until(d =>
                            d.FindElement(By.XPath("//*[@id=\"productConfigurableOptions\"]/div/div[2]/div/div[7]")));
                        if (webElement.Displayed && webElement.Enabled)
                        {
                            var location = webElement.Location;
                            int x = location.X;
                            int y = location.Y;
                            js.ExecuteScript($"window.scrollTo({x}, {y});");
                            webElement.Click();
                            isSingaporeSelected = true;
                            Console.WriteLine("Đã chọn server location: Singapore");
                        }
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine("Hết thời gian chờ khi tìm phần tử Singapore.");
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("Không tìm thấy phần tử Singapore.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi chọn Singapore: {ex.GetType().Name} - {ex.Message}");
                        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    }

                    break;
                case 2:

                default:
                    ConsoleUI.Display_ErrorMessage();
                    break;
            }

            // Tìm và nhấn Continue.
            if (isSingaporeSelected)
            {
                try
                {
                    webElement = wait.Until(d => d.FindElement(By.Id("btnCompleteProductConfig")));
                    if (webElement.Displayed && webElement.Enabled)
                    {
                        var location = webElement.Location;
                        int x = location.X;
                        int y = location.Y;
                        js.ExecuteScript($"window.scrollTo({x}, {y});");
                        webElement.Click();
                        Console.WriteLine("Đã nhấn vào nút Continue.");
                    }
                    else
                    {
                        Console.WriteLine("Nút Continue không hiển thị hoặc không thể tương tác.");
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Hết thời gian chờ khi tìm nút Continue.");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Không tìm thấy nút Continue.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi nhấn Continue: {ex.GetType().Name} - {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine("Chưa chọn server location !!!");
                return;
            }

            // Tìm và nhấn nút Checkout
            // Định nghĩa danh sách các cặp (locator, tên phương thức)
            var locators = new List<(Func<By> Locator, string MethodName)>
            {
                (() => By.Id("checkout"), "Id"),
                (() => By.CssSelector("a[href='/cart.php?a=checkout&e=false']"), "CssSelector"),
                (() => By.XPath("//a[@id='checkout']"), "XPath"),
                (() => By.LinkText("Checkout"), "LinkText")
            };

            bool isCheckoutBtnPressed = false;
            int maxRetries = 3; // Số lần thử lại tối đa
            int retryDelayMs = 2000; // Thời gian chờ giữa các lần thử lại (2 giây)

            for (int attempt = 0; attempt < maxRetries && !isCheckoutBtnPressed; attempt++)
            {
                foreach (var locator in locators)
                {
                    try
                    {
                        // Chờ cho đến khi phần tử có thể nhấn được
                        webElement = wait.Until(ExpectedConditions.ElementToBeClickable(locator.Locator()));

                        // Cuộn đến vị trí của phần tử
                        var location = webElement.Location;
                        int x = location.X;
                        int y = location.Y;
                        js.ExecuteScript($"window.scrollTo({x}, {y});");

                        // Thử nhấn bằng Selenium
                        try
                        {
                            webElement.Click();
                            isCheckoutBtnPressed = true;
                            Console.WriteLine($"Đã nhấn nút Checkout (bằng {locator.MethodName}).");
                            break; // Thoát vòng lặp nếu nhấn thành công
                        }
                        catch (ElementClickInterceptedException)
                        {
                            Console.WriteLine(
                                $"Nút Checkout bị che khuất khi dùng {locator.MethodName}, thử nhấn bằng JavaScript...");
                            // Nếu bị che khuất, thử nhấn bằng JavaScript
                            js.ExecuteScript("arguments[0].click();", webElement);
                            isCheckoutBtnPressed = true;
                            Console.WriteLine($"Đã nhấn nút Checkout (bằng JavaScript với {locator.MethodName}).");
                            break;
                        }
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine($"Hết thời gian chờ khi tìm nút Checkout bằng {locator.MethodName}.");
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine($"Không tìm thấy nút Checkout bằng {locator.MethodName}.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            $"Lỗi khi nhấn Checkout bằng {locator.MethodName}: {ex.GetType().Name} - {ex.Message}");
                    }

                    // Nếu đã nhấn thành công, thoát vòng lặp ngoài
                    if (isCheckoutBtnPressed) break;

                    // Chờ một chút trước khi thử lại với cách định vị tiếp theo
                    System.Threading.Thread.Sleep(retryDelayMs);
                }

                // Nếu đã thử hết các cách định vị mà vẫn thất bại, in thông báo
                if (!isCheckoutBtnPressed && attempt < maxRetries - 1)
                {
                    Console.WriteLine($"Thử lại lần {attempt + 2}/{maxRetries}...");
                }
            }

            // Kiểm tra kết quả cuối cùng
            if (!isCheckoutBtnPressed)
            {
                Console.WriteLine("Không thể nhấn nút Checkout sau tất cả các lần thử.");
            }
        }
        catch (Exception ex)
        {
            // Bắt các lỗi không mong muốn ở cấp cao nhất
            Console.WriteLine($"Lỗi không mong muốn: {ex.GetType().Name} - {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
        finally
        {
            Console.WriteLine("Nhấn Enter để tiếp tục...");
            Console.ReadLine();
            // Đảm bảo driver được đóng
            if (driver != null)
            {
                try
                {
                    driver.Quit();
                    Console.WriteLine("Đã đóng trình duyệt.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi đóng trình duyệt: {ex.GetType().Name} - {ex.Message}");
                }
            }
        }
    }
}