<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Event.ascx.cs" Inherits="Web.FrontEnd.Modules.Event" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<%= this.DisplayTitle%>
<%= this.DisplayDate%>
<%= this.DisplayImage%>

Id: <%=dto.Id%> <br/>
Title: <%=dto.Title%> <br/>
Brief: <%=dto.Brief%> <br/>
Contents: <%=dto.Contents%> <br/>
ImageName: <%=dto.ImageName%> <br/>
Time: <%=dto.Time%> <br/>