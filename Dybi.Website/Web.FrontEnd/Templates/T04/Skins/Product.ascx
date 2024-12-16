<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="product_section">
    <h1 title="<%=Data.Title %>"><%=Data.Title %></h1>
    <div class="w3-row productTab">
        <div class="w3-col l5 s12 product_image">
            <div id="product<%=Data.Id %>">
                <picture>
					<source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
					<source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                    <img class="mySl" data-large="<%=HREF.DomainStore + Data.Image.FullPath%>" src="<%= HREF.DomainStore + Data.Image.FullPath%>" alt="<%=Title %>"/>
                </picture>
                <%if(this.Images.Count > 0) { %>
                    <%foreach(var image in Images) 
                    {%>
                        <picture>
		                    <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                    <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                            <img class="mySl" data-large="<%=HREF.DomainStore + image.FullPath%>" src="<%=HREF.DomainStore + image.FullPath%>"/>
                        </picture>  
                    <%} %>	
               
                <div class="w3-row-padding w3-section">
                    <%var col = 12 / (this.Images.Count + 1); var i = 1; %>
                    <div class="w3-col s<%=col %>">
                        <picture>
					        <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
					        <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                            <img class="mySl1 w3-opacity w3-hover-opacity-off" data-large="<%=HREF.DomainStore + Data.Image.FullPath%>" src="<%= HREF.DomainStore + Data.Image.FullPath%>" alt="<%=Title %>" onclick="currentdiv(<%= i++ %>)"/>
                        </picture>
                    </div>
                    <%foreach (var image in this.Images)
                    {%>
                        <div class="w3-col s<%=col %>">
                            <picture>
		                        <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                        <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                                <img class="mySl1 w3-opacity w3-hover-opacity-off" data-large="<%=HREF.DomainStore + image.FullPath%>" src="<%=HREF.DomainStore + image.FullPath%>" onclick="currentdiv(<%= i++ %>)"/>
                            </picture> 
                        </div>	
                    <%} %>	
                </div>
                <%} %>	
                </div>
        </div>
        <div class="w3-col l7 s12 pl30">
            <%=Data.Brief%>
            <table>
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
            <div style="border-top: 1px solid #ddd;margin: 20px 0px" class="w3-row">
            <div class="w3-col rps100">
                <div style="border-bottom: 1px solid #ddd;padding:10px 0">
                    <p class="price">
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
                    </p>
                </div>

                <div><!-- nút mua hàng-->
                    <p style="padding:10px 0"><%=Language["Quantity"] %>&nbsp&nbsp&nbsp<input class="input-number" type="number" value="1" id="txtQuantity" placeholder="Nhập số lượng" min="0" max="1000"></p>
                        <button type="button" class="w3-btn w3-blue w3-border" onclick="AddProductToCart(false);fly('hanggio');"><i class="fa fa-shopping-cart"></i><%=Language["AddToCart"] %></button>
                        <button type="button" class="w3-btn w3-red w3-border" onclick="AddProductToCart(true);"><%=Language["BuyNow"] %></button>
                </div>
            </div>
        </div>
        </div>
    </div>
    
    <div class="productTab">
        <div class="panel">
            <h3><%=Language["Description"] %></h3>
        </div>
        <div class="product_content">
            <%=Data.Content%>
        </div>
        <div class="productTab">
            Tag: <%=string.Join(", ", Tags)%> 
        </div>
    </div>
    
</section>

<section class="section-all">
    <div class="w3-container">
        <div class="comment w3-row w3-row-mobile plr70">
            <div class="w3-col m12">
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
                                    <div class="w3-row-padding row_flex_5">
                                        <div class="w3-col m6 i12 input-group mb-2">
                                            <input id="reviewName" type="text" class="input_all" name="name" placeholder="Họ và tên" required>
                                        </div>
                                        <div class="w3-col m6 i12 input-group mb-2">
                                            <input id="reviewPhone" type="text" class="input_all" name="phone" placeholder="Số điện thoại" required>
                                        </div>
                                        <div class="w3-col m12 form-validate mb-3" style="margin-top:15px">
                                            <textarea id="reviewComment" name="content" placeholder="Bình luận" m-required="Hãy để lại bình luận" cols="26" required></textarea>
                                        </div>
                                        <div class="w3-col m12 formComment__action">
                                            <button id="btnReview" class="btn btn-t13" type="button" onclick="AddReview()">Bình luận ngay</button>
                                        </div>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

<script>
    var slideindex = 1;
    showdivs(slideindex);

    function plusdivs(n) {
        showdivs(slideindex += n);
    }

    function currentdiv(n) {
        showdivs(slideindex = n);
    }

    function showdivs(n) {
      var i;
      var x = document.getElementsByClassName("mySl");
      var dots = document.getElementsByClassName("mySl1");
      if (n > x.length) {slideindex = 1}
      if (n < 1) {slideindex = x.length}
      for (i = 0; i < x.length; i++) {
         x[i].style.display = "none";
      }
      for (i = 0; i < dots.length; i++) {
         dots[i].className = dots[i].className.replace(" w3-opacity-off", "");
      }
      x[slideindex-1].style.display = "block";
      dots[slideindex-1].className += " w3-opacity-off";
    }
</script>

<!-- /Product Tabs -->
<script type="text/javascript">
    // Send the rating information somewhere using Ajax or something like that.
    function AddProductToCart(go) {
        var productQuantity = $("#txtQuantity").val();
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
                    if (data != "") {
                        if (go == true) {
                            location.href = '<%=HREF.LinkComponent("Cart",Language["cart"].ConvertToUnSign(), true)%>';
                        } else {
                            $.ajax({
                                type: "POST",
                                url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetCarts",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    var total = 0;
                                    for (var i = 0; i < data.d.length; i++) {
                                        total += data.d[i].Quantity;
                                    }
                                
                                    $("#hanggio").html('<a class="share_header_icon w3-hover-text-orange w3-center" href="/vit-carts" title="('+ total +') <%=Language["Product"]%>"><span><i style="line-height: 1.7;" class="material-icons"></i><span></span></span></a>');
                                }
                            });
                        }
                    }
                }
            });
        }
    }
</script>	


<script type="text/javascript" language="javascript">
    function fly(iddivGio) {
        iddivSP = "#product" + "<%=Data.Id%>";
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
                    type: "POST",
                    url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetCarts",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //var total = 0;
                        //for (var i = 0; i < data.d.length; i++) {
                        //    total += data.d[i].Quantity;
                        //}
                        //$("#hanggio").html('<a class="share_header_icon w3-hover-text-orange w3-center" href="/vit-carts" title="('+ total +') <%=Language["Product"]%>"><span><i style="line-height: 1.7;" class="material-icons"></i><span></span></span></a>');
                    }
                });
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