<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div class="social_box">
    <h6 title="<%=Title %>"><%=Title %></h6>

    <%foreach(var item in this.Data) 
    {%> 
        <a href="<%=item.Url%>" target="<%=item.Target%>" aria-label="<%=item.Title%>">
        <i class="fa fa-<%=item.Title%>" aria-hidden="true"></i>
        </a>
    <%} %>
</div>