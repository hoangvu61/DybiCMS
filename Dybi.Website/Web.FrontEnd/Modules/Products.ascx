<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<script type="text/javascript">
    function SelectProduct(id)
    {
        $("#ProdicyId").val(id);
    }

    // Send the rating information somewhere using Ajax or something like that.
    function AddToCart(go) {
        var productQuantity = $("#txtQuantity").val();
        var proId = $("#ProdicyId").val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>Components/Page/JsonPost.aspx/AddProductsToCarts",
            data: JSON.stringify({ productId: proId, quantity: productQuantity}),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (data) {
                 if (data != "" && go == true) {
                     location.href = '<%=HREF.BaseUrl %>/vit-product-carts';
                 }
             }
         });
    }
</script>

<script type="text/javascript" language="javascript">
    function fly(iddivGio) {
        var proId = $("#ProdicyId").val();
        iddivSP = "#product" + proId;
        var productX = $(iddivSP).offset().left;
        var productY = $(iddivSP).offset().top;
        var basketX = 0;
        var basketY = 0;

        basketX = $("#" + iddivGio).offset().left;
        basketY = $("#" + iddivGio).offset().top;

        var gotoX = basketX - productX;
        var gotoY = basketY - productY;

        var newImageWidth = $(iddivSP).width() / 6;
        var newImageHeight = $(iddivSP).height() / 6;

        $(iddivSP + " img")
		    .clone()
		    .prependTo(iddivSP)
		    .css({ 'position': 'absolute' })
		    .animate({ opacity: 0.4 }, 100)
		    .animate({ opacity: 0.1, marginLeft: gotoX, marginTop: gotoY, width: newImageWidth, height: newImageHeight }, 1200, function () {
		        $(this).remove();

		        //reload cart
		        $.ajax({
		            type: "GET",
		            url: "<%=HREF.BaseUrl %>Components/Page/AjaxCartCount.aspx",
		            success: function (msg) {
		                $("#" + iddivGio).html("");
		                $("#" + iddivGio).html(msg);
		                $("#" + iddivGio).show("slow");
		                <%--$(location).attr('href', '<%=HREF.LinkComponent("Product", "Carts")%>');--%>
		            }
		        });
		    }); 
    }
</script>