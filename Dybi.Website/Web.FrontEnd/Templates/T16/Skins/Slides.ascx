<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

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
                            <%for(int i = 0; i<this.Data.Count; i++) 
                            {%>  
                            <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                              <div class="carousel-caption relative">
                                 <div class="row d_flex">
                                    <div  class="col-md-8">
                                       <div class="board">
                                          <h1 style="<%=Skin.HeaderFontSize > 0 ? "font-size:" + Skin.HeaderFontSize + "pt" : ""%>">
                                              <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                                                <%=Data[i].Title%>
                                                  <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("SubTitle"))){ %>
                                                  <br /> <%=Data[i].GetAttribute("SubTitle") %>
                                                  <%} %>
                                                </a>
                                          </h1>
                                          <p>
                                              <%=Data[i].Brief.Replace("\n","<br />") %>
                                          </p>
                                          <a class="read_more" href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>">Xem thêm</a>
                                       </div>
                                    </div>
                                    <div class="col-md-4">
                                       <div class="every_img">
                                           <figure>
                                           <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                                                <picture>
						                          <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						                          <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                                    <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                                </picture>
                                            <%} %>
                                           </figure> 
                                       </div>
                                    </div>
                                 </div>
                              </div>
                           </div>
                        <%} %>
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
