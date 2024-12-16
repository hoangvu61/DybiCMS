<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<h6>
    <%=Title %>
</h6>
<ul>
    <%for(int i = 0; i < this.Data.Count; i++) 
    {%>  
        <li>
            <a href="<%=Data[i].Url%>" target="<%=Data[i].Target%>" title="<%=Data[i].Title%>"><%=Data[i].Title%></a>
        </li>
    <%} %>
</ul>