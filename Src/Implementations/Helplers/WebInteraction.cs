using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Src.Interface.Helplers;

namespace Src.Implementations.Helplers;

public class WebInteraction : IWebInteraction
{
    /// <summary>
        /// Cuộn mượt đến vị trí của element, mô phỏng thao tác của con người.
        /// </summary>
        /// <param name="driver">IWebDriver instance</param>
        /// <param name="element">Element cần cuộn đến</param>
        public static void SmoothScrollToElement(IWebDriver driver, IWebElement element)
        {
            try
            {
                // Lấy vị trí của element
                var elementPosition = element.Location.Y;

                // Sử dụng JavaScript để cuộn mượt
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(
                    "window.scrollTo({" +
                    "  top: arguments[0]," +
                    "  behavior: 'smooth'" +
                    "});",
                    elementPosition - 100 // Trừ 100px để element không bị che bởi header
                );

                // Thêm độ trễ ngẫu nhiên để mô phỏng hành vi con người
                Random random = new Random();
                Thread.Sleep(random.Next(500, 1000)); // Đợi 0.5-1 giây
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cuộn đến element: {ex.Message}");
            }
        }

        /// <summary>
        /// Click mượt vào element, mô phỏng thao tác của con người.
        /// </summary>
        /// <param name="driver">IWebDriver instance</param>
        /// <param name="element">Element cần click</param>
        public static void SmoothClickElement(IWebDriver driver, IWebElement element)
        {
            try
            {
                // Đảm bảo element hiển thị và có thể tương tác
                if (!element.Displayed || !element.Enabled)
                {
                    throw new Exception("Element không hiển thị hoặc không thể click.");
                }

                // Sử dụng Actions để di chuyển chuột đến element trước khi click
                Actions actions = new Actions(driver);
                actions.MoveToElement(element)
                       .Pause(TimeSpan.FromMilliseconds(new Random().Next(100, 300))) // Tạm dừng ngẫu nhiên
                       .Click()
                       .Build()
                       .Perform();

                // Thêm độ trễ ngẫu nhiên sau khi click
                Thread.Sleep(new Random().Next(200, 500)); // Đợi 0.2-0.5 giây
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi click element: {ex.Message}");
            }
        }

        /// <summary>
        /// Nhập dữ liệu mượt vào ô input, mô phỏng thao tác của con người.
        /// </summary>
        /// <param name="driver">IWebDriver instance</param>
        /// <param name="element">Ô input cần nhập dữ liệu</param>
        /// <param name="text">Chuỗi văn bản cần nhập</param>
        public static void SmoothInputText(IWebDriver driver, IWebElement element, string text)
        {
            try
            {
                // Đảm bảo ô input hiển thị và có thể tương tác
                if (!element.Displayed || !element.Enabled)
                {
                    throw new Exception("Ô input không hiển thị hoặc không thể nhập.");
                }

                // Cuộn đến ô input để đảm bảo nó nằm trong tầm nhìn
                SmoothScrollToElement(driver, element);

                // Click vào ô input để focus
                SmoothClickElement(driver, element);

                // Xóa nội dung hiện tại (nếu có) một cách tự nhiên
                element.Clear();
                Thread.Sleep(new Random().Next(100, 300)); // Đợi ngẫu nhiên

                // Nhập từng ký tự một cách mượt mà
                Actions actions = new Actions(driver);
                foreach (char c in text)
                {
                    actions.SendKeys(c.ToString())
                           .Pause(TimeSpan.FromMilliseconds(new Random().Next(50, 150))) // Tạm dừng ngẫu nhiên giữa các ký tự
                           .Build()
                           .Perform();
                }

                // Thêm độ trễ ngẫu nhiên sau khi nhập xong
                Thread.Sleep(new Random().Next(300, 600)); // Đợi 0.3-0.6 giây
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi nhập dữ liệu: {ex.Message}");
            }
        }
}