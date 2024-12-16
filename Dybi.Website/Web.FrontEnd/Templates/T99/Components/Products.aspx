<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <VIT:Position runat="server" ID="psTop"></VIT:Position>

    <section class="service_section">
        <%if(Config.Language == "vi"){ %>
        <div class="heading_container heading_center">
            <h1 title="<%=Title %>">
                Bảng Giá Thiết Kế Website Trọn Gói
            </h1>
        </div>
        <table class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th style="width:150px"></th>
                    <th style="font-size:x-large">Gói Cơ Bản</th>
                    <th style="font-size:x-large">Gói Thiết Kế</th>
                    <th style="font-size:x-large">Gói Quản Lý</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Giao diện</td>
                    <td style="text-align:center">Chọn mẫu, chỉnh sửa theo yêu cầu</td>
                    <td colspan="2" style="text-align:center"><strong>Thiết kế theo yêu cầu</strong></td>
                </tr>
                <tr>
                    <td>Tên miền</td>
                    <td colspan="2" style="text-align:center">Theo giá thị trường (Miễn phí năm đầu)</td>
                    <td style="text-align:center">Miễn phí</td>
                </tr>
                <tr>
                    <td><a href="/Hosting-va-domain-la-gi" target="_blank">Hosting</a></td>
                    <td colspan="2" style="text-align:center">Không giới hạn dung lượng, băng thông (Miễn phí năm đầu)</td>
                    <td style="text-align:center">Miễn phí</td>
                </tr>
                <tr>
                    <td>Ngôn ngữ</td>
                    <td style="text-align:center">1 ngôn ngữ</td>
                    <td colspan="2" style="text-align:center;font-weight:600">Đa ngôn ngữ</td>  
                </tr>
                <tr>
                    <td><a href="/HTTPS-va-SSL-HTTPS-co-anh-huong-SEO-khong" target="_blank">SSL</a></td>
                    <td colspan="3" style="text-align:center">Miễn phí</td>
                </tr>
                <tr>
                    <td>Chuẩn SEO</td>
                    <td colspan="3" style="text-align:center">
                        <a href="/Nhu-the-nao-la-mot-website-chuan-SEO" target="_blank">Chuẩn hóa cấu trúc HTML</a>, 
                        <a href="/Schema-Markup-la-gi-Schema-anh-huong-nhu-the-nao-den-SEO" target="_blank">Schema</a>, 
                        <a href="/WebP-la-gi-Huong-dan-cho-nguoi-moi-bat-dau" target="_blank">hình ảnh</a>, 
                        <a href="/Cach-viet-bai-hieu-qua-SEO-cao" target="_blank">nội dung</a>
                    </td>
                </tr>
                <tr>
                    <td>Responsive</td>
                    <td colspan="3" style="text-align:center">Hỗ trợ giao diện Mobile, Tablet, Laptop</td>
                </tr>
                <tr>
                    <td>Chức năng</td>
                    <td style="text-align:center">
                        Quản lý bài viết<br />
                        Quản lý hình ảnh, logo, banner<br />
                        Quản lý video, audio<br />
                        Quản lý bình luận<br />
                        Quản lý thông tin liên hệ, hotline, ... <br />
                        Chỉnh sửa thông tin website: tiêu đề, nội dung mô tả, ...<br />
                        Chỉnh sửa giao diện: menu, màu sắc, cỡ chữ, ... <br />
                        Quản lý liên kết: link, liên kết mạng xã hội, youtube, tiktok, ...
                    </td>
                    <td style="text-align:center">
                        Quản lý bài viết<br />
                        Quản lý hình ảnh, logo, banner<br />
                        Quản lý video, audio<br />
                        Quản lý bình luận<br />
                        Quản lý thông tin liên hệ, hotline, ... <br />
                        Chỉnh sửa thông tin website: tiêu đề, nội dung mô tả, ...<br />
                        Chỉnh sửa giao diện: menu, màu sắc, cỡ chữ, ... <br />
                        Quản lý liên kết: link, liên kết mạng xã hội, youtube, tiktok, ...<br />
                        <b>Quản lý sản phẩm<br />
                        Quản lý bán hàng<br />
                        Quản lý đơn hàng<br />
                        Quản lý khách hàng</b>
                    </td>
                    <td style="text-align:center">Theo yêu cầu</td>
                </tr>
                <tr>
                    <td>Hình ảnh</td>
                    <td style="text-align:center">Thiết kế 1 Logo + 2 Banners</td>
                    <td style="text-align:center">Thiết kế 1 Logo + 3 Banners</td>
                    <td style="text-align:center">Thiết kế 1 Logo + 3 Banners</td>
                </tr>
                <tr>
                    <td><a href="/Cach-viet-bai-hieu-qua-SEO-cao" target="_blank">Nội dung</a></td>
                    <td><strong>Viết bài chuẩn SEO: Bài giới thiệu + 3 bài viết dạng tin tức + 10 Sản phẩm</strong></td>
                    <td><strong>Viết bài chuẩn SEO: Bài giới thiệu + 3 bài viết dạng tin tức + 15 Sản phẩm</strong></td>
                    <td style="text-align:center"><strong>Quản lý nội dung</strong></td>
                </tr>
                <tr>
                    <td>Cài đặt khác</td>
                    <td colspan="3" style="text-align:center">Cài đặt 
                        <a href="/Huong-dan-cai-dat--Google-Search-Console-hay-Google-Webmaster-Tool" target="_blank"> Search Console</a>, 
                        Site Map, 
                        <a href="/Cach-tao-them-dia-diem-tren-Google-Maps-nhanh-nhat" target="_blank">Google Map</a>, 
                        Google Analytics, 
                        <a href="/Chung-chi-DMCA-la-gi" target="_blank">Bảo vệ bản quyền nội dung DMCA</a>, 
                        tạo Facebook Fanpage</td>
                </tr>
                <tr>
                    <td>Thời gian thực hiện</td>
                    <td style="text-align:center">3 - 5 ngày</td>
                    <td style="text-align:center">5 - 7 ngày</td>
                    <td style="text-align:center">7 - 10 ngày</td>
                </tr>
                <tr>
                    <td>Phí</td>
                    <td style="text-align:center"><span style="font-size:x-large;color:red;font-weight:bold">4.000.000 <sup>đ</sup></span></td>
                    <td style="text-align:center"><span style="font-size:x-large;color:red;font-weight:bold">6.000.000 <sup>đ</sup></span></td>
                    <td style="text-align:center"><span style="font-size:x-large;color:red;font-weight:bold">9.000.000 <sup>đ</sup></span></td>
                </tr>
                <tr>
                    <td>Ghi chú</td>
                    <td colspan="2">
                        <div>Phí hosting 1.200.000 / năm, tính từ năm thứ 2</div>
                        <div>Phí tên miền theo giá thị trương, tham khảo tại <a href="https://www.pavietnam.vn/vn/ten-mien-bang-gia.html" target="_blank" title="Bảng giá tên miền">https://www.pavietnam.vn/vn/ten-mien-bang-gia.html</a>, tính từ năm thứ 2</div>
                    </td>
                    <td>Phí quản lý 3.000.000 / tháng, tính từ tháng thứ 2</td>
                </tr>
            </tbody>
        </table>
        <%} else { %>
        <div class="heading_container heading_center">
            <h1 title="Service price list">Package Website Design Price List</h1>
            <table class="table table-striped table-bordered">
                <thead>
                <tr>
                    <th style="width:120px"></th>
                    <th style="font-size:x-large">Basic Package</th>
                    <th style="font-size:x-large">Design Package</th>
                    <th style="font-size:x-large">Management Package</th>
                </tr>
                </thead>
                <tbody>
                <tr>
                    <td>Template</td>
                    <td style="text-align:center">Choose a template, edit as required</td>
                    <td colspan="2" style="text-align:center"><strong>Design upon request</strong></td>
                </tr>
                <tr>
                    <td>Domain</td>
                    <td colspan="2" style="text-align:center">According to market price (Free for the first year)</td>
                    <td style="text-align:center">Free</td>
                </tr>
                <tr>
                    <td><a href="/What-is-hosting-and-domain" target="_blank">Hosting</a></td>
                    <td colspan="2" style="text-align:center">Unlimited capacity and bandwidth (Free for the first year)</td>
                    <td style="text-align:center">Free</td>
                </tr>
                <tr>
                    <td>Language</td>
                    <td style="text-align:center">1 language</td>
                    <td colspan="2" style="text-align:center;font-weight:600">Multi language</td>
                </tr>
                <tr>
                    <td><a href="/HTTPS-and-SSL-Does-HTTPS-affect-SEO" target="_blank">SSL</a></td>
                    <td colspan="3" style="text-align:center">Free</td>
                </tr>
                <tr>
                    <td>SEO standards</td>
                    <td colspan="3" style="text-align:center">
                        <a href="/What-is-an-SEO-standard-website" target="_blank">Standardize HTML structure</a>, 
                        <a href="/What-is-Schema-Markup-How-does-Schema-affect-SEO" target="_blank">Schema</a>, 
                        <a href="/What-is-WebP-Guide-for-beginners" target="_blank">images</a>, 
                        <a href="/How-to-write-highly-effective-SEO-articles" target="_blank">content</a></td>
                </tr>
                <tr>
                    <td>Responsive</td>
                    <td colspan="3" style="text-align:center">Supports Mobile, Tablet, Laptop interfaces</td>
                </tr>
                <tr>
                    <td>Function</td>
                    <td style="text-align:center">
                        Manage posts<br />
                        Manage images, logos, banners<br />
                        Manage videos, audio<br />
                        Manage comments<br />
                        Manage contact information, hotline, ... <br />
                        Edit website information: title, description, ...<br />
                        Edit interface: menu, color, font size, ... <br />
                        Manage links: links, social network links, youtube, tiktok, ...
                    </td>
                    <td style="text-align:center">
                        Manage posts<br />
                        Manage images, logos, banners<br />
                        Manage videos, audio<br />
                        Manage comments<br />
                        Manage contact information, hotline, ... <br />
                        Edit website information: title, description, ...<br />
                        Edit interface: menu, color, font size, ... <br />
                        Manage links: links, social network links, youtube, tiktok, ...
                        <b>Product Management<br />
                        Sales Management<br />
                        Order Management<br />
                        Customer Management</b>
                    </td>
                    <td>As required</td>
                </tr>
                <tr>
                    <td>Image</td>
                    <td style="text-align:center">Design 1 Logo + 2 Banners</td>
                    <td style="text-align:center">Design 1 Logo + 3 Banners</td>
                    <td style="text-align:center">Design 1 Logo + 3 Banners</td>
                </tr>
                <tr>
                    <td><a href="/How-to-write-highly-effective-SEO-articles" target="_blank">Content</a></td>
                    <td><strong>Write SEO standard articles: Introduction article + 3 news articles + 10 Products</strong></td>
                    <td><strong>Write SEO standard articles: Introduction article + 3 news articles + 15 Products</strong></td>
                    <td style="text-align:center"><strong>Content Management</strong></td>
                </tr>
                <tr>
                    <td>Other settings</td>
                    <td colspan="3" style="text-align:center">Install 
                        <a href="/Instructions-for-installing-Google-Search-Console-or-Google-Webmaster-Tool" target="_blank">Search Console</a>, 
                        Site Map, 
                        <a href="/How-to-register-an-address-with-Google-Maps" target="_blank">Google Map</a>, 
                        Google Analytics, 
                        <a href="/What-is-a-DMCA-certificate" target="_blank">DMCA content copyright protection</a>, 
                        create Facebook Fanpage
                    </td>
                </tr>
                <tr>
                    <td>Execution time</td>
                    <td style="text-align:center">3 - 5 days</td>
                    <td style="text-align:center">5 - 7 days</td>
                    <td style="text-align:center">7 - 10 days</td>
                </tr>
                <tr>
                    <td>Fee</td>
                    <td style="text-align:center"><span style="font-size:x-large;color:red;font-weight:bold;">4,000,000 <sup>VNĐ</sup></span></td>
                    <td style="text-align:center"><span style="font-size:x-large;color:red;font-weight:bold;">6,000,000 <sup>VND</sup></span></td>
                    <td style="text-align:center"><span style="font-size:x-large;color:red;font-weight:bold;">9,000,000 <sup>VND</sup></span></td>
                </tr>
                <tr>
                    <td>Note</td>
                    <td colspan="2">
                        <div>
                            Hosting fee 1,200,000 / year, starting from the 2nd year
                        </div>
                        <div>
                            Domain name fee according to market price, refer to <a href="https://www.pavietnam.vn/vn/ten-mien-bang-gia.html" alt="Domain price list">https://www.pavietnam.vn/vn/name-mien-bang-gia.html</a> , calculated from the 2nd year
                        </div>
                    </td>
                    <td>Management fee 3,000,000 / month, calculated from the 2nd month</td>
                </tr>
                </tbody>
            </table>
        </div>
        <%} %>
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
    </section>
</asp:Content>