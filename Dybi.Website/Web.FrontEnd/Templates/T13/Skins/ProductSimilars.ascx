<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<!-- shop section -->

  <section class="shop_section layout_padding">
    <div class="container">
      <div class="heading_container heading_center">
        <h2>
            <%=Title %>
        </h2>
      </div>
      <div class="row"> 
        <%foreach(var item in this.Data) 
        {%>  
        <div class="col-6 col-md-4 col-lg-3">
          <div class="box">
            <div>
              <div class="img-box"> 
                  <%if(item.Image != null && !string.IsNullOrEmpty(item.Image.FullPath)){ %>
                   <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                        <picture>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore +  item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
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
    </div>
  </section>