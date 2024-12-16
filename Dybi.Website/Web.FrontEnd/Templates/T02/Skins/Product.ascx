<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="container">
<!-- Portfolio Item Row -->
    <div id="product-single">
	    <!-- Product -->
	    <div class="prd-detail">
            <h1 class="product-name prd-title" title="<%=Data.Title%>"><%=Data.Title%></h1>
            <div class="row">
		        <!-- Product Images Carousel -->
		        <div class="col-lg-5 col-md-5 col-sm-5 product-single-image">
			        <div id="product-slider">
				        <ul class="slides">
                            <li id="product<%=Data.Id %>">
                                <img class="cloud-zoom" data-large="<%=HREF.DomainStore + Data.Image.FullPath%><%=Data.Image.FileExtension != ".webp" ? ".webp" : "" %>" src="<%=!string.IsNullOrEmpty(Data.ImageName) ? HREF.DomainStore + Data.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=Title %>"/>
						        <a class="fullscreen-button" href="<%=HREF.DomainStore + Data.Image.FullPath%>">
							        <div class="product-fullscreen">
								        <i class="icons icon-resize-full-1"></i>
							        </div>
						        </a>
					        </li>						                        
				        </ul>
			        </div>
			        <div id="product-carousel">
				        <ul class="slides">
                            <li>
						        <a class="fancybox" rel="product-images" href="<%=HREF.DomainStore + Data.Image.FullPath%>"></a>
                                <picture>
						          <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
						          <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                                    <img data-large="<%=HREF.DomainStore + Data.Image.FullPath%>" src="<%=!string.IsNullOrEmpty(Data.ImageName) ? HREF.DomainStore + Data.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=Title %>"/>
                                </picture> 
					        </li>
                            <%foreach (var image in this.Images)
                                           {%>
                                    <li>
						                <a class="fancybox" rel="product-images" href="<%=HREF.DomainStore + image.FullPath%>"></a>
                                        <picture>
		                                    <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                                    <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                                            <img data-large="<%=HREF.DomainStore + image.FullPath%>" src="<%=HREF.DomainStore + image.FullPath%>"/>
                                        </picture>
					                </li>
                                <%} %>
				        </ul>
				        <div class="product-arrows">
					        <div class="left-arrow">
						        <i class="icons icon-left-dir"></i>
					        </div>
					        <div class="right-arrow">
						        <i class="icons icon-right-dir"></i>
					        </div>
				        </div>
			        </div>
		        </div>
		        <!-- /Product Images Carousel -->			
								
		        <div class="col-lg-7 col-md-7 col-sm-7 product-single-info">
                    <div class="product-to-buy">
                        <%if(!string.IsNullOrEmpty(Data.Code)){ %>
                        <h4>Mã: <%=Data.Code%></h4>
                        <%} %>
                        <p><%=Data.Brief.Replace("\n","<br />")%></p>
                    </div>
                    <hr />	
                    <%if(Attributes.Count > 0){ %>
                    <table class="table table-striped table-bordered table-hover">
                    <%foreach(var attribute in Attributes){ %>
                        <%if(!string.IsNullOrEmpty(attribute.ValueName)){ %>
                            <tr>
                                <td>
                                    <%=attribute.Name %>
                                </td>
                                <td>
                                    <%=attribute.ValueName == "True" ? "Có" : attribute.ValueName == "False" ? "Không" : attribute.ValueName %>
                                </td>
                            </tr>
                        <%} %>
                    <%} %>
                    </table>
                    <hr />	
                    <%} %>
                    <div class="nutmuahang prd-buy uk-clearfix">
                        <div class="wrap-price" style="text-align:left">
                            <span class="prd-price">
                            <%if(Data.DiscountType > 0) {%>
                                <del>
                                    <%=Data.Price.ToString("N0") %> ₫
                                </del>
                            <%} %>
                            <%if(Data.Price > 0) {%>
                                <%=Data.PriceAfterDiscount.ToString("N0") %> ₫
                            <%} else {%>
                                Liên hệ
                            <%} %>
                            </span>
                        </div>
                        <label>Số lượng:</label> <input type="number" value="30" id="txtProductQuantity" class="form-control" min="30" style="width: 100px;display: inline;">
                        <div class=" prd-property uk-flex uk-flex-bottom" style="margin-top:20px">
                        <a class="btn-add-to-cart ajax-addtocart" onclick="AddProductToCart(true)" href="javascript:void(0);">MUA NGAY</a></div>
                    </div>	
		        </div>							
	
                <%if (this.Relatieds.Count > 0)
                {%>
                <div class="col-lg-12 col-md-12 col-sm-12">
                    <div class="prd-infor">
                    <h2 class="main-title" title="Sản phẩm tương tự <%=Data.Title %>">Gợi ý khác cho bạn</h2>
                        <div class="row">
                        <%foreach (var r in Relatieds)
                        { %>
                            <div class="col-md-2 col-sm-3 col-xs-4">
                                <div class="product">
                                    <div></div>
                                    <a class="image img-scaledown" href="<%=HREF.LinkComponent(HREF.CurrentComponent,r.Title.ConvertToUnSign(), r.Id, SettingsManager.Constants.SendProduct, r.Id)%>" title="<%=r.Title%>">
                                        <picture>
						                    <source srcset="<%=HREF.DomainStore + r.Image.FullPath%>.webp" type="image/webp">
						                    <source srcset="<%=HREF.DomainStore + r.Image.FullPath%>" type="image/jpeg"> 
                                            <img style="max-height:100%; max-width:100%;display: inline;" src="<%=HREF.DomainStore + r.Image.FullPath%>" alt="<%=Title %>"/>
                                        </picture>
                                    </a>
                                    <div class="info">
                                        <a class="title" href="<%=HREF.LinkComponent(HREF.CurrentComponent,r.Title.ConvertToUnSign(), r.Id, SettingsManager.Constants.SendProduct, r.Id)%>" title="<%=r.Title%>">
                                            <%=r.Title.Length > 30 ? r.Title.Substring(0, 30) + "..." : r.Title%>
                                        </a>
                                        <div class="price uk-flex uk-flex-middle uk-flex-space-between">
                                            <%if(r.DiscountType > 0) {%>
                                            <del>
                                                <%=r.Price.ToString("N0") %> <sup>đ</sup>
                                            </del>
                                            <%} %>
                                            <span class='sale-price'>
                                                <%if(r.Price > 0) {%>
                                                    <%=r.PriceAfterDiscount.ToString("N0") %> 
                                                <%} else {%>
                                                    Liên hệ
                                                <%} %>
                                            </span>
                                        </div>
                                    </div>
                                </div>   
                            </div>
                        <%} %>
                        </div>
                    </div>
                </div>
                <%}%>

                <div class="col-md-12 panel-body">
                    <div class="prd-infor">
                        <h2 class="main-title">Thông tin sản phẩm</h2>
                        <div class="content">
                            <%=Data.Content%>
                        </div> 
                        <div class="tag">
                            Tag: 
                            <%for (int i = 0; i < Tags.Count; i++ )
                            {%>
                                <%=Tags[i]%>,  
                            <%} %>    
                        </div>
                    </div>
                </div>
            </div>
        </div>
	    <!-- /Product -->				
    </div>

    <!-- Review -->
    <div class="review-content">
        <div class="comment">
            <div class="comment-box">
                <div class="comment-box__content">

                    <p class="comment-box__title">Đánh giá và nhận xét</p>
                    <div class="comment-box__percent">
                        <div class="rating-score">
                            <p class="score-big"><%=Reviews.Count == 0 ? 5 : Reviews.Sum(e => e.Vote) / Reviews.Count %>/5</p>
                            <p class="count-comment"><%=Reviews.Count%> đánh giá</p>
                        </div>        
                        <ul class="rating-list">
                            <li>
                                <div class="progress">
                                    <div class="progress-bar" style=" width:<%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 5) * 100 / Reviews.Count  %>%" role="progressbar" aria-valuenow="<%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 5) * 100 / Reviews.Count %>" aria-valuemin="0" aria-valuemax="100">
                                        <%=Reviews.Count(e => e.Vote == 5)%>
                                    </div> 
                                </div>
                                <div class="rating-list__star">
                                    <span class="rating" style="">
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <div class="rating--active" style="width:100%;">
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                        </div>
                                    </span>            
                                    <p class="rating-list__star-percent">100%</p>
                                </div>
                            </li>
                            <li>
                                <div class="progress">
                                    <div class="progress-bar" style="width: <%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 4) * 100 / Reviews.Count %>%" role="progressbar" aria-valuenow="<%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 4) * 100 / Reviews.Count %>" aria-valuemin="0" aria-valuemax="100">
                                        <%=Reviews.Count(e => e.Vote == 4)%>
                                    </div> 
                                </div>
                                <div class="rating-list__star">
                                    <span class="rating" style="">
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <div class="rating--active" style="width:100%;">
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                        </div>
                                    </span>            
                                    <p class="rating-list__star-percent">80%</p>
                                </div>
                            </li>
                            <li>
                                <div class="progress">
                                    <div class="progress-bar" style="width: <%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 3) * 100 / Reviews.Count %>%" role="progressbar" aria-valuenow="<%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 3) * 100 / Reviews.Count %>" aria-valuemin="0" aria-valuemax="100">
                                        <%=Reviews.Count(e => e.Vote == 3)%>
                                    </div> 
                                </div>
                                <div class="rating-list__star">
                                    <span class="rating" style="">
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <div class="rating--active" style="width:100%;">
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                        </div>
                                    </span>            
                                    <p class="rating-list__star-percent">60%</p>
                                </div>
                            </li>
                            <li>
                                <div class="progress">
                                    <div class="progress-bar" style="width: <%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 2) * 100 / Reviews.Count %>%" role="progressbar" aria-valuenow="<%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 2) * 100 / Reviews.Count %>" aria-valuemin="0" aria-valuemax="100">
                                        <%=Reviews.Count(e => e.Vote == 2)%>
                                    </div> 
                                </div>
                                <div class="rating-list__star">
                                    <span class="rating" style="">
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <div class="rating--active" style="width:100%;">
                                            <i class="glyphicon glyphicon-star"></i>
                                            <i class="glyphicon glyphicon-star"></i>
                                        </div>
                                    </span>            
                                    <p class="rating-list__star-percent">40%</p>
                                </div>
                            </li>
                            <li>
                                <div class="progress">
                                    <div class="progress-bar" style="width: <%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 1) * 100 / Reviews.Count %>%" role="progressbar" aria-valuenow="<%=Reviews.Count == 0 ? 0 : Reviews.Count(e => e.Vote == 1) * 100 / Reviews.Count %>" aria-valuemin="0" aria-valuemax="100">
                                        <%=Reviews.Count(e => e.Vote == 1)%>
                                    </div> 
                                </div>
                                <div class="rating-list__star">
                                    <span class="rating" style="">
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <i class="glyphicon glyphicon-star-empty"></i>
                                        <div class="rating--active" style="width:100%;">
                                            <i class="glyphicon glyphicon-star"></i>
                                        </div>
                                    </span>            
                                    <p class="rating-list__star-percent">20%</p>
                                </div>
                            </li>
                        </ul>    
                    </div>

                    <p class="comment-box__title">Bình luận</p>
                    <div class="comment-box__list">
                        <%foreach(var review in Reviews){ %>
                        <div class="comment-item">
                            <div class="comment-item__top">
                                <div class="comment-item__info">
                                    <div class="comment-user__info ">
                                        <div class="user-info__name">
                                            <%=review.Name %>
                                            <span class="rating" style="">
                                                <i class="glyphicon glyphicon-star-empty"></i>
                                                <i class="glyphicon glyphicon-star-empty"></i>
                                                <i class="glyphicon glyphicon-star-empty"></i>
                                                <i class="glyphicon glyphicon-star-empty"></i>
                                                <i class="glyphicon glyphicon-star-empty"></i>
                                                <div class="rating--active" style="width:100%;">
                                                    <%for(int i = 1; i <= review.Vote; i++){ %>
                                                    <i class="glyphicon glyphicon-star"></i>
                                                    <%} %>
                                                </div>
                                            </span>
                                        </div>
                                    </div>
                                    <%if(review.IsBuyer){ %>
                                    <div class="d-flex align-items-center">
                                        <div class="me-3">
                                            <span class="check-buyked">Đã mua hàng tại <%=Component.Company.NickName %></span>
                                        </div>
                                    </div>
                                    <%} %>
                                </div>
                            </div>
                            <div class="comment-item__content">
                                <p>
                                    <%= review.Comment%>
                                </p>    
                            </div>
                            <%if(review.Images != null && review.Images.Count > 0){ %>
                            <div class="comment-item__imgs">
                                <%foreach(var image in review.Images){ %>
                                <picture>
		                            <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                            <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                                    <img src="<%=HREF.DomainStore + image.FullPath%>"/>
                                </picture>
                                <%} %>
                            </div>
                            <%} %>
                            <%if(review.Replies != null && review.Replies.Count > 0){ %>
                            <div class="comment-childs">
                                <%foreach(var reply in review.Replies){ %>
                                <div class="comment-item">
                                    <div class="comment-item__top">
                                        <div class="comment-item__info">
                                            <div class="comment-user__info mb-0  ">
                                                <div class="user-info__name">
                                                    <%=reply.Name %>
                                                </div>
                                            </div>
                                            <div class="comment-item__content">
                                                <%=reply.Comment %>
                                            </div>
                                        </div>
                                    </div>
                                </div>   
                                <%} %> 
                            </div>
                            <%} %>
                        </div>    
                        <%} %>
                    </div>
                                        
                    <p class="comment-box__title mt-4">Đánh giá và bình luận</p>
                    <div class="comment-box__form">
                        <div class="formComment formValidation">
                            <div class="flex">
                                <div class="form-validate">
                                    <div class="user-rating">
                                        <div class="rating" m-checked="Vui lòng đánh giá">
                                            <input class="star star-5" id="star-5" type="radio" value="5" name="rate">
                                            <label class="star star-5" for="star-5"></label>
                                            <input class="star star-4" id="star-4" type="radio" value="4" name="rate">
                                            <label class="star star-4" for="star-4"></label>
                                            <input class="star star-3" id="star-3" type="radio" value="3" name="rate">
                                            <label class="star star-3" for="star-3"></label>
                                            <input class="star star-2" id="star-2" type="radio" value="2" name="rate">
                                            <label class="star star-2" for="star-2"></label>
                                            <input class="star star-1" id="star-1" type="radio" value="1" name="rate">
                                            <label class="star star-1" for="star-1"></label>
                                            </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6" style="margin-bottom:15px">
                                    <input id="reviewName" type="text" class="input_all" name="name" placeholder="Họ và tên" required>
                                </div>
                                <div class="col-sm-6" style="margin-bottom:15px">
                                    <input id="reviewPhone" type="text" class="input_all" name="phone" placeholder="Số điện thoại" required>
                                </div>
                            </div>
                            <div class="form-validate mb-3">
                                <textarea id="reviewComment" name="content" placeholder="Bình luận" m-required="Hãy để lại bình luận" cols="26" required></textarea>
                            </div>
                            <div class="formComment__action">
                                <button id="btnReview" class="btn btn--orange" type="button" onclick="AddReview()">Bình luận ngay</button>
                                <%--<label for="formComment__file" class="formComment__label formComment__label--upload">
                                    <i class="fa fa-upload" aria-hidden="true"></i>
                                    <span onclick="$("input[id='formComment__file']").focus().click();">Upload</span>
                                    <input type="file" id="formComment__file" name="img" multiple="" input-file="">
                                </label>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script>
    /* Single Product Page */
    function singleProduct() {
        /* Product Images Carousel */
        $('#product-carousel').flexslider({
            animation: "slide",
            controlNav: false,
            animationLoop: false,
            directionNav: false,
            slideshow: false,
            itemWidth: 80,
            itemMargin: 0,
            start: function (slider) {

                setActive($('#product-carousel li:first-child img'));
                slider.find('.right-arrow').click(function () {
                    slider.flexAnimate(slider.getTarget("next"));
                });

                slider.find('.left-arrow').click(function () {
                    slider.flexAnimate(slider.getTarget("prev"));
                });

                slider.find('img').click(function () {
                    var large = $(this).attr('data-large');
                    setActive($(this));
                    $('#product-slider img').fadeOut(300, changeImg(large, $('#product-slider img')));
                    $('#product-slider a.fullscreen-button').attr('href', large);
                });

                function changeImg(large, element) {
                    var element = element;
                    var large = large;
                    setTimeout(function () { startF() }, 300);
                    function startF() {
                        element.attr('src', large)
                        element.attr('data-large', large)
                        element.fadeIn(300);
                    }

                }

                function setActive(el) {
                    var element = el;
                    $('#product-carousel img').removeClass('active-item');
                    element.addClass('active-item');
                }

            }

        });



        /* FullScreen Button */
        $('a.fullscreen-button').click(function (e) {
            e.preventDefault();
            var target = $(this).attr('href');
            $('#product-carousel a.fancybox[href="' + target + '"]').trigger('click');
        });


        /* Cloud Zoom */
        $(".cloud-zoom").imagezoomsl({
            zoomrange: [3, 3]
        });


        ///* FancyBox */
        //$(".fancybox").fancybox();


    }


    /* Set Carousels */
    function installCarousels() {

        $('.owl-carousel').each(function () {

            /* Max items counting */
            var max_items = $(this).attr('data-max-items');
            var tablet_items = max_items;
            if (max_items > 1) {
                tablet_items = max_items - 1;
            }
            var mobile_items = 1;


            /* Install Owl Carousel */
            $(this).owlCarousel({

                items: max_items,
                pagination: false,
                itemsDesktop: [1199, max_items],
                itemsDesktopSmall: [1000, max_items],
                itemsTablet: [920, tablet_items],
                itemsMobile: [560, mobile_items],
            });


            var owl = $(this).data('owlCarousel');

            /* Arrow next */
            $(this).parent().parent().find('.icon-left-dir').click(function (e) {
                owl.prev();
            });

            /* Arrow previous */
            $(this).parent().parent().find('.icon-right-dir').click(function (e) {
                owl.next();
            });

        });

    }
</script>

<script>
    $(document).ready(function () {
        singleProduct(); // Cloud Zoom
        installCarousels();
    });
</script>

<!-- /Product Tabs -->
<script type="text/javascript">
    // Send the rating information somewhere using Ajax or something like that.
    function AddProductToCart(go) {
        var productQuantity = $("#txtProductQuantity").val();
        if (parseInt(productQuantity, 10) < parseInt(<%=Data.SaleMin%>, 10))
        {
            alert('Số lượng mua ít nhất là <%=Data.SaleMin%>, bạn không thể mua ' + productQuantity + ' sản phẩm');
        }
        else {
            $.ajax({
                type: "POST",
                url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
                data: JSON.stringify({ productId: '<%=Data.Id%>', quantity: productQuantity, properties: '', addonIds: '' }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != "" && go == true) {
                        location.href = '<%=HREF.BaseUrl %>/cart/gio-hang';
                    }
                }
            });
        }
    }
</script>	

<script>
    function AddReview() {
        
        var reviewName = $('#reviewName').val();
        var reviewPhone = $('#reviewPhone').val();
        var reviewComment = $('#reviewComment').val();
        var reviewVote = $('input[name="rate"]:checked').val();
        if (!reviewVote) reviewVote = 5;
        if (reviewName && reviewPhone && reviewComment) {
            $('#btnReview').prop('disabled', true);
            $.ajax({
                type: "POST",
                url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddReview",
                data: JSON.stringify({ itemId: '<%=Data.Id%>', name: reviewName, phone: reviewPhone, comment: reviewComment, vote: reviewVote }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#btnReview').prop('disabled', false);
                    alert('Đã gửi đánh giá thành công, Vui lòng chờ kiểm duyệt trong vài phút.');
                    $('#reviewName').val('');
                    $('#reviewPhone').val('');
                    $('#reviewComment').val('');
                }
            });
        }
        else {
            alert('Vui lòng nhập đầy đủ thông tin.');
        }
     }
</script>

<script type="application/ld+json">
{
    "@context": "https://schema.org/",
    "@type": "Product",
    "name": "<%=Data.Title %>",
    "image": [
        "<%=HREF.DomainStore + Data.Image.FullPath%><%=Data.Image.FileExtension == ".webp" ? "" : ".webp" %>"
        <%foreach (var image in this.Images)
        {%>
            ,"<%=HREF.DomainStore + image.FullPath%><%=image.FileExtension == ".webp" ? "" : ".webp" %>"
        <%} %>
        ],
    "description": "<%=Data.Brief.Replace("\"","") %>",
    <%if(!string.IsNullOrEmpty(Data.GetAttribute("Brand"))){ %>
        "brand": {
            "@type": "Brand",
            "name": "<%=Data.GetAttribute("Brand") %>"
        },
    <%} %>
    <%if(Reviews.Count > 0){ %>
    "aggregateRating" : {
        "@type": "AggregateRating",
        "ratingValue": "<%=Reviews.Sum(e => e.Vote) / Reviews.Count %>",
        "reviewCount": "<%=Reviews.Count %>"
        },
    "review": [
        <%for(int i = 0; i < Reviews.Count; i++){ %>
        <%= i == 0 ? "" : "," %>
        {
            "@type": "Review",
            "author": 
            {
                "@type": "Person",
                "name": "<%=Reviews[i].Name %>"
            },
            "datePublished": "<%=Reviews[i].Date %>",
            "reviewBody": "<%=Reviews[i].Comment %>",
            "reviewRating": {
            "@type": "Rating",
            "bestRating": "5",
            "ratingValue": "<%=Reviews[i].Vote %>",
            "worstRating": "1"
            }
        }
        <%} %>
    ], 
    <%} %>
    <%if(!string.IsNullOrEmpty(this.Page.MetaKeywords)){ %>
    "keywords":"<%= this.Page.MetaKeywords%>",
    <%} %>
    "offers": {
        "@type": "Offer",
        "availability": "https://schema.org/InStock",
        "price": "<%=Data.PriceAfterDiscount %>",
        "priceCurrency": "VND"
    }
}
</script>