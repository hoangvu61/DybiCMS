<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Hotline.ascx.cs" Inherits="Web.FrontEnd.Modules.Hotline" %>

<div id="widget">
    <a href="/" title="Về trang chủ" class="home"><i class="glyphicon glyphicon-home"></i></a>
    <a href="<%=HREF.LinkComponent("Cart","gio-hang", true) %>" title="Giỏ hàng" class="popup">
        <img src="/templates/t02/img/boton-carrito-compras.gif"/ alt="giảo hàng"></a>
    <%--<a href="<%=HREF.LinkComponent("OrderInfo") %>" title="Tra cứu đơn hàng" class="popup">
        <img src="/templates/t02/img/find.png"/></a>
    <a href="<%=HREF.LinkComponent("Payment") %>" title="Thanh toán online" class="popup">
        <img src="/templates/t02/img/card-in-use.png"/></a>
    <a href="<%=HREF.LinkComponent("Articles") %>" title="Tin tức" class="popup">
        <img src="/templates/t02/img/news.png"/></a>--%>
        <a href="<%=ZaloLink %>" title="Zalo" class="popup">
            <img src="/Includes/img/zalo_color.png" alt="Zalo"/>
        </a>
    <%if(!string.IsNullOrEmpty(FacebookUsername)){ %>
    <a href="https://m.me/<%=FacebookUsername %>" title="Messenger" class="popup">
        <img src="/Includes/img/messenger_color.png" alt="Messenger"/>
    </a>
    <%} %>
</div>

