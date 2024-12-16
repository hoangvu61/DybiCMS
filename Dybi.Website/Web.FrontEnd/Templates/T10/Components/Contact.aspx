<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script src="/Templates/T10/js/moment.min.js"></script>
    <link href="/Templates/T10/css/newpub.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Templates/T10/css/simplePagination.css">
    <link rel="stylesheet" href="/Templates/T10/css/adpia_minishop_style.css">
    <style>
	.newpub-discount-code-data-table {
        font-size:14px;
	}
	.list-event {
		border-bottom: 1px solid #dddddd !important;
		margin-top: 15px;
		overflow-y: hidden;
	}
	.tb-coupoun tbody td {
		color: black;
		vertical-align: middle !important;
		text-align: center;
	}
	.tb-coupoun tbody td.text-left {
		text-align: left;
	}
	.tb-coupoun tbody td a {
		color: black;
	}
	.dt-buttons {
		text-align: right;
	}
	.btn-sm {
		padding: 1px 3px;
	}
	
	table .input-group {
		margin-bottom: 0;
	}
	table button {
		margin-bottom: 0 !important;
		margin-right: 0 !important;
		border-radius: 0 !important;
	}
	table input {
		color: rgb(183, 183, 183) !important;
	}
	table tr td:nth-child(1) span:nth-child(2) {
		background-color: rgb(249, 104, 62); color: #fff; padding: 5px;
		position: relative;
		text-transform: uppercase;
	}
	table tr td:nth-child(1) span:nth-child(1) {
		border-top: 13px solid transparent;
		border-bottom: 13px solid transparent;
		border-right:8px solid rgb(249, 104, 62);
		width: 0;
		height: 0;
		display: inline-block;
		transform: translateY(8px);
	}
	table tr td:nth-child(1) span:nth-child(3) {
		border-top: 13px solid transparent;
		border-bottom: 13px solid transparent;
		border-left:8px solid rgb(249, 104, 62);
		width: 0;
		height: 0;
		display: inline-block;
		transform: translateY(8px);
	}
	.export-file-button-row:hover {
		background:  olivedrab !important;
	}
</style>
    <%{
            this.Title = "Săn mã giảm giá Shopee Tiki Lazada - Săn hàng khuyến mãi";
            this.MetaDescription = "Danh mục tổng hợp các chương trình khuyến mãi và mã giảm giá khi mua sắm online tại các trang web uy tín như Lazada, Tiki, Shopee. Mã giảm giá mới nhất hôm nay.";
            this.MetaKeywords = "san ma giam gia, hang khuyen mai, deal shock, voucher giam gia, xa hang ban lo";
        } %>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 title="Tìm mã giảm giá">Tìm mã giảm giá</h1>
    <div class="newpub-discount-code-merchant-filter">
    <div class="newpub-discount-code-merchant-filter-box">
				<div class="newpub-discount-code-merchant-select-all filter-merchant-button" data-mid="all">
					<span>Chọn Website:</span>
				</div>
				<div class="newpub-discount-code-merchant-box">

				</div>
			</div>
    </div>
    <div class="newpub-discount-code-data-table" data-apr-merchants="tiki,klook,lazada,yes24,sendo,shopee,fahasa,">
			<div class="newpub-discount-code-data-table-box">
				<div style="overflow: auto;">
    <table  id="coupons_area" class="table table-hover table-bordered tb-coupoun table-striped jambo_table bulk_action">
						<thead>
							<tr>
								<th style="width: 160px;">Nhà cung cấp</th>
								<th>Nội dung</th>
								<th>Khuyến mãi</th>
								<th style="width: 140px;">Ngày hết hạn</th>
								<th>Mã giảm giá</th>
							</tr>
						</thead>
						<tbody>
                            <tr><td colspan='5'>Đang tải dữ liệu ...</td></tr>
						</tbody>
					</table>
                    </div>
			</div>
			<div class="table-paginate-overview"></div>
		</div>
    <script src="/Templates/T10/js/simplePagination.js?20210325-1407"></script>

   <script>
	var couponTotalArray = '';
	var shopeeTotalArray = '';
	var lazadaTotalArray = '';
	var tikiTotalArray = '';
	var sendoTotalArray = '';
	var fahasaTotalArray = '';
	var dbTotalArray = '';
	var currentArray = [];
	var c_limit = 20;
	var f_mid = null;
	var f_end_date = null;

	$(document).ready(function() {
		$('.loading').show();
		getAllDiscountCodeAPI();
	});

	function promiseShopee(f) {
		return new Promise(function(resolve, reject) {
			setTimeout(() => {
				
				resolve(f);
			}, 500);
		});
	}

	function promiseLazada(f) { 
		return new Promise(function(resolve, reject) {
			setTimeout(() => {
				
				resolve(f);
			}, 500);
		});
	}

	function promiseSendo(f) {
		return new Promise(function(resolve, reject) {
			setTimeout(() => {
				
				resolve(f);
			}, 500);
		});
	}

	function promiseTiki(f) { 
		return new Promise(function(resolve, reject) {
			setTimeout(() => {
				
				resolve(f);
			}, 500);
		});
	}

	function promiseFahasa(f) { 
		return new Promise(function(resolve, reject) {
			setTimeout(() => {
				
				resolve(f);
			}, 500);
		});
	}

	function promiseDB(f) { 
		return new Promise(function(resolve, reject) {
			setTimeout(() => {
				
				resolve(f);
			}, 2000);
		});
	}

	async function getAllDiscountCodeAPI() {
		const x1 = await promiseShopee(get_shopee_promo_code_api());
		const x2 = await promiseLazada(get_lazada_promo_code_api());
		//const x3 = await promiseSendo(get_sendo_promo_code_api());
		const x4 = await promiseTiki(get_tiki_promo_code_api());
		//const x5 = await promiseFahasa(get_fahasa_promo_code_api());
		const x6 = await promiseDB(dbDiscountCode());
		
		couponTotalArray = shopeeTotalArray + tikiTotalArray + lazadaTotalArray + sendoTotalArray + fahasaTotalArray + dbTotalArray;
		couponTotalArray = couponTotalArray.substring(0, couponTotalArray.length - 1);
		couponTotalArray = "[" + couponTotalArray + "]";
		couponTotalArray = couponTotalArray.replaceAll(",,", ",");
		couponTotalArray = couponTotalArray.replace("[,", "[");
		couponTotalArray = couponTotalArray.replace(",]", "]");
		couponTotalArray = JSON.parse(couponTotalArray);

		couponTotalArray.sort(function (a, b) {
			date1 = a.end_date.split('/');
			date2 = b.end_date.split('/');
			x = date1[2] + date1[1] + date1[0];
			y = date2[2] + date2[1] + date2[0];
			return x < y ? -1 : x > y ? 1 : 0;
		});

		getDiscountCodeByPage(1);

		$(".table-paginate-overview").pagination({
			items: couponTotalArray.length,
			itemsOnPage: c_limit,
			displayedPages: 4,
			cssStyle: 'light-theme',
			prevText: '&laquo',
			nextText: '&raquo',
			onPageClick : function(pageNumber){
				getDiscountCodeByPage(pageNumber);
			}
		});

		$('.loading').hide();
	}

	function getDiscountCodeByPage(page) {
		currentArray = [];

		for(var i = 0; i < couponTotalArray.length; i++) {
			var c_ed = '99999999';

			if(couponTotalArray[i]['end_date'] != '') {
				c_ed = moment(couponTotalArray[i]['end_date'], 'DD/MM/YYYY').format('YYYYMMDD');
			}

			if(f_mid != 'all' && f_mid != null && f_end_date != null) {
				if(couponTotalArray[i]['mid'] == f_mid && c_ed <= f_end_date) {
					currentArray.push(couponTotalArray[i]);
				}
			} else if (f_mid != 'all' && f_mid != null) {
				if(couponTotalArray[i]['mid'] == f_mid) {
					currentArray.push(couponTotalArray[i]);
				}
			} else if(f_end_date != null) {
				if(c_ed <= f_end_date) {
					currentArray.push(couponTotalArray[i]);
				}
			} else {
				currentArray.push(couponTotalArray[i]);
			}
		}

		drawDiscountCodeDataRow(page, currentArray);
	}

	function drawDiscountCodeDataRow(page, currentArray = []) {

		var row = ``;
		var realLength = 0;
		var limit = page * c_limit;
		if(limit > currentArray.length) {
			limit = currentArray.length;
		}

		for(var i = ((page - 1) * c_limit); i < limit; i++) {
			row += drawDiscountCodeRowSchema(i);
		}

		$(".tb-coupoun > tbody").html(row);
	}

	function dbDiscountCode() {
		var tmpArray = [];
		return $.ajax({
			url: 'https://event.adpia.vn/AdpiaWebFrame/getDisCount/A100047119?check=1',
			type: 'GET',
		})
		.done(function(data) {
			$.each(data, function(index, value) {
				if(index != 'lazada' && index != 'shopee' && index != 'klook') {
					$.each(value, function(c_index, c_value){
						let mid = c_value['merchant_id'];
						let title = c_value['title'];
						let desc = c_value['desciprtion'];
						let discount = c_value['discount'];
						let end_date = moment(c_value['end_date']).format('DD/MM/YYYY');
						let code = c_value['discount_code'];
						let url = c_value['link'];
						tmpArray.push({mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: url});
					});
				}
			});

			

			var tmp = JSON.stringify(tmpArray);
			tmp = tmp.substring(1, tmp.length - 1);
			var result = tmp + ",";
			dbTotalArray += result;
		});
	}

$(".newpub-discount-code-merchant-filter").on('click', '.filter-merchant-button', function(event) {
	$('.loading').show();
	$(".filter-merchant-button").removeClass('filter-merchant-active');
	if($(this).attr('data-mid') != 'all') {
		$(this).addClass('filter-merchant-active');
	} else {
		$(".filter-merchant-button").addClass('filter-merchant-active');
	}

	f_mid = $(this).attr('data-mid');
	var customLength = 0;

	getDiscountCodeByPage(1);

	if(f_end_date != null) {
		for(var k = 0; k < couponTotalArray.length; k++) {
			var edArr = couponTotalArray[k]['end_date'].split('/');
			var c_ed = edArr[2] + edArr[1] + edArr[0];

			if(f_mid != 'all') {
				if(c_ed <= f_end_date && couponTotalArray[k]['mid'] == f_mid) {
					customLength++;
				}
			} else {
				if(c_ed <= f_end_date) {
					customLength++;
				}
			}
		}
	} else {
		for(var k = 0; k < couponTotalArray.length; k++) {
			if(f_mid != 'all') {
				if(couponTotalArray[k]['mid'] == f_mid) {
					customLength++;
				}
			} else {
				customLength++;
			}
		}
	}

	$(".table-paginate-overview").pagination({
		items: customLength,
		itemsOnPage: c_limit,
		displayedPages: 4,
		cssStyle: 'light-theme',
		prevText: '&laquo',
		nextText: '&raquo',
		onPageClick : function(pageNumber){
			getDiscountCodeByPage(pageNumber);
		}
	});

	$('.loading').hide();
});

$("input[name=to_ed]").change(function(event) {
	$('.loading').show();

	var end_date = $(this).val();
	f_end_date = moment(end_date).format('YYYYMMDD');
	var customLength = 0;

	getDiscountCodeByPage(1);

	if(f_mid != null && f_mid != 'all') {
		for(var h = 0; h < couponTotalArray.length; h++) {
			var edArr = couponTotalArray[h]['end_date'].split('/');
			var c_ed = edArr[2] + edArr[1] + edArr[0];

			if(c_ed <= f_end_date && couponTotalArray[h]['mid'] == f_mid) {
				customLength++;
			}
		}
	} else {
		for(var h = 0; h < couponTotalArray.length; h++) {
			var edArr = couponTotalArray[h]['end_date'].split('/');
			var c_ed = edArr[2] + edArr[1] + edArr[0];

			if(c_ed <= f_end_date) {
				customLength++;
			}
		}
	}

	$(".table-paginate-overview").pagination({
		items: customLength,
		itemsOnPage: c_limit,
		displayedPages: 4,
		cssStyle: 'light-theme',
		prevText: '&laquo',
		nextText: '&raquo',
		onPageClick : function(pageNumber){
			getDiscountCodeByPage(pageNumber);
		}
	});

	$('.loading').hide();
});

function drawDiscountCodeRowSchema(i = 0) {
	var row = ``;
	var c_stt = i + 1;
	var c_title = currentArray[i]['title'];
	var c_desc = currentArray[i]['desc']
	.replace(' (...chi tiết)', '')
	.replace('ĐH tối thiểu', '. ĐH tối thiểu')
	.replace('Hiệu lực lúc', '. Hiệu lực lúc')
	.replace(' Ngày hết hạn','. Ngày hết hạn')
	.replace(' Ngành hàng', '. Ngành hàng');
	var c_end_date = currentArray[i]['end_date'];
	if(c_end_date.length == 0) {
		c_end_date = 'Vô thời hạn';
	}
	var c_discount = currentArray[i]['discount'];
	var c_code = currentArray[i]['code'] ? currentArray[i]['code'] : '';
	var c_url = 'https://click.adpia.vn/click.php?m=' + currentArray[i]['mid'] + '&a=A100047119&l=8888&l_cd1=3&l_cd2=0&tu=' + currentArray[i]['url'];
	var c_mid = currentArray[i]['mid'];
	var apr_merchants = $(".newpub-discount-code-data-table").attr('data-apr-merchants').split(',');

	row += `<tr>
	<td style="width: 120px; text-align: center; vertical-align: middle !important;"><span></span><span>` + c_mid + `</span><span></span></td>
	<td class="text-left" style="vertical-align: middle !important;">
	<span style="margin-top: 2px; margin-bottom: 2px; font-weight: 600;">
	` + c_title + `
	</span>
	<br>
	<small>` + c_desc + `</small>
	</td>
	<td style="width: 120px; text-align:center; vertical-align: middle !important; font-weight: 600;">` + c_discount + `</td>
	<td style="width: 120px; text-align:center; vertical-align: middle !important; font-weight: 600;">` + c_end_date + `</td>
	<td style="width: 190px; text-align:center; vertical-align: middle !important; font-weight: 600;">`;

	if($.inArray(c_mid, apr_merchants) != -1) {
		if(c_code.length > 0) {
			row += `<div class="input-group">
			<input type="text" readonly
			value="` + c_code + `"
			class="form-control">
			</div>`;
		} else {
			row += `<a href="` + c_url + `" class="btn btn-dark" target="_blank" style="color: #fff !important; width: 100%;"><i class="fa fa-hand-o-right" aria-hidden="true"></i> MUA NGAY</a>`;
		}
	} else {
		row += `<a href="https://newpub.adpia.vn/newpub/offer_list/` + c_mid + `" class="btn btn-warning" target="_blank" style="color: #fff !important; width: 100%;"><i class="fa fa-hand-o-right" aria-hidden="true"></i> CHƯA LIÊN KẾT</a>`;
	}

	row += `</td>
	<td style="display: none; width: 500px; vertical-align: middle !important;">` + c_url + `</td>
	</tr>`;
	return row;
}

function exportExcelFile() {
	fnExcelReport();
}

function fnExcelReport()
{
	var tab_text="<table border='2px'><tr><th>STT</th><th>Nhà cung cấp</th><th>Nội dung</th><th>Khuyến mãi</th><th>Ngày hết hạn</th><th>Mã giảm giá</th><th>Link sự kiện</th></tr>";
	var textRange; var j=0;
	tab = document.getElementsByClassName('tb-coupoun')[0];

	for(j = 0 ; j < currentArray.length ; j++)
	{
		tab_text=tab_text+drawDiscountCodeRowSchema(j);
	}

	tab_text=tab_text+"</table>";
	tab_text= tab_text.replace(/<a[^>]*>|<\/a>/g, "");
	tab_text= tab_text.replace(/<img[^>]*>/gi,"");
	tab_text= tab_text.replace(/<input[^>]*>|<\/input>/gi, "");
	tab_text = tab_text.replace(/<th>/g, "<th style=\"text-align: center; height: 50px; line-height: 50px !important; background-color: rgb(25, 55, 89); color: #ffffff;\">");
	tab_text= tab_text.replace(/MUA NGAY/gi, "");

	var dt = new Date();
	var day = dt.getDate();
	var month = dt.getMonth() + 1;
	var year = dt.getFullYear();
	var hour = dt.getHours();
	var mins = dt.getMinutes();
	var postfix = day + "." + month + "." + year + "_" + hour + "." + mins;
	var a = document.createElement('a');
	var data_type = 'data:application/vnd.ms-excel';
	var table_div = document.getElementById('dvData');
	var table_html = encodeURIComponent(tab_text);
	a.href = data_type + ', ' + table_html;
	a.download = 'adpia_ma_giam_gia_' + postfix + '.xls';
	a.click();
}

function deeplinkRedirect(url) {
	window.open(url, '_blank');
}

function copyShortLink(url) {
	showLoading();
	$.ajax({
		url: 'https://newpub.adpia.vn/newpub/short-link',
		method: 'POST',
		data: {_token: 'HCvnZ3qDoWzE0YXNACboKT99kcqCYZRNttOO24fO', url: url},
		success: function (data) {

			if (data && data.status === 200) {
				copyToClipboard(data.data);
			}
			hideLoading();
		},
		error: function (e) {
			toastr.error('Có lỗi xảy ra, xin vui lòng thử lại sau!');
			hideLoading();
		}
	});
}

$(".newpub-discount-code-merchant-filter").on('click', '.filter-merchant-button', function(event) {
    $('.loading').show();
    $(".filter-merchant-button").removeClass('filter-merchant-active');
    if($(this).attr('data-mid') != 'all') {
        $(this).addClass('filter-merchant-active');
    } else {
        $(".filter-merchant-button").addClass('filter-merchant-active');
    }

    f_mid = $(this).attr('data-mid');
    var customLength = 0;

    getDiscountCodeByPage(1);

    if(f_end_date != null) {
        for(var k = 0; k < couponTotalArray.length; k++) {
            var edArr = couponTotalArray[k]['end_date'].split('/');
            var c_ed = edArr[2] + edArr[1] + edArr[0];

            if(f_mid != 'all') {
                if(c_ed <= f_end_date && couponTotalArray[k]['mid'] == f_mid) {
                    customLength++;
                }
            } else {
                if(c_ed <= f_end_date) {
                    customLength++;
                }
            }
        }
    } else {
        for(var k = 0; k < couponTotalArray.length; k++) {
            if(f_mid != 'all') {
                if(couponTotalArray[k]['mid'] == f_mid) {
                    customLength++;
                }
            } else {
                customLength++;
            }
        }
    }

    $(".table-paginate-overview").pagination({
        items: customLength,
        itemsOnPage: c_limit,
        displayedPages: 4,
        cssStyle: 'light-theme',
        prevText: '&laquo',
        nextText: '&raquo',
        onPageClick : function(pageNumber){
            getDiscountCodeByPage(pageNumber);
        }
    });

    $('.loading').hide();
});
</script>
    <script type="text/javascript">
	function jsonp(url, callback) {
	    var callbackName = 'jsonp_callback_' + Math.round(100000 * Math.random());
	    window[callbackName] = function(data) {
	        delete window[callbackName];
	        document.body.removeChild(script);
	        callback(data);
	    };

	    var script = document.createElement('script');
	    script.src = url + (url.indexOf('?') >= 0 ? '&' : '?') + 'callback=' + callbackName;
	    document.body.appendChild(script);
	}

	function callApiProxy(url, callback) {
		// $.getJSON('https://www.whateverorigin.org/get?url=' + encodeURIComponent(url) + '&callback=?', function(data){
		// 	callback(JSON.parse(data.contents));
		// });
		$.ajax({
				url: url,
				type: 'get',
				crossDomain: true,
				data: {},
				success: function (data) {
					callback(data);
				}
			});
	}

	function addCommas(nStr) {
		nStr += '';
		x = nStr.split('.');
		x1 = x[0];
		x2 = x.length > 1 ? '.' + x[1] : '';
		var rgx = /(\d+)(\d{3})/;
		while (rgx.test(x1)) {
			x1 = x1.replace(rgx, '$1' + ',' + '$2');
		}
		return x1 + x2;
	}

	function get_shopee_promo_code_api() {
		var tmpArray = [];
		var url = "https://data.polyxgo.com/api/v1/datax/shopee_vouchers";
		callApiProxy(url, function(res) {
			var data = JSON.parse(res.value);
			data.forEach(function(ev, index) {
				ev.vouchers.forEach(function(ev2, index2) {
					let mid = "shopee";
					let title = "";
					let discount = "";
					if(ev2.coin_percentage) {
						title += 'Hoàn ' + ev2.coin_percentage + '%';
					} else if(ev2.discount_percentage) {
						title += 'Giảm ' + ev2.discount_percentage + '%';
					}

					if(!title) {
						if(ev2.coin_cap) {
							title += 'Hoàn ' + addCommas(ev2.coin_cap) + ' xu';
						} else if(ev2.discount_value) {
							title += 'Giảm ' + addCommas(ev2.discount_value / 10000) + 'đ';
						}
					}

					if(!title) {
						title = 'Voucher';
					}

					if(ev2.coin_cap) {
						discount += addCommas(ev2.coin_cap) + ' xu';
					} else if(ev2.discount_value) {
						discount += addCommas(ev2.discount_value / 10000) + 'đ';
					}

					if(!discount) {
						if(ev2.coin_percentage) {
							discount += ev2.coin_percentage + '%';
						} else if(ev2.discount_percentage) {
							discount += ev2.discount_percentage + '%';
						}
					}

					if(!discount) {
						discount = '0đ';
					}

					let desc = ev2.usage_terms;
					var date = new Date(ev2.end_time * 1000);
					let end_date = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate()) + '/' + (date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '/' + date.getFullYear();
					let code = ev2.voucher_code != 'POLYXGO' ? ev2.voucher_code : '';
					let url = 'https://shopee.vn/search?promotionId='+ev2.promotionid+'&signature='+ev2.signature+'&voucherCode='+ev2.voucher_code;
					tmpArray.push({mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url)});
				});
			});

			if(tmpArray) {
				if($(".newpub-discount-code-merchant-box").html().indexOf('data-mid="shopee"') == -1) {
					let html = '<div class="newpub-discount-code-merchant-item filter-merchant-button filter-merchant-active" data-mid="shopee"><img src="https://img.adpia.vn/adpia/logo/shopee.png"></div>';

					$(".newpub-discount-code-merchant-box").append(html);
				}
			}

			var tmp = JSON.stringify(tmpArray);
			tmp = tmp.substring(1, tmp.length - 1);
			var result = tmp + ",";
			shopeeTotalArray += result;
		});
	}

	function get_lazada_promo_code_api() {
		var tmpArray = [];
		var url = "https://data.polyxgo.com/api/v1/datax/lazada_vouchers";
		callApiProxy(url, function(res) {
			var data = JSON.parse(res.value);
			data.forEach(function(ev, index) {
				ev.vouchers.forEach(function(ev2, index2) {
					let mid = "lazada";
					let title = "Giảm "+addCommas(ev2.amount)+(ev2.amount < 1000 ? '%' : 'đ');
					let desc = "Mã giảm giá lazada "+addCommas(ev2.amount)+(ev2.amount < 1000 ? '%' : 'đ')+" đơn hàng từ "+addCommas(ev2.min_spend)+(ev2.min_spend < 1000 ? '%' : 'đ')+" đặt mua sản phẩm tại "+ev2.shop_name;
					let discount = addCommas(ev2.amount)+(ev2.amount < 1000 ? '%' : 'đ');

					var date = new Date(parseInt(ev2.end_time));
					let end_date = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate()) + '/' + (date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '/' + date.getFullYear();
					let code = ev2.code != 'POLYXGO' ? ev2.code : '';
					let url = ev2.link;
					tmpArray.push({mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url)});
				});
			});

			if(tmpArray) {
				if($(".newpub-discount-code-merchant-box").html().indexOf('data-mid="lazada"') == -1) {
					let html = '<div class="newpub-discount-code-merchant-item filter-merchant-button filter-merchant-active" data-mid="lazada"><img src="https://ac.adpia.vn/upload/images/lazada.png"></div>';

					$(".newpub-discount-code-merchant-box").append(html);
				}
			}

			var tmp = JSON.stringify(tmpArray);
			tmp = tmp.substring(1, tmp.length - 1);
			var result = tmp + ",";
			lazadaTotalArray += result;
		});
	}

	function get_sendo_promo_code_api() {
		var tmpArray = [];
		var url = "https://data.polyxgo.com/api/v1/datax/sendo_vouchers";
		callApiProxy(url, function(res) {
			var data = JSON.parse(res.value);
			data.forEach(function(ev, index) {
				ev.vouchers.forEach(function(ev2, index2) {
					let mid = "sendo";
					let title = ev2.voucher_type == "cashback" ? "Hoàn " : "Giảm " + addCommas(ev2.discount);
					title += ev2.type == "percent" ? "%" : "đ";
					let desc = "";
					if(ev2.voucher_type == "cashback") {
						desc += "Mã hoàn tiền sendo ";
					} else {
						desc += "Mã giảm giá sendo ";
					}
					desc += addCommas(ev2.discount) + " đơn hàng từ ";
					desc += addCommas(ev2.min_order) + (ev2.type == "percent" ? "%" : "đ");
					let discount = addCommas(ev2.discount) + (ev2.type == "percent" ? "%" : "đ");

					var date = new Date(parseInt(ev2.end_time * 1000));
					let end_date = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate()) + '/' + (date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '/' + date.getFullYear();
					let code = ev2.code != 'POLYXGO' ? ev2.code : '';
					let url = ev2.link;
					tmpArray.push({mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url)});
				});
			});

			if(tmpArray) {
				if($(".newpub-discount-code-merchant-box").html().indexOf('data-mid="sendo"') == -1) {
					let html = '<div class="newpub-discount-code-merchant-item filter-merchant-button filter-merchant-active" data-mid="sendo"><img src="https://ac.adpia.vn/upload/images/sendo.png"></div>';

					$(".newpub-discount-code-merchant-box").append(html);
				}
			}

			var tmp = JSON.stringify(tmpArray);
			tmp = tmp.substring(1, tmp.length - 1);
			var result = tmp + ",";
			sendoTotalArray += result;
		});
	}

	function get_tiki_promo_code_api() {
		var tmpArray = [];
		var url = "https://data.polyxgo.com/api/v1/datax/tiki_vouchers";
		callApiProxy(url, function(res) {
			var data = JSON.parse(res.value);
			data.forEach(function(ev, index) {
				ev.vouchers.forEach(function(ev2, index2) {
					let mid = "tiki";
					let title = "Giảm " + addCommas(ev2.discount) + (ev2.discount < 1000 ? '%' : 'đ');
					let desc = ev2.description;
					let discount = addCommas(ev2.discount) + (ev2.discount < 1000 ? '%' : 'đ');

					var date = new Date(parseInt(ev2.end_time * 1000));
					let end_date = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate()) + '/' + (date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '/' + date.getFullYear();
					let code = ev2.code != 'POLYXGO' ? ev2.code : '';
					let url = ev2.link;
					tmpArray.push({mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url)});
				});
			});

			if(tmpArray) {
				if($(".newpub-discount-code-merchant-box").html().indexOf('data-mid="tiki"') == -1) {
					let html = '<div class="newpub-discount-code-merchant-item filter-merchant-button filter-merchant-active" data-mid="tiki"><img src="https://ac.adpia.vn/upload/images/tiki_logo.png"></div>';

					$(".newpub-discount-code-merchant-box").append(html);
				}
			}

			var tmp = JSON.stringify(tmpArray);
			tmp = tmp.substring(1, tmp.length - 1);
			var result = tmp + ",";
			tikiTotalArray += result;
		});
	}

	function get_fahasa_promo_code_api() {
		var tmpArray = [];
		var url = "https://data.polyxgo.com/api/v1/datax/fahasa_vouchers";
		callApiProxy(url, function(res) {
			var data = JSON.parse(res.value);
			data.forEach(function(ev, index) {
				ev.vouchers.forEach(function(ev2, index2) {
					let mid = "fahasa";
					let title = "Giảm " + addCommas(ev2.amount) + (ev2.amount < 1000 ? '%' : 'đ');
					let desc = ev2.condition;
					let discount = addCommas(ev2.amount) + (ev2.amount < 1000 ? '%' : 'đ');

					let end_date = ev2.end_time.substring(0, 10);
					let code = ev2.code != 'POLYXGO' ? ev2.code : '';
					let url = ev2.link;
					tmpArray.push({mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url)});
				});
			});

			if(tmpArray) {
				if($(".newpub-discount-code-merchant-box").html().indexOf('data-mid="fahasa"') == -1) {
					let html = '<div class="newpub-discount-code-merchant-item filter-merchant-button filter-merchant-active" data-mid="fahasa"><img src="https://ac.adpia.vn/upload/images/logo_fahasha.png"></div>';

					$(".newpub-discount-code-merchant-box").append(html);
				}
			}

			var tmp = JSON.stringify(tmpArray);
			tmp = tmp.substring(1, tmp.length - 1);
			var result = tmp + ",";
			fahasaTotalArray += result;
		});
	}

	function exportXML() {
		showLoading();
		$.ajax({
			url: 'https://newpub.adpia.vn/newpub/export-xml',
			type: 'POST',
			data: {
				_token: 'HCvnZ3qDoWzE0YXNACboKT99kcqCYZRNttOO24fO',
				data: currentArray
			}
		})
		.done(function(res) {
			var data = new Blob([res.res], {type: 'text/xml'});

			saveAs(data, 'mgg_'+(new Date().getTime())+'.xml');

			hideLoading();
		});
	}

	function handleShowExportFile(e) {
		e.preventDefault();
		if($(".export-file-button").css('display') == "none") {
			$(".export-file-button").css('display', 'block');
		} else {
			$(".export-file-button").css('display', 'none');
		}
	}
</script>
        <VIT:Position runat="server" ID="psContent"></VIT:Position>

    <%--<div class="adpia_minishop" aff-id=A100047119 top-slide="true" under-slide="true" flash-sale="true" discount-code="true" hot-deal="true" cpo-offer="true"></div>
    <div class="adpia_coupon_list" shopee="true" lazada="true" klook="true" fahasa="true"></div>
    <div class="adpia_categories_list" data-list="[DT,TBDT,MT,CMR,HB,FA,HL,GD,MB,TG,SK,BS,SP,DL]"></div>
    <script src="https://event.adpia.vn/js/minishop/adpia_minishop.js" type="text/javascript"></script>--%>
    
    <h2 title="Sản phẩm sale off mạnh giá shock">Sản phẩm Giảm giá cực hot trên Shopee</h2>
    <p class="jumbotron" style="padding:20px">Sở hữu nhiều mặt hàng đa dạng về mẫu mã và chủng loại với giá cả phải chăng, <a href="https://truyencuoi.top/san-hang-khuyen-mai-vit-contact">truyencuoi.top</a> luôn mong muốn đem đến cho khách hàng sự hài lòng, sự tin tưởng cũng như sự quý mến.
        <br />
Hãy thoải mái mua sắm và tận hưởng điều đó nhé!</p>
    <div id="flashsale" class="row">

    </div>
    <script>
        $.ajax({
            url: 'https://front.adpia.vn/ApiShopee/getDataflashSale?affiliate_id=A100047119',
            method: "GET",
            dataType: 'json',
            success: function (res) {
                var i = 0;
                if(res[i])
                {
                    var start = moment(res[0].start_time).format('DD/MM/YYYY');
                    var end = moment(res[0].end_time).format('DD/MM/YYYY');

                    do {
                        var item = res[i];
                        var product = "<div class='col-lg-3 col-md-3 col-sm-6 col-xs-12' style='margin-bottom: 20px'>";
                        product += "<a href='" + item.link_click + "' target='_blank' title='" + item.name + "'>";
                        product += "<div class='item'><div class='wrap_item_flash_sale_slide'>";
                        product += "<img src='" + item.image + "' alt='" + item.name + "' style='object-fit: cover; max-height: 269px; width:100%'>";
                        product += "<div style='color: #4a5568; font-weight: 700; font-size: .875rem; padding: 1rem 1rem 0 1rem; height: 60px;'>" + (item.name.length > 50 ? item.name.substring(0,50) + "..." : item.name) + "</div>";
                        product += "<div style='padding: 0 1rem; color: #c1c1c1; text-decoration: line-through;'>";
                        product += "<p style='margin: 0;'>" + item.price_before_discount + "<span style='font-size: .7rem;'> vnd</span></p>";
                        product += "</div>";
                        product += "<div class='price_sp_flash_sale'><p>" + item.price + "<span style='font-size: 1rem;'> vnd</span></p></div>";
                        product += "<div style='padding: 0 1rem; background: linear-gradient(90deg, #ffba00 0%, #ff6c00 100%); text-align: center;'><div style='color: #fff; height: 40px; line-height: 40px;'>MUA NGAY</div></div>";
                        product += "<div class='shopee-item-card__badge-wrapper shopee-fs-item-card__badge-wrapper'>";
                        product += "<div class='shopee-badge shopee-badge--fixed-width shopee-badge--promotion'>";
                        product += "<div class='shopee-badge--promotion__label-wrapper shopee-badge--promotion__label-wrapper--vi'>";
                        product += "<div class='percent'>" + item.discount + "</div>";
                        product += "<span class='shopee-badge--promotion__label-wrapper__off-label shopee-badge--promotion__label-wrapper__off-label--vi'>Giảm</span>";
                        product += "</div>";
                        product += "</div>";
                        product += "</div>";
                        product += "</div>";
                        product += "</a></div>";
                        $("#flashsale").append(product);
                        i = i+1;
                    }
                    while (res[i]);
                }
            },
            error: function (err) {
                console.log(err);
            },
            fail: function (err) {
                console.log(err);
            }
        });
    </script>
    
</asp:Content>