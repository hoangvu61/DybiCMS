<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="prdcategory-child list">
    <div class="container">
        <%if(DisplayTitle){ %>
        <div class="panel-head">
            <%if(HREF.CurrentComponent == "home"){ %>
            <h2 class="h-title" title="<%=Category.Title %>">
                <%=Title %>
                <%=!string.IsNullOrEmpty(AttributeName) ? " - " + AttributeName : "" %> 
                <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName : "" %>
            </h2>
            <%} else {%>
            <h1 class="h-title" title="<%=Category.Title %>">
                <%=Title %>
                <%=!string.IsNullOrEmpty(AttributeName) ? " - " + AttributeName : "" %> 
                <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName : "" %>
                <%=!string.IsNullOrEmpty(TagName) ? " : " + TagName : "" %>
            </h1>
            <%} %>

            <p>
                <%=Category.Brief %>
            </p>

            <%if(DisplaySort){ %>
                <div class="products-short">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Sắp xếp</span>
                        <div class="input-group-btn">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <%=OrderBy == "" ? "Mới nhất" 
                                    : OrderBy == "priceup" ? "Giá tăng dần"
                                    : OrderBy == "pricedown" ? "Giá giảm dần"
                                    : OrderBy == "view" ? "Mua nhiều nhất"
                                    : OrderBy == "discountdown" ? "Khuyến mãi nhiều nhất"
                                    : "Khuyến mãi ít nhất" %> <span class="caret">
                                   </span></button>
                            <ul class="dropdown-menu">
                                <li><a class="dropdown-item" href="<%=LinkChangeProductOrder("") %>">Mới nhất</a></li>
                                <li><a class="dropdown-item" href="<%=LinkChangeProductOrder("PRICEUP") %>">Giá tăng dần</a></li>
                                <li><a class="dropdown-item" href="<%=LinkChangeProductOrder("PRICEDOWN") %>">Giá giảm dần</a></li>
                                <li><a class="dropdown-item" href="<%=LinkChangeProductOrder("VIEW") %>">Mua nhiều nhất</a></li>
                                <li><a class="dropdown-item" href="<%=LinkChangeProductOrder("DISCOUNTDOWN") %>">Khuyến mãi nhiều nhất</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            <%} %>
            <div style="clear:both"></div>
        </div> 
        <%} %>
        <div class="feature-product">
            <div class="row">
                <div class="main-featured-content">
                <%foreach(var product in Data){ %>
                <div class="col-lg-2 col-md-3 col-sm-4 col-xs-6">
			        <div class="product">	
                        <div class="thumb img-shine">
                            <%if(!string.IsNullOrEmpty(product.ImageName)){ %>
					        <a class="image img-scaledown" href="<%=HREF.LinkComponent(Category.ComponentDetail,product.Title.ConvertToUnSign(), product.Id, SettingsManager.Constants.SendProduct, product.Id)%>" title="<%=product.Title %>%>">
                                <picture>
					                <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>.webp" type="image/webp">
					                <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>" type="image/jpeg"> 
                                    <img id="bay<%=product.Id %>" src="<%= HREF.DomainStore + product.Image.FullPath%>" alt="<%=product.Title %>" style="max-height:100%; max-width:100%;display: inline;"/>
                                </picture>
                            </a>
                            <%} %>
                        </div>   
                    
                        <div class="info">
                            <h3 class="title">
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail,product.Title.ConvertToUnSign(), product.Id, SettingsManager.Constants.SendProduct, product.Id)%>" title="<%=product.Title %>">
                                    <%=product.Title%>
                                </a>
                            </h3>
                            <div class="price uk-flex uk-flex-middle uk-flex-space-between">
                                <%if(product.DiscountType > 0) {%>
                                  <del>
                                      <%=product.Price.ToString("N0") %> <sup>đ</sup>
                                  </del>
                                <%} %>
                                <span class="sale-price">
                                    <%if(product.Price > 0) {%>
                                        <%=product.PriceAfterDiscount.ToString("N0") %> <sup>đ</sup>
                                    <%} else {%>
                                        Liên hệ
                                    <%} %>
                                </span>
                            </div>
                            <button type="button" data-toggle="modal" data-target="#buyModal" style='width:100%' class="addCarts btn btn-primary" onclick="SelectProduct('<%=product.Id%>','<%=product.Title%>','<%=HREF.DomainStore + product.Image.FullPath%>','<%=product.SaleMin %>','<%=product.Brief.Replace(",", " ").Replace("'", "\"").Replace("\n", ".").Replace("\r", ".").Trim()%>','<%=product.PriceAfterDiscount.ToString("N0")%>')" >Mua ngay</button>
                        </div>
			        </div>
		        </div> 
                <%} %> 
                <div class="paging">
                    <%if(Top > 0 && TotalItems > Top) {%>
                    <div class="btn-box">
                        <%if(HREF.CurrentComponent == "home") {%>
                            <a class="w3-btn w3-ripple w3-green" href="<%=HREF.LinkComponent("Products", Category.Title.ConvertToUnSign(), true, "scat", Category.Id)%>" title="<%=Category.Title %>">
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
                </div>
            </div>
        </div>
        </div>

        <%if(HREF.CurrentComponent != "home"){ %>
            <div class="alert alert-info category-contain" role="alert">
                <%=Category.Content %>
            </div>
        <%} %>

    </div>
</section>

<script>
    $(document).ready(function () {
        if ($('.feature-product img:first').height() == 0)
            $('.feature-product .thumb').height($('.feature-product .product .img:first').width());
        else
            $('.feature-product .thumb').height($('.feature-product img:first').height());
    });
</script>
