<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- info section -->
<div class="container">
    <div class="icons mt-3 text-center">
        <ul class="d-flex justify-content-center pb-3 social-icons">
            <%foreach(var item in this.Data) 
            {%> 
            <a class="fa fa-<%=item.Title%>" href=" target="<%=item.Target%>"></a>
            <li class="mx-3">
                <a class="fa fa-<%=item.Title%>" href="<%=<%=item.Url%>" target="<%=item.Target%>"></a>
            </li>
            <%} %>
        </ul>
    </div>
</div>
<!-- end info_section -->