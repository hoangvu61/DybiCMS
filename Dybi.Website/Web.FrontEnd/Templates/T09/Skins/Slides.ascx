<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- slider section -->
<div id="myCarousel" class="carousel slide banner" data-ride="carousel">
    <ol class="carousel-indicators"> 
        <%for(int i = 0; i < this.Data.Count; i++) 
        {%>
            <li data-target="#myCarousel" data-slide-to="<%=i %>" class="<%= i== 0 ? "active" : "" %>"></li>
        <%} %>
    </ol>
    <div class="carousel-inner">
         <%for(int i = 0; i < this.Data.Count; i++) 
         {%>
        <div class="carousel-item <%= i== 0 ? "active" : "" %>">
            <picture>
		        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
		        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
            </picture> 
        </div>
<%} %>
    </div>
    <a class="carousel-control-prev" href="#myCarousel" role="button" data-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
    </a>
    <a class="carousel-control-next" href="#myCarousel" role="button" data-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
    </a>
</div>
<!-- end slider section -->