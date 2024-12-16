<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<!-- our_room -->
<div  class="our_room">
    <div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="titlepage">
                <h2>
                    <%=Title %>
                </h2>
            </div>
        </div>
    </div>
    <div class="row">
        <%foreach(var item in this.Data) 
        {%>
        <div class="col-md-4 col-sm-6">
            <div id="serv_hover"  class="room">
                <div class="room_img">
                <figure>
                    <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>">
                      <picture>
						<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                        <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore +  item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                      </picture>
                    </a>
                </figure>
                </div>
                <div class="bed_room">
                <h3>
                    <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>">
                      <%=item.Title %>
                    </a>
                </h3>
                    <%if(item.DiscountType > 0) {%>
                      <h6>
                          <%=item.PriceAfterDiscount.ToString("N0") %> đ - 
                      <%} %>
                        <%if(item.Price > 0) {%>
                            <%=item.Price.ToString("N0") %> đ
                        <%} else {%>
                            Liên hệ
                        <%} %>
                    </h6>
                <p><%=item.Brief %></p>
                </div>
            </div>            
        </div>
        <%} %>
    </div>
    </div>
</div>
<!-- end our_room -->