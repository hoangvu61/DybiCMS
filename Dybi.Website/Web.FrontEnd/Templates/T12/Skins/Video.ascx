<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Media.ascx.cs" Inherits="Web.FrontEnd.Modules.Media" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library" %>

<section class="video">
        <div style="height:500px" id="videocontent">
	        <%=Data.Embed %>
        </div>
        <p class="post-header">
	        Nếu không tải được Video. Vui lòng vào link sau để xem tại kênh youtube "<a class='youtubechannel' target='_blank'></a>": 
	        <strong><a class='youtubevideo' target='_blank' href='<%=this.Data.Url %>'><%=this.Data.Url %></a></strong>
        </p>
    <div class="container">	
        <h1 style="font-size: 2rem; line-height: 2.8rem; font-weight: 700;" title="<%=Data.Title%>"><%=Data.Title %></h1>
        <div class="row" style="margin-top:20px">
            <div class="grid_9">
                <strong>
                    <%=Data.Brief != null ? Data.Brief.Replace("\n","<br /><br />") : "" %>
                </strong>
                <div class="content">
                    <%=Data.Content %>
                </div>
                <button id="btnViewMore" class="btn--orange" type="button" onclick="ViewMore()">Xem thêm</button>
            </div>
            <div class="grid_3">
                <%if(!string.IsNullOrEmpty(Data.ImageName)){ %>
			        <img src="<%=HREF.DomainStore + Data.Image.FullPath%><%= Data.Image.FileExtension != ".webp" ? ".webp" : "" %>" alt="<%=Data.Title %>" style="max-height:500px"/> 
                <%} %>
                <table style="width:100%">
                    <tbody>
                         <%foreach(var attribute in Data.Attributes){ %>
                              <tr>
                                  <td>
                                      <label><%=attribute.Name %></label>
                                  </td>
                                  <td>
                                      <%=attribute.ValueName == "True" ? "Có" : attribute.ValueName == "False" ? "Không" : attribute.ValueName %>
                                  </td>
                              </tr>
                          <%} %>
                    </tbody>
                </table>
            </div>
        </div>

        <div id="goyoutube" style="display:none">
		    <div class="row">
                <%if(!string.IsNullOrEmpty(Data.ImageName)){ %>
			    <div class="grid_6">
				    <img src="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" alt="<%=Data.Title %>" style="max-height:500px"/> 
			    </div>
			    <div class="grid_6">
                    <div style="padding:50px">
					    <h4>Video không thể tự tải</h4> <br />
					    Vui lòng vào link bên dưới để xem tại kênh youtube "<a class='youtubechannel' target='_blank'></a>": <br/><br/>
					    <strong><a class='youtubevideo' href='<%=Data.Url %>' target='_blank'></a></strong>
				    </div>
			    </div>
                <%} else { %>
                <div class="grid_12">
                    <div style="padding:50px">
					    <h4>Video không thể tự tải</h4> <br />
					    Vui lòng vào link bên dưới để xem tại kênh youtube "<a class='youtubechannel' target='_blank'></a>": <br/><br/>
					    <strong><a class='youtubevideo' href='<%=Data.Url %>' target='_blank'></a></strong>
				    </div>
			    </div>
                <%} %>
		    </div>
        </div>
	</div>
</section>

<section class="section-all">
    <div class="container">
        <div class="comment row row-mobile">
            <div class="col grid_12">
                <div class="comment">
                    <div class="comment-box">
                        <div class="comment-box__content">

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
                                                    <span class="check-buyked">&nbsp; Fan cứng của <%=Component.Company.NickName %></span>
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
    $(document).ready(function () {
        if ($('.video .container .grid_9').height() > $('.video .container .grid_3').height()) {
            $('.video .container .grid_9 .content').height($('.video .container .grid_3 img').height());
            $('#btnViewMore').css('display', 'block');
        }
    });
    function ViewMore() {
        $('.video .container .grid_9 .content').css('height', 'auto');
        $('#btnViewMore').css('display', 'none');
    }
</script>

<script>
var target = $('#videocontent iframe').attr('src');
var code = target.split("embed/")[1];
function getYouTubeInfo() {
                $.ajax({
                        url: "https://www.googleapis.com/youtube/v3/videos?id="+code+"&key=AIzaSyCAFHg9YJxpZRjiEv4fkmXMHpg0LUryH8U&part=status,snippet",
                        dataType: "json",
                        success: function (data) {
						$(".youtubechannel").html(data.items[0].snippet.channelTitle);
						$(".youtubechannel").attr('href','https://www.youtube.com/channel/' + data.items[0].snippet.channelId);
						$(".youtubevideo").html(data.items[0].snippet.title);
						if( !data.items[0].status.embeddable 
							//|| !data.items[0].status.publicStatsViewable 
							|| !data.items[0].status.privacyStatus == 'public')
						{
							$("#videocontent").html($('#goyoutube').html());
						}
						}
                });
        }
$(document).ready(function () {
        getYouTubeInfo();
});
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
    "@context": "https://schema.org",
    "@type": "VideoObject",
    "name": "<%=Data.Title %>",
    "keywords":"<%=Data.Title %>
        <%=!string.IsNullOrEmpty(Data.GetAttribute("author")) ? "," + Data.GetAttribute("author") : "" %>
        <%=!string.IsNullOrEmpty(Data.GetAttribute("actor")) ? "," + Data.GetAttribute("actor") : "" %>
        <%=!string.IsNullOrEmpty(Data.GetAttribute("characterName")) ? "," + Data.GetAttribute("characterName") : "" %>",
    <%if(!string.IsNullOrEmpty(Data.ImageName)){ %>
    "thumbnailUrl": [
    "<%=HREF.DomainStore + Data.Image.FullPath%><%=Data.Image.FileExtension == ".webp" ? "" : ".webp" %>"
    ],
    "image": [
    "<%=HREF.DomainStore + Data.Image.FullPath%><%=Data.Image.FileExtension == ".webp" ? "" : ".webp" %>"
    ],
    <%} %>
    <%if(!string.IsNullOrEmpty(Data.GetAttribute("uploadDate"))){ %>
    "uploadDate": "<%=Data.GetAttribute("uploadDate") %>",
    <%} %>
    <%if(!string.IsNullOrEmpty(Data.GetAttribute("author"))){ %>
    "author": {
        "@type": "Person",
        "name": "<%=Data.GetAttribute("author") %>"
        },
    <%} %>
    <%if(!string.IsNullOrEmpty(Data.GetAttribute("director"))){ %>
    "director": "<%=Data.GetAttribute("director") %>",
    <%} %>
    <%if(!string.IsNullOrEmpty(Data.GetAttribute("creator"))){ %>
    "creator": "<%=Data.GetAttribute("creator") %>",
    <%} %>
    <%if(!string.IsNullOrEmpty(Data.GetAttribute("musicBy"))){ %>
    "musicBy": "<%=Data.GetAttribute("musicBy") %>",
    <%} %>
    "embedUrl": "<%=this.Data.Url %>",
    <%if(!string.IsNullOrEmpty(Data.GetAttribute("actor"))){ %>
    "actor": {
        <%if(!string.IsNullOrEmpty(Data.GetAttribute("characterName"))){ %>
        "@type": "PerformanceRole",
        "actor": { 
          "@type": "Person",
          "name": "<%=Data.GetAttribute("actor") %>"
        }
        ,"characterName": "<%=Data.GetAttribute("characterName") %>"
        <%} else { %>
          "@type": "Person",
          "name": "<%=Data.GetAttribute("actor") %>"
        <%} %>
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
    "interactionStatistic": [{
        "@type": "InteractionCounter",
        "interactionType": "https://schema.org/WatchAction",
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
    "description": "<%=Data.Brief.Replace("\"","")%>"
}
</script>
