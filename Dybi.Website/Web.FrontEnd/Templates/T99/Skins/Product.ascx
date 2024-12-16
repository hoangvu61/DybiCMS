<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<%if(!string.IsNullOrEmpty(Data.Title)){ %>
    <!-- blog section -->
    <section class="product_section">
        <div class="heading_container">
            <h1>
                <%=Title %>
            </h1>
        </div>

    <div class="detail-box">
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-6 brief">
                <strong>
                    <%=Data.Brief %>
                </strong> 
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6 price">
                <span><%=Language["registeronly"]%>:</span>
                <div class="row regist">
                    <%if(Data.DiscountType == 0) {%>
                        <div class="col-xs-12 col-sm-7 col-md-7">
                            <h6>
                                <%if(Data.Price > 0) {%>
                                    <%=Data.PriceAfterDiscount.ToString("N0") %> ₫ / <%=Language[Data.GetAttribute("Unit")] %>
                                <%} else {%>
                                    <%=Language["contact"]%>
                                <%} %>
                            </h6>
                        </div>
                        <div class="col-xs-12 col-sm-5 col-md-5">
                        <%if(Data.Price > 0) {%>
                            <button type="button" onclick="AddToCart(false)" class="btn btn-t13"><%=Language["register"]%></button>
                        <%} %>
                        </div>
                    <%} else if(Data.DiscountType == 1) {%>
                        <div class="col-md-12">
                            <h6 class="originalprice">
                                <%=Data.Price.ToString("N0") %> ₫
                            </h6>
                        </div>
                        <div class="col-xs-12 col-sm-7 col-md-7">
                            <h6>
                                <%if(Data.Price > 0) {%>
                                    <%=Data.PriceAfterDiscount.ToString("N0") %> ₫ / <%=Language[Data.GetAttribute("Unit")] %>
                                <%} else {%>
                                    <%=Language["contact"]%>
                                <%} %>
                            </h6>
                        </div>
                        <div class="col-xs-12 col-sm-5 col-md-5">
                        <%if(Data.Price > 0) {%>
                            <button type="button" onclick="AddToCart(false)" class="btn btn-t13"><%=Language["register"]%></button>
                        <%} %>
                        </div>
                    <%} else if(Data.DiscountType == 2) { %>
                        <div class="col-xs-12">
                            <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>
                                        Đăng ký mới
                                        (/ <%=Language[Data.GetAttribute("Unit")] %>)
                                    </th>
                                    <th>
                                        Gia hạn
                                        (/ <%=Language[Data.GetAttribute("Unit")] %>)
                                    </th>
                                </tr>
                                
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <h6>
                                            <%=Data.PriceAfterDiscount.ToString("N0") %> ₫
                                        </h6>
                                    </td>
                                    <td>
                                        <h6>
                                            <%=Data.Discount.ToString("N0") %> ₫
                                        </h6>
                                    </td>
                                </tr>
                            </tbody>
                        </table> 
                        </div>
                        <div class="col-xs-12">
                            <button type="button" onclick="AddToCart(false)" class="btn btn-t13"><%=Language["register"]%></button>
                        </div>
                    <%} else if(Data.DiscountType == 3) { %>
                        <div class="col-xs-12 col-sm-7 col-md-7">
                            <h6>
                                <%=Language["from"]%> 
                                <%if(Data.Price > 0) {%>
                                    <%=Data.PriceAfterDiscount.ToString("N0") %> ₫ / <%=Language[Data.GetAttribute("Unit")] %>
                                <%} else {%>
                                    <%=Language["contact"]%>
                                <%} %>
                            </h6>
                        </div>
                        <div class="col-xs-12 col-sm-5 col-md-5">
                        <%if(Data.Price > 0) {%>
                            <button type="button" onclick="AddToCart(false)" class="btn btn-t13"><%=Language["register"]%></button>
                        <%} %>
                        </div>
                    <%} %>
                </div>
            </div>
        </div>
        
    </div>

    <div class="productdetail">
        <%=Data.Content%> 
    </div>               
      
        <%if(Relatieds.Count > 0){ %>
        <div class="row relatieds">
            <%foreach(var item in Relatieds)
                { %>
            <div class="col-xs-6 col-sm-4 col-md-34 col-lg-4">
            <div class="box">
            <div>
                <div class="img-box"> 
                    <a href="https://<%=item.Title %>">
                    <picture>
					    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                        <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore +  item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                    </picture>
                    </a>
                </div>
                <div class="detail-box">
                <a href="https://<%=item.Title %>">
                    <%=item.Title %>
                </a>
                </div>
            </div>
            </div>
        </div>
            <%} %>
        </div>
        <%} %>

        <%if(AddOns.Count > 0 && Data.Price > 0){ %>
        <div class="row addOns">
            <h2><%=Language["promotion"]%></h2>
            <table class="table">
                    
                <tbody>
                    <tr>
                        <td rowspan="<%=AddOns.Count + 1 %>" style="text-align:center;vertical-align:middle">
                            <strong>
                                <%=Data.Title %>
                                <br />
                                <%=Data.PriceAfterDiscount.ToString("N0") %> ₫
                            </strong>
                        </td>
                    </tr>
            <%foreach(var item in AddOns)
                { %>
                    <tr>
                        <td></td>
                        <td><input type="checkbox" name="addon" value="<%=item.Id %>"/></td>
                        <td>
                            <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>">
                                <%=item.Title %>
                            </a>
                        </td>
                        <td style="text-align:right">
                            <span style="text-decoration:line-through">
                                <%=item.Price.ToString("N0") %> ₫
                            </span> 
                            <%=item.AddOnPrice.ToString("N0") %> ₫
                        </td>
                    </tr>
            <%} %>
                    
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="4" style="text-align:center">
                            <button type="button" onclick="AddToCart(true)" class="btn btn-t13"><%=Language["register"]%></button>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <%} %>

        <div class="tag">
            <%=string.Join(", ", Tags)%> 
        </div>


    </section>
    <!-- end blog section -->
<script type="text/javascript">
    function AddToCart(isaddon) {
        var addon = '';
        if (isaddon)
        {
            var selected = new Array();
            $(".addOns input[type=checkbox]:checked").each(function () {
                selected.push(this.value);
            });
            if (selected.length > 0) {
                addon = selected.join(",");
            }
        }
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
            data: JSON.stringify({ productId: '<%=Data.Id%>', quantity: 1, properties: '', addonIds: addon}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    location.href = '<%=HREF.BaseUrl %>cart/don-hang';
                 }
             }
        });
     }
</script>	

    <script type="application/ld+json">
      {
        "@context": "https://schema.org/",
        "@type": "Product",
        "name": "<%=Data.Title %>",
        "image": [
          "<%=HREF.DomainStore + Data.Image.FullPath%>.webp",
          "<%=HREF.DomainStore + Data.Image.FullPath%>"
         ],
        "description": "<%=Data.Brief %>",
        "offers": {
          "@type": "Offer",
          "price": "<%=Data.PriceAfterDiscount.ToString("N0") %>",
          "priceCurrency": "VNĐ"
        }
      }
    </script>
<%} else { %>
<div class="row"> 
    <div class="col-md-10 col-md-offset-1">
        <div class="alert alert-danger">
            The content does not exist
        </div>
    </div>
</div>
<%} %>