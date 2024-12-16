<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- customers -->
      <div class="customers">
         <div class="container">
            <div class="row">
               <div class="col-sm-12">
                  <div class="titlepage text_align_left">
                     <h2><%=Title %></h2>
                  </div>
               </div>
            </div>
            <!-- start slider section -->
            <div class="row">
               <div class="col-md-12">
                  <div id="myCarousel" class="carousel slide customers_banner" data-ride="carousel">
                     <ol class="carousel-indicators">
                         <%for(int i = 0; i < this.Data.Count; i++) 
                        {%>
                            <li data-target="#myCarousel" data-slide-to="<%=i %>" class="<%= i== 0 ? "active" : "" %>"></li>
                        <%} %>
                     </ol>
                     <div class="carousel-inner">
                         <%for(int i = 0; i < this.Data.Count; i++) 
                         {%>           
                         <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                           <div class="container-fluid">
                              <div class="carousel-caption relative">
                                 <div class="row d_flex">
                                    <div class="col-md-3">
                                       <div class="por_pic">
                                            <%if(!string.IsNullOrEmpty(this.Data[i].ImageName)){%>
                                            <i>
                                                <picture>
						                            <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp" type="image/webp">
						                            <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" type="image/jpeg"> 
                                                    <img src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" alt="<%=this.Data[i].Title %>"/>
                                                </picture>
                                            </i>
					                        <%}%>
                                       </div>
                                    </div>
                                    <div class="col-md-9">
                                       <div class="test_box text_align_left">
                                          <h4><%=this.Data[i].Title %>  <img class="img_responsive" src="/Templates/T16/images/te2.png" alt="#"/></h4>
                                          <p><%=this.Data[i].Brief %></p>
                                       </div>
                                    </div>
                                 </div>
                              </div>
                           </div>
                        </div>      
            <%} %>
                     </div>
                     <a class="carousel-control-prev" href="#myCarousel" role="button" data-slide="prev">
                     <i class="fa fa-angle-left" aria-hidden="true"></i>
                     <span class="sr-only">Previous</span>
                     </a>
                     <a class="carousel-control-next" href="#myCarousel" role="button" data-slide="next">
                     <i class="fa fa-angle-right" aria-hidden="true"></i>
                     <span class="sr-only">Next</span>
                     </a>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <!-- end customers -->