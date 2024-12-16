<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Hotline.ascx.cs" Inherits="Web.FrontEnd.Modules.Hotline" %>

<div id="zalo-vr" class="callnow">
    <div class="phone-vr-circle-fill"></div>
    <div class="phone-vr-img-circle">
    <a href="<%=ZaloLink %>" title="<%=Title %>" target="_blank"><img src="/Includes/img/zalo_color.png" alt="<%=Title %>"/></a>
    </div>
</div>

<%if(string.IsNullOrEmpty(FacebookUsername)){ %>
<div id="footer1">
    <!-- Footer -->
    <table cellpadding="0" cellspacing="0" style="<%=string.IsNullOrEmpty(this.Skin.HeaderBackground) ? "" : ";background-color:" + this.Skin.HeaderBackground %>">
    <tbody>
        <tr>
        <td>
            <a class="blink_me" href="tel:<%=Component.Company.Branches[0].Phone %>"><img src="/Includes/img/call.png" alt="CALL"> Gọi điện</a>
        </td>
        <td height="50">
            <a target="_blank" href="sms:<%=Component.Company.Branches[0].Phone %>"><img src="/Includes/img/fa.png" alt="SMS"> SMS</a>
        </td>
        <td>
            <a target="_blank" href="<%=ZaloLink %>"><img src="/Includes/img/zalo.png" alt="ZALO"> Zalo</a>
        </td>
        </tr>
    </tbody>
    </table>
</div>
<%} else { %>
<div class="giuseart-nav" style="<%=string.IsNullOrEmpty(this.Skin.HeaderBackground) ? "" : ";background-color:" + this.Skin.HeaderBackground %>"> 
    <ul> 
        <li class="cta-home">
            <a href="/"><i class="ticon-home"></i>Trang chủ</a>
        </li> 
        <li class="cta-zalo">
            <a href="<%=ZaloLink %>" rel="nofollow" target="_blank"><i class="ticon-zalo-circle2"></i>Zalo</a>
        </li> 
        <li class="cta-phone-mobile"><a href="tel:<%=Component.Company.Branches[0].Phone %>" rel="nofollow" class="button"><span class="phone_animation animation-shadow"> <i class="icon-phone-w" aria-hidden="true"></i></span> <span class="btn_phone_txt">Gọi Ngay</span></a></li> 
        <li class="cta-messenger"><a href="https://m.me/<%=FacebookUsername %>" rel="nofollow" target="_blank"><i class="ticon-messenger"></i>Messenger</a></li> 
        <li class="cta-order"> 
            <a href="<%=HREF.LinkComponent("Contact","lien-he",true)%>"><img src="/Includes/img/chiduong.png" alt="MAP"> Chỉ Đường </a>
        </li> 
        <li class="cta-top"><a href="#"><i class="ticon-top"></i></a></li> 
    </ul>
</div>
<%} %>