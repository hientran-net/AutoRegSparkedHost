using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Script;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Src.Implementations.Helplers;
using Src.Interface.Task;

namespace Src.Implementations.TaskGame;

public class MinecraftTask : IAutomationTask
{
    private static IWebDriver driver = null;

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
                    Console.WriteLine($"Lỗi khi kởi tạo ChromeDriver: {ex.GetType().Name} - {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    return;
                }
            }

            // Tạo các instance
            // Khởi tạo instance Element.
            IWebElement webElement;

            // khởi tạo instance WaitElement.
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            // Khởi tạo instance Javascript.
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            // Khởi tạo instance Random.
            Random random = new Random();


            // Điều hướng đến url
            try
            {
                string url = "https://billing.sparkedhost.com/cart.php?a=add&pid=292";
                driver.Navigate().GoToUrl(url);
                Console.WriteLine($"Đã điều hướng đến url: {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi điều hướng đến URL: {ex.GetType().Name} - {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return;
            }


            // Server location:
            try
            {
                webElement = wait.Until(d => d.FindElement(By.XPath("//*[@id=\"productConfigurableOptions\"]/div[1]")));
                WebInteraction.SmoothScrollToElement(driver, webElement);
                Console.WriteLine($"Đã cuộn đến: {webElement.Text}");

                int serverLocationOption;
                Console.Clear();
                Console.WriteLine("1. Singapore.");
                Console.WriteLine("2. Mumbai.");
                Console.Write("Chọn: ");
                serverLocationOption = Convert.ToInt32(Console.ReadLine());

                switch (serverLocationOption)
                {
                    case 1:
                        webElement = wait.Until(d =>
                            d.FindElement(By.XPath(
                                "//*[@id=\"productConfigurableOptions\"]/div[1]/div[2]/div/div[7]/div/div/label")));
                        if (webElement.Displayed && webElement.Enabled)
                        {
                            WebInteraction.SmoothScrollToElement(driver, webElement);
                            WebInteraction.SmoothClickElement(driver, webElement);
                        }
                        else
                        {
                            Console.WriteLine("Element bị ẩn hoặc không thể click.");
                        }

                        break;

                    case 2:
                        webElement = wait.Until(d =>
                            d.FindElement(By.XPath(
                                "//*[@id=\"productConfigurableOptions\"]/div[1]/div[2]/div/div[8]/div/div/label")));
                        if (webElement.Displayed && webElement.Enabled)
                        {
                            WebInteraction.SmoothScrollToElement(driver, webElement);
                            WebInteraction.SmoothClickElement(driver, webElement);
                        }
                        else
                        {
                            Console.WriteLine("Element bị ẩn hoặc không thể click.");
                        }

                        break;
                }

                // Continue
                webElement = wait.Until(d => d.FindElement(By.XPath("//*[@id=\"btnCompleteProductConfig\"]")));
                WebInteraction.SmoothClickElement(driver, webElement);
            }
            catch (Exception ex)
            {
                // Bắt các lỗi không mong muốn ở cấp cao nhất
                Console.WriteLine($"Lỗi không mong muốn: {ex.GetType().Name} - {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

            // Checkout button
            try
            {
                webElement =
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("checkout")));
                WebInteraction.SmoothScrollToElement(driver, webElement);
                WebInteraction.SmoothClickElement(driver, webElement);
                Console.WriteLine("Đã click nút Checkout.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi click nút Checkout: {ex.Message}");
            }

            // Điền thông tin billing
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("frmCheckout")));
                Console.WriteLine("Đã vào trang điền thông tin thanh toán.");
                Thread.Sleep(random.Next(1000, 2000)); // Đợi tự nhiên khi vào form

                // 1. Personal Information
                // First Name
                IWebElement firstNameInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputFirstName")));
                string firstName = FakeDataGenerator.GetFirstName();
                WebInteraction.SmoothInputText(driver, firstNameInput, firstName);
                Console.WriteLine($"Đã điền First Name: {firstName}");

                // Last Name
                IWebElement lastNameInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputLastName")));
                string lastName = FakeDataGenerator.GetLastName();
                WebInteraction.SmoothInputText(driver, lastNameInput, lastName);
                Console.WriteLine($"Đã điền Last Name: {lastName}");

                // Email
                string emailPath =
                    @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\Minecraft\emails.txt";
                var existingEmail = FakeDataGenerator.LoadExistingEmails();
                Console.WriteLine($"Tạo email thành công.");
                IWebElement emailInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputEmail")));
                string newEmail = FakeDataGenerator.GenerateEmail(existingEmail, emailPath);
                WebInteraction.SmoothInputText(driver, emailInput, newEmail);
                Console.WriteLine($"Đã điền Email: {newEmail}");

                // Phonenumber:
                // 1. Mở danh sách mã vùng bằng cách nhấp vào selected-flag
                IWebElement flagContainer =
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("selected-flag")));
                WebInteraction.SmoothClickElement(driver, flagContainer);

                // 2. Chọn mã vùng (ví dụ: +84 cho Việt Nam)
                IWebElement vietnamOption =
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@data-dial-code='84']")));
                WebInteraction.SmoothClickElement(driver, vietnamOption);

                // 3. Tạo số điện thoại
                string phonenumberPath =
                    @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\Palworld\phonenumbers.txt";
                var existingPhonenumber = FakeDataGenerator.LoadExistingPhones(phonenumberPath);
                string newPhonenumber = FakeDataGenerator.GenerateVietnamPhone(existingPhonenumber);
                Console.WriteLine("Tạo số điện thoại mới thành công.");
                Console.WriteLine($"Số điện thoại mới: {newPhonenumber}.");

                // 4. Nhập số điện thoại vào inputPhone
                IWebElement phoneInput = driver.FindElement(By.Id("inputPhone"));
                phoneInput.Clear(); // Xóa giá trị hiện tại
                WebInteraction.SmoothInputText(driver, phoneInput, newPhonenumber);

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
                WebInteraction.SmoothInputText(driver, address1Input, address_1);
                Console.WriteLine($"Đã điền Address 1: {address_1}");

                // City
                IWebElement cityInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputCity")));
                cityInput.Clear();
                WebInteraction.SmoothInputText(driver, cityInput, city);
                Console.WriteLine($"Đã điền City: {city}");

                // Country
                // Tìm phần tử <select> bằng ID
                IWebElement countrySelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("inputCountry")));

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
                WebInteraction.SmoothInputText(driver, postcodeInput, postCode);
                Console.WriteLine($"Đã điền Postcode: {postCode}");

                // State
                // Tìm trường <input> bằng ID
                IWebElement stateInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("stateinput")));
                // Xóa giá trị hiện tại nếu có và nhập giá trị mới
                stateInput.Clear();
                string stateName = FakeDataGenerator.GetState();
                WebInteraction.SmoothInputText(driver, stateInput, stateName);
                // Kiểm tra giá trị đã nhập
                string enteredValue = stateInput.GetAttribute("value");
                Console.WriteLine($"Giá trị đã nhập: {enteredValue}");

                // Tìm checkbox bằng ID
                IWebElement checkbox =
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("customfield279")));
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
                IWebElement generateButton =
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                        By.ClassName("generate-password")));
                generateButton.Click();

                // 2. Chờ popup xuất hiện và nhấn nút "Copy to clipboard and Insert"
                // Tìm nút "Copy to clipboard and Insert" bằng văn bản
                IWebElement copyAndInsertButton =
                    wait.Until(ExpectedConditions.ElementToBeClickable(
                        By.XPath("//button[contains(text(), 'Copy to clipboard and Insert')]")));
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
                        @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\Minecraft\password.txt";
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
                    "//*[@id=\"paymentGatewaysContainer\"]/div/div[1]/div/div[6]/div/div/label" // Paymentwall
                };

                // Chọn ngẫu nhiên một XPath
                string selectedXPath = paymentMethodXPaths[random.Next(0, paymentMethodXPaths.Count)];
                Console.WriteLine($"Đang chọn phương thức thanh toán với XPath: {selectedXPath}");

                // Tìm và nhấp vào phương thức thanh toán
                IWebElement clickChoice = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(selectedXPath)));
                clickChoice.Click();
                Console.WriteLine("Chọn phương thức thanh toán thành công!");

                // 2. Tick chọn "I have read"
                IWebElement tickIHaveRead =
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("iCheck-accepttos")));
                tickIHaveRead.Click();
                Console.WriteLine("Tick chọn \"I have read\" thành công!");

                // 3. Nhấn "Complete Order"
                // Thêm độ trễ 2-3 giây sau khi tick chọn
                int delaySeconds = new Random().Next(2000, 4000); // Ngẫu nhiên từ 2 đến 3 giây
                Console.WriteLine($"Đợi {delaySeconds / 1000.0} giây trước khi tiếp tục...");
                IWebElement completeOrder =
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnCompleteOrder")));
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

    public static void LoginAccount()
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
                    Console.WriteLine($"Lỗi khi kởi tạo ChromeDriver: {ex.GetType().Name} - {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    return;
                }
            }

            // Tạo các instance
            // Khởi tạo instance Element.
            IWebElement webElement;

            // khởi tạo instance WaitElement.
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            // Khởi tạo instance Javascript.
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            // Khởi tạo instance Random.
            Random random = new Random();
                
            // Điều hướng đến URL
            driver.Navigate().GoToUrl("https://control.sparkedhost.us/");

            // Login via Billing
            webElement = wait.Until(d => d.FindElement(By.XPath(@"//*[@id=""app""]/div[2]/div/div/button[2]")));
            WebInteraction.SmoothClickElement(driver, webElement);

            // Email
            IWebElement emailInput =
                wait.Until(d => d.FindElement(By.Id("inputEmail")));
            WebInteraction.SmoothClickElement(driver, emailInput);
            string emailFilePath =
                @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\Minecraft\emails.txt";

            string email = FileInteract.GetEmailToLogin(emailFilePath);
            WebInteraction.SmoothInputText(driver, emailInput, email);
            Console.WriteLine("Nhập email thành công.");
            Console.WriteLine($"Email vừa nhập: {email}");

            // Password
            IWebElement passwordInput = wait.Until(d => d.FindElement(By.Id("inputPassword")));
            string passwordFilePath =
                @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\Minecraft\password.txt";
            string password = FileInteract.GetPasswordToLogin(email, passwordFilePath);
            WebInteraction.SmoothInputText(driver, passwordInput, password);
            Console.WriteLine("Nhập password thành công.");
            Console.WriteLine($"Password vừa nhập: {password}");

            // Login btn
            webElement = wait.Until(d => d.FindElement(By.Id("btnLogin")));
            WebInteraction.SmoothClickElement(driver, webElement);
            Console.WriteLine("Đã nhấn login.");

            // check authorise btn
            try
            {
                WebDriverWait waitBtn = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement authoriseBtn = waitBtn.Until(d => d.FindElement(By.Id("userAuthorizationAccepted")));
                Console.WriteLine("Phát hiện Pop up ủy quyền.");
                WebInteraction.SmoothClickElement(driver, authoriseBtn);
                Console.WriteLine("Đã nhấn ủy quyền");
            }
            catch (TimeoutException exTimeout)
            {
                Console.WriteLine("Không phát hiện pop up ủy quyền. Tiếp tục ...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã có lỗi xảy ra trong lúc check pop up ủy quyền: {ex.Message}");
            }
            
            // kiểm tra setup
            bool isServerSetuped = false;
            IWebElement serverSetup = wait.Until(d => d.FindElement(By.Id("list")));
            if (serverSetup.Displayed)
            {
                Console.WriteLine($"Tìm thấy: {serverSetup.Text}");
                string serverStatus = serverSetup.Text;
                if (serverStatus.ToLower().Contains("pending"))
                {
                    Console.WriteLine("Server chưa được setup.");
                    WebInteraction.SmoothClickElement(driver, serverSetup);
                    string url = driver.Url;
                    string serverId = StringInteract.GetServerIdFromUrl(url);
                    string filePath =
                        @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\Minecraft\serverId";
                    FileInteract.SaveContentToFile(filePath, serverId);
                    
                    // popup welcome to server
                    serverSetup = wait.Until(d => d.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div/div")));
                    Console.WriteLine($"POP UP: {serverSetup.Text}");
                    
                    // nextBtn
                    serverSetup = wait.Until(d =>
                        d.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div/div/div/button[2]")));
                    WebInteraction.SmoothClickElement(driver, serverSetup);
                    Console.WriteLine($"Nhấn next");
                    
                    // nextBtn
                    serverSetup = wait.Until(d =>
                        d.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div/div/div[2]/div[2]/button")));
                    WebInteraction.SmoothClickElement(driver, serverSetup);
                    Console.WriteLine($"Nhấn next");
                    
                    // skipBtn
                    serverSetup = wait.Until(d =>
                        d.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[2]/div/div/div[3]/button[1]")));
                    WebInteraction.SmoothClickElement(driver, serverSetup);
                    Console.WriteLine($"Nhấn skip");
                    
                    // // skipCreationBtn
                    // try
                    // {
                    //     serverSetup = wait.Until(d =>
                    //         d.FindElement(By.XPath("//*[@id=\"headlessui-dialog-panel-:r1f:\"]/div[2]/button[2]")));
                    //     WebInteraction.SmoothClickElement(driver, serverSetup);
                    //     Console.WriteLine($"Nhấn skip creation");
                    // }
                    // catch (TimeoutException ex)
                    // {
                    //     Console.WriteLine($"Không thể tương tác với element. {ex.Message}");
                    // }
                    
                    driver.Navigate().GoToUrl($"https://control.sparkedhost.us/server/{serverId}/setup/version");

                    WebDriverWait waitToSelect = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    serverSetup = waitToSelect.Until(ExpectedConditions.ElementExists(
                        By.CssSelector("div.flex.mt-4.gap-2")
                    ));

                    string jsScript = @"
                function selectOptionByValue(selectIndex, value) {
                    var selects = document.querySelectorAll('div.flex.mt-4.gap-2 select');
                    if (selectIndex < 0 || selectIndex >= selects.length) {
                        console.error('Invalid select index: ' + selectIndex);
                        return false;
                    }
                    var select = selects[selectIndex];
                    for (var i = 0; i < select.options.length; i++) {
                        if (select.options[i].value === value) {
                            select.selectedIndex = i;
                            var event = new Event('change', { bubbles: true });
                            select.dispatchEvent(event);
                            return true;
                        }
                    }
                    console.error('Option with value ' + value + ' not found in select ' + selectIndex);
                    return false;
                }

                function waitForVersionOptions(callback) {
                    var versionSelect = document.querySelectorAll('div.flex.mt-4.gap-2 select')[1];
                    var attempts = 0;
                    var maxAttempts = 20;
                    var interval = setInterval(function() {
                        if (versionSelect.options.length > 1 || attempts >= maxAttempts) {
                            clearInterval(interval);
                            callback(versionSelect.options.length > 1);
                        }
                        attempts++;
                    }, 500);
                }

                var successPaper = selectOptionByValue(0, 'Paper');
                if (successPaper) {
                    waitForVersionOptions(function(hasOptions) {
                        if (hasOptions) {
                            var successVersion = selectOptionByValue(1, '181130');
                            if (successVersion) {
                                return 'Successfully selected Paper and 1.21';
                            } else {
                                return 'Failed to select version 1.21';
                            }
                        } else {
                            return 'Version options did not load within the timeout period';
                        }
                    });
                } else {
                    return 'Failed to select Paper';
                }
            ";

                    if (serverSetup.Displayed)
                    {
                        Console.WriteLine("Chờ 3s");
                        Thread.Sleep(3000);
                        
                        // Thực thi JavaScript
                        js = (IJavaScriptExecutor)driver;
                        string result = (string)js.ExecuteScript(jsScript);

                        Console.WriteLine(result);
                    }

                    Console.WriteLine("Đã chọn kiểu và phiên bản.");
                    Console.WriteLine("Chờ 2s");
                    Thread.Sleep(2000);
                    driver.Navigate().GoToUrl($"https://control.sparkedhost.us/server/{serverId}/setup/schedules");
                    
                    // nhấn next
                    // Chờ div chứa nút "Next" xuất hiện
                    serverSetup = waitToSelect.Until(ExpectedConditions.ElementExists(
                        By.CssSelector("div.mt-4.text-right")
                    ));

                    // Định nghĩa đoạn code JavaScript
                    
                    jsScript = @"
                function clickNextButton() {
                    var nextButton = document.querySelector('div.mt-4.text-right button._button_xywqc_1._primary_xywqc_37');
                    if (nextButton && nextButton.textContent.trim() === 'Next') {
                        nextButton.click();
                        return true;
                    } else {
                        console.error('Next button not found');
                        return false;
                    }
                }
                return clickNextButton();
            ";

                    if (serverSetup.Displayed)
                    {
                        // Thực thi JavaScript
                        js = (IJavaScriptExecutor)driver;
                        bool success = (bool)js.ExecuteScript(jsScript);

                        if (success)
                        {
                            Console.WriteLine("Đã nhấn next.");
                        }
                        else
                        {
                            Console.WriteLine("Lỗi khi nhấn next.");
                        }
                    }
                    
                    // Độ khó
                    IWebElement difficultyInput = wait.Until(d => d.FindElement(By.Name("difficulty")));
                    if (difficultyInput.Displayed)
                    {
                        difficultyInput.Clear();
                        WebInteraction.SmoothInputText(driver, difficultyInput, "normal");
                    }
                    
                    // Người chơi tối đa
                    IWebElement maxPlayerInput = wait.Until(d => d.FindElement(By.Name("max-players")));
                    if (maxPlayerInput.Displayed)
                    {
                        maxPlayerInput.Clear();
                        WebInteraction.SmoothInputText(driver, maxPlayerInput, "10");
                    }
                    
                    // motd
                    IWebElement motd = wait.Until(d => d.FindElement(By.Name("motd")));
                    if (motd.Displayed)
                    {
                        motd.Clear();
                        WebInteraction.SmoothInputText(driver, motd, "Chai nước rổn lừa");
                    }
                    
                    // online mode
                    serverSetup = waitToSelect.Until(ExpectedConditions.ElementExists(
                        By.CssSelector("div._switch_toggle_1t3cx_41")
                    ));
                    
                    jsScript = @"
                function turnOffOnlineMode() {
                    var toggle = document.querySelector('input[name=""online-mode""]');
                    if (toggle) {
                        if (toggle.checked) {
                            toggle.checked = false;
                            var event = new Event('change', { bubbles: true });
                            toggle.dispatchEvent(event);
                            return true;
                        } else {
                            console.log('Online mode toggle is already turned off');
                            return false;
                        }
                    } else {
                        console.error('Online mode toggle not found');
                        return false;
                    }
                }
                return turnOffOnlineMode();
            ";

                    if (serverSetup.Displayed)
                    {
                        // Thực thi JavaScript
                        js = (IJavaScriptExecutor)driver;
                        bool success = (bool)js.ExecuteScript(jsScript);

                        if (success)
                        {
                            Console.WriteLine("Đã tắt online mode.");
                        }
                        else
                        {
                            Console.WriteLine("Lỗi khi tắt online mode.");
                        }
                    }
                    
                    // chờ 2s
                    Thread.Sleep(2000);
                    
                    // nhấn next
                    // Chờ div chứa nút "Next" xuất hiện
                    serverSetup = waitToSelect.Until(ExpectedConditions.ElementExists(
                        By.CssSelector("div.mt-4.text-right")
                    ));

                    // Định nghĩa đoạn code JavaScript
                    
                    jsScript = @"
                function clickNextButton() {
                    var nextButton = document.querySelector('div.mt-4.text-right button._button_xywqc_1._primary_xywqc_37');
                    if (nextButton && nextButton.textContent.trim() === 'Next') {
                        nextButton.click();
                        return true;
                    } else {
                        console.error('Next button not found');
                        return false;
                    }
                }
                return clickNextButton();
            ";

                    if (serverSetup.Displayed)
                    {
                        // Thực thi JavaScript
                        js = (IJavaScriptExecutor)driver;
                        bool success = (bool)js.ExecuteScript(jsScript);

                        if (success)
                        {
                            Console.WriteLine("Đã nhấn next.");
                        }
                        else
                        {
                            Console.WriteLine("Lỗi khi nhấn next.");
                        }
                    }
                    
                    // chờ 2s
                    Thread.Sleep(2000);
                    
                    // nhấn next
                    // Chờ div chứa nút "Next" xuất hiện
                    serverSetup = waitToSelect.Until(ExpectedConditions.ElementExists(
                        By.CssSelector("div.mt-4.text-right")
                    ));

                    // Định nghĩa đoạn code JavaScript
                    
                    jsScript = @"
                function clickAcceptButton() {
                    var acceptButton = Array.from(document.querySelectorAll('button')).find(
                        button => button.textContent.trim() === 'I Accept'
                    );
                    if (acceptButton) {
                        acceptButton.click();
                        return true;
                    } else {
                        console.error('I Accept button not found');
                        return false;
                    }
                }
                return clickAcceptButton();
            ";

                    if (serverSetup.Displayed)
                    {
                        // Thực thi JavaScript
                        js = (IJavaScriptExecutor)driver;
                        bool success = (bool)js.ExecuteScript(jsScript);

                        if (success)
                        {
                            Console.WriteLine("Accepted Eula.");
                        }
                    }
                    
                    // nhấn finish
                    // Chờ div chứa nút "Finish" xuất hiện
                    serverSetup = waitToSelect.Until(ExpectedConditions.ElementExists(
                        By.CssSelector("div.mt-4.text-right")
                    ));

                    // Định nghĩa đoạn code JavaScript
                    
                    jsScript = @"
                function clickFinishButton() {
                    var finishButton = document.querySelector('div.mt-4.text-right button._button_xywqc_1._primary_xywqc_37');
                    if (finishButton && finishButton.textContent.trim() === 'Finish') {
                        finishButton.click();
                        return true;
                    } else {
                        console.error('Finish button not found');
                        return false;
                    }
                }
                return clickFinishButton();
            ";

                    if (serverSetup.Displayed)
                    {
                        // Thực thi JavaScript
                        js = (IJavaScriptExecutor)driver;
                        bool success = (bool)js.ExecuteScript(jsScript);

                        if (success)
                        {
                            Console.WriteLine("Đã nhấn finish.");
                        }
                    }

                    Console.WriteLine("Setup thành công. Chờ server load ....");
                    Console.WriteLine("Nhấn enter để tiếp tục. (setup server)");
                    Console.ReadLine();
                }

                string svIdPath = @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\Minecraft\serverId";
                string svId = File.ReadAllText(svIdPath);
                driver.Navigate().GoToUrl($"https://control.sparkedhost.us/server/{svId}");
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
            Console.WriteLine("Nhấn Enter để tiếp tục. (global)");
            Console.ReadLine();
        }
    }
}