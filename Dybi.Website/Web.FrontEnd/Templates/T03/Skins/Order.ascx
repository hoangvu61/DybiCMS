<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<div id="orderModal" class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <VIT:Form ID="frmMain" runat="server">
                <div class="modal-header">
                    <h5 class="modal-title fs-5" id="orderTitle">Đặt hàng</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    <%if (!string.IsNullOrEmpty(Message.MessageString))
                    {%>
                    <script>
                        alert('<%=Message.MessageType == "ERROR" ? "Lỗi" : Message.MessageType == "WARNING" ? "Cảnh báo" : ""%> <%=Message.MessageString %>')
                    </script>
                    <%} %>  
                </div>
                <div class="modal-body">
                    <div style="padding:15px">
                        <input type="hidden" name="infoLable" value="Nội dung"/>
                        <div class="row">
                            <div class="col-md-5">
                                <img id="order_Image" style="width:100%"/>
                            </div>
                            <div class="col-md-7">
                                <div><b id="order_Title"></b></div>
                                <hr />
                                <div id="order_Price"></div>
                                <br />
                                <div style="display:none">
                                    Upload <asp:FileUpload ID="flu" runat="server" /><br />
                                </div>
                                <div class="form-floating mb-3">
                                    <input type="number" value="1" id="order_txtQuantity" placeholder="Nhập số lượng" class="form-control"/>
                                    <label>Số lượng muốn đặt:</label> 
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-floating mb-3">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Họ và tên" required="required" MaxLength="300"></asp:TextBox>
                                        <label>Họ và tên</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Số điện thoại" required="required" MaxLength="300"></asp:TextBox>
                                        <label>Số điện thoại</label>
                                    </div>
                                
                                    <div class="form-floating mb-3">
                                        <textarea name="infoValue0" placeholder="Ghi chú" class="form-control" style="height: 100px"></textarea>
                                        <label>Ghi chú</label>
                                    </div>
                                <div class="form-group">
                                    <asp:ScriptManager runat="server" ID="SptManagerOrder"></asp:ScriptManager>
                                    <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional" Visible="false">
                                        <ContentTemplate>
                                            <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                                                <ProgressTemplate>
                                                    <%--Đang gửi...--%>
                                                </ProgressTemplate> 
                                            </asp:UpdateProgress>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Nhập mã xác nhận</span>
                                                </div> 
                                                <div class="input-group-prepend">
                                                    <img id="imgCaptcha" runat="server" alt="Confirm Code" height="38"/>
                                                </div> 
                                                <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                <div class="input-group-prepend">
                                                    <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-secondary" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>	
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn btn-light" Text="Gửi đi" />
                </div>	
            </VIT:Form>
        </div>
    </div>
</div>

<script type="text/javascript">
    function SelectOrderProduct(type, title, image, salemin, price) {
        if (type == 'REQUEST')
        {
            $("#orderTitle").text('Yêu cầu báo giá');
            $("#order_Price").css('display', 'none');
            $("#order_txtQuantity").val(salemin);
        }
        else
        {
            $("#orderTitle").html('Đặt hàng');
            $("#order_Price").html("Giá: " + price + " đ");
            $("#order_Price").css('display', 'block');
            $("#order_txtQuantity").attr({ "min": salemin });
            $("#order_txtQuantity").val(salemin);
        }
    
        $("#order_Title").html(title);
        $("#order_Image").attr("src", image);
    }
</script>