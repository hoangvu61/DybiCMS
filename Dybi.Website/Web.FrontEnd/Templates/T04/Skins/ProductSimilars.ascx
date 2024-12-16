<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="products">
    <div class="pro_list_title">
        <h2 class="cate_title" title="<%=Title %>">
            <%=Title %>
        </h2>
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
</div>

<script>
    $(document).ready(function () {
        if ($('.product-image:first').height() == 0)
            $('.product-image').height($('.product-image').width());
        else
            $('.product-image').height($('.product-image:first').height());
    });
</script>