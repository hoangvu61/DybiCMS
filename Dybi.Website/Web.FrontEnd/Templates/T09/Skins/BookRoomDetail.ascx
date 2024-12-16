<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<div class="back_re">
         <div class="container">
            <div class="row">
               <div class="col-md-12">
                  <div class="title">
                      <h2><%=Title %></h2>
                  </div>
               </div>
            </div>
         </div>
      </div>

<!--  contact -->
      <div class="contact">
         <div class="container">
            <div class="row">
                <%if (!string.IsNullOrEmpty(Message.MessageString))
                        {
                            var type = Message.MessageType == "ERROR" ? "danger"
                                    : Message.MessageType == "WARNING" ? "warning"
                                    : "info";%>
                <div class="col-md-12">
                    <div class="alert alert-<%=type %>" role="alert">
                        <h4 class="alert-heading"><%=Message.MessageString %></h4>
                    </div>
                </div>
                <%} %>
               <div class="col-md-6">
                  <div id="request" class="main_form">
                     <div class="row">
                        <div class="col-md-12 "> 
                            <%{txtName.Text = Language["name"];} %>
                            <asp:TextBox ID="txtName" runat="server" CssClass="contactus"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <%{txtPhone.Text = Language["phone"];} %>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="contactus"></asp:TextBox>                       
                        </div> 
                        <div class="col-md-12">
                            <input class="contactus" placeholder="<%=Language["Arrival"] %>" type="date" name="infoValue0">
                        </div>
                        <div class="col-md-12">
                            <input class="contactus" placeholder="<%=Language["Departure"] %>" type="date" name="infoValue1">
                        </div>                        
                        <div class="col-md-12">
                           <input class="contactus" placeholder="<%=Language["clientsnumber"] %>" type="number" name="infoValue2"> 
                        </div>
                        <div class="col-md-12">
                           <textarea class="textarea" placeholder="<%=Language["note"] %>" type="type" Message="Name" name="infoValue3"></textarea>
                        </div>
                        <div class="col-md-12">
                            <%{btnSave.Text = Language["BookNow"];} %>
                            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="send_btn" />	
                        </div>
                     </div>
                  </div>
               </div>
               <div class="col-md-6">
                  <div class="map_main">
                     <div class="map-responsive">
                        <iframe style="Width:100%;height:565px" src="//www.google.com/maps/embed/v1/search?q=<%=Component.Company.Branches[0].Address %>
                              &zoom=16
                              &key=AIzaSyCZXdpCRgYzYNMLHoBnK_RcooQ8lwby_nc">
                          </iframe>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <!-- end contact -->

<asp:TextBox ID="txtAddress" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
<asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file" Visible="false"/>
<input type="hidden" name="infoLable" value="<%=Language["Arrival"] %>|<%=Language["Departure"] %>|<%=Language["clientsnumber"] %>|<%=Language["note"] %>"/>
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
                    
