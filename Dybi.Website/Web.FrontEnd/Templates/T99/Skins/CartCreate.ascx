<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartCreate.ascx.cs" Inherits="Web.FrontEnd.Modules.CartCreate" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<VIT:Form ID="frmMain" runat="server">
    <div class="shippingbox" style="margin:0px">
        <div class="detail-box">
            <div class="form-group">
                <label><%=Language["fullname"]%>:</label>
                <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
            </div>
            <div class="form-group">
                <label><%=Language["phone"]%>:</label>
                <asp:TextBox ID="txtDienThoai" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
            </div>
            <div class="form-group">
                <label><%=Language["address"]%>:</label>
                <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
            </div>
            <div class="form-group">
                <label><%=Language["note"]%>:</label>
                <asp:TextBox TextMode="MultiLine" ID="txtNote" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
            </div>
            <%if(Carts.Count > 0){ imbHoanTat.Text = Language["register"];%>
                <asp:Button ID="imbHoanTat" runat="server" OnClick="imbHoanTat_Click" CssClass="btn btn-t13" Text="Tạo đơn" ForeColor="#000"/>
            <%} %>
        </div>
    </div>
    <input type="hidden" name="infoLable" value=""/>
</VIT:Form>