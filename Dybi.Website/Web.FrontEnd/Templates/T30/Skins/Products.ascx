<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<!-- product section -->
<section class="product_section layout_padding">
    <div class="container">
        <%if(HREF.CurrentComponent == "home"){ %>
        <div class="heading_container">
            <h2>
                <a href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
                    <%=Title %> 
                </a>
            </h2>
        </div>
        <%} else {%>
            <h1 title="<%=Category.Title %>"><%=Title %></h1>
            <div class="row">
                <div class="col-10 mx-auto" style="text-align:center"><%=Category.Brief %></div>
            </div>
        <%} %>
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
        
        <%if(Top > 0 && TotalItems > Top) {%>
        <div class="btn-box">
            <%if(HREF.CurrentComponent == "home") {%>
                <a class="btn btn-warning" href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
                Xem tất cả sản phẩm
                </a>
            <%} else {%>
                <a class="btn btn-warning" href="<%=LinkPage(1)%>">Trang đầu</a> 
                <%for(int i = 1; i <= TotalPages; i++){ %>
                <a class="btn btn-warning" style="padding: 10px;<%= i == CurrentPage ? "background:" + Config.Background + "75 !important" : "" %>;" href="<%=LinkPage(i)%>"><%=i %></a> 
                <%} %>
                <a class="btn btn-warning" href="<%=LinkPage(TotalPages)%>">Trang cuối</a> 
            <%} %>
        </div>
        <%} %>

        <%if(HREF.CurrentComponent != "home"){ %>
        <div class="alert alert-warning" style="margin-top:50px" role="alert">
            <div class="category-contain">
                <%=Category.Content %>
            </div>
        </div>
        <%} %>
        
    </div>
</section>
<!-- end product section -->