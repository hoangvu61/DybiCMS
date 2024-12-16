<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartCreate.ascx.cs" Inherits="Web.FrontEnd.Modules.CartCreate" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<div class="send_info">
    <VIT:Form ID="frmMain" runat="server">
        <div class="w3-row-padding">
            <%{txtHoTen.Attributes.Add("placeholder", "Tên người nhận");} %>
            <asp:TextBox ID="txtHoTen" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
        </div>
        <div class="w3-row-padding">
            <%{txtDienThoai.Attributes.Add("placeholder", Language["Phone"]);} %>
            <asp:TextBox ID="txtDienThoai" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
        </div>
        <div class="w3-row-padding">
            <%{txtDiaChi.Attributes.Add("placeholder", "Địa chỉ");} %>
            <asp:TextBox ID="txtDiaChi" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
        </div>
        <div class="w3-row-padding">
            <%{txtNote.Attributes.Add("placeholder", "Ghi chú");} %>
            <asp:TextBox TextMode="MultiLine" ID="txtNote" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
        </div>
        <div class="w3-row-padding" style="margin-top:20px">
            <%if(Carts.Count > 0){ %>
                <%{ imbHoanTat.Text = "Tạo đơn"; } %>
                <asp:Button ID="imbHoanTat" runat="server" OnClick="imbHoanTat_Click" CssClass="w3-button w3-green" />
            <%} %>
        </div>
        <input type="hidden" name="infoLable" value=""/>
    </VIT:Form>
</div>