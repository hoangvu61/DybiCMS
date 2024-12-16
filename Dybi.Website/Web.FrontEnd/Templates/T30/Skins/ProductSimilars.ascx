<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<!-- product section -->
<section class="product_section layout_padding">
    <div class="container">
        <div class="heading_container">
            <h2 class="comment-box__title"><%=Title %></h2>
        </div>
        <div class="product_container">
        <%foreach(var item in this.Data) 
        {%>
            <div class="box">
            <div class="img-box">
                <%if(item.Image != null)
                { %>
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
                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                    <%=item.Title %>
                </a>
            </h5>
            <h4>
                <%if(item.DiscountType > 0) {%>
                <del>
                    <%=item.Price.ToString("N0") %><span> đ</span>
                </del> 
                <%} %>

                <%if(item.Price > 0) {%>
                    <%=item.PriceAfterDiscount.ToString("N0") %><span> đ</span>
                <%} else {%>
                    Liên hệ
                <%} %>
            </h4>
            </div>
        </div>
        <%} %>
        </div>
    </div>
</section>
<!-- end product section -->