﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookLike.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookLike" %>

<div class="fb-box">
    <script id="facebook-jssdk" src="//connect.facebook.net/vi_VN/all.js#xfbml=1"></script>
    <div class="fb-like<%=Box?"-box" : "" %>" data-href="<%= YourUrl%>" <%= Skin.Width == 0 ? "" : "data-width='"+Skin.Width+"'"%> data-show-faces="<%= ShowFaces%>" data-border-color="<%= BorderColor%>" data-stream="<%= Stream%>" data-header="<%= Header%>"></div>
</div>