<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>

<h5 title="<%=Title %>"><%=Title %></h5>
<div class="mxh">
    <%foreach(var item in this.Data) 
    {%> 
        &nbsp; <a href="<%=item.Url%>" target="<%=item.Target%>" rel="nofollow" aria-label="<%=item.Title%>">
            <i class="fa fa-<%=item.Title%>" aria-hidden="true"></i>
        </a> 
    <%} %>
</div>