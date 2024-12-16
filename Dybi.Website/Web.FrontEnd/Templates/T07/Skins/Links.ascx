<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div class="t_foot_2">
    <%=Title %>
</div>
<div class="m_foot_2">
    <ol class="ol2_foot_2">
        <%for(int i = 0; i < this.Data.Count; i++) 
        {%>  
            <li>
                <a href="<%=Data[i].Url%>" target="<%=Data[i].Target%>" title="<%=Data[i].Title%>"><%=Data[i].Title%></a>
            </li>
        <%} %>
    </ol>
</div>