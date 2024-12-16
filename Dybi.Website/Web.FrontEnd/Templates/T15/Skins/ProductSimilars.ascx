<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<!-- shop section -->

  <section class="product_section layout_padding">
      <div class="container py_mobile_45">
        <div class="heading_container heading_center">
          <h2>
              <%=Title %>
          </h2>
        </div>
        <div class="row">
        <%foreach(var item in this.Data) 
        {%>  
        <div class="col-md-<%= this.Data.Count % 4 == 0 ? 3 : 4%>">
            <div class="box ">
                <div class="img-box">
                    <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>" title="<%=item.Title%>">
                    <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <picture>
						      <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						      <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        <%} %>
                    </a>
                </div>
                <div class="detail-box">
                    <h5> 
                        <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>" title="<%=item.Title%>">
                            <%=item.Title%>
                        </a>
                    </h5>
                    <p>
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
                    </p>
                </div>
            </div>
        </div>
        <%} %>
        </div>
      </div>
      <div class="btn-box">
        <%if(Top > 0 && TotalItems > Top){ %>
            <%if(Top < 9) {%>
                <a href="<%=HREF.LinkComponent("Products", Title.ConvertToUnSign(), true) %>">
                Xem tất cả sản phẩm
                </a>
            <%} else {%>
                <a href="<%=HREF.LinkComponent("Products", Title.ConvertToUnSign(), true, SettingsManager.Constants.SendProduct, ProductId, SettingsManager.Constants.SendPage, 1)%>">Trang đầu</a> 
                <%for(int i = 1; i <= TotalPages; i++){ %>
                <a style="padding: 5px;background: <%= i == CurrentPage ? "red" : "pink" %>;border: red 1px solid;border-radius: 10px;margin: 0px 3px;" href="<%=HREF.LinkComponent("Products", Title.ConvertToUnSign(), true, SettingsManager.Constants.SendProduct, ProductId, SettingsManager.Constants.SendPage, i)%>"><%=i %></a> 
                <%} %>
                <a href="<%=HREF.LinkComponent("Products", Title.ConvertToUnSign(), true, SettingsManager.Constants.SendProduct, ProductId, SettingsManager.Constants.SendPage, TotalPages)%>">Trang cuối</a> 
            <%} %>
        <%} %>
      </div>
    </div>
  </section>