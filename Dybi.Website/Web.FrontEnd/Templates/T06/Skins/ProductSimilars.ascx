<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<section class="team py-lg-4 py-md-3 py-sm-3 py-3" id="team">
    <div class="container py-lg-5 py-md-4 py-sm-4 py-3">
        <h1 class="title text-center mb-md-4 mb-sm-3 mb-3 mb-2 clr" title="<%=Title%>" style="<%=string.IsNullOrEmpty(this.Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
            <%=Title%>
        </h1>
        <div class="row ">
        <%foreach (var product in Data) {%>
            <div class="team-grid-colum text-center simpleCart_shelfItem col-lg-3 col-md-3 col-sm-6" style='text-align:center;overflow:hidden;margin-bottom:20px'>
                <div>
                    <%if (!string.IsNullOrEmpty(product.ImageName)) { %>
			        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, product.Title.ConvertToUnSign(), product.Id, SettingsManager.Constants.SendProduct, product.Id)%>">
                        <picture>
					        <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>.webp" type="image/webp">
					        <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%= HREF.DomainStore + product.Image.FullPath %>" style="max-height:100%; max-width:100%" class="img-fluid" alt="<%=product.Title %>"/>
                        </picture>
                    </a>
                    <%} %>
		        </div>
                  <div class="text-grid-gried">
                     <h5>
                         <a href="<%=HREF.LinkComponent(Category.ComponentDetail,product.Title.ConvertToUnSign(), product.Id, SettingsManager.Constants.SendProduct, product.Id)%>">
                             <%=product.Title%>
                         </a>
                     </h5>
                  </div>               
            </div>
        <%} %>
        </div>
    </div>
</section>