<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<!-- contact section -->
<section id="Contact" class="contact_section layout_padding">
    <div class="container">
        <div class="heading_container"> 
            <h2>
            <%=Title %>
            </h2>
            <%if (!string.IsNullOrEmpty(Message.MessageString))
            {
                var type = Message.MessageType == "ERROR" ? "danger"
                        : Message.MessageType == "WARNING" ? "warning"
                        : "info";%>
                <div class="alert alert-<%=type %>" role="alert">
                    <h4 class="alert-heading"><%=Message.MessageString %></h4>
                </div>
            <%} %>
        </div>
        <div class="row">
            <div class="col-md-9 mx-auto">
                <div class="contact-form">
                    <VIT:Form ID="frmMain" runat="server">
                        <asp:TextBox CssClass="input" ID="txtName" runat="server" placeholder="Tên"></asp:TextBox>
                        <asp:TextBox CssClass="input" ID="txtPhone" runat="server" placeholder="Số điện thoại"></asp:TextBox>
                        <asp:TextBox CssClass="input" ID="txtAddress" runat="server" placeholder="Địa chỉ "></asp:TextBox>
                        <textarea class="input" placeholder="Message" rows="5" type="text" name="infoValue0"> </textarea>
                        <asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
                        <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                                    <ProgressTemplate>
                                        <%--Đang gửi...--%>
                                    </ProgressTemplate> 
                                </asp:UpdateProgress>
                                <div class="input-group input-group-sm mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Mã xác nhận</span>
                                    </div> 
                                    <div class="input-group-prepend">
                                        <img id="imgCaptcha" runat="server" alt="Confirm Code" height="31"/>
                                    </div> 
                                    <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                    <div class="input-group-prepend">
                                        <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-secondary" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="d-flex justify-content-center">
                            <%{ btnSave.Text = Language["Send"]; } %>
                            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn_on-hover" Text="Gửi đi" />	
                        </div>
                        <asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file" Visible="false"/>
                        <input type="hidden" name="infoLable" value="Nội dung"/>
                    </VIT:Form>
                </div>
            </div>
        </div>
    </div>
</section>
<!-- end contact section -->

                    
