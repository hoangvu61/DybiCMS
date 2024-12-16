<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if (this.Data.Count > 1)
        {%>
    <h1 title="<%=Title%>"><strong> <%=Title%></strong></h1>
    <p class="jumbotron" style="padding:20px"><%=Category.Brief%></p>
    <%} %>
    <%foreach (var item in this.Data)
            {%>
    <div class="videoitem">
		<%=item.Embed %>
        <%=item.Brief %>
        <a href="<%=item.Url != null ? item.Url : HREF.LinkComponent("Album",item.Title.ConvertToUnSign(), true, "sMid", item.Id)%>"><%=item.Title %></a>
    </div>
            <%} %>	
    <div style="clear:both"></div>
