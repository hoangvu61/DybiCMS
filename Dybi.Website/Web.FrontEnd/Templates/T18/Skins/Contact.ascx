<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<section class="contact_section layout_padding">
    <div class="container container-bg">
        <div class="form">
            <div class="heading_container heading_center">
                <h1 class="<%=Page.Title %>">
                    <%=Title %>
                </h1>
            </div>
            <div>
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
            </div>
            <VIT:Form ID="frmMain" runat="server">
                <div class="form-group">
                    <asp:TextBox ID="txtName" CssClass="form-control form-control-sm" runat="server" placeholder="Tên người liên hệ"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPhone" CssClass="form-control form-control-sm" runat="server" placeholder="Số điện thoại"></asp:TextBox>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Hình sản phẩm</span>
                        </div> 
                        <asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control"/>
                    </div>
                </div>
                <div class="form-group">
                    <label>Mô tả sản phẩm:</label>
                    <textarea class="form-control form-control-sm" name="infoValue0"></textarea>
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
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nhập mã xác nhận</span>
                                </div> 
                                <div class="input-group-prepend">
                                    <img id="imgCaptcha" runat="server" alt="Confirm Code" height="50"/>
                                </div> 
                                <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="input-group-prepend">
                                    <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-secondary" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="form-group text-center">
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn btn-success btnsend" Text="Gửi đi" />	
                </div>
                <input type="hidden" name="infoLable" value="Nội dung"/>
            </VIT:Form>
        </div>
    </div>
</section>
                    
