<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/AdpiaCoupon.ascx.cs" Inherits="Web.FrontEnd.Modules.AdpiaCoupon" %>

 <h1 title="<%=Title%>"><strong> <%=Title%></strong></h1>
<div class="row">
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
        <label for="cbDevice">Nền tảng hỗ trợ:</label>
        <select id="cbDevice" class="form-control" onchange="Filter();">
            <option value="">Tất cả thiết bị</option>
            <option value="2">Áp dụng trên App</option>
            <option value="3">Áp dụng trên Website</option>
        </select>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
        <label for="cbWebsite">Website:</label>
        <select id="cbWebsite" class="form-control" onchange="Filter();">
            <option value="">Tất cả</option>
        </select>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
        <label for="cbCategory">Danh mục:</label>
        <select id="cbCategory" class="form-control" onchange="Filter();">
            <option value="">Tất cả</option>
        </select>
    </div>
</div>
<br />
<div id="coupon_area" class="row">

</div>
<%if(!string.IsNullOrEmpty(Data)){ %>
<script>
    var adpiaData = JSON.parse('<%=Data %>');
    var data = adpiaData.data.items;
    var coupons = data;

    // add accesstrade
    $.ajax({
        url: 'https://api.accesstrade.vn/v1/offers_informations?status=1',
        method: "GET",
        dataType: 'json',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "token NJ3RQYYedUODY2HmZd78qUMgvhXuTNJl");
        },
        success: function (res) {
            res.data.forEach(function (item) {
                if (item.coupons.length > 0) {
                    var obj = new Object();
                    obj.merchant_id = item.merchant;
                    if (obj.merchant_id === "tikivn") obj.merchant_id = "tiki";
                    else if (obj.merchant_id === "yes24vn") obj.merchant_id = "yes24";

                    if (item.categories.length > 0) obj.category_name = item.categories[0].category_name_show;
                    else obj.category_name = "Khác";
                    obj.title = item.name;
                    obj.description = item.coupons[0].coupon_desc;
                    obj.discount_code = item.coupons[0].coupon_code;
                    if (item.coupons[0].coupon_save) obj.discount = item.coupons[0].coupon_save;
                    else obj.discount = '';
                    obj.aff_link = item.aff_link;
                    obj.start_time = item.start_time;

                    data.push(obj);
                }
            });

            coupons = data;

            var merchants = new Set(coupons.map(function (item) { return item.merchant_id; }));
            var categories = new Set(coupons.map(function (item) { return item.category_name; }));
            
            merchants.forEach(function (item) { if(item) $("#cbWebsite").append(new Option(item, item)); });
            categories.forEach(function (item) { if(item) $("#cbCategory").append(new Option(item, item)); });

            ShowCoupons();
        },
        fail: function () {
            var merchants = new Set(coupons.map(function (item) { return item.merchant_id; }));
            var categories = new Set(coupons.map(function (item) { return item.category_name; }));

            merchants.forEach(function (item) { $("#cbWebsite").append(new Option(item, item)); });
            categories.forEach(function (item) { $("#cbCategory").append(new Option(item, item)); });

            ShowCoupons();
    }
    });

    function Filter()
    {
        coupons = data;

        var device = $("#cbDevice").val();
        if (device) {
            coupons = coupons.filter(function (obj) {
                return obj.device === device;
            });
        }

        var website = $("#cbWebsite").val();
        if (website) {
            coupons = coupons.filter(function (obj) {
                return obj.merchant_id === website;
            });
        }

        var category = $("#cbCategory").val();
        if (category) {
            coupons = coupons.filter(function (obj) {
                return obj.category_name === category;
            });
        }

        ShowCoupons();
    }

    function ShowCoupons()
    {
        $("#coupon_area").empty();
        if (coupons.length == 0) {
            $("#coupon_area").html("<p>Không tìm thấy mã giảm giá phù họp.</p>");
        }
        else
        {
            coupons.forEach(function (item) {
                var startdate = item.START_DATE;
                if (isNumeric(startdate)) {
                    startdate = (new Date(item.START_DATE * 1000)).toLocaleDateString("vi-VN");
                    startdate = startdate.substring(0, startdate.length - 5);
                }
                var content = "<div class='col-lg-4 col-md-4 col-sm-6 col-xs-12' style='margin-bottom: 20px'><div style='border: 1px solid #ccc; padding:10px'>";
                content += "<div style='font-size:12pt'><strong>Ưu đãi:</strong><font style='color:red'> " + item.discount + "</font></div>";
                content += "<div style='font-size:12pt'><strong>Mã giảm giá:</strong><a target='_blank' href='" + item.AFF_LINK + "' title='" + item.title + "'> " + (item.discount_code ? item.discount_code : "") + "</a></div>";
                content += "<div style='font-size:12pt'><strong>Áp dụng tại:</strong><a target='_blank' href='" + item.AFF_LINK + "' title='" + item.title + "'> " + item.merchant_id + "</a></div>";
                content += "<div style='font-size:12pt'><strong>Hiệu lực lúc:</strong> " + startdate + "</div>";
                content += "<div style='font-size:12pt'>" + (item.description && item.description.length > 100 ? item.description.substring(100) + "..." : item.description) + "</div>";
                content += "</div></div>";
                $("#coupon_area").append(content);
            });
           
        }
    }

    function isNumeric(str) {
        if (typeof str != "string") return false // we only process strings!  
        return !isNaN(str) && // use type coercion to parse the _entirety_ of the string (`parseFloat` alone does not do this)...
               !isNaN(parseFloat(str)) // ...and ensure strings of whitespace fail
    }
</script>
<%} %>