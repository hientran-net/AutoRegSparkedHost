using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Src.Implementations.Helplers;
using SeleniumExtras.WaitHelpers;
using Src.Interface.Task;
using Src.Interface.Helplers;

namespace Src.Implementations.TaskGame;

public class PalworldTask : IAutomationTask
{
    private static IWebDriver driver = null;

    // Thêm phương thức hỗ trợ
    private static void HumanLikeClick(IWebElement element, IJavaScriptExecutor js, Random random)
    {
        // Cuộn từ từ đến phần tử
        var location = element.Location;
        js.ExecuteScript($"window.scrollBy(0, {location.Y - 100});"); // Cuộn gần đến
        Thread.Sleep(random.Next(300, 800)); // Đợi ngẫu nhiên 300-800ms
        js.ExecuteScript($"window.scrollTo({location.X}, {location.Y - 50});"); // Cuộn chính xác
        
        // Thêm chút ngẫu nhiên vào vị trí click
        int offsetX = random.Next(-5, 5);
        int offsetY = random.Next(-5, 5);
        Actions actions = new Actions(driver);
        actions.MoveToElement(element, offsetX, offsetY)
            .Pause(TimeSpan.FromMilliseconds(random.Next(100, 300)))
            .Click()
            .Perform();
    }

    private static void HumanLikeType(IWebElement element, string text, Random random)
    {
        element.Clear();
        foreach (char c in text)
        {
            element.SendKeys(c.ToString());
            Thread.Sleep(random.Next(50, 150)); // Tốc độ gõ ngẫu nhiên 50-150ms mỗi ký tự
        }
    }
    
    public static void RegisterAccount()
    {
        try
        {
            if (driver == null)
            {
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
            }

            // Khởi tạo instance Element.
            IWebElement webElement;

            // khởi tạo instance WaitElement.
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            // Khởi tạo instance Javascript.
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            
            // Khởi tạo instance Random.
            Random random = new Random();

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
            bool isServerLocationSelected = false;

            Console.Clear();
            Console.WriteLine("DANH SÁCH SERVER LOCATION:");
            Console.WriteLine("1. SINGAPORE.");
            Console.WriteLine("2. MUMBAI, INDIA.");
            Console.WriteLine("0. THOÁT CHƯƠNG TRÌNH.");
            Console.WriteLine();
            Console.WriteLine("Chọn server location: ");
            option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 0:
                    ConsoleUI.Display_ThanksMessage();
                    return;
                case 1:
                    try
                    {
                        webElement = wait.Until(d =>
                            d.FindElement(By.XPath("//*[@id=\"productConfigurableOptions\"]/div/div[2]/div/div[6]")));
                        if (webElement.Displayed && webElement.Enabled)
                        {
                            HumanLikeClick(webElement, js, random);
                            isServerLocationSelected = true;
                            Console.WriteLine($"Đã chọn server location: {webElement.Text}");
                            Thread.Sleep(random.Next(500, 1500)); // Đợi ngẫu nhiên sau khi click
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
                    try
                    {
                        webElement = wait.Until(d =>
                            d.FindElement(By.XPath("//*[@id=\"productConfigurableOptions\"]/div/div[2]/div/div[7]")));
                        if (webElement.Displayed && webElement.Enabled)
                        {
                            HumanLikeClick(webElement, js, random);
                            isServerLocationSelected = true;
                            Console.WriteLine($"Đã chọn server location: {webElement.Text}");
                            Thread.Sleep(random.Next(500, 1500)); // Đợi ngẫu nhiên sau khi click
                        }
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine("Hết thời gian chờ khi tìm phần tử Mumbai, India.");
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("Không tìm thấy phần tử Singapore Mumbai, India.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi chọn Mumbai, India: {ex.GetType().Name} - {ex.Message}");
                        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    }

                    break;
                default:
                    ConsoleUI.Display_ErrorMessage();
                    break;
            }

            // Tìm và nhấn Continue.
            if (isServerLocationSelected)
            {
                try
                {
                    webElement = wait.Until(d => d.FindElement(By.Id("btnCompleteProductConfig")));
                    if (webElement.Displayed && webElement.Enabled)
                    {
                        HumanLikeClick(webElement, js, random);
                        Thread.Sleep(random.Next(500, 1500)); // Đợi ngẫu nhiên sau khi click
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
                    Thread.Sleep(retryDelayMs);
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
            
            // Điền thông tin billing
            if (isCheckoutBtnPressed)
            {
                try
                {
                    // Đợi form xuất hiện
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("frmCheckout")));
                    Console.WriteLine("Đã vào trang điền thông tin thanh toán.");
                    Thread.Sleep(random.Next(1000, 2000)); // Đợi tự nhiên khi vào form
                    
                    // 1. Personal Information
                    // First Name
                    IWebElement firstNameInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputFirstName")));
                    string firstName = FakeDataGenerator.GetFirstName();
                    HumanLikeType(firstNameInput, firstName, random);
                    Console.WriteLine($"Đã điền First Name: {firstName}");
                    Thread.Sleep(random.Next(300, 800));
                    
                    // Last Name
                    IWebElement lastNameInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputLastName")));
                    string lastName = FakeDataGenerator.GetLastName();
                    HumanLikeType(lastNameInput, lastName, random);
                    Console.WriteLine($"Đã điền Last Name: {lastName}");
                    Thread.Sleep(random.Next(300, 800));
                    
                    // Email
                    string emailPath = @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\emails.txt";
                    var existingEmail = FakeDataGenerator.LoadExistingEmails(emailPath);
                    Console.WriteLine($"Tạo email thành công.");
                    IWebElement emailInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputEmail")));
                    string newEmail = FakeDataGenerator.GenerateEmail(existingEmail);
                    HumanLikeType(emailInput, newEmail, random);
                    Console.WriteLine($"Đã điền Email: {newEmail}");
                    Thread.Sleep(random.Next(500, 1200));
                    
                    // PhoneNumber:
                    // 1. Mở danh sách mã vùng bằng cách nhấp vào selected-flag
                    IWebElement flagContainer = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("selected-flag")));
                    HumanLikeClick(flagContainer, js, random);
                    Thread.Sleep(random.Next(400, 900));
                    
                    // 2. Chọn mã vùng (ví dụ: +84 cho Việt Nam)
                    IWebElement vietnamOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@data-dial-code='84']")));
                    HumanLikeClick(vietnamOption, js, random);
                    Thread.Sleep(random.Next(400, 900));
                    
                    // 3. Tạo số điện thoại
                    string phonenumberPath =
                        @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\phonenumbers.txt";
                    var existingPhonenumber = FakeDataGenerator.LoadExistingPhones(phonenumberPath);
                    string newPhonenumber = FakeDataGenerator.GenerateVietnamPhone(existingPhonenumber);
                    Console.WriteLine("Tạo số điện thoại mới thành công.");
                    Console.WriteLine($"Số điện thoại mới: {newPhonenumber}.");
                    
                    // 4. Nhập số điện thoại vào inputPhone
                    IWebElement phoneInput = driver.FindElement(By.Id("inputPhone"));
                    phoneInput.Clear(); // Xóa giá trị hiện tại
                    HumanLikeType(phoneInput, newPhonenumber, random);
                    
                    // 5. Cập nhật giá trị input ẩn
                    js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("document.getElementById('populatedCountryCodephonenumber').value = '84';");
                    
                    // In ra kết quả để kiểm tra
                    Console.WriteLine("Đã chọn mã vùng +84 và nhập số điện thoại thành công!");
                    
                    
                    // 2. Billing Address
                    // Address 1
                    IWebElement address1Input = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputAddress1")));
                    string street = FakeDataGenerator.GetStreet();
                    string city = FakeDataGenerator.GetCity();
                    string address_1 = $"{street}, {city}";
                    HumanLikeType(address1Input, address_1, random);
                    Console.WriteLine($"Đã điền Address 1: {address_1}");
                    
                    // City
                    IWebElement cityInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputCity")));
                    cityInput.Clear();
                    cityInput.SendKeys(city);
                    HumanLikeType(cityInput, city, random);
                    Console.WriteLine($"Đã điền City: {city}");
                    
                    // Country
                    // Tìm phần tử <select> bằng ID
                    IWebElement countrySelect = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("inputCountry")));

                    // Tạo đối tượng SelectElement để xử lý dropdown
                    SelectElement selectElement = new SelectElement(countrySelect);

                    // Chọn quốc gia "Viet Nam" bằng giá trị (value="VN")
                    selectElement.SelectByValue("VN");

                    // Kiểm tra giá trị đã chọn
                    string selectedValue = selectElement.SelectedOption.GetAttribute("value");
                    Console.WriteLine($"Quốc gia đã chọn: {selectedValue}");

                    // In tên quốc gia đã chọn
                    string selectedText = selectElement.SelectedOption.Text;
                    Console.WriteLine($"Tên quốc gia: {selectedText}");
                    
                    // Postcode
                    IWebElement postcodeInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputPostcode")));
                    string postCode = FakeDataGenerator.getPostCode();
                    HumanLikeType(postcodeInput, postCode, random);
                    Console.WriteLine($"Đã điền Postcode: {postCode}");
                    
                    // State
                    // Tìm trường <input> bằng ID
                    IWebElement stateInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("stateinput")));
                    // Xóa giá trị hiện tại nếu có và nhập giá trị mới
                    stateInput.Clear();
                    string stateName = FakeDataGenerator.GetState();
                    HumanLikeType(stateInput, stateName, random);
                    // Kiểm tra giá trị đã nhập
                    string enteredValue = stateInput.GetAttribute("value");
                    Console.WriteLine($"Giá trị đã nhập: {enteredValue}");
                    
                    // Tìm checkbox bằng ID
                    IWebElement checkbox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("customfield279")));
                    // Kiểm tra xem checkbox đã được chọn chưa, nếu chưa thì chọn
                    if (!checkbox.Selected)
                    {
                        IWebElement iCheckContainer = driver.FindElement(By.Id("iCheck-customfield279"));
                        iCheckContainer.Click();
                    }
                    // (Tùy chọn) Kiểm tra trạng thái sau khi chọn
                    bool isChecked = checkbox.Selected;
                    Console.WriteLine($"Checkbox đã được chọn: {isChecked}");
                    
                    // Password
                    // 1. Nhấn nút "Generate Password"
                    IWebElement generateButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("generate-password")));
                    generateButton.Click();

                    // 2. Chờ popup xuất hiện và nhấn nút "Copy to clipboard and Insert"
                    // Tìm nút "Copy to clipboard and Insert" bằng văn bản
                    IWebElement copyAndInsertButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Copy to clipboard and Insert')]")));
                    copyAndInsertButton.Click();

                    // 3. (Tùy chọn) Kiểm tra giá trị đã điền vào các trường mật khẩu
                    IWebElement passwordField1 = driver.FindElement(By.Id("inputNewPassword1"));
                    IWebElement passwordField2 = driver.FindElement(By.Id("inputNewPassword2"));

                    string password1 = passwordField1.GetAttribute("value");
                    string password2 = passwordField2.GetAttribute("value");

                    Console.WriteLine($"Mật khẩu trong inputNewPassword1: {password1}");
                    Console.WriteLine($"Mật khẩu trong inputNewPassword2: {password2}");

                    // Kiểm tra xem hai trường có cùng giá trị không
                    if (password1 == password2 && !string.IsNullOrEmpty(password1))
                    {
                        Console.WriteLine("Mật khẩu đã được điền thành công và khớp nhau!");
                        string passwordFilePath =
                            @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\passwords.txt";
                        FakeDataGenerator.SavePasswordToFile(passwordFilePath, password1);
                    }
                    else
                    {
                        Console.WriteLine("Có lỗi: Mật khẩu không khớp hoặc không được điền.");
                    }
                    
                    // Chọn phương thức thanh toán.
                    // 1. Chọn ngẫu nhiên một phương thức thanh toán (từ ô thứ hai trở đi)
                    List<string> paymentMethodXPaths = new List<string>
                    {
                        "//*[@id=\"paymentGatewaysContainer\"]/div/div[1]/div/div[3]/div/div/label", // ACH Bank Transfer (Plaid)
                        "//*[@id=\"paymentGatewaysContainer\"]/div/div[1]/div/div[4]/div/div/label", // PaySafeCard
                        "//*[@id=\"paymentGatewaysContainer\"]/div/div[1]/div/div[5]/div/div/label", // For Skrill, Venmo or CashApp
                        "//*[@id=\"paymentGatewaysContainer\"]/div/div[1]/div/div[6]/div/div/label"  // Paymentwall
                    };

                    // Chọn ngẫu nhiên một XPath
                    string selectedXPath = paymentMethodXPaths[random.Next(0, paymentMethodXPaths.Count)];
                    Console.WriteLine($"Đang chọn phương thức thanh toán với XPath: {selectedXPath}");

                    // Tìm và nhấp vào phương thức thanh toán
                    IWebElement clickChoice = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(selectedXPath)));
                    clickChoice.Click();
                    Console.WriteLine("Chọn phương thức thanh toán thành công!");

                    // 2. Tick chọn "I have read"
                    IWebElement tickIHaveRead = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("iCheck-accepttos")));
                    tickIHaveRead.Click();
                    Console.WriteLine("Tick chọn \"I have read\" thành công!");
                    
                    // 3. Nhấn "Complete Order"
                    // Thêm độ trễ 2-3 giây sau khi tick chọn
                    int delaySeconds = new Random().Next(2000, 4000); // Ngẫu nhiên từ 2 đến 3 giây
                    Console.WriteLine($"Đợi {delaySeconds / 1000.0} giây trước khi tiếp tục...");
                    IWebElement completeOrder = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnCompleteOrder")));
                    completeOrder.Click();
                    Console.WriteLine("Nhấn \"Complete Order\" thành công!");
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Hết thời gian chờ khi điền thông tin thanh toán.");
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine($"Không tìm thấy phần tử khi điền thông tin thanh toán: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi điền thông tin thanh toán: {ex.GetType().Name} - {ex.Message}");
                }
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
        }
    }

    public static void LoginForTheFirstTime()
    {
        try
        {
            // Kiểm tra xem driver đã được khởi tạo chưa
            if (driver == null)
            {
                Console.WriteLine("Trình duyệt chưa được khởi tạo. Vui lòng chạy RegisterAccount() trước.");
                return;
            }
            // Khởi tạo các instance
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Random random = new Random();
            
            // Điều hướng đến trang đăng nhập (thay bằng URL thực tế của bạn)
            string loginUrl = "https://control.sparkedhost.us/"; // Thay bằng URL đăng nhập thực tế
            driver.Navigate().GoToUrl(loginUrl);
            Console.WriteLine($"Đã điều hướng đến trang đăng nhập: {loginUrl}");
            Thread.Sleep(random.Next(1000, 2000)); // Đợi tự nhiên
            
            // Nhấn nút đăng nhập
            IWebElement billingLoginButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//button[contains(text(), 'Login via Billing')]")));
            HumanLikeClick(billingLoginButton, js, random);
            Console.WriteLine("Đã nhấn 'Login via Billing'");
            Thread.Sleep(random.Next(1000, 2000));
            
            // Kiểm tra trang Authorise
            try
            {
                IWebElement authoriseButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.Id("userAuthorizationAccepted")));
                if (authoriseButton.Displayed && authoriseButton.Enabled)
                {
                    HumanLikeClick(authoriseButton, js, random);
                    Console.WriteLine("Đã nhấn nút 'Authorise' trên trang ủy quyền");
                    Thread.Sleep(random.Next(1000, 2000)); // Chờ sau khi nhấn Authorise
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Không tìm thấy trang Authorise. Tiếp tục kiểm tra đăng nhập...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xử lý trang Authorise: {ex.GetType().Name} - {ex.Message}");
            }
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi đăng nhập: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Nhấn Enter để tiếp tục (trình duyệt vẫn mở)...");
            Console.ReadLine();
            // Không đóng driver ở đây nếu muốn tiếp tục sử dụng
        }
    }

    public static void LoginAccount()
    {
        try
        {
            // Kiểm tra xem driver đã được khởi tạo chưa
            if (driver == null)
            {
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
            }
            
            // Khởi tạo các instance
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Random random = new Random();
            
            // Điều hướng đến trang đăng nhập (thay bằng URL thực tế của bạn)
            string loginUrl = "https://control.sparkedhost.us/"; // Thay bằng URL đăng nhập thực tế
            driver.Navigate().GoToUrl(loginUrl);
            Console.WriteLine($"Đã điều hướng đến trang đăng nhập: {loginUrl}");
            Thread.Sleep(random.Next(1000, 2000)); // Đợi tự nhiên
            
            // Nhấn nút đăng nhập
            IWebElement billingLoginButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//button[contains(text(), 'Login via Billing')]")));
            HumanLikeClick(billingLoginButton, js, random);
            Console.WriteLine("Đã nhấn 'Login via Billing'");
            Thread.Sleep(random.Next(1000, 2000));
            
            // Tìm phần input email và password
            string email = FileReader.GetEmailToLogin();
            string password = FileReader.GetPasswordToLogin(email);
            
            IWebElement emailInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputEmail")));
            HumanLikeType(emailInput, email, random);
            Console.WriteLine($"Đã nhập email: {email}");
            
            IWebElement passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputPassword")));
            HumanLikeType(passwordInput, password, random);
            Console.WriteLine($"Đã nhập password: {password}");
            
            // Tìm nút login
            IWebElement loginBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnLogin")));
            HumanLikeClick(loginBtn, js, random);
            Console.WriteLine("Đã nhấn login.");
            
            // Kiểm tra trang Authorise
            try
            {
                IWebElement authoriseButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.Id("userAuthorizationAccepted")));
                if (authoriseButton.Displayed && authoriseButton.Enabled)
                {
                    HumanLikeClick(authoriseButton, js, random);
                    Console.WriteLine("Đã nhấn nút 'Authorise' trên trang ủy quyền");
                    Thread.Sleep(random.Next(1000, 2000)); // Chờ sau khi nhấn Authorise
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Không tìm thấy trang Authorise. Tiếp tục kiểm tra đăng nhập...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xử lý trang Authorise: {ex.GetType().Name} - {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi đăng nhập: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Nhấn Enter để tiếp tục (trình duyệt vẫn mở)...");
            Console.ReadLine();
            // Không đóng driver ở đây nếu muốn tiếp tục sử dụng
        }
    }
}