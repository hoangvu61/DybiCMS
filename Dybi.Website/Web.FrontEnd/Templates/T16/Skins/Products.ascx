<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

 <!-- our_fishs -->
      <div class="our_fishs">
         <div class="container">
            <div class="row">
               <div class="col-md-12">
                  <div class="titlepage text_align_center">
                     <h2><%=Title %></h2>
                     <p>
                         <%=Category.Brief %> 
                     </p>
                  </div>
               </div>
            </div>
            <div class="row">
               <div class="col-md-12">
                  <!--  Demos -->
                  <div class="owl-carousel owl-theme">
                  <%foreach(var item in this.Data) 
                  {%>  
                      <div class="item">
                        <div class="our_fishs_box text_align_center">
                           <div class="ser_img">
                               <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>">
                                  <figure>
                                      <picture>
						              <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						              <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                        <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore +  item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                                    </picture>
                                  </figure>
                               </a>
                           </div>
                           <h3>
                               <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>">
                                <%=item.Title %>
                                </a>
                           </h3>
                           <strong>
                            <%if(item.DiscountType > 0) {%>
                              <span style="text-decoration:line-through">
                                  <%=item.Price.ToString("N0") %>
                              </span>
                              <%} %>
                            <span>
                                <%if(item.Price > 0) {%>
                                    <%=item.PriceAfterDiscount.ToString("N0") %>
                                <%} else {%>
                                    Liên hệ
                                <%} %>
                            </span>
                           </strong>
                        </div>
                     </div>
                  <%} %>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <!-- end our_fishs --> 
<div class="btn-box">
        <%if(Top > 0 && TotalItems > Top) {%>
                <%if(Top < 9) {%>
                    <a href="<%=HREF.LinkComponent("Products", Title.ConvertToUnSign(), true) %>" style="background:<%=Skin.HeaderBackground%>;color:<%=Skin.HeaderFontColor%>">
                    Xem tất cả sản phẩm
                    </a>
                <%} else {%>
                    <a href="<%=HREF.LinkComponent("Products", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, 1)%>">Trang đầu</a> 
                    <%for(int i = 1; i <= TotalPages; i++){ %>
                    <a style="padding: 5px;background: <%= i == CurrentPage ? "red" : "pink" %>;border: red 1px solid;border-radius: 10px;margin: 0px 3px;" href="<%=HREF.LinkComponent("Products", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, i)%>"><%=i %></a> 
                    <%} %>
                    <a href="<%=HREF.LinkComponent("Products", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, TotalPages)%>">Trang cuối</a> 
                <%} %>
        <%} %>
      </div>
<!-- shop section -->