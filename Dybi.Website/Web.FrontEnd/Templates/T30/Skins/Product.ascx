﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>


 <!-- blog section -->

<section class="product_section">
    <div class="container">
        <div class="layout_padding2">
            <h1>
                <%=Title %>
            </h1>
        </div>
        <div class="row">
            <div class="col-12 col-sm-6 col-md-5 col-lg-5 mx-auto">
                <div class="box" style="background:none">
                    <div class="img-box">
                        <div id="slider" class="flexslider">
                            <ul class="slides">
                                <li>
                                    <picture>
						                <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
						                <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                                        <img class="cloud-zoom" data-large="<%=HREF.DomainStore + Data.Image.FullPath%>" src="<%= HREF.DomainStore + Data.Image.FullPath%>" alt="<%=Title %>"/>
                                    </picture>
                                </li>
                                <%foreach(var image in Images) 
                                {%>
                                    <li>
                                        <picture>
		                                    <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                                    <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                                            <img class="cloud-zoom" data-large="<%=HREF.DomainStore + image.FullPath%>" src="<%=HREF.DomainStore + image.FullPath%>"/>
                                        </picture>     
                                    </li>
                                <%} %>
                            </ul>
                        </div>
                        <div id="carousel" class="flexslider">
                            <ul class="slides">
                                <li>
                                    <picture>
						                <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
						                <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + Data.Image.FullPath%>" alt="<%=Title %>"/>
                                    </picture>
                                </li>
                                <%foreach(var image in Images) 
                                {%>
                                    <li>
                                        <picture>
		                                    <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                                    <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                                            <img src="<%=HREF.DomainStore + image.FullPath%>"/>
                                        </picture>     
                                    </li>
                                <%} %>
                            </ul>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-6 col-md-7 col-lg-7">
                <div class="product_container" style="display:block">
                    <div class="box" style="display:block;text-align:left;margin-top:0px">
                        <div class="detail-box">
                            <p>
                                <%=Data.Brief.Replace("\n","<br />") %>
                            </p> 
                            <hr />
                                <table border="1" style="width:100%">
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
                            <h4 class="price" style="margin:30px 0px">
                                <%if(Data.DiscountType > 0) {%>
                                <del>
                                    <%=Data.Price.ToString("N0") %><span> ₫</span>
                                </del>
                                <%} %>
                                <%if(Data.Price > 0) {%>
                                    <%=Data.PriceAfterDiscount.ToString("N0") %><span> ₫</span>
                                <%} else {%>
                                    Liên hệ
                                <%} %>
                            </h4>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Số lượng:</span>
                                </div>
                                <input type="number" value="1" id="txtProductQuantity" class="form-control">
                                <%if(Data.Price > 0) {%>
                                <div class="input-group-append">
                                    <button type="button" onclick="AddToCart()" class="btn btn-warning">Chọn mua</button>  
                                </div>
                                <%} %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%if(AddOns.Count > 0){ %>
            <div class="col-12 col-md-<%= Relatieds.Count == 0 ? "12" : "6"%> mx-auto">
                <div class="addons">
                    <h4>Ưu đãi khi mua sản phẩm</h4>
                    <table class="table">
                        <%if(AddOns.Any(e => e.Price == 0)){ %>
                        <thead>
                            <tr>
                                <th scope="col" colspan="3">Tặng kèm</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach(var r in AddOns.Where(e => e.Price == 0)){ %>
                            <tr scope="row">
                                <td colspan="2">
                                    <a class="image img-scaledown" href="<%=HREF.LinkComponent(HREF.CurrentComponent,r.Title.ConvertToUnSign(), r.Id, SettingsManager.Constants.SendProduct, r.Id)%>" title="<%=r.Title%>">
                                        <picture>
						                    <source srcset="<%=HREF.DomainStore + r.Image.FullPath%>.webp" type="image/webp">
						                    <source srcset="<%=HREF.DomainStore + r.Image.FullPath%>" type="image/jpeg"> 
                                            <img style="max-height:75px; max-width:100%;display: inline;" src="<%=HREF.DomainStore + r.Image.FullPath%>" alt="<%=Title %>"/>
                                        </picture> 
                                    </a>
                                </td>
                                <td>
                                    <a target="_blank" class="addon_title" href="<%=HREF.LinkComponent(HREF.CurrentComponent,r.Title.ConvertToUnSign(), r.Id, SettingsManager.Constants.SendProduct, r.Id)%>" title="<%=r.Title%>">
                                        <%=r.Quantity%> x <%=r.Title%>
                                        
                                    </a>
                                    <br />
                                    <del>
                                        <%=r.Price.ToString("N0") %> <sup>đ</sup>
                                    </del>
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                        <%} %>
                        <%if(AddOns.Any(e => e.Price > 0)){ %>
                        <thead>
                            <tr>
                                <th scope="col">Ưu đãi</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach(var r in AddOns.Where(e => e.Price > 0)){ %>
                            <tr scope="row">
                                <td>
                                    <a class="image img-scaledown" href="<%=HREF.LinkComponent(HREF.CurrentComponent,r.Title.ConvertToUnSign(), r.Id, SettingsManager.Constants.SendProduct, r.Id)%>" title="<%=r.Title%>">
                                        <picture>
						                    <source srcset="<%=HREF.DomainStore + r.Image.FullPath%>.webp" type="image/webp">
						                    <source srcset="<%=HREF.DomainStore + r.Image.FullPath%>" type="image/jpeg"> 
                                            <img style="max-height:75px; max-width:100%;display: inline;" src="<%=HREF.DomainStore + r.Image.FullPath%>" alt="<%=Title %>"/>
                                        </picture> 
                                    </a>
                                    <a target="_blank" class="addon_title" href="<%=HREF.LinkComponent(HREF.CurrentComponent,r.Title.ConvertToUnSign(), r.Id, SettingsManager.Constants.SendProduct, r.Id)%>" title="<%=r.Title%>">
                                        <%=r.Title.Length > 30 ? r.Title.Substring(0, 30) + "..." : r.Title%>
                                    </a>
                                </td>
                                <td><%=r.Price.ToString("N0") %> <sup>đ</sup></td>
                            </tr>
                            <%} %>
                        </tbody>
                        <%} %>
                    </table>
                </div>
            </div>
            <%} %> 
            <%if(Relatieds.Count > 0){ %>
            <div class="col-12 col-md-<%= AddOns.Count == 0 ? "12" : "6"%> mx-auto">
                <div class="relatied">
                    <h4>Có thể bạn quan tâm</h4>
                    <div class="row">
                    <%foreach (var r in Relatieds) { %>
                    <div class="col-6 col-md-<%= AddOns.Count == 0 ? "2" : "3"%>">
                        <a class="image img-scaledown" href="<%=HREF.LinkComponent(HREF.CurrentComponent,r.Title.ConvertToUnSign(), r.Id, SettingsManager.Constants.SendProduct, r.Id)%>" title="<%=r.Title%>">
                            <picture>
						      <source srcset="<%=HREF.DomainStore + r.Image.FullPath%>.webp" type="image/webp">
						      <source srcset="<%=HREF.DomainStore + r.Image.FullPath%>" type="image/jpeg"> 
                            <img style="max-height:100%; max-width:100%;display: inline;" src="<%=!string.IsNullOrEmpty(r.ImageName) ? HREF.DomainStore + r.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=Title %>"/>
                        </picture> 
                        </a>
                            <a class="relatied_title" href="<%=HREF.LinkComponent(HREF.CurrentComponent,r.Title.ConvertToUnSign(), r.Id, SettingsManager.Constants.SendProduct, r.Id)%>" title="<%=r.Title%>">
                                <%=r.Title.Length > 30 ? r.Title.Substring(0, 30) + "..." : r.Title%>
                            </a>
                        <div class="price uk-flex uk-flex-middle uk-flex-space-between">
                            <%if(r.DiscountType > 0) {%>
                  <del>
                      <%=r.Price.ToString("N0") %> <sup>đ</sup>
                  </del>
                  <%} %>
                <span class='price'>
                    <%if(r.Price > 0) {%>
                        <%=r.PriceAfterDiscount.ToString("N0") %> 
                    <%} else {%>
                        Liên hệ
                    <%} %>
                </span>
                    </div>
                        
                </div>
                    <%} %>
                    </div>
                </div>
            </div>
            <%} %>
            <div class="productdetail">
                <div class="heading_container">
                    <h2>Mô tả</h2>
                </div>
                <%=Data.Content%> 

                <div class="tag">
                    <%=string.Join(", ", Tags)%> 
                </div>
            </div>
        </div>
    </div>
</section>

<section class="section-all">
    <div class="container">
        <div class="heading_container">
            <h2 class="comment-box__title">Đánh giá và nhận xét</h2>
        </div>
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
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <div class="rating--active" style="width:100%;">
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
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
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <div class="rating--active" style="width:100%;">
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
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
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <div class="rating--active" style="width:100%;">
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
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
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <div class="rating--active" style="width:100%;">
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
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
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <i class="fa fa-star-o"></i>
                            <div class="rating--active" style="width:100%;">
                                <i class="fa fa-star"></i>
                            </div>
                        </span>            
                        <p class="rating-list__star-percent">20%</p>
                    </div>
                </li>
            </ul>    
        </div>

        <%if(Reviews.Count > 0){ %>
        <div class="heading_container">
            <h2 class="comment-box__title">Bình luận</h2>
        </div>
        <div class="comment-box__list">
            <%foreach(var review in Reviews){ %>
            <div class="comment-item">
                <div class="comment-item__top">
                    <div class="comment-item__info">
                        <div class="comment-user__info ">
                            <div class="user-info__name">
                                <%=review.Name %>
                                <span class="rating" style="">
                                    <i class="fa fa-star-o"></i>
                                    <i class="fa fa-star-o"></i>
                                    <i class="fa fa-star-o"></i>
                                    <i class="fa fa-star-o"></i>
                                    <i class="fa fa-star-o"></i>
                                    <div class="rating--active" style="width:100%;">
                                        <%for(int i = 1; i <= review.Vote; i++){ %>
                                        <i class="fa fa-star"></i>
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
        <%} %>

        <div class="heading_container">
            <h2 class="comment-box__title">Đánh giá và bình luận</h2>
        </div>                               
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
                <div class="row row_flex_5">
                    <div class="col-sm-6 input-group mb-2">
                        <input id="reviewName" type="text" class="input_all" name="name" placeholder="Họ và tên" required>
                    </div>
                    <div class="col-sm-6 input-group mb-2">
                        <input id="reviewPhone" type="text" class="input_all" name="phone" placeholder="Số điện thoại" required>
                    </div>
                </div>
                <div class="form-validate mb-3">
                    <textarea id="reviewComment" name="content" placeholder="Bình luận" m-required="Hãy để lại bình luận" cols="26" required></textarea>
                </div>
                <div class="formComment__action">
                    <button id="btnReview" class="btn btn-warning" type="button" onclick="AddReview()">Bình luận ngay</button>
                    <%--<label for="formComment__file" class="formComment__label formComment__label--upload">
                        <i class="fa fa-upload" aria-hidden="true"></i>
                        <span onclick="$("input[id='formComment__file']").focus().click();">Upload</span>
                        <input type="file" id="formComment__file" name="img" multiple="" input-file="">
                    </label>--%>
                </div>
            </div>
        </div>
    </div>
</section>

  <!-- end blog section -->

<script>
    $(window).load(function () {
        // The slider being synced must be initialized first
        $('#carousel').flexslider({
            animation: "slide",
            controlNav: false,
            animationLoop: true,
            slideshow: false,
            itemWidth: 84,
            itemMargin: 5,
            asNavFor: '#slider'
        });

        $('#slider').flexslider({
            animation: "slide",
            controlNav: false,
            animationLoop: true,
            slideshow: false,
            sync: "#carousel"
        });

        /* Cloud Zoom */
        $(".flex-active-slide .cloud-zoom").imagezoomsl({
            zoomrange: [3, 3]
        });
        $("#carousel img").click(function () {
            setTimeout(function () {
                $(".flex-active-slide .cloud-zoom").imagezoomsl({
                    zoomrange: [3, 3]
                });
            }, 400);
            
        });
    });
</script>

<script type="text/javascript">
    function AddToCart() {
        var productQuantity = $("#txtProductQuantity").val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
            data: JSON.stringify({ productId: '<%=Data.Id%>', quantity: productQuantity, properties: '', addonIds: '' }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    location.href = '<%=HREF.BaseUrl %>cart/don-hang';
                 }
             }
        });
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
        else
        {
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
