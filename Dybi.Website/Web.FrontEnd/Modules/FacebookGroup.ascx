﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacebookGroup.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookGroup" %>

<script id="facebook-jssdk" src="//connect.facebook.net/vi_VN/all.js#xfbml=1"></script>

<div class="fb-group" data-href="<%=LinkGroup%>" data-width="<%=Width%>" data-show-metadata="<%=ShowMetadata%>"><blockquote cite="<%=LinkGroup%>" class="fb-xfbml-parse-ignore"><%=Title%></blockquote></div>