<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<!-- contact section start -->
<section class="contact_section layout_padding" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "": ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
        <VIT:Form ID="frmMain" runat="server">
            <h2 title="<%=Title %>"><%=Title %> </h2>
            <div class="info-text"><%=SubTitle %></div>
            <%if (!string.IsNullOrEmpty(Message.MessageString))
                {
                    var type = Message.MessageType == "ERROR" ? "danger"
                            : Message.MessageType == "WARNING" ? "warning"
                            : "info";%>
            <p class="blog_text">
                <div class="alert alert-<%=type %>" role="alert">
                    <h4 class="alert-heading"><%=Message.MessageString %></h4>
                </div>
            </p>
            <%} %>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <label>Họ và tên<span class="required">*</span></label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Số điện thoại<span class="required">*</span></label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>

                <div class="col">
                    <div class="form-group">
                        <label>Để lại lời nhắn cho chúng tôi</label>
                        <textarea rows="5" class="form-control" name="infoValue1"></textarea>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
                <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional" Visible="false">
                    <ContentTemplate>
                        <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                            <ProgressTemplate>
                                <%--Đang gửi...--%>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <div class="input-group input-group-sm mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><%=Language["Confirm"] %></span>
                            </div>
                            <div class="input-group-prepend">
                                <img id="imgCaptcha" runat="server" alt="Confirm Code" height="36" />
                            </div>
                            <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-prepend">
                                <%{ btnDoiMa.Text = Language["ChangeCode"]; } %>
                                <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-secondary" ViewStateMode="Disabled" OnClick="btnDoiMa_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="send_bt form-group text-center">
                <%{ btnSave.Text = Language["Send"]; } %>
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="Gửi đi" />
            </div>
        </VIT:Form>
    </div>


</section>
<!-- contact section end -->

<asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file" Visible="false" />
<input type="hidden" name="infoLable" value="Nội dung" />
