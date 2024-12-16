<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>


<!-- why section -->
<section class="why_section layout_padding">
    <div class="container">
    <div class="heading_container heading_center">
        <h2>
            <%=Title %>
        </h2>

        <%if(!string.IsNullOrEmpty(Category.Brief)){ %>
        <p>
            <%=Category.Brief %>
        </p>
        <%} %>
    </div>
    <div class="row">
        <%foreach(var item in this.Data) 
        {%> 
        <div class="col-md-4">
            <div class="box ">
                <div class="img-box">
                    <a href="<%=item.Url%>" title="<%=item.Title%>">
                    <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <picture>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img style="width:100%" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    <%} %>
                </a>
                </div>
                <div class="detail-box">
                <h5>
                    <a href="<%=item.Url%>" title="<%=item.Title%>">
                            <%=item.Title%>
                    </a>
                </h5>
                </div>
            </div>
        </div>
        <%} %>
    </div>
    </div>
</section>
<!-- end why section -->


    