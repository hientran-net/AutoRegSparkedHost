from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

# Khởi tạo Chrome không cần chỉ đường dẫn driver
driver = webdriver.Chrome(service=Service(ChromeDriverManager().install()))

# Thử mở Google
driver.get("https://billing.sparkedhost.com/store/free-trials/24-hour-trial-palworld")

# In tiêu đề trang
print(driver.title)

# Đợi phần tử xuất hiện trong vòng 60 giây
wait = WebDriverWait(driver, 60)
element = wait.until(EC.presence_of_element_located((By.XPATH, '//*[@id="productConfigurableOptions"]/div/div[2]/div/div[7]')))

# In ra thông tin của phần tử
print(element.text)
print(element.location)
print(element.is_displayed())
print(element.is_enabled())
print(element.is_selected())

# Lấy tọa độ và cuộn chuột đến vị trí
x = element.location['x']
y = element.location['y']
driver.execute_script(f"window.scrollTo({x}, {y});")

# Click vào phần tử
element.click()

# Đợi phần tử tiếp theo trong vòng 60 giây
element = wait.until(EC.presence_of_element_located((By.XPATH, '//*[@id="orderSummary"]/div[2]')))
print(element.text)
print(element.location)
print(element.is_displayed())
print(element.is_enabled())
print(element.is_selected())

# Lấy tọa độ và cuộn chuột đến vị trí
x = element.location['x']
y = element.location['y']
driver.execute_script(f"window.scrollTo({x}, {y});")

# Click vào phần tử
element.click()

# Đợi phần tử tiếp theo trong vòng 60 giây
element = wait.until(EC.presence_of_element_located((By.XPATH, '//*[@id="checkout"]')))
print(element.text)
print(element.location)
print(element.is_displayed())
print(element.is_enabled())
print(element.is_selected())

# Lấy tọa độ và cuộn chuột đến vị trí
x = element.location['x']
y = element.location['y']
driver.execute_script(f"window.scrollTo({x}, {y});")

# Chờ người dùng nhấn Enter để tiếp tục
input("Nhấn Enter để tiếp tục...")

# Đóng trình duyệt
driver.quit()
