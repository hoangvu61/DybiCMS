<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row justify-content-around">
      <div class="col-6">
      <div class="alert alert-primary" role="alert" style="text-align:center; margin:100px 0px">
  Đơn hàng đã được gửi thành công.
        <br />  Mã đơn hàng: <strong>
            <%=Request["sord"] %>
                             </strong>
</div>
    </div>
    </div>

   </div>
</asp:Content>