<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<section class="contact py-lg-4 py-md-3 py-sm-3 py-3" id="contact">
    <div class="container py-lg-5 py-md-4 py-sm-4 py-3">
        <h1 class="text-center mb-5" title="<%=Title %>"><%=Title %></h1>
        <%if (!string.IsNullOrEmpty(Message.MessageString))
            {
                var type = Message.MessageType == "ERROR" ? "danger"
                        : Message.MessageType == "WARNING" ? "warning"
                        : "info";%>
        <div class="title-wls-text text-center mb-lg-5 mb-md-4 mb-sm-4 mb-3">
            <div class="alert alert-<%=type %>" role="alert">
                <p class="alert-heading"><%=Message.MessageString %></p>
            </div>
        </div>
        <%} %>   

        <div class="row">
            <div class="col-lg-6 col-md-6">
                <%if(Skin.BodyBackgroundFile != null){ %>
                <picture>
				    <source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>.webp" type="image/webp">
				    <source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" type="image/jpeg"> 
                    <img src="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" alt="<%=Title %>"/>
                </picture>
                <%} %>
            </div>
            <div class="col-lg-6 col-md-6 contact-details">
                <VIT:Form ID="frmMain" runat="server">
                <div class=" form-group contact-forms">
                    <%{txtName.Attributes.Add("placeholder", Language["Name"]);} %>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group contact-forms">
                    <%{txtEmail.Attributes.Add("placeholder", Language["Email"]);} %>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group contact-forms">
                    <%{txtPhone.Attributes.Add("placeholder", Language["Phone"]);} %>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group contact-forms">
                    <%{txtAddress.Attributes.Add("placeholder", Language["Address"]);} %>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group contact-forms">
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon1"><%=Language["Sample"] %></span>
                        </div>
                        <asp:FileUpload ID="flu" runat="server" CssClass="form-control"/>
                    </div>
                </div>
                <div class="form-group contact-forms">
                    <input type="text" name="infoValue0" class="form-control" placeholder="<%=Language["Quantity"] %>"/>
                </div>
                <div class="form-group contact-forms">
                    <input type="text" name="infoValue1" class="form-control" placeholder="<%=Language["SuggestedPrice"] %>"/>
                </div>
                <div class="form-group contact-forms">
                    <textarea type="text" rows="3" name="infoValue2" class="form-control" placeholder="<%=Language["SampleDescripe"] %>"></textarea>
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
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><%=Language["ConfirmCode"] %></span>
                                </div> 
                                <div class="input-group-prepend">
                                    <img id="imgCaptcha" runat="server" alt="Confirm Code" width="100" />
                                </div> 
                                <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="input-group-prepend">
                                    <%{ btnDoiMa.Text = Language["ChangeCode"]; } %>
                                    <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-secondary" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                 <%{ btnSave.Text = Language["Send"]; } %>
                 <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="sent-butnn" Text="Gửi đi" />	
                 <input type="hidden" name="infoLable" value="<%=Language["Quantity"] %>|<%=Language["SuggestedPrice"] %>|<%=Language["SampleDescripe"] %>"/>
                 </VIT:Form>
            </div>
        </div>
    </div>
</section>