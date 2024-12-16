<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>

<div class="mxh">
    <h6 title="<%=Title %>"><%=Title %></h6>
    <%foreach(var item in this.Data) 
    {%> 
        &nbsp; <a href="<%=item.Url%>" target="<%=item.Target%>" rel="nofollow" aria-label="<%=item.Title%>">
            <i class="fa fa-<%=item.Title%>" aria-hidden="true"></i>
        </a> 
    <%} %>
</div>