<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(HREF.CurrentComponent != "home"){%>
 <!-- inner page section -->
      <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h1>
                         <%=Title %>
                     </h1>
                  </div>
               </div>
            </div>
         </div>
      </section>
      <!-- end inner page section -->
<%} %>

<!-- product section -->
<section class="product_section layout_padding">
    <div class="container">
        <div class="heading_container heading_center">
            <h2>
                <span>
                    <%=Category.Brief %>
                </span>
            </h2>
        </div>
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
             
        <%if(Top > 0 && TotalItems > Top){ %>
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
 
<script>
    $(document).ready(function () {
        if ($('.product_section .img-box img:first').height() == 0)
            $('.product_section .img-box img').height($('.product_section .img-box:first').width());
        else
            $('.product_section .img-box img').height($('.product_section .img-box img:first').height());
    });
</script>
<!-- end product section -->