<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- banner section start -->
      <div class="banner_section layout_padding">
         <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
            <div class="carousel-inner">
                <%for(int i = 0; i<this.Data.Count; i++) 
                {%>  
               <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                  <div class="container">
                     <div class="row">
                        <div class="col-sm-6">
                           <h1 class="banner_taital" style="<%=Skin.HeaderFontSize > 0 ? "font-size:" + Skin.HeaderFontSize + "px" : ""%>">
                                <%=Data[i].Title%>
                           </h1>
                           <p class="banner_text">
                               <%=Data[i].Brief.Replace("\n","<br />") %>
                           </p>
                           <div class="read_bt"><a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>">Chi tiết</a></div>
                        </div>
                        <div class="col-sm-6">
                           <div class="banner_img">
                               <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                                    <picture>
						                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                    </picture>
                                <%} %>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
                <%} %>
            </div>
         </div>
      </div>
      <!-- banner section end -->

<!-- top -->
      <div class="full_bg">
         <div class="slider_main">
            <div class="container">
               <div class="row">
                  <div class="col-md-12">
                     <!-- carousel code -->
                     <div id="banner1" class="carousel slide">
                        <ol class="carousel-indicators">
                            <%for(int i = 0; i < this.Data.Count; i++) 
                            {%>
                                <li data-target="#banner1" data-slide-to="<%=i %>" class="<%= i== 0 ? "active" : "" %>"></li>
                            <%} %>
                        </ol>
                        <div class="carousel-inner">
                            
                        </div>
                        <!-- controls -->
                        <a class="carousel-control-prev" href="#banner1" role="button" data-slide="prev">
                        <i class="fa fa-angle-left" aria-hidden="true"></i>
                        <span class="sr-only">Previous</span>
                        </a>
                        <a class="carousel-control-next" href="#banner1" role="button" data-slide="next">
                        <i class="fa fa-angle-right" aria-hidden="true"></i>
                        <span class="sr-only">Next</span>
                        </a>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
<!-- end banner -->
