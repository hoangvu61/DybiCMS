<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="agileits-services py-md-5 py-4" id="services">
    <%if(Skin.BodyBackgroundFile != null){ %>
        <style>
            #services{background: url('<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>')no-repeat top; background-size:cover}
        </style>
    <%} else if(!string.IsNullOrEmpty(this.Skin.BodyBackground)){%>
        <style>
            #services{background: <%=this.Skin.BodyBackground%> !important}
        </style>
    <%}%>

    <div class="container py-lg-5">
        <div class="text-center wthree-title pb-sm-5 pb-3">
            <%if(HREF.CurrentComponent == "home"){ %>
            <h2 class="w3l-sub pb-5" title="<%=Title %>">
                <%=Title %>
            </h2>
            <%} else { %>
            <h1 class="w3l-sub pb-5" title="<%=Title %>">
                <%=Title %>
            </h1>
            <%} %>
            <p class="mx-auto"><%=Category.Brief %></p>
        </div>
        <div class="agileits-services-row row no-gutters">
            <%foreach(var item in this.Data) 
            {%>
            <div class="col-lg-4 col-md-6 mb-3">
               <div class="agileits-abt-grids p-3">
                    <div class="d-flex" style="align-items:center">
                        <span>
                            <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>">
                                <picture>
					                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                    <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                </picture>
                            </a>
                            <%} %>
                        </span>
                         <%if(HREF.CurrentComponent == "home"){ %>
                            <h3 title="<%=item.Title %>">
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>"><%=item.Title%></a>
                            </h3>
                        <%} else {%>
                            <h2 title="<%=item.Title %>">
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>"><%=item.Title%></a>
                            </h2>
                        <%} %>
                    </div>
                    <p class="lead about-text-wthree">
                        <%=item.Brief.Trim().Length > 125 ? item.Brief.Trim().Substring(0, 125) + "..." : item.Brief.Trim() %>
                    </p>
                </div>  
            </div>
            <%} %>
        </div>
    </div>
</div>

<%if(HREF.CurrentComponent == "article"){ %>
<script>
    $(document).ready(function () {
        $("#mnu<%=Category.Id%>").addClass("active");
    });
</script>
<%} %>
