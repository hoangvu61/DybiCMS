<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<!-- product section start -->
<div class="product_section layout_padding" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
    <div class="row">
        <div class="col-sm-12">
            <h1 class="product_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
        </div>
    </div>
    <div class="product_section_2 layout_padding">
        <div class="row">
            <%foreach(var item in this.Data) 
            {%>  
            <div class="col-lg-3 col-sm-6 col-6">
                <div class="product_box">
                <h4 class="bursh_text">
                    <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>">
                        <%=item.Title %>
                    </a>
                </h4>
                <p class="lorem_text"></p>
                <div class="imagebox">
                <%if(item.Image != null)
                { %>
                <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>">
                    <picture>
						<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                        <img class="image_1" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore +  item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                    </picture>
                </a>
                <%} %>

                </div>
                <div class="btn_main" style="<%=string.IsNullOrEmpty(this.Skin.HeaderBackground) ? "" : ";background-color:" + this.Skin.HeaderBackground %>">
                    <div class="buy_bt hidden-xs">
                        <ul>
                            <li><a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>">Mua ngay</a></li>
                        </ul>
                    </div>
                    <h3 class="price_text">
                        <%if(item.DiscountType > 0) {%>
                        <span style="text-decoration:line-through; font-size:9pt">
                            <%=item.Price.ToString("N0") %>
                        </span> 
                        <%} %>
                        <span>
                            <%if(item.Price > 0) {%>
                                 <%=item.PriceAfterDiscount.ToString("N0") %> đ
                            <%} else {%>
                                Liên hệ
                            <%} %>
                        </span>
                    </h3>
                </div>
                </div>
            </div>
            <%} %>
        </div>
        <div class="seemore_bt">
        <%if(Top > 0 && TotalItems > Top) {%>
            <%if(Top < 9) {%>
                <a href="<%=HREF.LinkComponent("Products", Title.ConvertToUnSign(), true) %>" style="background:<%=Skin.HeaderBackground%>;color:<%=Skin.HeaderFontColor%>">
                Xem tất cả
                </a>
            <%} else {%>
                <a href="<%=HREF.LinkComponent("Product", Title.ConvertToUnSign(), true, SettingsManager.Constants.SendProduct, ProductId, "PromotionOnly", true, SettingsManager.Constants.SendPage, 1)%>">Trang đầu</a> 
                <%for(int i = 1; i <= TotalPages; i++){ %>
                <a style="padding: 5px;background: <%= i == CurrentPage ? "red" : "pink" %>;border: red 1px solid;border-radius: 10px;margin: 0px 3px;" href="<%=HREF.LinkComponent("Product", Title.ConvertToUnSign(), true, SettingsManager.Constants.SendProduct, ProductId, SettingsManager.Constants.SendPage, i)%>"><%=i %></a> 
                <%} %>
                <a href="<%=HREF.LinkComponent("Product", Title.ConvertToUnSign(), true, SettingsManager.Constants.SendProduct, ProductId, "PromotionOnly", true, SettingsManager.Constants.SendPage, TotalPages)%>">Trang cuối</a> 
            <%} %>
        <%} %>
        </div>
    </div>
    </div>
</div>
<!-- product section end -->


