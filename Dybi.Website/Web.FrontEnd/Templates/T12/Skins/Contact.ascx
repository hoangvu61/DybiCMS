<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<div id="contact-form" class="contact-form">
<%if (!string.IsNullOrEmpty(Message.MessageString))
        {
            var type = Message.MessageType == "ERROR" ? "danger"
                    : Message.MessageType == "WARNING" ? "warning"
                    : "info";%>
    <div class="alert alert-<%=type %>" role="alert">
        <h4 class="alert-heading"><%=Message.MessageString %></h4>
    </div>
    <%} %>             
    <VIT:Form ID="frmMain" runat="server">

                            <div class="contact-form-loader"></div>
                            <div class="row">
                                <label class="name grid_4 wow fadeIn fadeInLeft animated">
								<asp:TextBox ID="txtName" data-constraints="@Required" CssClass="form-control" name="name" required="required" runat="server" MaxLength="300" placeholder="Họ và Tên:"></asp:TextBox>
                                    <span class="empty-message">*Vui lòng cho Khánh Tâm biết bạn là ai?.</span>
                                    <span class="error-message">*Bạn phải nhập đầy đủ họ và tên.</span>
                                </label>

                                <label class="phone grid_4 wow fadeIn fadeInLeft animated">
									<asp:TextBox ID="txtPhone" CssClass="form-control" name="name" data-constraints="@JustNumbers" required="required" runat="server" MaxLength="300" placeholder="Số điện thoại:"></asp:TextBox>
									
                                    <span class="empty-message">*Vui lòng nhập số điện thoại.</span>
                                    <span class="error-message">*Vui lòng nhập số điện thoại.</span>
                                </label>
								

                                <label class="email grid_8 wow fadeIn fadeInRight animated" style="margin-top:20px">
                                    <input type="text" class="form-control" name="infoValue2" placeholder="Email:" />
								</label>
								
								<label class="grid_4 wow fadeIn fadeInLeft animated" style="margin-top:20px">
									<input type="checkbox" name="infoValue0" checked='checked'/> Hiện tên bạn trên danh sách người hâm mộ
                                </label>
								<label class="grid_4 wow fadeIn fadeInLeft animated" style="margin-top:20px">
									<input type="checkbox" name="infoValue1" checked='checked'/> Nhận thông tin mới nhất qua email
                                </label>
                            </div>
							
<input type="hidden" name="infoLable" value="Hiển thị trên danh sách|Nhận thông tin mới|Email"/>

                            <div class="btn-wr">
								<asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn4 pulse__mod" Text="Đăng ký" />
                            </div>
        <input type="hidden" name="infoLable" value="Nội dung"/>
<asp:FileUpload ID="flu" runat="server" AllowMultiple="true" Visible="false"/>
    <asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
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
    </VIT:Form>
</div>