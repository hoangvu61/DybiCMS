<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>


 <!-- blog section -->

  <div class="blog_section layout_padding">
    <div class="container">
      <div class="heading_container">
        <h1 class="product_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
      </div>

      <div class="row">
        <div class="col-12 col-md-6 col-lg-4 mx-auto">
            <div class="box" style="background:none">
            <div class="img-box">
            <div id="customCarousel1" class="carousel  slide" data-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active">
                        <div class="slider_img_box">
                             <picture>
						  <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
						  <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=!string.IsNullOrEmpty(Data.ImageName) ? HREF.DomainStore + Data.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=Title %>"/>
                        </picture> 
                        </div>
                    </div>
                <%foreach(var image in Images) 
                {%>
                    <div class="carousel-item">
                        <div class="slider_img_box">
                            <picture>
		                        <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                        <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + image.FullPath%>"/>
                            </picture>     
                        </div>
                    </div>
                <%} %>
            </div>
            </div>
          <div class="carousel_btn-box">
        <a class="carousel-control-prev" href="#customCarousel1" role="button" data-slide="prev">
            <i class="fa fa-angle-left" aria-hidden="true"></i>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#customCarousel1" role="button" data-slide="next">
            <i class="fa fa-angle-right" aria-hidden="true"></i>
            <span class="sr-only">Next</span>
        </a>
        </div>
            </div>
              </div>
            </div>
          <div class="col-12 col-md-6 col-lg-4 mx-auto">
              <div class="box">
            <div class="detail-box">
              <p>
                  <%=Data.Brief %>
              </p> 
                <p>
                    <%if(Data.DiscountType > 0) {%>
                  <h6 style="text-decoration:line-through">
                      <%=Data.Price.ToString("N0") %> ₫
                  </h6>
                  <%} %>
                <h6>
                    <%if(Data.Price > 0) {%>
                        <%=Data.PriceAfterDiscount.ToString("N0") %> ₫
                    <%} else {%>
                        Liên hệ
                    <%} %>
                </h6>
                </p>
                <%if(Data.Price > 0) {%>
                <button type="button" onclick="AddToCart()" class="btn btn-t13">Chọn mua</button>
                <%} %>
            </div>
                  </div>
          </div>
          <div class="col-12 col-md-12 col-lg-4 mx-auto">
              <div class="box" style="background:none">
            <div class="detail-box">
              <table border="1" style="width:100%">
                  <%foreach(var attribute in Attributes){ %>
                      <tr>
                          <td>
                              <%=attribute.Name %>
                          </td>
                          <td>
                              <%=attribute.ValueName == "True" ? "Có" : attribute.ValueName == "False" ? "Không" : attribute.ValueName %>
                          </td>
                      </tr>
                  <%} %>
              </table>
            </div>
                  </div>
          </div>
            <div class="productdetail">
                <%=Data.Content%> 
                <div class="tag">
                    <%=string.Join(", ", Tags)%> 
                </div>
            </div>
      </div>
    </div>
  </div>

  <!-- end blog section -->


<script type="text/javascript">
    function AddToCart() {
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
            data: JSON.stringify({ productId: '<%=Data.Id%>', quantity: 1, properties: '', addonIds: '' }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    location.href = '<%=HREF.BaseUrl %>cart/don-hang';
                 }
             }
        });
     }
</script>	

<script type="application/ld+json">
      {
        "@context": "https://schema.org/",
        "@type": "Product",
        "name": "<%=Data.Title %>",
        "image": [
          "<%=HREF.DomainStore + Data.Image.FullPath%>.webp",
          "<%=HREF.DomainStore + Data.Image.FullPath%>"
         ],
        "description": "<%=Data.Brief.Replace("\"","") %>",
    <%if(!string.IsNullOrEmpty(Data.GetAttribute("Brand"))){ %>
        "brand": {
          "@type": "Brand",
          "name": "<%=Data.GetAttribute("Brand") %>"
        },
    <%} %>
        "offers": {
          "@type": "Offer",
          "availability": "https://schema.org/InStock",
          "price": "<%=Data.PriceAfterDiscount.ToString("N0") %>",
          "priceCurrency": "VND"
        }
      }
    </script>