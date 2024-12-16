<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductAttributes.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductAttributes" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>


<!-- category section -->
<%if(Skin.HeaderFontSize > 0){ %>
    <style>
        .cat_section .detail-box h2{font-size:<%= Skin.HeaderFontSize%>px}
    </style>
<%}%>
<%if(Skin.BodyFontSize > 0){ %>
    <style>
        .cat_section .detail-box a{font-size:<%= Skin.BodyFontSize%>px}
    </style>
<%}%>
<style>
.cat_section .detail-box h2{color:<%= Skin.BodyFontColor%>}
.cat_section .detail-box a{color:<%= Skin.BodyFontColor%> !important}
</style>

<section class="cat_section">
    <div class="container-fluid">
        <div class="row">
        <%foreach(var item in Data) 
        {%>
        <div class="col-12 col-sm-6 col-md-<%=12/Data.Count %> mx-auto wow animate__animated animate__zoomIn animated" data-wow-duration="0.8s" data-wow-delay="0.3s">
            <a href="<%=HREF.LinkComponent(ComponentProducts, Title.ConvertToUnSign(), true, SettingsManager.Constants.SendAttributeId, SourceId, SettingsManager.Constants.SendAttributeValue, item.Key) %>" title="<%=item.Value %>">
                <div class="box">
                    <img src="/Templates/T13/images/<%=item.Key %>.jpg" alt="<%=item.Value %>">
                </div>
                <h2 title="<%=item.Value %>"><%=item.Value %></h2>
            </a>
        </div>
        <%} %>
        </div>
    </div>
</section>