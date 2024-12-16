<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleCommentFacebook.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleCommentFacebook" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library.Web"%>

<%=dto.CONTENT != null ? dto.CONTENT.PageArticleLink() : dto.CONTENT%>
    
<VIT:Position runat="server" ID="psComment"></VIT:Position>