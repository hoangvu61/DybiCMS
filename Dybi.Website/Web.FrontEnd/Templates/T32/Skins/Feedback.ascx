<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- client section -->
<section class="client_section layout_padding">
    <div class="container">
        <div class="heading_container heading_center">
            <h2>
                <%=Title %>
            </h2>
        </div>
    <div class="row">
    <div class="col-md-9 mx-auto">
        <div id="customCarousel2" class="carousel slide" data-ride="carousel">
        <div class="row">
            <div class="col-md-11">
            <div class="carousel-inner">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                <div class="box">
                    <div class="client_id">
                    <div class="img-box">
                        <%if(!string.IsNullOrEmpty(this.Data[i].ImageName)){%>
                            <picture>
						        <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" alt="<%=this.Data[i].Title %>"/>
                            </picture>
					    <%}%>
                    </div>
                    <h5>
                        <%=this.Data[i].Title %>
                    </h5>
                    </div>
                    <div class="detail-box">
                    <p>
                        <%=this.Data[i].Brief %>
                    </p>
                    </div>
                </div>
                </div>
            <%} %>
            </div>
            </div>
            <div class="col-md-1">
            <ol class="carousel-indicators">
                <%for(int i = 0; i < this.Data.Count; i++) 
                {%>
                    <li data-target="#customCarousel2" data-slide-to="<%=i %>" class="<%= i== 0 ? "active" : "" %>"></li>
                <%} %>
            </ol>
            </div>
        </div>
        </div>
    </div>
    </div>
</div>
</section>
<!-- end client section -->