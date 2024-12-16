<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<ul class="social-iconsv2 agileinfo pt-3" style="margin-bottom:20px">
    <%foreach(var item in this.Data) 
    {%> 
    <li>
        <a class="fb-nav" href="<%=item.Url%>" target="<%=item.Target%>">
        <i class="fab fa fa-<%=item.Title%>"></i>
        </a>
    </li>
    <%} %>
</ul>