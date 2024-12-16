<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Hotline.ascx.cs" Inherits="Web.FrontEnd.Modules.Hotline" %>

<style>
#footer1 {z-index: 1000;position: fixed;bottom: 0;width: 100%;left: 0;display: none;}
#footer1 table {width: 100%;text-align: center;margin: auto;background: #cc0000;border: 3px solid #e8e8e8;}
#footer1 table td {border: none;height: 45px;}
#footer1 a {color: #fff;font-size: 14px;}
#footer1 img {width: 30%;max-width: 35px;vertical-align: middle;}
.callnow{position: fixed; bottom: 20px; right: 20px; z-index: 999;}
.callnow img{height:50px}
@media only screen and (max-width: 812px) {
    #footer1 {display: block;}
    .callnow{display: none;}
}
</style>

<div class="callnow">
    <a href="<%=ZaloLink %>" title="<%=Title %>" target="_blank"><img src="/Includes/img/zalo_color.png" alt="<%=Title %>"/></a>
</div>
<div id="footer1">
    <!-- Footer -->
    <table cellpadding="0" cellspacing="0">
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