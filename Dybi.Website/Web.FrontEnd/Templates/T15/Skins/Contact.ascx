<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

  <section class="blog_section layout_padding">
    <div class="container">
      <div class="heading_container">
        <h1>
          <%=Title %>
        </h1>
      </div>
    <div class="container">
        <div class="row justify-content-around">
      <div class="col-6">
<%if (!string.IsNullOrEmpty(Message.MessageString))
        {
            var type = Message.MessageType == "ERROR" ? "danger"
                    : Message.MessageType == "WARNING" ? "warning"
                    : "info";%>
    <div class="alert alert-<%=type %>" role="alert">
        <h4 class="alert-heading"><%=Message.MessageString %></h4>
    </div>
    <%} %>

          <div class="form-group">
    <label>Tên khách hàng:</label>
    <asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
</div>
<div class="form-group">
    <label>Điện thoại:</label>
    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
</div>
<div class="form-group">
    <label>Địa chỉ:</label>
    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
</div>
<div class="form-group">
    <label>Mô tả sản phẩm:</label>
    <textarea class="form-control form-control-sm" name="infoValue0"></textarea>
</div>

<asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn btn-t13" Text="Gửi đi" />	

    </div>
    </div>
   </div>
</div>
  </section>

<asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file" Visible="false"/>
<input type="hidden" name="infoLable" value="Nội dung"/>
<asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
 <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional" Visible="false">
    <ContentTemplate>
        <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
            <ProgressTemplate>
                <%--Đang gửi...--%>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <img id="imgCaptcha" runat="server" alt="Confirm Code" width="100" />
        Mã xác nhận: 
        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="ChangeCode" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
                    
