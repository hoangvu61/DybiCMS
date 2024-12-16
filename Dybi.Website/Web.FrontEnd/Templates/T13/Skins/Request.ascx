<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<section class="blog_section layout_padding">
    <div class="container">
        <div class="heading_container wow animate__ animate__fadeInUp animated" data-wow-duration="1.2s" data-wow-delay="0.2s">
            <h1><%=Title %></h1>
        </div>
        <div class="row justify-content-around">
            <div class="col-12 col-sm-8 col-md-6">
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
                        <label>Hình ảnh:</label>
                        <asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file"/>
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
                    </div>
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn btn-t13" Text="Gửi đi" />	
                    <input type="hidden" name="infoLable" value="Nội dung"/>
                </VIT:Form>
            </div>
        </div>
    </div>
</section>


                    
