<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- slider section -->
<section class="slider_section position-relative">
    <div class="container-fluid">
        <div class="row">
        <div class="col-md-6 mx-auto detail_container wow animate__animated animate__fadeInLeft animated" data-wow-duration="0.8s" data-wow-delay="0.3s">
            <div class="detail-box">
                <h1 style='<%= Skin.HeaderFontSize > 0 ? "font-size:" + Skin.HeaderFontSize + "px" : ""%>'>
                    <%=Title %>
                </h1>
                <p>
                    <%if(!string.IsNullOrEmpty(Category.Brief)){ %>
                        <%=Category.Brief.Replace("\n","<br />") %>
                    <%} %>
                </p>
            </div>
        </div>
        <div class="col-md-4 px-0 wow animate__animated animate__fadeInRight animated" data-wow-duration="0.8s" data-wow-delay="0.3s">
            <div id="customCarousel1" class="carousel  slide" data-ride="carousel">
            <div class="carousel-inner">
                <%for(int i = 0; i < this.Data.Count; i++) 
                {%>
                    <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                        <div class="slider_img_box">
                            <picture>
		                        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
		                        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                            </picture>     
                        </div>
                    </div>
                <%} %>
            </div>
            </div>
        </div>
        </div>
        <div class="carousel_btn-box">
        <a class="carousel-control-prev" href="#customCarousel1" role="button" data-slide="prev">
            <i class="fa fa-angle-left" aria-hidden="true"></i>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#customCarousel1" role="button" data-slide="next">
            <i class="fa fa-angle-right" aria-hidden="true"></i>
            <span class="sr-only">Next</span>
        </a>
        </div>
    </div>
</section>
<!-- end slider section -->