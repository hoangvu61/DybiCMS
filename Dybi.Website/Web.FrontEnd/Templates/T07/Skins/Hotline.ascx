<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Hotline.ascx.cs" Inherits="Web.FrontEnd.Modules.Hotline" %>

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
        <li class="cta-order"> <a href="<%=HREF.LinkComponent("Cart","gio-hang", true)%>" rel="nofollow" title="Giỏ hàng"> <i class="ticon-cart" aria-hidden="true" title="Giỏ hàng"></i> Giỏ hàng </a> </li> 
        <li class="cta-top"><a href="#" aria-label="Lên đầu trang"><i class="ticon-top"></i></a></li> 
    </ul>
</div>