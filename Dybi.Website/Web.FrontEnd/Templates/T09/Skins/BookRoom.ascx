<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<div class="booking_ocline">
    <div class="container">
        <div class="row">
            <div class="col-md-5">
                <%if (!string.IsNullOrEmpty(Message.MessageString))
                        {
                            var type = Message.MessageType == "ERROR" ? "danger"
                                    : Message.MessageType == "WARNING" ? "warning"
                                    : "info";%>
                    <div class="alert alert-<%=type %>" role="alert">
                        <h4 class="alert-heading"><%=Message.MessageString %></h4>
                    </div>
                <%} %>
                <div class="book_room">
                <h1><%=Title %></h1>
                <div class="book_now">
                    <div class="row">
                        
                        <div class="col-md-12">
                            <span>
                                <%= Language["Arrival"]%>
                            </span>
                            <img class="date_cua" src="/Templates/T09/images/date.png">
                            <input class="online_book" placeholder="dd/mm/yyyy" type="date" name="infoValue0">
                        </div>
                        <div class="col-md-12">
                            <span><%= Language["Departure"]%></span>
                            <img class="date_cua" src="/Templates/T09/images/date.png">
                            <input class="online_book" placeholder="dd/mm/yyyy" type="date" name="infoValue1">
                        </div>
                        <div class="col-md-12">
                            <span><%= Language["phone"]%></span>   
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="online_book"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <%{btnSave.Text = Language["BookNow"];} %>
                            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="book_btn" />	
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>
    </div>
</div>




<asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtAddress" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
<asp:FileUpload ID="flu" runat="server" AllowMultiple="true" CssClass="form-control-file" Visible="false"/>
<input type="hidden" name="infoLable" value="Ngày đến|Ngày đi"/>
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
                    
