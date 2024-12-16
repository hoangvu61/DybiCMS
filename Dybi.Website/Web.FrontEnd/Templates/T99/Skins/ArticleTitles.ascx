<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>


<h6 title="<%=Title %>">
    <%=Title %>
</h6>

<ul class="system">
    <li><a href="<%=HREF.LinkComponent("AboutUs", Language["aboutus"].ConvertToUnSign(), true) %>"><%=Language["aboutus"] %></a></li>
    <%foreach(var item in this.Data) 
    {%>  
    <li>
        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
            <%=item.Title %>
        </a>
    </li>
    <%} %>
    <li>
        <a href="<%=HREF.LinkComponent("Contact", Language["contact"].ConvertToUnSign(), true) %>"><%=Language["contact"] %></a>
    </li>
</ul>