<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tags.ascx.cs" Inherits="Web.FrontEnd.Modules.Tags" %>

<%=this.Skin.Width%>
<%=this.Skin.Height%>

<%=this.RederectComponent%>
<%=this.RederectSendKey%>

<%foreach(var item in this.Data) 
  {%>
        <%=item.Is%>
        <%=item.CategoryId%>
        <%=item.Title%>
        <%=item.ImageName%>
        <%=item.Brief%> 
        <%=item.URL%> 
  <%} %>

                

