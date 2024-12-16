<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>


<!-- services section start -->
      <div class="services_section layout_padding">
         <div class="container">
            <div class="services_section_2">
               <div class="row">
                   <%foreach(var item in this.Data) 
                    {%>
                      <div class="col-md-4" style="margin-bottom:30px">
                         <div>
                             <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                             <a href="<%=HREF.LinkComponent("Article",item.Title.ConvertToUnSign(), true, "sart", item.Id)%>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="services_img" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture> 
                                 </a>
                             <%} %>
                         </div>
                             <div class="btn_main"><a href="<%=HREF.LinkComponent("Article",item.Title.ConvertToUnSign(), true, "sart", item.Id)%>"><%=item.Title %></a></div>
                      </div>
                    <%} %>
                   
               </div>
            </div>
         </div>
      </div>
      <!-- services section end -->
