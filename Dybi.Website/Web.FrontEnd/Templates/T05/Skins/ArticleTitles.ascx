<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- services section start -->
    <h6 class="about_text"><%=Title %></h6>
    <ul>
    <%foreach(var item in this.Data) {%>
        <li>
            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>">
                <%=item.Title %>
            </a>
        </li>
    <%} %>
    </ul>
