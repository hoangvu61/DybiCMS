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
        <div class="carousel-wrap ">
          <div class="owl-carousel">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>                 
            <div class="item">
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
                  <h5>
                    <%=this.Data[i].Title %>
                    <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("Job"))){ %>   
                    <br />         
                    <span>
                        <%=Data[i].GetAttribute("Job") %>
                    </span>
                    <%} %>
                  </h5>
                  <i class="fa fa-quote-left" aria-hidden="true"></i>
                  <p>
                    <%=this.Data[i].Brief %>
                  </p>
                </div>
              </div>
            </div>
            <%} %>
          </div>
        </div>
      </div>
    </section>
  <!-- end client section -->
