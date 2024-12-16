<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="sp_spec">
    <div class="container">
        <%if(HREF.CurrentComponent == "home"){ %>
        <h2 class="t_h"><%=Title %></h2>
        <%} else { %>
        <h1 class="t_h"><%=Title %></h1>
        <%} %>
        <p class="subtitle">
            <%=Category.Brief %>
        </p>
        <div class="row">
            <%foreach(var item in this.Data) 
            {%>  
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="box">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail,item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id)%>">
                            <%if(item.Image != null){ %>
                            <picture>
                                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
                                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="img_object_fit" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Brief %>"/>
                            </picture>
                            <%} %>
                            <h3 class="post4title" title="<%=item.Title %>"><%=item.Title %></h3>
                        </a>
                        <div class="price-wrapper" style="height: 24px;"> 
                            <span class="price">
                                <%if(item.DiscountType > 0) {%>
                                <del aria-hidden="true">
                                    <span class="woocommerce-Price-amount amount">
                                        <bdi>
                                            <%=item.Price.ToString("N0")%>&nbsp;<span class="woocommerce-Price-currencySymbol">₫</span>
                                        </bdi>
                                    </span>
                                </del> 
                                <%} %>
                                <%if(item.Price > 0) {%>
                                <ins>
                                    <span class="woocommerce-Price-amount amount">
                                        <bdi>
                                            <%=item.PriceAfterDiscount.ToString("N0")%>&nbsp;<span class="woocommerce-Price-currencySymbol">₫</span>
                                        </bdi>
                                    </span>
                                </ins>
                                <%} else {%>
                                    Liên hệ
                                <%} %>
                            </span>
                        </div>
                    </div>
                </div>
            <%} %>
        </div><!-- End .ul_dv_spec -->
        <div class="seemore_bt">
        <%if(Top > 0 && TotalItems > Top) {%>
            <%if(HREF.CurrentComponent == "home") {%>
                <a href="<%=HREF.LinkComponent(Category.ComponentList, Title.ConvertToUnSign(), Category.Id) %>" style="background:<%=Skin.HeaderBackground%>;color:<%=Skin.HeaderFontColor%>">
                Xem tất cả sản phẩm
                </a>
            <%} else {%>
                <a href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, 1)%>">Trang đầu</a> 
                <%for(int i = 1; i <= TotalPages; i++){ %>
                <a style="padding: 5px;background: <%= i == CurrentPage ? "red" : "pink" %>;border: red 1px solid;border-radius: 10px;margin: 0px 3px;" href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, i)%>"><%=i %></a> 
                <%} %>
                <a href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, TotalPages)%>">Trang cuối</a> 
            <%} %>
        <%} %>
    </div><!-- End .min_wrap -->
        <%if(HREF.CurrentComponent != "home"){ %>
        <div class="category-contain">
            <%=Category.Content %>
        </div>
        <%} %>
    </div>
</section>

<script>
    $(document).ready(function () {
        if ($('.sp_spec .box img:first').height() == 0) 
            $('.sp_spec .box img').height($('.sp_spec .box:first').width());
        else
            $('.sp_spec .box img').height($('.sp_spec .box img:first').height());
    });
</script>