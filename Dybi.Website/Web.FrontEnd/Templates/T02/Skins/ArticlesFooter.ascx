<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>


<h3 title="<%=Category.Title %>">
    <%=Title %>
</h3>
<ul class="menu" style="color:#000">
    <%foreach(var item in this.Data) 
    {%>  
    <li>
        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
            <%=item.Title %>
        </a>
    </li>
    <%} %>
</ul>