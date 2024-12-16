<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Hotline.ascx.cs" Inherits="Web.FrontEnd.Modules.Hotline" %>

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