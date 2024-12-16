<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>

<%=this.Skin.Width%>
<%=this.Skin.Height%>

<%foreach(var item in this.Data) 
  {%>
        <%=item.Id%>
        <%=item.CategoryId%>
        <%=item.Title%>
        <%=item.ImageName%>
        <%=item.Brief%> 
  <%} %>

                

