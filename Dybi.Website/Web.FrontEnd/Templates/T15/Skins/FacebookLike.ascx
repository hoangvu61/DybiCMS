<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookLike.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookLike" %>
<style>.fb_iframe_widget, .fb_iframe_widget span, .fb_iframe_widget span iframe[style] 
{
    width: 100% !important;
}</style>
<style>
.navbar-brand span,.detail-box,.info_section{color:<%= Skin.BodyFontColor%>}
.contact_link_box a, .contact_link_box a span, .contact_link_box a i{color:<%= Skin.BodyFontColor%>}
.custom_menu-btn button span{background:<%= Skin.BodyFontColor%>}
#myNav .overlay-content a{color:<%= Skin.BodyFontColor%>}
</style>

<div class="grid_4 center"> 
<%if(Title != "."){%>
<h6 class="left mod-center" style="margin-bottom:20px"><%=Title%></h6>
<%} else {%>
<hr />
<%}%>
<script id="facebook-jssdk" src="//connect.facebook.net/vi_VN/all.js#xfbml=1"></script>
<div class="fb-like<%=Box?"-box" : "" %>" data-href="<%= YourUrl%>" <%= Skin.Width == 0 ? "" : "data-width='"+Skin.Width+"'"%> data-show-faces="<%= ShowFaces%>" data-border-color="<%= BorderColor%>" data-stream="<%= Stream%>" data-header="<%= Header%>"></div>
    </div>