<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<!-- contact section -->
<section class="contact_section layout_padding">
    <div class="container">
    <div class="heading_container heading_center">
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
    <div class="col-md-6 px-0">
        <div class="form_container">
        <div action="">
            <div class="form-row">
            <div class="form-group col">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Your Name"></asp:TextBox>
            </div>
            </div>
            <div class="form-row">
            <div class="form-group col"> 
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Phone Number"></asp:TextBox>
            </div>
            </div>
            <div class="form-row">
            <div class="form-group col">
                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>
            </div>
            </div>
            <div class="form-row">
            <div class="form-group col">
                <textarea class="message-box form-control" name="infoValue0" placeholder="Message"></textarea>
            </div>
            </div>
            <div class="btn_box">
                <%{ btnSave.Text = Language["Send"]; } %>
                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn btn-t13" />	
                
            </div>
        </div>
        </div>
    </div>
    <div class="col-md-6 px-0">
        <div class="map_main">
        <div class="map-responsive">
            <iframe style="Width:100%;height:500px" src="//www.google.com/maps/embed/v1/search?q=<%=Component.Company.Branches[0].Address %>
                    &zoom=16
                    &key=AIzaSyCZXdpCRgYzYNMLHoBnK_RcooQ8lwby_nc">
                </iframe> 
        </div>
        </div>
    </div>
    </div>
</div>
</section>
<!-- end contact section -->

<asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file" Visible="false"/>
<input type="hidden" name="infoLable" value="Nội dung"/>
<asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
 <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional" Visible="false">
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
                    
