zs<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<%if (!string.IsNullOrEmpty(Message.MessageString))
        {
            var type = Message.MessageType == "ERROR" ? "danger"
                    : Message.MessageType == "WARNING" ? "warning"
                    : "info";%>
    <div class="alert alert-<%=type %>" role="alert">
        <h4 class="alert-heading"><%=Message.MessageString %></h4>
    </div>
    <%} %>
<input type="text" name="infoValue1" placeholder="Thông tin 1"/>
<input type="text" name="infoValue2" placeholder="Thông tin 2"/>
<input type="text" name="infoValue3" placeholder="Thông tin 3"/> 
<input type="hidden" name="infoLable" value="Thông tin 1|Thông tin 2|Thông tin 3"/>
Phone <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox> <br />
Name <asp:TextBox ID="txtName" runat="server"></asp:TextBox> <br />
Upload <asp:FileUpload ID="flu" runat="server" AllowMultiple="true"/> <br />

<asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
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

<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Gởi thông tin"/>