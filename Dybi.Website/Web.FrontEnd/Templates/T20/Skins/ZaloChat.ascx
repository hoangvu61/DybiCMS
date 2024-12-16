<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ZaloChat.ascx.cs" Inherits="Web.FrontEnd.Modules.ZaloChat" %>

<%--<script src="https://sp.zalo.me/plugins/sdk.js"></script>

<div class="zalo-chat-widget" data-oaid="<%=OfficialAccountId %>" data-welcome-message="<%=Title %>" data-autopopup="<%= AutoPopup ? 1 : 0 %> " data-width="<%= Width%>" data-height="<%= Height%>"></div>--%>
<div style="position: fixed; bottom: 20px; right: 20px; z-index: 999;">
    <a href="<%=GetValueParam<string>("GroupLink")%>" title="<%=Title %>"><img src="/Templates/T03/images/zalo_sharelogo.png" alt="<%=Title %>" style="height:60px"/></a>
</div>
