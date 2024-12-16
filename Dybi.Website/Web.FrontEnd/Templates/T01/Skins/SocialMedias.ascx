<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div class="mxh">
    <h6 title="<%=Title %>" class="mb-3"><%=Title %></h6>
    <%foreach(var item in this.Data) 
    {%> 
        &nbsp; <a href="<%=item.Url%>" target="<%=item.Target%>">
            <i class="fa fa-<%=item.Title%>" aria-hidden="true"></i>
        </a> 
    <%} %>
</div>