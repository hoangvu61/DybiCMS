<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- slider section -->
<section class="slider_section">
    <%if(Skin.HeaderBackgroundFile != null){ %>
    <style>
        .hero_area{background-image: url('<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>.webp');}
    </style>
    <%} %>
    <div class="side-img">
        <%if(Skin.BodyBackgroundFile != null){ %>
        <picture>
			<source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>.webp" type="image/webp">
			<source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" type="image/jpeg"> 
            <img src="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" alt="<%=Component.Company.FullName %>"/>
        </picture>
        <%} else { %>
        <img src="/Templates/T30/images/side-img.png" alt="<%=Component.Company.FullName %>">
        <%} %>
    </div>
    <div class="container">
    <div class="row">
        <div class="col-md-6">
            <div class="detail-box">
                <div class="h2"><%=Component.Company.DisplayName%></div>
                <div class="h1"><%=Component.Company.NickName %></div>
            </div>
        </div>
        <div class="col-md-6">
        <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
            <div class="carousel-inner">
                <%for(int i = 0; i<this.Data.Count; i++) 
                {%> 
                    <div class="carousel-item <%= i== 0 ? "active" : "" %>">
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
                <%} %>
            </div>
            <div class="carousel_btn-container">
            <a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
            </div>
        </div>

        </div>
    </div>
    </div>
</section>
<!-- end slider section -->