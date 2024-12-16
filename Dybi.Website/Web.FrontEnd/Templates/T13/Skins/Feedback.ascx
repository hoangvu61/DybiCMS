<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

  <!-- client section -->
<section class="client_section layout_padding-bottom">
    <div class="container">
        <div class="heading_container heading_center wow animate__ animate__fadeInUp animated" data-wow-duration="1.2s" data-wow-delay="0.2s">
        <h2>
            <%=Title %>
        </h2>
        <p>
            <%=Category.Brief %>
        </p>
        </div>
    </div>
    <div class="container px-0">
        <div id="customCarousel2" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner wow animate__ animate__fadeInUp animated" data-wow-duration="1.2s" data-wow-delay="0.2s">
        <%for(int i = 0; i < this.Data.Count; i++) 
        {%> 
            <div class="carousel-item <%= i== 0 ? "active" : "" %>">
            <div class="container">
                <div class="row">
                    <div class="col-lg-10" style="margin-left:auto;margin-right:auto">
                        <div class="box">
                        <div class="img-box">
				        <%if(!string.IsNullOrEmpty(this.Data[i].ImageName)){%>
                            <picture>
						        <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" alt="<%=this.Data[i].Title %>"/>
                            </picture>
					        <%}%>
                        </div>
                        <div class="detail-box">
                            <div class="client_info">
                            <div class="client_name">
                                <h5> 
                                    <%=this.Data[i].Title %>
                                </h5>
                                <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("Job"))){ %>   
                                    <h6>
                                        <%=Data[i].GetAttribute("Job") %>
                                    </h6>
                                <%} %>
                            </div>
                            <i class="fa fa-quote-left" aria-hidden="true"></i>
                            </div>
                            <p>
                                <%=this.Data[i].Brief %>
                            </p>
                        </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        <%} %>
        </div>
        <div class="carousel_btn-box">
            <a class="carousel-control-prev wow animate__animated animate__fadeInLeft animated" data-wow-duration="0.8s" data-wow-delay="0.3s" href="#customCarousel2" role="button" data-slide="prev">
                <i class="fa fa-angle-left" aria-hidden="true"></i>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next wow animate__animated animate__fadeInRight animated" data-wow-duration="0.8s" data-wow-delay="0.3s" href="#customCarousel2" role="button" data-slide="next">
                <i class="fa fa-angle-right" aria-hidden="true"></i>
                <span class="sr-only">Next</span>
            </a>
        </div>
        </div>
    </div>
</section>
<!-- end client section -->
