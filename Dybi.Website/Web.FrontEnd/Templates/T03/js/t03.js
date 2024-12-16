
function SelectBuyProduct(id, title, image, salemin, brief, price) {
    $("#ProdicyId").val(id);
    $("#muaTitle").html(title);
    $("#muaImage").attr("src", image);
    $("#muaBrief").html(brief);
    $("#muaPrice").html("Giá: " + price + " đ");
    $("#txtQuantity").attr({ "min": salemin });
    $("#txtQuantity").val(salemin);
}

// Send the rating information somewhere using Ajax or something like that.
function AddToCart(go) {
    var productQuantity = $("#txtQuantity").val();
    var proId = $("#ProdicyId").val();
    var productMin = $('#txtQuantity').attr('min');
    if (parseInt(productQuantity, 10) < parseInt(productMin, 10)) {
        alert('Số lượng mua ít nhất là ' + productMin + ', bạn không thể mua ' + productQuantity + ' sản phẩm');
    }
    else {
        $.ajax({
            type: "POST",
            url: "/JsonPost.aspx/AddProductsToCarts",
            data: JSON.stringify({ productId: proId, quantity: productQuantity, properties: '', addonIds: '' }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    if (go == true) {
                        location.href = '/cart/gio-hang';
                    } else {
                        $.ajax({
                            type: "POST",
                            url: "/JsonPost.aspx/GetCarts",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var total = 0;
                                for (var i = 0; i < data.d.length; i++) {
                                    total += data.d[i].Quantity;
                                }
                                $(".cart").css("display", "block");
                                $(".cart span").text(total);
                            }
                        });
                    }
                }
            }
        });
    }
}

function SelectOrderProduct(type, title, image, salemin, price) {
    if (type == 'REQUEST') {
        $("#orderTitle").text('Yêu cầu báo giá');
        $("#order_Price").css('display', 'none');
        $("#order_txtQuantity").val(salemin);
    }
    else {
        $("#orderTitle").text('Đặt hàng');
        $("#order_Price").html("Giá: " + price + " đ");
        $("#order_Price").css('display', 'block');
        $("#order_txtQuantity").attr({ "min": salemin });
        $("#order_txtQuantity").val(salemin);
    }

    $("#order_Title").text(title);
    $("#order_Image").attr("src", image);
}

$(document).ready(function () {
    Captcha(5);
});

function SendOrder() {
    var check = ValidCaptcha();
    if (check == false) {
        $("#capcharerror").css('display', 'block');
    }
    else {
        var stitle = $("#orderTitle").text();
        var squantity = $("#order_txtQuantity").val();
        var sproductImg = $("#order_Image").attr("src");
        var sorder_Title = $("#order_Title").html();
        var scustomerName = $("#customerName").val();
        var scustomerPhone = $("#customerPhone").val();
        var scustomerNote = $("#customerNote").val();

        document.getElementById("btnOrder").disabled = true;
        $.ajax({
            type: "POST",
            url: "/JsonPost.aspx/SendEmailBooking",
            data: JSON.stringify({ name: scustomerName, phone: scustomerPhone, note: scustomerNote, productName: sorder_Title, productImg: sproductImg, quantity: squantity, title: stitle }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    $('#orderModal').modal('hide');
                    document.getElementById("btnOrder").disabled = false;
                    if (data.d = false) alert("Gửi thông tin thất bại, Xin lỗi vì sự cố này, bạn vui lòng liên hệ với Shop qua Số điện thoại, zalo hoặc facebook.");
                    else alert("Thông tin đã được gửi đi, chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất");
                }
            }
        });
    }
}
