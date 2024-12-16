<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="product-section">
    <div class="container">
        <h1 class="t_h" title="<%=Title%>"><%=Title%></h1>
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-5 col-lg-4">
                <div class="box">
                    <div class="img-box">
                        <div id="customCarousel1" class="carousel  slide" data-ride="carousel">
                            <div class="carousel-inner">
                                <div class="item active">
                                    <div class="slider_img_box">
                                        <%if(Data.ImageName != null){ %>
                                            <picture>
						                        <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
						                        <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                                                <img src="<%=HREF.DomainStore + Data.Image.FullPath%>" alt="<%=Title %>"/>
                                            </picture> 
                                        <%} %>
                                    </div>
                                </div>
                                <%foreach(var image in Images) 
                                {%>
                                    <div class="item">
                                        <div class="slider_img_box">
                                            <picture>
		                                        <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                                        <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                                                <img src="<%=HREF.DomainStore + image.FullPath%>"/>
                                            </picture>     
                                        </div>
                                    </div>
                                <%} %>
                            </div>
                        </div>
                        <div class="carousel_btn-box">
                            <a class="left carousel-control" href="#customCarousel1" role="button" data-slide="prev">
                                <i class="glyphicon glyphicon-chevron-left" aria-hidden="true"></i>
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="right carousel-control" href="#customCarousel1" role="button" data-slide="next">
                                <i class="glyphicon glyphicon-chevron-right" aria-hidden="true"></i>
                                <span class="sr-only">Next</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-7 col-lg-8">
                <div class="detail-box">
                    <p>
                        <%=Data.Brief %>
                    </p> 
                    <table border="1" style="width:100%">
                        <%foreach(var attribute in Attributes){ %>
                            <tr>
                                <td>
                                    <%=attribute.Name %>
                                </td>
                                <td>
                                    <%=attribute.ValueName == "True" ? "Có" : attribute.ValueName == "False" ? "Không" : attribute.ValueName %>
                                </td>
                            </tr>
                        <%} %>
                    </table>
                    <div style="height: 24px;margin-bottom:10px"> 
                        <span class="price">
                            <%if(Data.DiscountType > 0) {%>
                            <del aria-hidden="true">
                                <span class="woocommerce-Price-amount amount">
                                    <bdi>
                                        <%=Data.Price.ToString("N0")%>&nbsp;<span class="woocommerce-Price-currencySymbol">₫</span>
                                    </bdi>
                                </span>
                            </del> 
                            <%} %>
                            <%if(Data.Price > 0) {%>
                            <ins>
                                <span class="woocommerce-Price-amount amount">
                                    <bdi>
                                        <%=Data.PriceAfterDiscount.ToString("N0")%>&nbsp;<span class="woocommerce-Price-currencySymbol">₫</span>
                                    </bdi>
                                </span>
                            </ins>
                            <%} else {%>
                                Liên hệ
                            <%} %>
                        </span>
                    </div>
                    <%if(Data.Price > 0) {%>
                    <button type="button" onclick="AddToCart()" class="btn btn-t13">Chọn mua</button>
                    <%} %>
                </div>
            </div>
        </div>

        <div class="t_cont">
            <h2 class="h_t_cont">Mô tả</h2>
        </div><!-- End .t_cont -->
        <div class="productdetail">
            <%=Data.Content%> 
            <div class="tag">
                <%=string.Join(", ", Tags)%>
            </div>
        </div>

        <div class="t_cont">
            <h2 class="h_t_cont">Đánh giá và nhận xét</h2>
        </div><!-- End .t_cont -->
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
                                <span class="check-buyked">Khách hàng đã sử dụng sản phẩm</span>
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
        <div class="comment-box__form">
            <div class="formComment formValidation">
                <div class="row" style="margin-top:30px">
                    <div class="col-md-6" style="margin-bottom:15px">
                        <div style="padding-right:30px">
                            <input id="reviewName" type="text" class="input_all" name="name" placeholder="Họ và tên" required>
                        </div>
                    </div>
                    <div class="col-md-6" style="margin-bottom:15px">
                        <div style="padding-right:30px">
                            <input id="reviewPhone" type="text" class="input_all" name="phone" placeholder="Số điện thoại" required>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div style="padding-right:30px">
                        <textarea id="reviewComment" name="content" placeholder="Bình luận" m-required="Hãy để lại bình luận" cols="26" required></textarea>
                            </div>
                    </div>
                    <div class="col-md-12 formComment__action">
                        <button id="btnReview" class="btn btn-t13" type="button" onclick="AddReview()">Bình luận ngay</button>
                        <%--<label for="formComment__file" class="formComment__label formComment__label--upload">
                            <i class="fa fa-upload" aria-hidden="true"></i>
                            <span onclick="$("input[id='formComment__file']").focus().click();">Upload</span>
                            <input type="file" id="formComment__file" name="img" multiple="" input-file="">
                        </label>--%>
                    </div>
                </div>
                                    
            </div>
        </div>
        

        <script type="text/javascript">
        function AddToCart() {
        
            $.ajax({
                type: "POST",
                url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
                data: JSON.stringify({ productId: '<%=Data.Id%>', quantity: 1, properties: '', addonIds: '' }),
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
                if (reviewName && reviewPhone && reviewComment) {
                    $('#btnReview').prop('disabled', true);
                    $.ajax({
                        type: "POST",
                        url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddReview",
                        data: JSON.stringify({ itemId: '<%=Data.Id%>', name: reviewName, phone: reviewPhone, comment: reviewComment, vote: 5 }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            $('#btnReview').prop('disabled', false);
                            alert('Đã gửi bình luận thành công, Vui lòng chờ kiểm duyệt trong vài phút.');
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

        <%if(Data.Attributes.Any(e => e.Id == "ProductType" && e.Value == "Service")){ %>   
        <script type="application/ld+json">
        {
            "@context": "https://schema.org",
            "@type": "NewsArticle",
            "headline": "<%=Data.Title%>",
            "image": [
            "<%=HREF.DomainStore + Data.Image.FullPath%>.webp"
            ],
            "datePublished": "<%=String.Format("{0:dd/MM/yyyy}", Data.CreateDate)%>",
            <%if(!string.IsNullOrEmpty(this.Page.MetaKeywords)){ %>
            "keywords":"<%= this.Page.MetaKeywords%>",
            <%} %>
            "interactionStatistic": [{
                "@type": "InteractionCounter",
                "interactionType": "https://schema.org/ReadAction",
                "userInteractionCount": "<%=Data.Views %>"
            }
            <%if(Reviews.Count > 0){ %>
                ,{
                    "@type": "InteractionCounter",
                    "interactionType": "https://schema.org/CommentAction",
                    "userInteractionCount": "<%=Reviews.Count %>"
                }
            <%} %>
            ],
            "abstract": "<%=Data.Brief.Replace("\"","") %>",
            "articleBody":"<%=Data.Content.DeleteHTMLTag().Replace("\"","'")%>"
        }
        </script>
        <%} %>

        <%if(!Data.Attributes.Any(e => e.Id == "ProductType") || Data.Attributes.Any(e => e.Id == "ProductType" && e.Value == "Product")){ %>   
         <script type="application/ld+json">
            {
            "@context": "https://schema.org/",
            "@type": "Product",
            "name": "<%=Data.Title %>",
            "image": [
                "<%=HREF.DomainStore + Data.Image.FullPath%>.webp",
                "<%=HREF.DomainStore + Data.Image.FullPath%>"
                ],
            "description": "<%=Data.Brief.Replace("\"","'") %>",
            "offers": {
                "@type": "Offer",
                "availability": "https://schema.org/InStock",
                "price": "<%=Data.PriceAfterDiscount %>",
                "priceCurrency": "VND"
            }
            }
        </script>
        <%} %>
    </div>
</section>