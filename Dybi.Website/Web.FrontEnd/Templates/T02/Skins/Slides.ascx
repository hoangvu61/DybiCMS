<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>


<%if(Data.Count > 0){ %>
<div id="carousel-slideshow" class="carousel slide" data-ride="carousel">
    <!-- Indicators -->
    <ol class="carousel-indicators"> 
        <%for(int i = 0; i< this.Data.Count;i++)
        {%>
            <li data-target="#carousel-slideshow" data-slide-to="<%=i %>" class="<%=i == 0 ? "active" : "" %>"></li>
        <%} %>
    </ol>

    <!-- Wrapper for slides -->
    <div class="carousel-inner">
        <%for(int i = 0; i< this.Data.Count;i++)
        {%>
        <div class="item <%=i == 0 ? "active" : "" %>">
            <picture>
		        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
		        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
            </picture>
        </div>
        <%} %>	
    </div>

    <!-- Controls -->
    <a class="left carousel-control" href="#carousel-slideshow" role="button" data-slide="prev">
        <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
        <span class="sr-only">Previous</span>
    </a>
    <a class="right carousel-control" href="#carousel-slideshow" role="button" data-slide="next">
        <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
        <span class="sr-only">Next</span>
    </a>
</div>
<%} %>