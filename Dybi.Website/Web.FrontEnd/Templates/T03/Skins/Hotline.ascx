<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Hotline.ascx.cs" Inherits="Web.FrontEnd.Modules.Hotline" %>

<div id="zalo-vr" class="callnow2">
    <div class="phone-vr-circle-fill"></div>
    <div class="phone-vr-img-circle">
    <a href="<%=ZaloLink %>" title="<%=Title %>" target="_blank"><img src="/Includes/img/zalo_color.png" alt="<%=Title %>"/></a>
    </div>
</div>
<%if(!string.IsNullOrEmpty(FacebookUsername)){ %>
<div id="zalo-vr" class="callnow">
    <div class="phone-vr-circle-fill"></div>
    <div class="phone-vr-img-circle">
    <a href="https://m.me/<%=FacebookUsername %>" title="<%=Title %>" target="_blank">
        <img src="/Includes/img/messenger_color.png" alt="<%=Title %>"/>
    </a>
    </div>
</div>
<%} %>