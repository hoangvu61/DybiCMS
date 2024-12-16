<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="breacrum">
        <div class="min_wrap">
            <ul class="ul_breacrum">
                <li>
                    <a href="/">
                        <i class="glyphicon glyphicon-home"></i> Trang chủ
                    </a>
                </li>     
                <li>
                    Giỏ hàng
                </li>         
            </ul><!-- End .ul-breacrum -->
        </div><!-- End .min_wrap -->
    </section>

  <section style="margin:50px 0px">
    <div class="container">
            <h1 class="t_h">Đơn hàng</h1>

      <div class="row" style="margin:50px 0px">
        <div class="col-md-6 col-lg-8 mx-auto">
            <table class="table">
                <thead class="thead-light">
                    <tr>
                        <th scope="col">Sản phẩm</th>
                        <th scope="col" style="text-align:right">Đơn giá</th> 
                        <th scope="col" style="text-align:center">Số lượng</th>
                        <th scope="col" style="text-align:right">Thành tiền</th>
                    </tr>
                </thead>
                <tbody>
                    <%foreach(var product in Template.GioHang){ %>
                    <tr>
                        <td>
                                    <picture>
						              <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>.webp" type="image/webp">
						              <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>" type="image/jpeg"> 
                                      <img style="width: 50px; float:left; margin-right:10px" src="<%=!string.IsNullOrEmpty(product.ProductImage) ? HREF.DomainStore + product.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=product.ProductName %>"/>
                                    </picture> 
                                <div style="float:left">
                                    <p>
                                        <%=product.ProductName %>
                                    </p>
                                    <p>
                                        <%=product.ProductProperties %>
                                    </p>
                                </div>
                            </div>
                        </td>
                        <td style="text-align:right">
                            <span>
                                <%=product.PriceAfterDiscount.ToString("N0") %> ₫
                            </span>
                            <%if(product.Price > product.PriceAfterDiscount){ %>
                            <del>
                                <%=product.Price.ToString("N0") %> ₫
                            </del>
                            <%} %>
                        </td> 
                        <td>
                            <div class="input-group">
                                <div style="cursor:pointer" class="input-group-addon" onclick="EditCart('<%=product.ProductId %>',<%=product.Quantity - 1 %>,'<%=product.ProductProperties %>')">&lt;</div>
                                <span class="form-control" style="text-align:center"><%=product.Quantity %></span>
                                <div style="cursor:pointer" class="input-group-addon" onclick="EditCart('<%=product.ProductId %>',<%=product.Quantity + 1 %>,'<%=product.ProductProperties %>')">&gt;</div>
                            </div>
                        </td>
                        <td style="text-align:right">
                            <%=product.TotalCost.ToString("N0") %> ₫
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div> 
        <div class="col-md-6 col-lg-4 mx-auto">
            <VIT:Position runat="server" ID="psRight"></VIT:Position>
        </div>
      </div>
    </div>
  </section>

<script type="text/javascript">
    function EditCart(id, quantity, properties) {
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/EditCarts",
            data: JSON.stringify({ productId: id, quantity: quantity, properties: properties}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    location.href = '<%=HREF.BaseUrl %>cart/don-hang';
                 }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.responseText);
                console.log(thrownError);
            }
        });
     }
</script>	
</asp:Content>