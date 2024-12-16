<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<input type="hidden" name="infoLable" value="Khối lượng|Ghi chú"/>
<div autocomplete="off" method="post" class="ladi-form">
                           <div id="FORM_ITEM387" class="ladi-element">
                               <div class="ladi-form-item-container">
                                   <div class="ladi-form-item-background"></div>
                                   <div class="ladi-form-item">
									   <asp:TextBox ID="txtName" data-constraints="@Required" CssClass="ladi-form-control" name="name" required="required" runat="server" MaxLength="300" placeholder="Họ và Tên:"></asp:TextBox>
									   </div>
									   </div>
									  </div>
                           <div id="FORM_ITEM389" class="ladi-element"><div class="ladi-form-item-container"><div class="ladi-form-item-background"></div><div class="ladi-form-item">
						   <asp:TextBox ID="txtPhone" CssClass="ladi-form-control" name="name" data-constraints="@JustNumbers" required="required" runat="server" MaxLength="300" placeholder="Số điện thoại:"></asp:TextBox>
						   </div></div></div>
                           <div id="BUTTON390" class="ladi-element"><div class="ladi-button"><div class="ladi-button-background"></div>
                               <div id="BUTTON_TEXT390" class="ladi-element ladi-button-headline">
							   <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="ladi-headline" Text="ĐẶT HÀNG" />
							   </div></div></div><div id="FORM_ITEM503" class="ladi-element">
                                   <div class="ladi-form-item-container"><div class="ladi-form-item-background"></div><div class="ladi-form-item">
								   <asp:TextBox ID="txtAddress" CssClass="ladi-form-control" name="name" required="required" runat="server" MaxLength="300" placeholder="Địa chỉ:"></asp:TextBox>
								   </div></div></div><div id="FORM_ITEM523" class="ladi-element">
                                       <div class="ladi-form-item-container"><div class="ladi-form-item-background"></div><div class="ladi-form-item"><input autocomplete="off" tabindex="4" name="infoValue0" required="" class="ladi-form-control" type="number" placeholder="Số lượng (Túi 500 gram)" min="1" value=""></div></div></div><div id="FORM_ITEM557" class="ladi-element"><div class="ladi-form-item-container"><div class="ladi-form-item-background"></div><div class="ladi-form-item"><textarea autocomplete="off" tabindex="5" name="infoValue1" class="ladi-form-control" placeholder="Ghi chú thêm: "></textarea></div></div></div>
									   
									   </div>
									   






						
<asp:TextBox ID="txtEmail" runat="server" MaxLength="300" name="email" placeholder="Email:" Visible="false"></asp:TextBox>
<asp:FileUpload ID="flu" runat="server" AllowMultiple="true" Visible="false"/>
<asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
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

