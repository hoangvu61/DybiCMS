<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<div class="products">
    <div class="pro_list_title">
        <%if(HREF.CurrentComponent == "home"){ %>
        <h2 class="cate_title" title="<%=Category.Title %>">
            <%=Title %>
            <%=!string.IsNullOrEmpty(AttributeName) ? " - " + AttributeName : "" %> 
            <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName : "" %>
        </h2>
        <%} else {%>
        <h1 class="cate_title" title="<%=Category.Title %>">
            <%=Title %>
            <%=!string.IsNullOrEmpty(AttributeName) ? " - " + AttributeName : "" %> 
            <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName : "" %>
            <%=!string.IsNullOrEmpty(TagName) ? " : " + TagName : "" %>
        </h1>
        <%} %>
        <span><%=Category.Brief %></span>
    </div>
    <div class="w3-row-padding spham">
        <%foreach(var item in this.Data) 
        {%> 
        <div id="product<%=item.Id%>"  class="w3-col m3 s6 i6">
            <div class="pro_item">
                <div style="padding:5px">
                    <div class="product-image">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img id="bay<%=item.Id%>" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        </a>
                    </div>
                    <div class="pro_name">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                            <%=item.Title%></a>
                    </div>

                    <div class="pro_price">
                        <%if(item.DiscountType > 0) {%>
                        <del class='reduce'>
                            <%=item.Price.ToString("N0") %>
                        <del>
                        <%} %>
                        <span class='price'>
                            <%if(item.Price > 0) {%>
                                <%=item.PriceAfterDiscount.ToString("N0") %>
                            <%} else {%>
                                Liên hệ
                            <%} %>
                        </span>
                    </div>
                </div>
            </div>

            <div class="product-actions" style="cursor:pointer" onclick="SelectProduct('<%=item.Id%>','<%=item.Title%>','<%=HREF.DomainStore + item.Image.FullPath%>.webp','<%=item.Brief.Replace(",", " ").Replace("'", "\"").Replace("\n", ".").Replace("\r", ".").Trim()%>','<%=item.PriceAfterDiscount.ToString("N0") %>')">
                <span style="color:#fff">
                    <span>
                        <i style="font-size:24px" class="fa"></i>
                        <span><%=Language["AddToCart"] %></span>
                    </span>
                </span>
            </div>
        </div>
        <%} %>
    </div>

    <%if(Top > 0 && TotalItems > Top) {%>
    <div class="btn-box">
        <%if(HREF.CurrentComponent == "home") {%>
            <a class="w3-btn w3-ripple w3-green" href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
            Xem tất cả sản phẩm
            </a>
        <%} else {%>
            <a class="w3-btn w3-ripple w3-green" href="<%=LinkPage(1)%>">Trang đầu</a> 
            <%for(int i = 1; i <= TotalPages; i++){ %>
            <a class="btn-t13" style="padding: 10px;<%= i == CurrentPage ? "background:" + Config.Background + "75 !important" : "" %>;" href="<%=LinkPage(i)%>"><%=i %></a> 
            <%} %>
            <a class="w3-btn w3-ripple w3-green" href="<%=LinkPage(TotalPages)%>">Trang cuối</a> 
        <%} %>
    </div>
    <%} %>

     <%if(HREF.CurrentComponent != "home"){ %>
        <div class="category-contain">
            <%=Category.Content %>
        </div>
    <%} %>
</div>

<script>
    $(document).ready(function () {
        if ($('.product-image:first').height() == 0)
            $('.product-image').height($('.product-image').width());
        else
            $('.product-image').height($('.product-image:first').height());
    });
</script>





