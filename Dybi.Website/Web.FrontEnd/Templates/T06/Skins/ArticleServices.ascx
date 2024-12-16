<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>


<%foreach(var item in this.Data) 
{%>  
<div class="agileits-abt-grids mb-5">
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
        <h3><a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>"><%=item.Title%></a></h3>
    </div>
    <p class="lead about-text-wthree">
        <%=item.Brief.Trim().Length > 125 ? item.Brief.Trim().Substring(0, 125) + "..." : item.Brief.Trim() %>
    </p>
</div>    
<%} %>