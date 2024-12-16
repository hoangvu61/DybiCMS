<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div class="info_social">
    <%foreach(var item in this.Data) 
    {%> 
        <a href="<%=item.Url%>" target="<%=item.Target%>">
        <i class="fa fa-<%=item.Title%>" aria-hidden="true"></i>
        </a>
    <%} %>
</div>