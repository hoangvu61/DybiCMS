<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Category.ascx.cs" Inherits="Web.FrontEnd.Modules.Category" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider" %>

<!-- blog section -->
<section class="article_section py-5">
    <div class="container">
        <div class="row">
            <div class="col-12 col-md-7 mx-auto wow animate__animated animate__fadeInLeft animated">
                <a href="<%=HREF.LinkComponent(Data.ComponentList, Data.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Data.Id)%>">
                    <h2 title="<%=Data.Title %>"><%=Title %></h2>
                </a>
                <p class="text-justify"><%=Data.Brief %></p>
                <a class="btn btn-secondary mt-3 wow animate__animated animate__zoomIn animated" href="<%=HREF.LinkComponent(Data.ComponentList, Data.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Data.Id)%>"><%=Language["ReadMore"] %></a>
            </div>
            <%if (Data.Image != null) { %>
            <div class="col-12 col-md-5 wow animate__animated animate__fadeInRight animated">
                <a href="<%=HREF.LinkComponent(Data.ComponentList, Data.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Data.Id)%>">
                <picture>
                    <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
                    <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                    <img src="<%=HREF.DomainStore + Data.Image.FullPath%>" alt="<%=Title %>"/>
                </picture>
                </a>
            </div>
            <%} %>
        </div>
    </div>
</section>