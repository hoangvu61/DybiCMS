<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- client section -->
<section class="client_section layout_padding">
    <div class="container">
        <div class="heading_container">
        <h2>
            <%=Title %>
        </h2>
        </div>
        <div id="carouselExample3Controls" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
            <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                <div class="box">
                    <div class="img_container">
                    <div class="img-box">
                        <div class="img_box-inner">
                            <%if(!string.IsNullOrEmpty(this.Data[i].ImageName)){%>
                                <picture>
						            <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp" type="image/webp">
						            <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" type="image/jpeg"> 
                                    <img src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" alt="<%=this.Data[i].Title %>"/>
                                </picture>
					        <%}%>
                        </div>
                    </div>
                    </div>
                    <div class="detail-box">
                    <h5>
                        <%=this.Data[i].Title %>
                    </h5>
                    <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("Job"))){ %>   
                        <span>
                            <%=Data[i].GetAttribute("Job") %>
                        </span>
                    <%} %>
                    <p>
                        <%=this.Data[i].Brief %>
                    </p>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
        <a class="carousel-control-prev" href="#carouselExample3Controls" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#carouselExample3Controls" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
        </div>
    </div>
</section>
<!-- end client section -->