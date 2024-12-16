<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartCreate.ascx.cs" Inherits="Web.FrontEnd.Modules.CartCreate" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<VIT:Form ID="frmMain" runat="server">
<div class="box" style="margin:0px">
    <div class="detail-box" style="padding:15px">
<div class="form-group">
    <label>Tên người nhận:</label>
    <asp:TextBox ID="txtHoTen" runat="server" required="required" CssClass="form-control form-control-sm"></asp:TextBox>
</div>
<div class="form-group">
    <label>Điện thoại:</label>
    <asp:TextBox ID="txtDienThoai" runat="server" required="required" CssClass="form-control form-control-sm"></asp:TextBox>
</div>
<div class="form-group">
    <label>Địa chỉ:</label>
    <asp:TextBox ID="txtDiaChi" runat="server" required="required" CssClass="form-control form-control-sm"></asp:TextBox>
</div>
<div class="form-group">
    <label>Ghi chú:</label>
    <asp:TextBox TextMode="MultiLine" ID="txtNote" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
</div>
<%if(Carts.Count > 0){ %>
<asp:Button ID="imbHoanTat" runat="server" OnClick="imbHoanTat_Click" CssClass="btn btn-warning" Text="Tạo đơn" />
<%} %>
</div>
</div>

<input type="hidden" name="infoLable" value=""/>
</VIT:Form>