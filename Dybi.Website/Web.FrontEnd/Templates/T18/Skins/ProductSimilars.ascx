<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<!-- shop section -->
<section class="shop_section layout_padding" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <aside class="p-4">
        <div class="row">
            <%foreach(var item in this.Data) 
            {%>  
            <div class="col-6 col-sm-6 col-md-4 col-lg-3 col-xl-2">
                <div class="product-item">
                    <div class="box">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                            <div class="img-box">
                                <%if(item.Image.FileExtension == ".webp"){ %>
                                    <img src="HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                <%} else { %>
                                    <picture>
						                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                    </picture>
                                <%} %>
                            </div>
                        </a>
                        <div class="detail-box">
                            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id)%>">
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
    </aside>
</section>
<!-- end shop section -->