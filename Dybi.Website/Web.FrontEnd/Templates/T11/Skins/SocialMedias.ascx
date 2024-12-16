<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div class="social_icon">
    <ul>
        <%foreach(var item in this.Data) 
        {%>
            <li><a href="<%=item.Url%>" target="<%=item.Target%>"><img src="/Templates/T11/images/<%=item.Title%>-icon.png"></a></li>
        <%} %>
    </ul>
</div>