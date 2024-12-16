<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<section class="sp_spec">
    <div class="container">
        <h2 class="t_h">Sản phẩm cùng loại</h2>
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