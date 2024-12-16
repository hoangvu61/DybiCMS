<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/AdpiaBonus.ascx.cs" Inherits="Web.FrontEnd.Modules.AdpiaBonus" %>

<h1 title="<%=Title%>"><strong> <%=Title%></strong></h1>
<div class="product-bonus-content-inner row"></div>
<div style="margin-bottom:30px; clear:both"></div>

<script>
const obj = <%=Data %>;
if(obj.data && obj.data.length > 0) {
	var htmlRow = ``;

	obj.data.forEach((item, index) => {
		htmlRow += `
			<div class="col-lg-3 col-md-4 col-sm-4 col-xs-6" style="padding: 20px">
				<a href="${item.url}" title="${item.product}">
					<img src="${item.img}" alt="${item.product}" loading="lazy" style="width: 100%; border: 1px solid orangered; border-radius: 10px; overflow: hidden;">
				</a>
				<div class="cw-550-white" style="font-size: 12px; text-overflow: ellipsis; -webkit-box-orient: vertical; -webkit-line-clamp: 2; word-wrap: break-word; overflow: hidden; display: -webkit-box; margin-top: 10px; height: 35px;">
					<a href="${item.url}" title="${item.product}">
					${item.product}
					</a>
				</div>
				<div style="display: flex; margin-top: 10px;">
					<div style="font-size: 12px; flex: 0 0 50%; max-width: 50%; font-weight: 600;">
						${item.price}
					</div>`;
					if(item.origin_price) {
					htmlRow += `<div style="font-size: 10px; color: grey; flex: 0 0 50%; max-width: 50%; text-align: right; align-self: center;">
						${item.origin_price}
					</div>`;
					}
				htmlRow += `</div></div>`;
	});

	$(".product-bonus-content-inner").html(htmlRow);

	$(".product-bonus-content-inner .item img").css({
		'height': $(".product-bonus-content-inner .item img").width(),
		'objectFit': 'cover'
	});
} else {
	$(".product-bonus-content-inner").html(`
	<div class="not_found_mess" style="flex: 100%;">
	  	<div style="text-align: center; padding: 10px 0;">
				<img src="https://cdn-icons-png.flaticon.com/512/3350/3350122.png" style="width: 50px;">
			</div>
			<div style=" text-align: center; color: #f00; padding: 10px 0;">
				Không tìm thấy ưu đãi phù hợp!
			</div>
		</div>
	`);
}
</script>
