<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<%if(!string.IsNullOrEmpty(this.Skin.BodyBackground)){%>
    <style>
        .header_section, .news_section{background: <%=this.Skin.BodyBackground%> !important}
    </style>
<%}%>
<!-- contact section start -->
<div class="contact_section layout_padding"style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
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
        </div>
    </div>
    <div class="container-fluid">
        <div class="contact_section_2">
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
                    <div class="mail_section_1"> 
                        <VIT:Form ID="frmMain" runat="server">
                            <asp:TextBox ID="txtName" runat="server" CssClass="mail_text" placeholder="Tên"></asp:TextBox>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="mail_text" placeholder="Số điện thoại"></asp:TextBox>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="mail_text" placeholder="Địa chỉ "></asp:TextBox>
                            <textarea class="massage-bt" placeholder="Message" rows="5" type="text" name="infoValue0"> </textarea>
                            <asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                                        <ProgressTemplate>
                                            <%--Đang gửi...--%>
                                        </ProgressTemplate> 
                                    </asp:UpdateProgress>
                                    <div class="input-group input-group-sm mb-3" style="margin:10px 0px">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Mã xác nhận</span>
                                        </div> 
                                        <div class="input-group-prepend">
                                            <img id="imgCaptcha" runat="server" alt="Confirm Code" height="31" style="height:31px"/>
                                        </div> 
                                        <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                        <div class="input-group-prepend">
                                            <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-secondary" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="send_bt">
                                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="main_bt" Text="Gửi đi" />	
                            </div>
                            <asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file" Visible="false"/>
                            <input type="hidden" name="infoLable" value="Nội dung"/>
                        </VIT:Form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- contact section end -->


                    
