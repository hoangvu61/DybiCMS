<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<!-- contact section start -->
<div class="contact_section layout_padding" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
        <h1 class="contact_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
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
    <div class="container layout_padding2-top">
        <div class="row">
            <div class="col-md-6 padding_left_0">
                <div class="map_main">
                <div class="map-responsive">
                    <iframe style="Width:100%;height:500px" src="//www.google.com/maps/embed/v1/search?q=<%=Component.Company.Branches[0].Address %>
                            &zoom=16
                            &key=AIzaSyCZXdpCRgYzYNMLHoBnK_RcooQ8lwby_nc">
                        </iframe> 
                </div>
                </div>
            </div>
            <div class="col-md-6">
                <VIT:Form ID="frmMain" runat="server">
                    <asp:TextBox ID="txtName" runat="server" required="required" placeholder="Tên"></asp:TextBox>
                    <asp:TextBox ID="txtPhone" runat="server" required="required" placeholder="Số điện thoại"></asp:TextBox>
                    <textarea class="message-box" placeholder="Nội dung" rows="5" type="text" name="infoValue0"></textarea>
                    <asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
                    <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                                <ProgressTemplate>
                                    <%--Đang gửi...--%>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend" style="height: 50px">
                                    <span class="input-group-text">Mã xác nhận</span>
                                </div> 
                                <div class="input-group-prepend">
                                    <img id="imgCaptcha" runat="server" alt="Confirm Code" width="100" height="50"/>
                                </div> 
                                <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="input-group-prepend">
                                    <%{ btnDoiMa.Text = "Đổi mã"; } %>
                                    <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-secondary" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="d-flex justify-content-center">
                        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="button" Text="Gửi đi" />	
                    </div>
                    <asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file" Visible="false"/>
                    <input type="hidden" name="infoLable" value="Nội dung"/>
                </VIT:Form>
            </div>
        </div>
    </div>
</div>
<!-- contact section end -->


                    
