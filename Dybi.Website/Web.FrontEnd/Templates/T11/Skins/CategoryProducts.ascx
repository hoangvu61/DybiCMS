<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(Data.Count > 0){ %>
<!-- services section start -->
      <div class="services_section layout_padding">
         <div class="container">
               <div class="row">
                    <%foreach(var item in this.Data) 
                    {%>
                      <div class="col-2">
                         <a href="<%=HREF.LinkComponent("products",item.Title.ConvertToUnSign(), true, "scat", item.Id)%>">
                         <div class="category-item">
                             <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                                 <img class="services_img" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                 <div class="category-menu-name"><%=item.Title %></div>
                             <%} %>
                         </div>
                         </a>
                      </div>
                    <%} %>
               </div>
         </div>
      </div>
      <!-- services section end -->
<%} %>
