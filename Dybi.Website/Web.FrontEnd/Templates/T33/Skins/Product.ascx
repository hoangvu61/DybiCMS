<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

 <!-- blog section -->

<section class="product_section">
    <div class="header-page">
        <div class="container">
            <div class="row">
	        <div class="col-xs-12 col-md-6 col-left-banner">
		        <div class="wrap-ehd-text-banner">
			        <h1>
                        <%=Title %>
			        </h1>
			        <div class="wrap-list-text-banner">
				        <strong>
                            <%=Data.Brief %>
                        </strong> 
			        </div>
		        </div>
	        </div>
	        <div class="col-xs-12 col-md-6">
		        <div class="bg-eHD-Tax">
			        <picture>
						<source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
						<source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                        <img class="img-fluid" src="<%=!string.IsNullOrEmpty(Data.ImageName) ? HREF.DomainStore +  Data.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=Data.Title %>"/>
                    </picture>
		        </div>		
	        </div>
        </div>
        </div>
    </div>
    <div class="container">
        <div class="productdetail">
            <%=Data.Content%> 
        </div>               
    </div>
</section>

  <!-- end blog section -->

    <script type="application/ld+json">
      {
        "@context": "https://schema.org/",
        "@type": "Product",
        "name": "<%=Data.Title %>",
        "image": [
          "<%=HREF.DomainStore + Data.Image.FullPath%>.webp",
          "<%=HREF.DomainStore + Data.Image.FullPath%>"
         ],
        "description": "<%=Data.Brief %>",
        "offers": {
          "@type": "Offer",
          "price": "<%=Data.PriceAfterDiscount.ToString("N0") %>",
          "priceCurrency": "VNĐ"
        }
      }
    </script>