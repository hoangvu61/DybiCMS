<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- customer section start -->
<div class="customer_section layout_padding">
    <div class="container">
    <div class="row">
        <div class="col-sm-12">
            <h1 class="customer_taital">
                <%=Title %>
            </h1>
        </div>
    </div>
    <div id="main_slider" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%> 
            <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                <div class="client_section_2">
                <div class="client_main">
                    <div class="client_left">
                        <div class="client_img">
                            <%if(!string.IsNullOrEmpty(this.Data[i].ImageName)){%>
                                <picture>
						            <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp" type="image/webp">
						            <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" type="image/jpeg"> 
                                    <img src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" alt="<%=this.Data[i].Title %>"/>
                                </picture>
					        <%}%>
                        </div>
                    </div>
                    <div class="client_right">
                        <h3 class="name_text"><%=this.Data[i].Title %></h3>
                        <p class="dolor_text"><%=this.Data[i].Brief %></p>
                    </div>
                </div>
                </div>
            </div>
            <%} %>
        </div>
        <a class="carousel-control-prev" href="#main_slider" role="button" data-slide="prev">
        <i class="fa fa-angle-left"></i>
        </a>
        <a class="carousel-control-next" href="#main_slider" role="button" data-slide="next">
        <i class="fa fa-angle-right"></i>
        </a>
    </div>
    </div>
</div>
<!-- customer section end -->