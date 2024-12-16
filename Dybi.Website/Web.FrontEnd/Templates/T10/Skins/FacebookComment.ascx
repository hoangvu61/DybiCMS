<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookComment.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookComment" %>
<div id="divBinhluan" class="share">
    <div id="fb-root"></div>
    <%if (!string.IsNullOrEmpty(this.FacebookAppId)){%>
    <script src="http://connect.facebook.net/en_US/all.js#xfbml=1&appId=<%=this.FacebookAppId %>"></script>
    <%} %>
    <fb:comments colorscheme="light" href="<%=YourUrl %>" num_posts="<%=NumberPost %>" width="<%=Skin.Width == 0 ? "100%" : Skin.Width + "px" %>"></fb:comments> 
</div>