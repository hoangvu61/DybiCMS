<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- store section -->
<section class="store_section layout_padding-bottom">
    <div class="container">
        <div class="heading_container">
        <h2>
            <%=Title %>
        </h2>
        </div>
    </div>
    <div id="carouselExampleCaptions" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
        <%for(int i = 0; i<this.Data.Count; i++) 
        {%>
            <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                <div class="container">
                <div class="img-box">
                    <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                        <picture>
						    <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                        </picture>
                    <%} %>
                </div>
                </div>
                <div class="carousel-caption d-none d-md-block">
                <h5><%=Data[i].Title %></h5>
                </div>
            </div>
        <%} %>
        </div>
        <div class="carousel_btn-container">
        <a class="carousel-control-prev" href="#carouselExampleCaptions" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#carouselExampleCaptions" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
        </div>
    </div>
</section>
<!-- end store section -->