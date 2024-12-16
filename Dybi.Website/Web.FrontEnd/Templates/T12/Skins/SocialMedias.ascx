<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div class="mg-add3 mod-position2">
    <h4 class="color-1"><%=Title %></h4>
<ul class="inline-list">
    <%foreach(var item in this.Data) 
    {%> 
        <li>
            <a class="fa fa-<%=item.Title%>" href="<%=item.Url%>" target="<%=item.Target%>"></a>
        </li>
    <%} %>
</ul>
</div>