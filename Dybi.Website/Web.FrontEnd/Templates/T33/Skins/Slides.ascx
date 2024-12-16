<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div id="carousel-example-generic" class="carousel slide" data-bs-ride="carousel"   >
<!-- Indicators -->
    <div class="carousel-indicators">
    <%for(int i = 0; i < this.Data.Count; i++) 
    {%>
        <button type="button" data-bs-target="#carousel-example-generic" data-bs-slide-to="<%=i %>" class="<%= i== 0 ? "active" : "" %>"></button>
    <%} %>
    </div>

    <!-- Wrapper for slides -->
    <div class="carousel-inner">
        <%for(int i = 0; i<this.Data.Count; i++) 
        {%>  
        <div class="carousel-item <%= i== 0 ? "active" : "" %>">
            <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                <picture>
				    <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
				    <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                    <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" class="d-block w-100" alt="<%=Data[i].Title %>"/>
                </picture>
            <%} %>
        </div>
    <%} %>
    </div>

    <button class="carousel-control-prev" type="button" data-bs-target="#carousel-example-generic" data-bs-slide="prev">
        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Previous</span>
    </button>
    <button class="carousel-control-next" type="button" data-bs-target="#carousel-example-generic" data-bs-slide="next">
        <span class="carousel-control-next-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Next</span>
    </button>
</div>

