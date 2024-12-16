<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<!-- section contact start -->
<div class="contact_section layout_padding">
    <div class="container">
    	<div class="row">
    		<div class="col-md-6">
                <%if (!string.IsNullOrEmpty(Message.MessageString))
                        {
                            var type = Message.MessageType == "ERROR" ? "danger"
                                    : Message.MessageType == "WARNING" ? "warning"
                                    : "info";%>
                    <div class="alert alert-<%=type %>" role="alert">
                        <h4 class="alert-heading"><%=Message.MessageString %></h4>
                    </div>
                    <%} %>
    			<div class="map">
                    <div id="googleMap">
                        <iframe style="Width:100%;height:250px" src="//www.google.com/maps/embed/v1/search?q=<%=Component.Company.Branches[0].Address %>
                            &zoom=16
                            &key=AIzaSyCZXdpCRgYzYNMLHoBnK_RcooQ8lwby_nc">
                        </iframe> 
                    </div>
                </div>
    		</div>
    		<div class="col-md-6">
    			<div class="email_main">
                    <div class="email_text">
                        <div class="form-group">
                            <asp:TextBox ID="txtName" runat="server" CssClass="email-bt" placeholder="Tên"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="email-bt" placeholder="Số điện thoại"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="email-bt" placeholder="Địa chỉ "></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <textarea class="massage-bt" placeholder="Message" rows="5" type="text" name="infoValue0"> </textarea>
                        </div>
                        <div class="send_btn">
                            <%{btnSave.Text = Title; }%>
                            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="main_bt" Text="Gửi đi" />	
                        </div>
                    </div>
    			</div>
    	</div>
    </div>
</div>
</div>
<!-- section contact end -->

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
                    
