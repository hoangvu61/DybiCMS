using Web.Models.SeedWork;

namespace Web.Models.Enums
{
    public class DataSource
    {
        public static List<CompanyType> CompanyTypes = new List<CompanyType>()
        {
            new CompanyType ("Organization", "Tổ chức", string.Empty),

            new CompanyType ("LocalBusiness", "Kinh doanh địa phương","Organization"),
            new CompanyType ("AutomotiveBusiness", "Kinh doanh ô tô","LocalBusiness"),
            new CompanyType ("Dentist", "Nha sĩ","LocalBusiness"),
            new CompanyType ("DryCleaningOrLaundry", "Giặt sấy","LocalBusiness"),
            new CompanyType ("FinancialService", "Dịch vụ tài chính","LocalBusiness"),
            new CompanyType ("FoodEstablishment", "Cơ sở thực phẩm","LocalBusiness"),
            new CompanyType ("HealthAndBeautyBusiness", "Sức khỏe và làm đẹp","LocalBusiness"),
            new CompanyType ("TouristInformationCenter", "Thông tin du lịch","LocalBusiness"),
            new CompanyType ("TravelAgency", "Đại lý du lịch","LocalBusiness"),
            new CompanyType ("ProfessionalService", "Dịch vụ chuyên nghiệp","LocalBusiness"),
            new CompanyType ("MedicalBusiness", "Cửa hàng y tế","LocalBusiness"),
            new CompanyType ("LodgingBusiness", "Nhà nghỉ, khách sạn","LocalBusiness"),
            new CompanyType ("Store", "Cửa hàng","LocalBusiness"),

            new CompanyType ("AutoPartsStore", "Cửa hàng phụ tùng ô tô","Store"),
            new CompanyType ("BikeStore", "Cửa hàng xe đạp","Store"),
            new CompanyType ("ClothingStore", "Cửa hàng quần áo","Store"),
            new CompanyType ("JewelryStore", "Cửa hàng nữ trang","Store"),
            new CompanyType ("ComputerStore", "Cửa hàng máy tính","Store"),
            new CompanyType ("MobilePhoneStore", "Cửa hàng điện thoại","Store"),
            new CompanyType ("ConvenienceStore", "Cửa hàng tiện dụng","Store"),
            new CompanyType ("GroceryStore", "Cửa hàng tạp hóa","Store"),
            new CompanyType ("FurnitureStore", "Cửa hàng nội thất","Store"),
            new CompanyType ("PetStore", "Cửa hàng vật nuôi","Store"),
            new CompanyType ("PawnShop", "Cửa hàng cầm đồ","Store"),
            new CompanyType ("ToyStore", "Cửa hàng đồ chơi","Store"),

            new CompanyType ("OnlineBusiness", "Kinh doanh trực tuyến","Organization"),
            new CompanyType ("OnlineStore", "Kinh doanh trực tuyến","OnlineBusiness"),
            
            new CompanyType ("EducationalOrganization", "Tổ chức giáo dục","Organization"),
            new CompanyType ("Preschool", "Trường mầm non","EducationalOrganization"),
            new CompanyType ("Professional Service","Dịch vụ chuyên nghiệp","LocalBusiness"),

            new CompanyType ("Person", "Cá nhân", string.Empty),
        };

        public static Dictionary<string, string> ArticleTypes = new Dictionary<string, string>()
        {
            { "Article", "Bài viết" },
            { "NewsArticle", "Tin tức" },
            { "BlogPosting", "Blog" }
        };

        public static Dictionary<string, string> CategoryTypes = new Dictionary<string, string>()
        {
            { "ART", "Bài viết" },
            { "PRO", "Sản phẩm" },
            { "MID", "Đa phương tiện" }
        };

        public static Dictionary<string, string> AttributeTypes = new Dictionary<string, string>()
        {
            { "Text", "Chữ" },
            { "Textarea", "Đoạn văn" },
            { "Number", "Số" },
            { "Date", "Ngày" },
            { "Color", "Màu" },
            { "Image", "Hình ảnh" },
            { "Boolean", "Có/Không" },
            { "Check", "Chọn nhiều từ danh sách" },
            { "Option", "Chọn một từ danh sách" }
        };

        public static Dictionary<int, string> DiscountTypes = new Dictionary<int, string>()
        {
            { 0, "Không giảm" },
            { 1, "Phần trăm" },
            { 2, "Giảm trừ tiền" },
            { 3, "Giá bán" }
        };

        public static Dictionary<string, string> MediaTypes = new Dictionary<string, string>()
        {
            { "LIN", "Liên kết" },
            { "IMG", "Hình ảnh" },
            { "DOC", "Tài liệu" },
            { "AUD", "Âm thanh" },
            { "VID", "Video" },
        };

        public static Dictionary<string, string> MenuTypes = new Dictionary<string, string>()
        {
            { "Categories", "Danh sách danh mục" },
            { "Articles", "Danh sách bài viết" },
            { "Links", "Danh sách liên kết" },
            { "Category", "Danh mục" },
            { "Article", "Bài viết" },
            { "Link", "Liên kết" }
        };

        public static Dictionary<string, string> ProductOrderTypes = new Dictionary<string, string>()
        {
            { "VIEW", "Theo lượt xem" },
            { "PRICEUP", "Giá tăng dần" },
            { "PRICEDOWN", "Giá giảm dần" },
            { "DISCOUNTUP", "Khuyến mãi tăng dần" },
            { "DISCOUNTDOWN", "Khuyến mãi giảm dần" }
        };

        public static Dictionary<int, string> Deliveries = new Dictionary<int, string>()
        {
            { 0, "Giao hàng tiết kiệm" },
            { 1, "Viettel Post" },
            { 2, "Giao hàng nhanh" },
            { 3, "Chành xe" }
        };

        public static Dictionary<int, string> WarehouseInputTypes = new Dictionary<int, string>()
        {
            { 1, "Nhập kho mua ngoài" },
            { 2, "Nhập kho sản xuất nội bộ" },
            { 3, "Nhập kho từ điều chuyển nội bộ" },
            { 4, "Nhập kho trả lại" },
            { 5, "Nhập kho do kiểm kê phát hiện thừa" }
        };

        public static Dictionary<int, string> WarehouseOutputTypes = new Dictionary<int, string>()
        {
            { 1, "Xuất kho để bán hàng" },
            { 2, "Xuất kho để sản xuất" },
            { 3, "Xuất kho để chuyển kho nội bộ" },
            { 4, "Xuất kho để trả lại hàng cho nhà cung cấp" },
            { 5, "Xuất tiêu hủy" }
        };

        public static Dictionary<string, string> ConfigTypes = new Dictionary<string, string>()
        {
            { "Number", "Số" },
            { "Boolean", "Có/Không" },
            { "Check", "Chọn nhiều từ danh sách" },
            { "Option", "Chọn một từ danh sách" }
        };

        public static Dictionary<string, Dictionary<int, string>> ConfigSources = new Dictionary<string, Dictionary<int, string>>()
        {
            { "KyKeToan", new Dictionary<int, string>()
                            {
                                { 1, "Tháng" },
                                { 2, "Quý" },
                                { 3, "Năm" }
                            } 
            },
            { "PhuongPhapTinhGiaXuatKho", new Dictionary<int, string>()
                            {
                                { 1, "Bình quân gia quyền" },
                                { 2, "Đích danh" },
                                { 3, "FIFO" }
                            } 
            }
        };

        public static Dictionary<int, string> DebtType = new Dictionary<int, string>()
        {
            { 1, "Nợ" },
            { 2, "Trả nợ" }
        };
    }
}
