<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartCreate.ascx.cs" Inherits="Web.FrontEnd.Modules.CartCreate" %>
 
Thông tin đơn hàng: 
<input type="text" name="infoValue1" placeholder="Họ và tên"/>
<input type="text" name="infoValue2" placeholder="Số Điện thoại"/>
<input type="text" name="infoValue3" placeholder="Địa chỉ"/>
<input type="hidden" name="infoLable" value="Họ và tên|Số Điện thoại|Địa chỉ"/>
Họ tên:<asp:TextBox ID="txtHoTen" runat="server"></asp:TextBox>
Địa chỉ:<asp:TextBox ID="txtDiaChi" runat="server"></asp:TextBox>
Điện thoại:<asp:TextBox ID="txtDienThoai" runat="server"></asp:TextBox>
Note:<asp:TextBox ID="txtNote" runat="server"></asp:TextBox>

<asp:Button ID="imbHoanTat" runat="server" OnClick="imbHoanTat_Click"/>