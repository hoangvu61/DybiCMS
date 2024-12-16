<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="team py-lg-4 py-md-3 py-sm-3 py-3" id="team">
    <div class="container py-lg-5 py-md-4 py-sm-4 py-3">
        <%if(HREF.CurrentComponent == "home"){ %>
        <h2 class="title text-center mb-md-4 mb-sm-3 mb-3 mb-2 clr" title="<%=Category.Title %>">
            <%=Title %>
            <%=!string.IsNullOrEmpty(AttributeName) ? " - " + AttributeName : "" %> 
            <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName : "" %>
        </h2>
        <%} else {%>
        <h1 class="title text-center mb-md-4 mb-sm-3 mb-3 mb-2 clr" title="<%=Category.Title %>">
            <%=Title %>
            <%=!string.IsNullOrEmpty(AttributeName) ? " - " + AttributeName : "" %> 
            <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName : "" %>
            <%=!string.IsNullOrEmpty(TagName) ? " : " + TagName : "" %>
        </h1>
        <%} %>
        <div class="title-wls-text text-center mb-lg-5 mb-md-4 mb-sm-4 mb-3 sub-colors">
            <p>
                <%=Category.Brief %> 
            </p>
        </div>
        <div class="row ">
        <%foreach(var product in Data){ %>
            <div class="team-grid-colum text-center simpleCart_shelfItem col-lg-3 col-md-3 col-sm-6" style='text-align:center;overflow:hidden;margin-bottom:20px'>
                <div>
                     <%if (!string.IsNullOrEmpty(product.ImageName)) { %>
			        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, product.Title.ConvertToUnSign(), product.Id, SettingsManager.Constants.SendProduct, product.Id)%>">
                        <picture>
					        <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>.webp" type="image/webp">
					        <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>" type="image/jpeg"> 
                            <img id="bay<%=product.Id %>" src="<%=HREF.DomainStore + product.Image.FullPath%>" style="max-height:100%; max-width:100%" class="img-fluid" alt="<%=product.Title %>"/>
                        </picture>
                    </a>
                    <%} %>
		        </div>
                  <div class="text-grid-gried">
                     <h4>
                         <a href="<%=HREF.LinkComponent(Category.ComponentDetail, product.Title.ConvertToUnSign(), product.Id, SettingsManager.Constants.SendProduct, product.Id)%>">
                             <%=product.Title%>
                         </a>
                     </h4>
                  </div>               
            </div>
        <%} %>
        </div>
    </div>
</section>