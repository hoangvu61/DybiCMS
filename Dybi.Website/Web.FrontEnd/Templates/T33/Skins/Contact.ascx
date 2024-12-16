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
                  <h1 class="contact_taital" style="text-align:center; <%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
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
         <div style ="text-align:center;max-width:500px; margin:0 auto"> 
             <VIT:Form ID="frmMain" runat="server">
                 <div class="form-group"> 
                     <%{txtName.Attributes.Add("placeholder", Language["Name"]);} %>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                 </div>
                 <div class="form-group">
                     <%{txtPhone.Attributes.Add("placeholder", Language["Phone"]);} %>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                 <div class="form-group">
                        <input class="form-control" name="infoValue0" placeholder="Email"/>
                 </div>
                 <div class="form-group">
                     <%{txtAddress.Attributes.Add("placeholder", Language["Address"]);} %>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
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
                                    <img id="imgCaptcha" runat="server" alt="Confirm Code" height="36"/>
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
                 <div class="send_bt form-group">
                     <%{ btnSave.Text = Language["Send"]; } %>
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn btn-primary" Text="Gửi đi" />	
                </div>
            </VIT:Form>
         </div>
      </div>
      <!-- contact section end -->

<asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file" Visible="false"/>
<input type="hidden" name="infoLable" value="Email"/>
