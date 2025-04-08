using Src.Interface.Helplers;

namespace Src.Implementations.Helplers;

public class FakeDataGenerator : IFakeDataGenerator
{
    private static readonly Random random = new Random();
    private static readonly string[] firstNames = {
        "Nguyen", "Tran", "Le", "Pham", "Hoang", "Phan", "Vu", "Dang", "Bui", "Do",
        "Ho", "Ngo", "Duong", "Ly", "Trinh", "Mai", "Truong", "Ha", "Quach", "Kieu"
    };

    private static readonly string[] lastNames = {
        "Van", "Thi", "Minh", "Quoc", "Huu", "Thanh", "Ngoc", "Duc", "Anh", "Bao",
        "Tuan", "Khanh", "Linh", "Nhat", "Thao", "Hanh", "Dat", "Cuong", "Son", "Trang"
    };
    
    private static readonly string[] street = {
        "Nguyen Trai", "Le Loi", "Tran Hung Dao", "Cach Mang Thang Tam", "Pham Van Dong",
        "Vo Van Kiet", "Dien Bien Phu", "Nam Ky Khoi Nghia", "Nguyen Thi Minh Khai", "Hai Ba Trung",
        "Ton Duc Thang", "Ly Thuong Kiet", "Ba Trieu", "Pham Ngu Lao", "Tran Quoc Toan",
        "Hoang Van Thu", "Nguyen Dinh Chieu", "Le Thanh Ton", "Bach Dang", "Mac Dinh Chi",
        "Nguyen Van Cu", "To Hieu", "Tran Phu", "Nguyen Thai Hoc", "Ngo Quyen",
        "Phan Dang Luu", "Truong Chinh", "Le Duan", "Vo Thi Sau", "Dang Van Bi"
    };
    
    private static readonly string[] city = {
        "Ho Chi Minh", "Ha Noi", "Da Nang", "Can Tho", "Hai Phong",
        "Nha Trang", "Hue", "Vung Tau", "Buon Ma Thuot", "Da Lat",
        "Bien Hoa", "Thai Nguyen", "Thanh Hoa", "Nam Dinh", "Quy Nhon",
        "Long Xuyen", "Rach Gia", "Cam Ranh", "My Tho", "Soc Trang",
        "Bac Lieu", "Cao Lanh", "Tay Ninh", "Thu Dau Mot", "Tan An",
        "Phan Rang", "Phan Thiet", "Tam Ky", "Ha Long", "Uong Bi"
    };
    
    private static readonly string[] stateNames = {
        "An Giang", "Ba Ria - Vung Tau", "Bac Giang", "Bac Kan", "Bac Lieu",
        "Bac Ninh", "Ben Tre", "Binh Dinh", "Binh Duong", "Binh Phuoc",
        "Binh Thuan", "Ca Mau", "Can Tho", "Cao Bang", "Da Nang",
        "Dak Lak", "Dak Nong", "Dien Bien", "Dong Nai", "Dong Thap",
        "Gia Lai", "Ha Giang", "Ha Nam", "Ha Noi", "Ha Tinh",
        "Hai Duong", "Hai Phong", "Hau Giang", "Hoa Binh", "Hung Yen"
    };
    
    private static readonly string[] postcodes = {
        "700000", // Ho Chi Minh
        "100000", // Ha Noi
        "550000", // Da Nang
        "900000", // Can Tho
        "180000", // Hai Phong
        "650000", // Nha Trang
        "530000", // Lao Cai
        "600000", // Dak Lak
        "670000", // Gia Lai
        "770000", // Ba Ria - Vung Tau
        "350000", // Ninh Binh
        "250000", // Phu Tho
        "260000", // Yen Bai
        "320000", // Quang Binh
        "470000", // Binh Dinh
        "590000", // Binh Thuan
        "230000", // Bac Giang
        "240000", // Bac Ninh
        "300000", // Nam Dinh
        "310000", // Thai Binh
        "330000", // Ha Tinh
        "340000", // Quang Tri
        "360000", // Quang Ngai
        "400000", // Kon Tum
        "420000", // Khanh Hoa
        "440000", // Phu Yen
        "480000", // Dong Nai
        "490000", // Binh Duong
        "500000", // Long An
        "800000"  // Ca Mau
    };


    
    public static string GetFirstName()
    {
        return firstNames[random.Next(0, firstNames.Length)];
    }

    public static string GetLastName()
    {
        return lastNames[random.Next(0, lastNames.Length)];
    }

    public static string GetStreet()
    {
        return street[random.Next(0, street.Length)];
    }

    public static string GetCity()
    {
        return city[random.Next(0, city.Length)];
    }

    public static string GetState()
    {
        return stateNames[random.Next(0, stateNames.Length)];
    }

    public static string getPostCode()
    {
        return postcodes[random.Next(0, postcodes.Length)];
    }
    
    public static HashSet<string> LoadExistingEmails(string filePath)
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }
        return new HashSet<string>(
            File.ReadAllLines(filePath)
                .Select(email => email.Trim())
                .Where(email => !string.IsNullOrEmpty(email))
        );
    }

    public static void SaveEmailToFile(string filePath, string email)
    {
        // Lấy múi giờ Việt Nam (UTC+7)
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
    
        string timestamp = vietnamTime.ToString("yyyy-MM-dd HH:mm:ss"); // Định dạng thời gian
        File.AppendAllText(filePath, $"{email}\t\t{timestamp}" + Environment.NewLine);
    }
    
    public static string GenerateEmail(HashSet<string> existingEmails, int maxAttempts = 10000)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            string firstName = firstNames[random.Next(firstNames.Length)];
            string lastName = lastNames[random.Next(lastNames.Length)];
            string randomNumbers = string.Join("", Enumerable.Range(0, 4).Select(_ => random.Next(0, 10).ToString()));
            string email = $"{firstName}{lastName}{randomNumbers}@gmail.com".ToLower();

            if (!existingEmails.Contains(email))
            {
                existingEmails.Add("\n");
                existingEmails.Add(email);
                string emailPath = @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\emails.txt";
                SaveEmailToFile(emailPath, email);
                return email;
            }
        }
        throw new Exception("Không thể tạo email mới: đã đạt giới hạn số lần thử.");
    }
    
    // PASSWORD FUNCTION
    public static void SavePasswordToFile(string filePath, string value)
    {
        // Lấy múi giờ Việt Nam (UTC+7)
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
    
        string timestamp = vietnamTime.ToString("yyyy-MM-dd HH:mm:ss"); // Định dạng thời gian
        File.AppendAllText(filePath, $"{value}\t\t{timestamp}" + Environment.NewLine);
    }

    // PHONE NUMBER FUNCTIONS
    public static HashSet<string> LoadExistingPhones(string filePath)
    {
        if (File.Exists(filePath))
        {
            return new HashSet<string>(File.ReadAllLines(filePath));
        }
        return new HashSet<string>();
    }

    public static void SavePhoneToFile(string filePath, string phone)
    {
        File.AppendAllText(filePath, phone + Environment.NewLine);
    }

    public static string GenerateVietnamPhone(HashSet<string> existingPhones)
    {
        string[] prefixes = new string[]
        {
            "086", "096", "097", "098",       // Viettel
            "032", "033", "034", "035", "036", "037", "038", "039",  // Viettel
            "090", "093",                    // Mobifone
            "070", "079", "077", "076", "078",  // Mobifone
            "091", "094",                    // Vinaphone
            "081", "082", "083", "084", "085",  // Vinaphone
            "092",                           // Vietnamobile
            "056", "058"                     // Vietnamobile
        };

        while (true)
        {
            string prefix = prefixes[random.Next(prefixes.Length)];
            string randomNumbers = string.Join("", Enumerable.Range(0, 7).Select(_ => random.Next(0, 10).ToString()));
            string phoneNumber = $"{prefix}{randomNumbers}";

            if (!existingPhones.Contains(phoneNumber))
            {
                existingPhones.Add("\n");
                existingPhones.Add(phoneNumber);
                string phonenumberPath =
                    @"D:\University\Own_Project\2025\AutoRegSparkedHost\Src\Resources\phonenumbers.txt";
                SavePhoneToFile(phonenumberPath, phoneNumber);
                return phoneNumber;
            }
        }
    }
}