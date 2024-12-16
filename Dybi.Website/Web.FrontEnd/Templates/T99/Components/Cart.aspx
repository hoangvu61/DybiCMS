<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">


  <section class="blog_section layout_padding">
    <div class="container">
      <div class="heading_container">
        <h1><%=Language["order"] %></h1>
      </div>

      <div class="row" style="margin:50px 0px">
        <div class="col-md-6 col-lg-8 mx-auto">
            <table class="table">
                <thead class="thead-light">
                    <tr>
                        <th scope="col">
                            <%=Language["productservice"] %>
                        </th>
                        <th scope="col" style="text-align:right">
                            <%=Language["price"] %>
                        </th> 
                        <th scope="col" style="text-align:right">
                            <%=Language["quantity"] %>
                        </th>
                        <th scope="col" style="text-align:right">
                            <%=Language["totalamounts"] %>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%foreach(var product in Template.GioHang){ %>
                    <tr>
                        <td>
                            <div class="row">
                                <div class="col-md-6 col-lg-6 mx-auto">
                                    <%if(!string.IsNullOrEmpty(product.ProductImage)){ %>
                                    <picture>
						              <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>.webp" type="image/webp">
						              <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>" type="image/jpeg"> 
                                      <img style="width: 50px" src="<%=HREF.DomainStore + product.Image.FullPath%>" alt="<%=product.ProductName %>"/>
                                    </picture> 
                                    <%} %>
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
                            <span>
                                <%=product.PriceAfterDiscount.ToString("N0") %> ₫
                            </span>
                            <%if(product.Price > 0){ %>
                            <del>
                                <%=product.Price.ToString("N0") %> ₫
                            </del>
                            <%} %>
                        </td> 
                        <td class="quantity">
                            <input type="number" id="sl<%=product.ProductId%>" value="<%=product.Quantity%>" 
                                onblur="EditCart('<%=product.ProductId %>','<%=product.ProductProperties %>')" style="width:80px;" <%=product.IsAddOn ? "readonly" : ""%>/>
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
</asp:Content>