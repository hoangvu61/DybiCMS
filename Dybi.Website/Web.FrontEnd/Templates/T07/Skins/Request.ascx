<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<section class="f_cont min_wrap clearfix">
    <div class="fc_1">
                <p class="p_fc_1">Vui lòng điền vào mẫu liên hệ, chúng tôi sớm liên hệ với bạn trong thời gian sớm nhất. Hoặc có thể liên hệ trực tiếp tại</p>
                <h2 class="t_fc_1">VĂN PHÒNG TẠI</h2>
                <div class="m_fc_1">
                    <ul class="ul_m_fc_1">
                        <li>
                            <span>
                                <svg class="svg-inline--fa fa-map-marker-alt fa-w-12" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="map-marker-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512" data-fa-i2svg=""><path fill="currentColor" d="M172.268 501.67C26.97 291.031 0 269.413 0 192 0 85.961 85.961 0 192 0s192 85.961 192 192c0 77.413-26.97 99.031-172.268 309.67-9.535 13.774-29.93 13.773-39.464 0zM192 272c44.183 0 80-35.817 80-80s-35.817-80-80-80-80 35.817-80 80 35.817 80 80 80z"></path></svg><!-- <i class="fas fa-map-marker-alt"></i> -->
                            </span>
                            Địa chỉ: <%=Component.Company.Branches[0].Address %></li>
                        <li>
                            <span>
                                <svg class="svg-inline--fa fa-phone-square fa-w-14" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="phone-square" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg=""><path fill="currentColor" d="M400 32H48C21.49 32 0 53.49 0 80v352c0 26.51 21.49 48 48 48h352c26.51 0 48-21.49 48-48V80c0-26.51-21.49-48-48-48zM94 416c-7.033 0-13.057-4.873-14.616-11.627l-14.998-65a15 15 0 0 1 8.707-17.16l69.998-29.999a15 15 0 0 1 17.518 4.289l30.997 37.885c48.944-22.963 88.297-62.858 110.781-110.78l-37.886-30.997a15.001 15.001 0 0 1-4.289-17.518l30-69.998a15 15 0 0 1 17.16-8.707l65 14.998A14.997 14.997 0 0 1 384 126c0 160.292-129.945 290-290 290z"></path></svg><!-- <i class="fas fa-phone-square"></i> -->
                            </span>
                            Điện thoại:  <a href="tel:<%=Component.Company.Branches[0].Phone %>"><%=Component.Company.Branches[0].Phone %>  </a>                     </li>
                        <li>
                            <span>
                                <svg class="svg-inline--fa fa-envelope fa-w-16" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="envelope" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg=""><path fill="currentColor" d="M502.3 190.8c3.9-3.1 9.7-.2 9.7 4.7V400c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V195.6c0-5 5.7-7.8 9.7-4.7 22.4 17.4 52.1 39.5 154.1 113.6 21.1 15.4 56.7 47.8 92.2 47.6 35.7.3 72-32.8 92.3-47.6 102-74.1 131.6-96.3 154-113.7zM256 320c23.2.4 56.6-29.2 73.4-41.4 132.7-96.3 142.8-104.7 173.4-128.7 5.8-4.5 9.2-11.5 9.2-18.9v-19c0-26.5-21.5-48-48-48H48C21.5 64 0 85.5 0 112v19c0 7.4 3.4 14.3 9.2 18.9 30.6 23.9 40.7 32.4 173.4 128.7 16.8 12.2 50.2 41.8 73.4 41.4z"></path></svg><!-- <i class="fa fa-envelope" aria-hidden="true"></i> -->
                            </span>
                            Email: <a href="mailto:<%=Component.Company.Branches[0].Email %>"><%=Component.Company.Branches[0].Email %></a>
                        </li>
                    </ul>                                



                </div>
            </div>


            <div class="fc_2">
                <div> 
                    <%if (!string.IsNullOrEmpty(Message.MessageString))
        {
            var type = Message.MessageType == "ERROR" ? "danger"
                    : Message.MessageType == "WARNING" ? "warning"
                    : "info";%>
    <div class="alert alert-<%=type %>" role="alert">
        <h4 class="alert-heading"><%=Message.MessageString %></h4>
    </div>
    <%} %> 
                    <vit:form id="frmMain" runat="server">
                    <ul class="ul_ct">
                        <li>
                            <asp:TextBox ID="txtName" CssClass="ipt_ct box-sizing-fix" name="name" required="required" runat="server" MaxLength="300" placeholder="Tên"></asp:TextBox>
                        </li>
                        <li>
                            <asp:TextBox ID="txtPhone" CssClass="ipt_ct box-sizing-fix" name="phone" required="required" runat="server" MaxLength="300" placeholder="Số điện thoại"></asp:TextBox>	
                        </li>
                        <li> 
                            <textarea class="txt_ct box-sizing-fix" placeholder="Nội dung" name="infoValue0"></textarea>
                        </li> 
                        <li>
                            <asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                                        <ProgressTemplate>
                                            <%--Đang gửi...--%>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <div class="input-group">
                                        <span class="input-group-addon" style="padding:0 10px">
                                            <span class="input-group-text">Mã xác nhận</span>
                                        </span> 
                                        <span class="input-group-addon" style="padding:0;width:100px">
                                            <img id="imgCaptcha" runat="server" alt="Confirm Code" width="100" height="34"/>
                                        </span> 
                                        <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon" style="padding:0">
                                            <%{ btnDoiMa.Text = "Đổi mã"; } %>
                                            <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-secondary" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                                        </span>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </li>
                    </ul>
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn_ct" Text="Gửi đi" />
                        <input type="hidden" name="infoLable" value="Nội dung"/>
                        <asp:FileUpload ID="flu" runat="server" AllowMultiple="true" Visible="false"/>
                         </vit:form>
                </div>
            </div><!-- End .fc_2 -->
</section>