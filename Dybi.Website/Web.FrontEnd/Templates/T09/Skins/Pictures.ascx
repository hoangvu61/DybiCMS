<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>


<%{ Data = Data.Where(e => e.Type == "IMG").ToList(); }%>
<%if(Data.Count > 0){ %>

<!-- gallery -->
      <div  class="gallery">
         <div class="container">
            <div class="row">
               <div class="col-md-12">
                  <div class="titlepage">
                     <h2><%=Title %></h2>
                  </div>
               </div>
            </div>
            <div class="row">
                <%for (int i = 0; i < Data.Count; i++)
			    {%> 
                    <div class="col-md-3 col-sm-6">
                      <div class="gallery_img">
                         <figure>
                             <img src="<%=HREF.DomainStore + Data[i].Image.FullPath %>.webp"/>
                         </figure>
                      </div>
                   </div>
			    <%} %>
            </div>
         </div>
      </div>
      <!-- end gallery -->
			
<%} %>