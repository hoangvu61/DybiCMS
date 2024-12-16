<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<section class="contact_section">
    <h1>
        <%=Title %>
    </h1>
<VIT:Form ID="frmMain" runat="server">
<input type="hidden" name="infoLable" value="Nội dung"/>

<div style="display:none">
Upload <asp:FileUpload ID="flu" runat="server" />
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
    <div class="page-content contact-form">
        <div class="w3-row">
            <div class="w3-col l6 s6 s12" style="padding:15px 30px">
                <div class="w3-row info">
                    <%foreach(var branch in Component.Company.Branches){ %>
                        <div class="w3-col m12">
                            <span>
                                <strong><%=branch.Name %>:</strong> <%=branch.Address %>
                            </span>
                        </div>
                    <%} %>
                    <div class="w3-col m12">
                        <a href="tel:<%=Component.Company.Branches[0].Phone %>">
                            <span>
                                <strong><%=Language["Phone"] %>:</strong> <%=Component.Company.Branches[0].Phone %>
                            </span>
                        </a>
                    </div>
                    <div class="w3-col m12">
                        <a href="mailto:<%=Component.Company.Branches[0].Email %>">
                            <span>
                                <strong>Email:</strong> <%=Component.Company.Branches[0].Email %>
                            </span>
                        </a>
                    </div>
                </div>
            </div>   
		    <div class="w3-col s12 m6 l6">
                <div class="w3-row w3-section">
                  <div class="w3-col" style="width:50px"><i class="w3-xxlarge fa fa-user"></i></div>
                    <div class="w3-rest">
                        <%{txtName.Attributes.Add("placeholder", Language["Name"]);} %>
                        <asp:TextBox ID="txtName" runat="server" CssClass="w3-input w3-border w3-round"></asp:TextBox>
                    </div>
                </div>
			    
                <div class="w3-row w3-section">
                  <div class="w3-col" style="width:50px"><i class="w3-xxlarge fa fa-phone"></i></div>
                    <div class="w3-rest">
                        <%{txtPhone.Attributes.Add("placeholder", Language["Phone"]);} %>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="w3-input w3-border w3-round"></asp:TextBox>
                    </div>
                </div>
						
                <div class="w3-row w3-section">
                  <div class="w3-col" style="width:50px"><i class="w3-xxlarge fa fa-pencil"></i></div>
                    <div class="w3-rest">
                        <textarea class="w3-input w3-border w3-round" name="infoValue0" placeholder="<%=Language["Message"] %>"></textarea>
                    </div>
                </div>	
                								  
                    <asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
                    <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                                <ProgressTemplate>
                                    <%--Đang gửi...--%>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            Mã xác nhận: <img id="imgCaptcha" runat="server" alt="Confirm Code" width="100" />
                            <%{ btnSave.Text = Language["ChangeCode"]; } %>
                            <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="w3-btn w3-grey w3-round" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                            <asp:TextBox ID="txtCode" runat="server" CssClass="w3-input w3-border w3-round"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                					
                <%{ btnSave.Text = Language["Send"]; } %>
                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="w3-button w3-block w3-section w3-green w3-ripple w3-padding"/>					
									
            </div>
		</div>				
    </div>
</VIT:Form>
    
</section>
