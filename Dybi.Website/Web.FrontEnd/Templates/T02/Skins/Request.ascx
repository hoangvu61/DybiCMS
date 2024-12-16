<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<section class="contact-section">
    <div class="container">
        <h1 class="h-title" title="<%=Title %>"><%=Title %></h1>
        <%if (!string.IsNullOrEmpty(Message.MessageString))
            {
                var type = Message.MessageType == "ERROR" ? "danger"
                        : Message.MessageType == "WARNING" ? "warning"
                        : "info";%>
        <div class="title-wls-text text-center">
            <div class="alert alert-<%=type %>" role="alert">
                <p class="alert-heading"><%=Message.MessageString %></p>
            </div>
        </div>
        <%} %>   

        <div class="row">
            
            <div class="col-md-<%=Config.WebImage != null ? "8 col-md-offset-2" : "6" %> contact-details">
                <VIT:Form ID="frmMain" runat="server">
                <div class=" form-group contact-forms">
                    <%{txtName.Attributes.Add("placeholder", "Họ tên");} %>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
                <div class="form-group contact-forms">
                    <%{txtPhone.Attributes.Add("placeholder", "Số điện thoại");} %>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
                <div class="form-group contact-forms">
                    <div class="input-group">
                        <span class="input-group-addon">Hình mẫu</span>
                        <asp:FileUpload ID="flu" runat="server" CssClass="form-control"/>
                    </div>
                </div>
                <div class="form-group contact-forms">
                    <input type="text" name="infoValue0" class="form-control" placeholder="Số lượng"/>
                </div>
                <div class="form-group contact-forms">
                    <textarea type="text" rows="3" name="infoValue1" class="form-control" placeholder="Mô tả"></textarea>
                </div>
                <div class="form-group contact-forms">
                    <asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
                    <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                                <ProgressTemplate>
                                    <%--Đang gửi...--%>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <div class="input-group">
                                <span class="input-group-addon" id="basic-addon1">Mã xác nhận</span>
                                <div class="input-group-addon" style="width:100px">
                                    <img id="imgCaptcha" runat="server" alt="Confirm Code" width="100" />
                                </div> 
                                <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="input-group-btn">
                                    <%{ btnDoiMa.Text = "Đổi mã"; } %>
                                    <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-info" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                 <%{ btnSave.Text = "Gửi"; } %>
                 <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn btn-primary" Text="Gửi đi" />	
                 <input type="hidden" name="infoLable" value="Số lượng|Mô tả"/>
                    </VIT:Form>
            </div> 
            <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
            <div class="col-md-6">
                <picture>
                    <source srcset="<%=HREF.DomainStore + this.Config.WebImage.FullPath%>.webp" type="image/webp">
                    <source srcset="<%=HREF.DomainStore + this.Config.WebImage.FullPath%>" type="image/jpeg"> 
                    <img style="margin-bottom:10px" src="<%=HREF.DomainStore + this.Config.WebImage.FullPath %>" alt="<%=Component.Company.DisplayName %>"/>
                </picture>
            </div>
            <%} %>
        </div>
    </div>
</section>