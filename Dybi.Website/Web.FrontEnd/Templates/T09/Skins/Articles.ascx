<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<%if(!string.IsNullOrEmpty(Skin.BodyBackground))
    { 
    if(Skin.BodyBackgroundFile != null){
    %>
    <style>
        .blog {background: url(<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>);}
    </style>
    <%}
    else { %>
    <style>
        .blog {background-color: <%=Skin.BodyBackground%>;background-image:none}
    </style>
    <%}
} %>

<%if(HREF.CurrentComponent != "home"){%>
<div class="back_re">
    <div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="title">
                <h2><%=Title %></h2>
            </div>
        </div>
    </div>
    </div>
</div>
<%} %>

<!-- blog -->
      <div class="blog">
         <div class="container">
            <div class="row">
               <div class="col-md-12">
                  <div class="titlepage">
                     <%if(HREF.CurrentComponent == "home"){%>
                     <h2>
                        <%=Title %>
                     </h2>
                     <%} %>
                     <p><%=Category.Brief %></p>
                  </div>
               </div>
            </div>
            <div class="row">
                <%foreach(var item in this.Data) 
        {%>  
                <div class="col-md-4">
                  <div class="blog_box">
                     <div class="blog_img">
                        <figure>
                            <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                                <picture>
						                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                    </picture>
                                <%} %>
                            </a>
                        </figure>
                     </div>
                     <div class="blog_room">
                        <h3>
                            <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>"> 
                                <%=item.Title%>
                            </a>
                        </h3>
                        <p><%=item.Brief %></p>
                     </div>
                  </div>
               </div>
        <%} %>
            </div>
         </div>
      </div>
      <!-- end blog -->
