<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/OrderInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.OrderInfo" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Web.Asp.ObjectData"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<section class="orderInfo_section row">
    <div class="col-8 mx-auto">
        <VIT:Form ID="frmMain" runat="server">
            <div class="input-group">
                <input type ="text" name ="<%=SettingsManager.Constants.SendOrder %>" class="form-control" placeholder="Nhập mã đơn hàng"/>
                <button class="btn btn-light" type="submit"><i class="fa fa-search"></i></button>
            </div> 
        </VIT:Form>
    </div>
</section>

<%if (!string.IsNullOrEmpty(Request[SettingsManager.Constants.SendOrder]))
    { %>
     <%if (Order != null)
         { %>
        <div class="row">
            <div class="col-md-6">
                <div class="card cart-info">
                    <div class="card-header">
                        Thông tin đơn hàng
                    </div>
                    <div class="card-body">
                        <div><label>Mã đơn hàng: </label> <%=Order.Id %></div>
                        <div class="divider"></div>
                        <div><label>Khách hàng: </label> <%=Order.CustomerName %></div>
                        <div><label>Điện thoại: </label> <%=Order.CustomerPhone.Length > 5 ? "*****" + Order.CustomerPhone.Substring(5) : Order.CustomerPhone%></div>
                        <div><label>Địa chỉ: </label> <%=Order.CustomerAddress %></div>
                        <div><label>Ghi chú: </label> <%=Order.Note %></div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
            <div class="panel panel-info"> 
                <div class="card">
                    <div class="card-header">
                        Trạng thái đơn hàng
                    </div>
                    <div class="card-body">
                        <div class="panel-body" style="font-size:12px">
                            <div><label>Ngày tạo: </label> <%= string.Format("{0: dd/MM/yyyy hh:mm:ss}", Order.CreateDate) %></div>
                            <div><label>Xác nhận: </label> <%= Order.ConfirmDate != null ? string.Format("{0: dd/MM/yyyy hh:mm:ss}", Order.ConfirmDate) : "" %></div>
                            <div><label>Ngày gửi: </label> <%= Order.SendDate != null ? string.Format("{0: dd/MM/yyyy hh:mm:ss}", Order.SendDate) : "" %></div>
                        </div> 
                        <%if (OrderDelivery != null) {  %>
                        <div class="panel-body" style="font-size:12px">
                            <div><label>Đơn vị vận chuyển: </label> <%=DataSource.Deliveries[OrderDelivery.DeliveryId] %></div>
                            <div><label>Mã vận đơn: </label> <%=OrderDelivery.DeliveryCode %></div>
                            <div><label>Phí vận chuyển: </label> <%=OrderDelivery.DeliveryFee.ToString("N0") %></div>
                            <div><label>Ghi chú: </label> <%= OrderDelivery.DeliveryNote %></div>
                        </div> 
                        <%} %>
                    </div>
                </div>
            </div>
        </div>
            <div class="col-md-12">
                <div class="panel panel-info py-3"> 
                    <div class="panel-heading"> 
                        <h3 class="panel-title">Sản phẩm</h3> 
                    </div> 
                    <div class="panel-body" style="font-size:12px">
                        <table class="table table-bordered table-striped"> 
                            <thead> 
                                <tr> 
                                    <th colspan="2">Sản phẩm</th> 
                                    <th>Số lượng</th>
                                </tr> 
                            </thead> 
                            <tbody> 
                        <% foreach (var product in Products)
                            {
                            %>
                                <tr> 
                                    <td>  
                                        <img src="<%=HREF.DomainStore + product.Image.FullPath %>" style="height:50px" alt="<%=product.ProductName %>"/>
                                    </td> 
                                    <td style="vertical-align:middle">
                                        <%=product.ProductName %>
                                        <%if (!string.IsNullOrEmpty(product.ProductCode)) { %>
                                            <br /> Mã <code><%=product.ProductCode %></code>
                                        <%} %>
                                    </td>
                                    <td style="vertical-align:middle"><%=product.Quantity %></td>
                                </tr>  
                            <%}%>
                                </tbody> 
                        </table>
                    </div> 
                </div>
            </div>
        </div>
    <%} else { %>
    <div class="contain">
        <div class="row justify-content-around">
            <div class="col-6">
                <div class="alert alert-primary" role="alert" style="text-align:center; margin:100px 0px">
                    Không tim thấy đơn hàng.<br />
                    <strong>
                    <%=Request[SettingsManager.Constants.SendOrder] %>
                    </strong>
                </div>
            </div>
        </div>
    </div>
    <%} %> 
<%} %>