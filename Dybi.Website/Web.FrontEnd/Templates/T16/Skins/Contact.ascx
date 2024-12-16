<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

 <!-- contact -->
      <div class="contact">
         <div class="container">
            <div class="row">
               <div class="col-md-12">
                  <div class="titlepage text_align_center">
                     <h2><%=Title %></h2>
                  </div>
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
            </div>
            <div class="row d_flex">
               <div class="col-md-6">
                  <div id="request" class="main_form"  data-aos="fade-right">
                     <div class="row">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtName" runat="server" CssClass="contactus" placeholder="Tên"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="contactus" placeholder="Số điện thoại"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="contactus" placeholder="Địa chỉ "></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                           <textarea style="color: #adafb0;" class="textarea" placeholder="Message" type="text" name="infoValue0"> </textarea>
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="send_btn" Text="Gửi đi" />	
                        </div>
                     </div>
                  </div>
               </div>
               <div class="col-md-6">
                  <div class="map_img" data-aos="fade-left">
                     <iframe style="Width:100%;height:565px" src="//www.google.com/maps/embed/v1/search?q=<%=Component.Company.Branches[0].Address %>
                              &zoom=16
                              &key=AIzaSyCZXdpCRgYzYNMLHoBnK_RcooQ8lwby_nc">
                          </iframe>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <!-- end contact -->





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
                    
