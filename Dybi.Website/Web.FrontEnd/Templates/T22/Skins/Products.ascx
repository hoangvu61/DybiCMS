<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<!-- project section -->
<section class="project_section layout_padding-bottom <%=HREF.CurrentComponent == "home" ? "" : "layout_padding-top"%>">
    <div class="container">
    <div class="heading_container">
        <h2>
        <%=Title %>
        </h2>
    </div>
    <div class="project_container layout_padding2-top">
        <%foreach(var item in this.Data) 
        {%> 
        <div class="box">
            <%if(item.Image != null){ %>
                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                <picture>
					<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                    <img src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore +  item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                </picture>
                </a>
            <%} %>
            <div class="box-text box-text-products">
			    <div class="title-wrapper">
                    <p class="name product-title woocommerce-loop-product__title">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>" class="woocommerce-LoopProduct-link woocommerce-loop-product__link">
                            <%=item.Title %>
                        </a>
                    </p>
			    </div>
                <div class="price-wrapper">
	                <span class="price">
                        <span class="woocommerce-Price-amount amount">
                            <%if(item.DiscountType > 0) {%>
                  <bdi style="text-decoration:line-through">
                      <%=item.Price.ToString("N0") %> ₫
                  </bdi>
                  <%} %>
                <bdi>
                    <%if(item.Price > 0) {%>
                        <%=item.PriceAfterDiscount.ToString("N0") %> ₫
                    <%} else {%>
                        Liên hệ
                    <%} %>
                </bdi>
                        </span>
	                </span>
                </div>		
            </div>
        </div>
        <%} %>
    </div>
    </div>
</section>
<!-- end project section -->