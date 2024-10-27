using Web.Models.SeedWork;

namespace Web.Models.Enums
{
    public class DataSource
    {
        public static Dictionary<string, string> TrangThaiNNTs = new Dictionary<string, string>
        {
            { "00", "NNT đang hoạt động" },
            { "01", "NNT ngưng hoạt động và đã hoàn thành thủ tục chấm dứt hiệu lực MST"},
            { "02", "NNT chuyển cơ quan thuế quản lý"},
            { "03", "NNT ngưng hoạt động nhưng chưa hoàn thành thủ tục chấm dứt hiệu lực MST"},
            { "04", "NNT đang hoạt động (chưa đầy đủ thủ tục cấp MST)" },
            { "05", "NNT tạm ngưng KD có thời hạn" },
            { "06", "NNT không hoạt động tại địa chỉ đã đăng ký" },
        };

        public static Dictionary<string, string> LoaiNNTs = new Dictionary<string, string>()
        {
            { "0100", "01/ĐK-TCT (TChức, DNghiệp, CTy)" },
            { "0110", "02/ĐK-TCT (TC, DN, CT TThuộc)"},
            { "0300", "03/ĐK-TCT (Cá nhân, nhóm cá nhân KD)"},
            { "0310", "03/ĐK-TCT (Dùng cấp mã 13 số)"},
            { "0400", "04/ĐK-TCT (Nhà thầu nước ngoài)"},
            { "0410", "04.1/ĐK-TCT (ĐV nộp hộ thuế NT NN)"},
            { "0420", "04.2/ĐK-TCT (NT, NTP không nột TT)"},
            { "0430", "04.3/ĐK-TCT (BĐH DA T.hiện HĐNT)"},
            { "0440", "04.4/ĐK-TCT (Mã số thuế ủy nhiệm thu)"},
            { "0450", "04.5/ĐK-TCT (Mã số thuế 13 số của 04.1)"},
            { "0500", "06/ĐK-TCT (Tổ chức ngoại giao)"},
            { "0700", "04-ĐK-TCT (KK-Nộp các loại thuế khác)"},
            { "0900", "05/ĐK-TCT (CN làm công ăn lương)"},
            { "1000", "01/ĐK-TCT (TK thiếu thông tin cấp 1)"},
            { "1001", "02/ĐK-TCT (TK thiếu thông tin cấp 2)"},
            { "9100", "01/ĐKT-DKDN (Tổ chức SX, KDHH, DV)"},
            { "9110", "02/ĐK-DKDN (Đơn vị trực thuộc)"},
        };

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
            { 0, "Tự đi giao khách" },
            { 1, "Giao hàng tiết kiệm" },
            { 2, "Viettel Post" },
            { 3, "Giao hàng nhanh" },
            { 4, "Chành xe" }
        };

        public static Dictionary<int, string> WarehouseTypes = new Dictionary<int, string>()
        {
            { 152, "Kho nguyên vật liệu" },
            { 155, "Kho thành phẩm" },
            { 154, "Kho bán thành phẩm" },
            { 153, "Kho công cụ, dụng cụ" },
            { 156, "Kho hàng hóa" }
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

        public static Dictionary<int, string> DebtTypes = new Dictionary<int, string>()
        {
            { 1, "Nợ" },
            { 2, "Trả nợ" }
        };

        public static Dictionary<string, string> BarCodeTypes = new Dictionary<string, string>()
        {
            { "EAN_8", "Mã EAN-8, có 8 chữ số - Sử dụng trong các sản phẩm kích thước nhỏ như mỹ phẩm." },
            { "EAN_13", "Mã EAN-13, có 13 chữ số - Sử dụng phổ biến trên sản phẩm bán lẻ quốc tế." },
            { "UPC_A", "Mã UPC-A, có 12 chữ số - Sử dụng trên sản phẩm bán lẻ tại Mỹ." },
            { "UPC_E", "Mã UPC-E, có 8 chữ số - Sử dụng cho sản phẩm bán lẻ nhỏ tại Mỹ." },
            { "CODE_39", "Mã Code 39 - Sử dụng trong sản xuất và quân đội." },
            { "CODE_93", "Mã Code 93 - Tương tự Code 39 nhưng ngắn gọn hơn." },
            { "CODE_128", "Mã Code 128 - Phổ biến trong vận chuyển, hậu cần và bán lẻ." },
            { "ITF", "Mã Interleaved 2 of 5 (ITF) - Thường được dùng trong lĩnh vực vận chuyển và kho hàng." },
            { "MSI", "Mã MSI - Sử dụng trong ngành bán lẻ và quản lý kho." },
            { "RSS_14", "Mã RSS 14 - Chủ yếu dùng cho các gói sản phẩm nhỏ." },
            { "RSS_EXPANDED", "Mã RSS Expanded - Sử dụng để mã hóa nhiều dữ liệu hơn trong một mã vạch nhỏ." },
            { "QR_CODE", "Mã QR (Quick Response) - Sử dụng phổ biến trong thanh toán, liên kết website, mạng xã hội, và thông tin sản phẩm." },
            { "DATA_MATRIX", "Mã Data Matrix - Sử dụng trong các ứng dụng công nghiệp, y tế, và vận chuyển." },
            { "AZTEC", "Mã Aztec - Phổ biến trong vé tàu điện ngầm và máy bay vì có thể quét tốt ở kích thước nhỏ." },
            { "PDF_417", "Mã PDF417 - Sử dụng trong thẻ căn cước, hộ chiếu, và vé máy bay. Lưu trữ được lượng lớn thông tin." },
            { "MAXICODE", "MaxiCode - Được UPS sử dụng trong vận chuyển và quản lý hàng hóa." },
            { "CODABAR", "Mã Codabar - Được sử dụng trong các hệ thống quản lý thư viện, ngân hàng máu, và vận chuyển." },
            { "PHARMA_CODE", "Mã PharmaCode - Sử dụng chủ yếu trong ngành dược phẩm." }
        };
    }
}
