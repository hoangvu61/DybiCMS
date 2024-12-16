<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Event.ascx.cs" Inherits="Web.FrontEnd.Modules.Event" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>


<h1 class="main-heading" title="<%=dto.Title%>"><strong><%=dto.Title%></strong></h1>

<div class="post-header">
    <span class="post-timestamp">
        🕓 <%=String.Format("{0:dd/MM/yyyy HH:mm}", dto.StartDate)%>
    </span>
    <span class="post-comment-link">
        👁 <%=dto.Views%>
        <%if(Reviews.Count > 0){ %>
        💬 <%=Reviews.Count %>
        <%} %>
    </span>
    <span class="post-labels tag">
        📍 <%=dto.Place%>
    </span>
</div>

<div class="row" style="margin-top:40px">
	<div class="grid_3">
        <%if(!string.IsNullOrEmpty(dto.ImageName)){ %>
				<img src="<%=HREF.DomainStore + dto.Image.FullPath%>.webp" alt="<%=dto.Title %>"/> 
        <%} %>
        <div class="map">
            <div id="googleMap">
                    <iframe style="Width:100%;height:445px" src="//www.google.com/maps/embed/v1/search?q=<%=dto.Place %>
                        &zoom=16
                        &key=AIzaSyCZXdpCRgYzYNMLHoBnK_RcooQ8lwby_nc">
                    </iframe> 
            </div>
        </div>
        
    </div>
    <div class="grid_9">
        <p id="coundown"></p>
        <table style="width:100%">
            <tbody>
                <tr>
                    <td><label>Biểu diển:</label></td>
                    <td>
                        <%=Component.Company.DisplayName %>
                    </td>
                </tr> 
                <tr>
                    <td><label>Thời gian:</label></td>
                    <td>
                        <%=String.Format("{0:dd/MM/yyyy HH:mm}", dto.StartDate)%>
                    </td>
                </tr>
                <tr>
                    <td><label>Địa điểm:</label></td>
                    <td>
                        <%=dto.Place%>
                    </td>
                </tr>
                <tr>
                    <td><label>Trạng thái:</label></td>
                    <td>
                        <%= dto.StartDate.Date >= System.DateTime.Now.Date ? "Sắp diển ra" : "Kết thúc" %>
                    </td>
                </tr>
            </tbody>
        </table>
        <p class="brief">
            <%=dto.Brief.Replace("\n","<br />")%>
        </p>
        <%=dto.Content%>
    </div>
</div>

<section class="section-all">
    <div class="container">
        <div class="comment row row-mobile">
            <div class="col grid_12">
                <div class="comment">
                    <div class="comment-box">
                        <div class="comment-box__content">

                            <p class="comment-box__title">Đánh giá và nhận xét</p>
                            <div class="comment-box__percent">
                                <div class="rating-score">
                                    <p class="score-big"><%=Reviews.Count == 0 ? 5 : Reviews.Sum(e => e.Vote) / Reviews.Count %>/5</p>
                                    <p class="count-comment"><%=Reviews.Count%> đánh giá / <%=dto.Views%> lượt xem</p>
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
                                                    <span class="check-buyked">Fan cứng của <%=Component.Company.NickName %></span>
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
                                    <div class="row" style="margin-top:30px">
                                        <div class="grid_6" style="margin-bottom:15px">
                                            <div style="padding-right:30px">
                                                <input id="reviewName" type="text" class="input_all" name="name" placeholder="Họ và tên" required>
                                            </div>
                                        </div>
                                        <div class="grid_6" style="margin-bottom:15px">
                                            <div style="padding-right:30px">
                                                <input id="reviewPhone" type="text" class="input_all" name="phone" placeholder="Số điện thoại" required>
                                            </div>
                                        </div>
                                        <div class="grid_12">
                                            <div style="padding-right:30px">
                                            <textarea id="reviewComment" name="content" placeholder="Bình luận" m-required="Hãy để lại bình luận" cols="26" required></textarea>
                                                </div>
                                        </div>
                                        <div class="grid_12 formComment__action">
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
        </div>
    </div>
</section>

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
                data: JSON.stringify({ itemId: '<%=dto.Id%>', name: reviewName, phone: reviewPhone, comment: reviewComment, vote: reviewVote }),
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

<script>
// Set the date we're counting down to
var countDownDate = new Date(" <%=String.Format("{0:MM/dd/yyyy HH:mm:ss}", dto.StartDate)%>").getTime();

// Update the count down every 1 second
var x = setInterval(function() {

  // Get today's date and time
  var now = new Date().getTime();

  // Find the distance between now and the count down date
  var distance = countDownDate - now;

  // Time calculations for days, hours, minutes and seconds
  var days = Math.floor(distance / (1000 * 60 * 60 * 24));
  var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
  var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
  var seconds = Math.floor((distance % (1000 * 60)) / 1000);

  // Display the result in the element with id="demo"
  document.getElementById("coundown").innerHTML = days + "d " + hours + "h "
  + minutes + "m " + seconds + "s ";

  // If the count down is finished, write some text
  if (distance < 0) {
    clearInterval(x);
    document.getElementById("coundown").innerHTML = "Đã kết thúc";
  }
}, 1000);
</script>

<script type="application/ld+json">
{
    "@context": "https://schema.org",
    "@type": "BreadcrumbList",
    "itemListElement": [{
        "@type": "ListItem",
        "position": 1,
        "name": "Trang chủ",
        "item": "<%=HREF.DomainLink %>"
    },
    {
        "@type": "ListItem",
        "position": 2,
        "name": "Sự kiện",
        "item": "<%=HREF.LinkComponent("Events", "su-kien", true) %>"
    },
    {
        "@type": "ListItem",
        "position": 3,
        "name": "<%=dto.Title.Replace("\"","'") %>"
    }
    ]
}
</script>
<script type="application/ld+json">
{
    "@context": "https://schema.org",
    "@type": "Event",
    "location": {
    "address": "<%=dto.Place %>"
    },
    "startDate": "<%=dto.StartDate %>",
    "endDate": "<%=dto.StartDate.AddHours(3) %>",
    "name": "<%=dto.Title.Replace("\"","'") %>",
    <%if(!string.IsNullOrEmpty(dto.ImageName)){ %>
    "image": [
    "<%=HREF.DomainStore + dto.Image.FullPath%>.webp"
    ],
    <%} %>
    "eventStatus": "https://schema.org/EventScheduled",
    "eventAttendanceMode": "https://schema.org/OfflineEventAttendanceMode",
    "performer": {
        "@type": "Person",
        "name": "<%=Component.Company.DisplayName %>",
        "alternateName": "<%=Component.Company.NickName %>"
      },
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
    "description": "<%=dto.Brief.Replace("\"","") %>"
    }
}
</script>