<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


 <!-- product section -->
<section class="product_section layout_padding product_similar">
    <div class="container">
        <div class="row">
            <%foreach(var item in this.Data) 
            {%>  
            <div class="col-6 col-sm-4 col-md-4 col-lg-3">
                <div class="box">
                    <div class="option_container">
                    <div class="options">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>" class="option1">
                            <%=item.Title %>
                        </a>
                        <button class="option2" onclick="AddToCart('<%=item.Id %>')">Mua ngay</button>
                    </div>
                    </div>
                    <div class="img-box">
                        <%if(item.Image != null){ %>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        </a>
                        <%} %>
                    </div>
                    <div class="detail-box">
                    <h5>
                        <%=item.Title %>
                    </h5>
                    <h6>
                        <%if(item.DiscountType > 0) {%>
                        <span style="text-decoration:line-through">
                            <%=item.Price.ToString("N0") %> đ  
                        </span> 
                        <%} %>
                        <span>
                            <%if(item.Price > 0) {%>
                                <%=item.PriceAfterDiscount.ToString("N0") %> đ
                            <%} else {%>
                                Liên hệ
                            <%} %>
                        </span>
                    </h6>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
    </div>
</section>

<script>
    $(document).ready(function () {
        if ($('.product_similar .img-box img:first').height() == 0)
            $('.product_similar .img-box img').height($('.product_similar .img-box:first').width());
        else
            $('.product_similar .img-box img').height($('.product_similar .img-box img:first').height());
    });
</script>
<!-- end product section -->