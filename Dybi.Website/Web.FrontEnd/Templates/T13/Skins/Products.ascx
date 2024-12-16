<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(Data.Count > 0){ %>
<!-- shop section -->
<section id="<%= Category.Id%>" class="shop_section <%= DisplayTitle ? "layout_padding-top" : ""%>">
    <div class="container">

        <%if(DisplayTitle){ %>
        <div class="heading_container heading_center wow animate__ animate__fadeInUp animated" data-wow-duration="1.2s" data-wow-delay="0.2s">
            <%if(HREF.CurrentComponent == "home"){ %>
            <h2>
                <a href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
                    <%=Title %> 
                </a>
            </h2>
            <%} else {%>
            <h1 style="margin-top:20px">
                <%=Title %> 
                <%=!string.IsNullOrEmpty(AttributeName) ? " - " + AttributeName : "" %> 
                <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName : "" %>
                <%=!string.IsNullOrEmpty(TagName) ? " : " + TagName : "" %>
            </h1>
            <p>
                <%=Category.Brief %>
            </p>
            <%} %>
        </div>
        <%} else {%>
            <%if(!string.IsNullOrEmpty(AttributeName)){ %>
            <div class="heading_container heading_center wow animate__ animate__fadeInUp animated" data-wow-duration="1.2s" data-wow-delay="0.2s">
                <h2 style="margin-top:20px">
                    <%=AttributeName%> 
                    <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName : "" %>
                </h2>
            </div>
            <%} %>
        <%} %>

        <div class="short-filter wow animate__ animate__fadeInUp  animated" data-wow-duration="1.2s" data-wow-delay="0.2s">
            <div class="input-group mb-3">
            <%if(DisplaySort){ %>
                <div class="input-group-prepend">
                    <span class="input-group-text">Sắp xếp</span>
                </div>
                <div class="dropdown">
                    <button class="btn btn-t13 dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <%=OrderBy == "" ? "Mới nhất" 
                            : OrderBy == "priceup" ? "Giá tăng dần"
                            : OrderBy == "pricedown" ? "Giá giảm dần"
                            : OrderBy == "view" ? "Mua nhiều nhất"
                            : OrderBy == "discountdown" ? "Khuyến mãi nhiều nhất"
                            : "Khuyến mãi ít nhất" %>
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                        <a class="dropdown-item" href="<%=LinkChangeProductOrder("") %>">Mới nhất</a>
                        <a class="dropdown-item" href="<%=LinkChangeProductOrder("PRICEUP") %>">Giá tăng dần</a>
                        <a class="dropdown-item" href="<%=LinkChangeProductOrder("PRICEDOWN") %>">Giá giảm dần</a>
                        <a class="dropdown-item" href="<%=LinkChangeProductOrder("VIEW") %>">Mua nhiều nhất</a>
                        <a class="dropdown-item" href="<%=LinkChangeProductOrder("DISCOUNTDOWN") %>">Khuyến mãi nhiều nhất</a>
                    </div>
                </div>
            <%} %>
            <%if(DisplayFilter && ProductAttributes != null && ProductAttributes.Count(e => e.SourceId != null) > 0){ %>
                <div class="input-group-prepend">
                    <span class="input-group-text">Lọc</span>
                </div>
                <%foreach(var source in ProductAttributes.Where(e => e.SourceId != null)){ %>
                <div class="dropdown">
                    <button class="btn btn-t13 dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <%=source.SourceTitle%>
                        <%if(!string.IsNullOrEmpty(AttributeValueName) && source.SourceValues.Any(e => e.Key.ToLower() == AttributeValue.ToLower())){%>
                            <%= ": " + AttributeValueName%>
                        <%} %>
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                        <%foreach(var sourceValue in source.SourceValues){ %>
                            <a class="dropdown-item" href="<%=LinkFilterAttribute(source.SourceId, sourceValue.Key)%>" title="<%=source.SourceTitle + " - " + sourceValue.Value%>">
                                <%=sourceValue.Value%>
                            </a>
                        <%}%>
                    </div>
                </div>
                <%} %> 
            <%} %>
            </div>
        </div>
        
        <div class="row"> 
        <%foreach(var item in this.Data) 
        {%>  
        <div class="col-6 col-sm-4 col-md-4 col-lg-<%=Data.Count > 4 ? 3 : 12/Data.Count %> mx-auto wow animate__animated animate__zoomIn animated" data-wow-duration="0.8s" data-wow-delay="0.3s">
            <div class="box">
            <div>
                <div class="img-box"> 
                    <%if(item.Image != null && !string.IsNullOrEmpty(item.Image.FullPath)){ %>
                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                        <picture>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img class="img_object_fit" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    </a>
                    <%} %>
                </div>
                <div class="detail-box">
                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                    <%=item.Title %>
                </a>
                    <%if(item.DiscountType > 0) {%>
                    <h6 style="text-decoration:line-through">
                        <%=item.Price.ToString("N0") %>
                    </h6>
                    <%} %>
                    <h6>
                        <%if(item.Price > 0) {%>
                            <%=item.PriceAfterDiscount.ToString("N0") %>
                        <%} else {%>
                            Liên hệ
                        <%} %>
                    </h6>
                </div>
            </div>
            </div>
        </div>
        <%} %>
        </div>
        <%if(Top > 0 && TotalItems > Top) {%>
        <div class="btn-box">
            <%if(HREF.CurrentComponent == "home") {%>
                <a class="btn-t13" href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
                Xem tất cả sản phẩm
                </a>
            <%} else {%>
                <a class="btn-t13" href="<%=LinkPage(1)%>">Trang đầu</a> 
                <%for(int i = 1; i <= TotalPages; i++){ %>
                <a class="btn-t13" style="padding: 10px;<%= i == CurrentPage ? "background:" + Config.Background + "75 !important" : "" %>;" href="<%=LinkPage(i)%>"><%=i %></a> 
                <%} %>
                <a class="btn-t13" href="<%=LinkPage(TotalPages)%>">Trang cuối</a> 
            <%} %>
        </div>
        <%} %>
    </div>
</section>

<%--<script>
    $(document).ready(function () {
        if ($('#<%= Category.Id%> img:first').height() == 0)
            $('#<%= Category.Id%> img').height($('#<%= Category.Id%> .img-box:first').width());
        else
            $('#<%= Category.Id%> img').height($('#<%= Category.Id%> img:first').height());
    });
</script>--%>
<%} %>