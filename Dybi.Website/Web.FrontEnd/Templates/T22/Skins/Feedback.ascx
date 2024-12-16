<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

 <!-- section testimonial start -->
    <div class="testimonial_section layout_padding">
    	<div class="container">

<div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
  <ol class="carousel-indicators"> 
    <%for(int i = 0; i < this.Data.Count; i++) 
    {%>
        <li data-target="#carouselExampleIndicators" data-slide-to="<%=i %>" class="<%= i== 0 ? "active" : "" %>"></li>
    <%} %>
  </ol>
  <div class="carousel-inner">

    
    <%for(int i = 0; i < this.Data.Count; i++) 
    {%>
    <div class="carousel-item <%=i==0?"active":"" %>">
      <h1 class="Testimonial_text"><%=Title %></h1>
    		<div class="img_3">
            <%if(!string.IsNullOrEmpty(this.Data[i].ImageName)){%>
                <picture>
					<source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp" type="image/webp">
					<source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" type="image/jpeg"> 
                    <img src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" alt="<%=this.Data[i].Title %>"/>
                </picture>
			<%}%>
    		</div>
    		<h2 class="denmark_text"><%=this.Data[i].Title %></h2>
    		<div><img src="/Templates/T21/images/img-4.png"></div>
    		<p class="lorem_ipsum_text"><%=this.Data[i].Brief %></p>
    		<div class="right_img_5"><img src="/Templates/T21/images/img-5.png"></div>
    </div>    
            <%} %>
  </div>
</div>
    	</div>
    </div>
    <!-- section testimonal end -->