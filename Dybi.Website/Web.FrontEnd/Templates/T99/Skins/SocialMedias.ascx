<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>


    <ul class="mxh">
        <%foreach(var item in this.Data) 
        {%> 
            <li>
                <a class="fb-nav" href="<%=item.Url%>" target="<%=item.Target%>" rel="nofollow" aria-label="<%=item.Title%>">
                <i class="fa fa-<%=item.Title%>" aria-hidden="true"></i>
                </a>
            </li>
        <%} %>
    </ul>