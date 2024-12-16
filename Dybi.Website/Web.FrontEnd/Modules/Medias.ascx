<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>

<%=this.Skin.Width%>
<%=this.Skin.Height%>

<%=this.RederectComponent%>

<%foreach(var item in this.Data) 
  {%>
        <%=item.Id%>
        <%=item.CategoryId%>
        <%=item.Title%>
        <%=item.ImageName%>
        <%=item.Brief%> 
  <%} %>

                

