<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="cart_section">
        <div class="container">
          <h1>Đơn hàng</h1>

          <div class="row">
            <div class="col-md-12 col-lg-8 mx-auto">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th scope="col">Sản phẩm</th>
                            <th scope="col" style="text-align:right">Đơn giá</th> 
                            <th scope="col" style="text-align:center;width:150px">Số lượng</th>
                            <th scope="col" style="text-align:right">Thành tiền</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%foreach(var product in Template.GioHang){ %>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-md-6 col-lg-6 mx-auto">
                                        <picture>
						                  <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>.webp" type="image/webp">
						                  <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>" type="image/jpeg"> 
                                          <img style="width: 50px" src="<%=!string.IsNullOrEmpty(product.ProductImage) ? HREF.DomainStore + product.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=product.ProductName %>"/>
                                        </picture> 
                                    </div>
                                    <div class="col-md-6 col-lg-6 mx-auto">
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
                                 <%if(product.DiscountType > 0){ %>
                                 <del style="font-size:14px; color:gray">
                                     <%=product.Price.ToString("N0") %> <sup>₫</sup>
                                 </del>&nbsp;
                                 <%} %>
                                <span>
                                    <%=product.PriceAfterDiscount.ToString("N0") %> <sup>₫</sup>
                                </span>
                            </td> 
                            <%--<td>
                                <div class="input-group input-group-sm mb-3">
                                    <button type="button" class="btn btn-sm" onclick="EditCart('<%=product.ProductId %>',<%=product.Quantity - 1 %>,'<%=product.ProductProperties %>')">&lt;</button>
                                    <span class="form-control form-control-sm" style="text-align:center; width:50px; display:contents"><%=product.Quantity %></span>
                                    <button type="button" class="btn btn-sm" onclick="EditCart('<%=product.ProductId %>',<%=product.Quantity + 1 %>,'<%=product.ProductProperties %>')">&gt;</button>
                                </div>
                            </td>--%>
                            <td class="quantity"> 
                                <input type="number" id="sl<%=product.ProductId%>" value="<%=product.Quantity%>" min="1" 
                                    onchange="EditCart('<%=product.ProductId %>','<%=product.ProductProperties %>')" 
                                    <%--onkeypress='this.onchange();' onpaste='this.onchange();' oninput='this.onchange();'--%>
                                    <%=product.IsAddOn ? "readonly" : ""%> class="form-control form-control-sm"/>
                            </td>
                            <td style="text-align:right">
                                <%=product.TotalCost.ToString("N0") %> <sup>₫</sup>
                            </td>
                        </tr>
                        <%} %>
                    
                    </tbody>
                    <tfoot>
                        <tr style="background:#fff"> 
                            <td>Tổng tiền</td>
                            <td colspan="3" style="text-align:right; color:blue; font-weight:bold">
                                <%= Template.GioHang.Sum(e => e.TotalCost).ToString("N0")%> <sup>₫</sup>
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div> 
            <div class="col-md-12 col-lg-4 mx-auto">
                <VIT:Position runat="server" ID="psContent"></VIT:Position>
            </div>
          </div>

    <script type="text/javascript">
        function EditCart(id, properties) {
            var quantity = $("#sl" + id).val();
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
        </div>
    </section>
</asp:Content>