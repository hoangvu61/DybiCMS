<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
    <!-- Indicators -->
    <ol class="carousel-indicators">
        <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
        <%for (int i = 0; i < Data.Count; i++)
            { %>
        <li data-target="#carousel-example-generic" data-slide-to="<%=i + 1 %>"></li>
        <%} %>
    </ol>

    <!-- Wrapper for slides -->
    <div class="carousel-inner">
        <%if (Config.WebImage != null)
            { %>
        <div class="carousel-item active">
            <div class="carousel-overlay">
                <div class="text-center wow animate__animated animate__fadeInUp animated" data-wow-duration="1.2s"
                    data-wow-delay="0.2s" style="width: 100%">
                    <div class="h1">
                        <%=Language["Welcome"] %> <strong><%=Component.Company.NickName %></strong>
                    </div>
                    <span class="h2"><%=Component.Company.Slogan %></span>
                </div>
            </div>
            <%if (Config.WebImage.FileExtension == ".webp")
                { %>
            <img src="<%= HREF.DomainStore +  Config.WebImage.FullPath%>" alt="<%=Component.Company.DisplayName %>" class="d-block w-100" />
            <%}
                else
                { %>
            <picture>
                <source srcset="<%=HREF.DomainStore + Config.WebImage.FullPath%>.webp" type="image/webp">
                <source srcset="<%=HREF.DomainStore + Config.WebImage.FullPath%>" type="image/jpeg">
                <img src="<%=HREF.DomainStore + Config.WebImage.FullPath%>" class="d-block w-100" alt="<%=Component.Company.FullName %>" />
            </picture>
            <%} %>
        </div>
        <%} %>
        <%for (int i = 0; i < this.Data.Where(e => e.Image != null).Count(); i++)
            {%>
        <div class="carousel-item <%=(Config.WebImage == null && i == 0) ? "active" : "" %>">
            <%if (Data[i].Title != ".")
                {%>
            <div class="carousel-overlay">
                <div class="text-center" style="width: 100%">
                    <div class="h1" title="<%=Data[i].Title %>">
                        <%=Data[i].Title %>
                    </div>
                </div>
            </div>
            <%} %>
            <%if (Data[i].Image.FileExtension == ".webp")
                { %>
            <img src="<%= HREF.DomainStore +  Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>" class="d-block w-100" />
            <%}
                else
                { %>
            <picture>
                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg">
                <img src="<%= HREF.DomainStore +  Data[i].Image.FullPath%>" class="d-block w-100" alt="<%=Data[i].Title %>" />
            </picture>
            <%} %>
        </div>
        <%} %>
    </div>
</div>

