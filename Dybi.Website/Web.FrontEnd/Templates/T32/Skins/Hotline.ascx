<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Hotline.ascx.cs" Inherits="Web.FrontEnd.Modules.Hotline" %>

<div id="phone-vr" class="callnow">
    <div class="phone-vr-circle-fill"></div>
    <div class="phone-vr-img-circle">
    <a href="tel:<%=Component.Company.Branches[0].Phone %>" title="<%=Title %>" target="_blank"><img src="/Includes/img/phone_color.png" alt="<%=Title %>"/></a>
    </div>
</div>
<%if(UseZalo){ %>
<div id="zalo-vr" class="callnow">
    <div class="phone-vr-circle-fill"></div>
    <div class="phone-vr-img-circle">
    <a href="https://zalo.me/<%=Component.Company.Branches[0].Phone %>" title="<%=Title %>" target="_blank"><img src="/Includes/img/zalo_color.png" alt="<%=Title %>"/></a>
    </div>
</div>
<%} %>
<%if(!string.IsNullOrEmpty(FacebookUsername)){ %>
<div id="facebook-vr" class="callnow">
    <div class="phone-vr-circle-fill"></div>
    <div class="phone-vr-img-circle">
        <a href="https://m.me/<%=FacebookUsername %>" rel="nofollow" target="_blank"><img src="/Includes/img/facebook_color.png" alt="<%=Title %>"/></a>
    <a href="https://zalo.me/<%=Component.Company.Branches[0].Phone %>" title="<%=Title %>" target="_blank"></a>
    </div>
</div>
<%} %>