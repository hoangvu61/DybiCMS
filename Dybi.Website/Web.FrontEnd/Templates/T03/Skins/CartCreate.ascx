<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartCreate.ascx.cs" Inherits="Web.FrontEnd.Modules.CartCreate" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<div class="box" style="margin:0px;padding:20px;background:#fff">
    <div class="detail-box">
        <VIT:Form ID="frmMain" runat="server">
            <div class="form-floating mb-3">
                <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control" placeholder="Tên khách hàng" required></asp:TextBox>
                <label>Tên khách hàng</label>
            </div>
            <div class="form-floating mb-3">
                 <asp:TextBox ID="txtDienThoai" runat="server" CssClass="form-control" placeholder="Điện thoại" required></asp:TextBox>
                <label>Điện thoại</label>
            </div>
            <div class="form-floating mb-3">
                <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" placeholder="Địa chỉ" required></asp:TextBox>
                <label>Địa chỉ</label>
            </div>
            <div class="form-floating mb-3">
                <asp:TextBox TextMode="MultiLine" ID="txtNote" runat="server" placeholder="Ghi chú" CssClass="form-control"></asp:TextBox>
                <label>Ghi chú</label>
            </div>
            <%if(Carts.Count > 0){ %>
                <asp:Button ID="imbHoanTat" runat="server" OnClick="imbHoanTat_Click" CssClass="btn btn-light" Text="Tạo đơn" />
            <%} %>
        </VIT:Form>
    </div>
</div>

<input type="hidden" name="infoLable" value=""/>