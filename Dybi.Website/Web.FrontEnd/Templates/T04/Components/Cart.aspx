﻿<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="breadcrumb">
        <li>
            <a href="/">
                <%=Language["Home"] %>
            </a>
        </li>
        <li>
            <%=Language["Cart"] %>
        </li>     
    </ul>
    <script type="application/ld+json">
    {
        "@context": "https://schema.org",
        "@type": "BreadcrumbList",
        "itemListElement": [{
            "@type": "ListItem",
            "position": 1,
            "name": "<%=Language["Home"] %>",
            "item": "<%=HREF.DomainLink %>"
        },
        {
            "@type": "ListItem",
            "position": 2,
            "name": "<%=Language["Cart"] %>",
            "item": "<%=HREF.LinkComponent("AboutUs", Language["Cart"].ConvertToUnSign(), true) %>"
        }
        ]
    }
    </script>

<section class="cart_section">
    <div class="w3-container">
      <h1><%=Language["Cart"] %></h1>

      <div class="w3-row" style="margin:50px 0px">
        <div class="w3-col m6 l8 i12 plr70">
            <table class="w3-table-all">
                <thead class="thead-light">
                    <tr>
                        <th scope="col"><%=Language["Product"] %></th>
                        <th scope="col" style="text-align:right">Đơn giá</th> 
                        <th scope="col" style="text-align:center"><%=Language["Quntity"] %></th>
                        <th scope="col" style="text-align:right">Thành tiền</th>
                    </tr>
                </thead>
                <tbody>
                    <%foreach(var product in Template.GioHang){ %>
                    <tr>
                        <td>
                            <div class="w3-row">
                                <div class="w3-col m4 mx-auto">
                                    <picture>
						              <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>.webp" type="image/webp">
						              <source srcset="<%=HREF.DomainStore + product.Image.FullPath%>" type="image/jpeg"> 
                                      <img style="width: 50px" src="<%=!string.IsNullOrEmpty(product.ProductImage) ? HREF.DomainStore + product.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=product.ProductName %>"/>
                                    </picture> 
                                </div>
                                <div class="w3-col m8 mx-auto">
                                    <%=product.ProductName %>
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
                            <%if(product.Discount > 0){ %>
                            <del>
                                <%=product.Price.ToString("N0") %> ₫
                            </del>
                            <%} %>
                        </td> 
                        <td>
                            <div class="input-group input-group-sm mb-3">
                                <button type="button" class="btn btn-sm" onclick="EditCart('<%=product.ProductId %>',<%=product.Quantity - 1 %>,'<%=product.ProductProperties %>')">&lt;</button>
                                <span class="form-control form-control-sm" style="text-align:center"><%=product.Quantity %></span>
                                <button type="button" class="btn btn-sm" onclick="EditCart('<%=product.ProductId %>',<%=product.Quantity + 1 %>,'<%=product.ProductProperties %>')">&gt;</button>
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
        <div class="w3-col m6 l4 i12 mx-auto">
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