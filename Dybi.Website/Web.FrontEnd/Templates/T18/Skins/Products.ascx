﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<!-- shop section -->
<section class="shop_section layout_padding" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
        <div class="heading_container heading_center" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
        <%if(HREF.CurrentComponent == "home"){ %>
            <h2 title="<%=Category.Title %>">
                <%=Title %>
            </h2>
        <%} else {%>
            <h1 title="<%=Page.Title %>">
                <%=Title %>
                <%=!string.IsNullOrEmpty(AttributeName) ? " - " + AttributeName : "" %> 
                <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName : "" %>
                <%=!string.IsNullOrEmpty(TagName) ? " : " + TagName : "" %>
            </h1>
        <%} %>
            <p>
                <%=Category.Brief %>
            </p>
        </div>
    </div>
    <div class="<%= HREF.CurrentComponent == "home" ? "container" : "p-4"%>">
        <div class="row">
            <%foreach(var item in this.Data) 
            {%>  
            <div class="col-6 col-sm-6 col-md-4 col-lg-3 <%= HREF.CurrentComponent == "home" ? "" : "col-xl-2"%>">
                <div class="product-item">
                    <div class="box">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                            <div class="img-box">
                                <%if(item.Image.FileExtension == ".webp"){ %>
                                    <img src="<%= HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                <%} else { %>
                                    <picture>
						                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%= HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                    </picture>
                                <%} %>
                            </div>
                        </a>
                        <div class="detail-box">
                            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id)%>" title="<%=item.Title %>">
                                <%=item.Title %>
                            </a>
                            <%if(item.DiscountType > 0) {%>
                            <span style="text-decoration:line-through">
                                <%=item.Price.ToString("N0") %> <sup>đ</sup>
                            </span>
                            <%} %>
                            <span class="price">
                                <%if(item.Price > 0) {%>
                                    <%=item.PriceAfterDiscount.ToString("N0") %> <sup>đ</sup>
                                <%} else {%>
                                    Liên hệ
                                <%} %>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <%} %>
        </div>

       
        <div class="btn-box">
            <%if(HREF.CurrentComponent == "home") {%>
                <a href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
                Xem thêm
                </a>
            <%} else {%>
                <%if(Top > 0 && TotalItems > Top) {%>
                    <a href="<%=LinkPage(1)%>">Trang đầu</a> 
                    <%for(int i = 1; i <= TotalPages; i++){ %>
                    <a style="padding: 10px;<%= i == CurrentPage ? "background:" + Config.Background + "75 !important" : "" %>;" href="<%=LinkPage(i)%>"><%=i %></a> 
                    <%} %>
                    <a href="<%=LinkPage(TotalPages)%>">Trang cuối</a> 
                <%} %>
            <%} %>
        </div>
        

        <%if(HREF.CurrentComponent != "home"){ %>
            <div class="category-contain">
                <%=Category.Content %>
            </div>
        <%} %>
    </div>
</section>
<!-- end shop section -->