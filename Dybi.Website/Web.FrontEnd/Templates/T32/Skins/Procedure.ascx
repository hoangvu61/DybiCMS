<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- service section -->
<section class="procedure_section layout_padding">
    <div class="container ">
        <div class="heading_container heading_center">
        <h2><%=Title %></h2>
        </div>
        <div class="row">
            <%foreach(var item in this.Data) {%> 
            <div class="col-6 col-sm-6 col-md-<%=Skin.Width > 0 ? 12/Skin.Width : 4 %>">
                <div class="box ">
                <div class="img-box">
                    <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <picture>
                            <%if(item.Image.FileExtension != ".webp"){ %>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <%} %>
                            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    <%} %>
                </div>
                <div class="detail-box">
                    <h5>
                            <%=item.Title%>
                    </h5>
                    <p style="<%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>">
                        <%=item.Content %>
                    </p>
                </div>
                </div>
            </div>
            <%} %>
        </div>
    </div>
</section>
<!-- end service section -->
