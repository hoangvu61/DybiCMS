<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Article.ascx.cs" Inherits="Web.FrontEnd.Modules.Article" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog section -->
<section class="article_section py-5">
    <div class="container">
         <%if (!DisplayImage) 
         { %>
            <h2 class="wow animate__animated animate__fadeInUp animated" title="<%=dto.Title %>"><%=dto.Title %></h2>
            <p class="text-justify wow animate__animated animate__zoomIn animated"><%=dto.Brief %></p>
            <a class="btn btn-secondary mt-3 wow animate__animated animate__zoomIn animated" href="<%=HREF.LinkComponent("Article", dto.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, dto.Id)%>"><%=Language["ReadMore"] %></a>
         <%} else if (!DisplayDate) { %>
            <div class="row">
                <div class="col-12 col-md-7 wow animate__animated animate__fadeInLeft animated">
                    <a href="<%=HREF.LinkComponent("Article", dto.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, dto.Id)%>">
                        <h2 title="<%=dto.Title %>"><%=dto.Title %></h2>
                    </a>
                    <p class="text-justify"><%=dto.Brief %></p>
                </div>
                 <div class="col-12 col-md-5 wow animate__animated animate__fadeInRight animated">
                     <a href="<%=HREF.LinkComponent("Article", dto.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, dto.Id)%>">
                        <picture>
                            <source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>.webp" type="image/webp">
                            <source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + dto.Image.FullPath%>" alt="<%=Title %>"/>
                        </picture>
                     </a>
                 </div>
            </div>
        <%} else { %>

        <div class="heading_container py-5">
            <h1 title="<%=dto.Title %>"><%=dto.Title %></h1>
            <div class="article_brief">
                <%=dto.Brief %>
            </div>
        </div>

        <div class="blog_content">
            <%=dto.Content%>
            
            <%if (!string.IsNullOrEmpty(dto.HTML)) { %>
                <style>
                    #<%=Config.Language%>-html { display: inline-table !important }
                </style>

                <%=dto.HTML%> 
            <%}%> 
        </div>
        <%if (DisplayTag && Tags.Count > 0) { %>
            <div class="tag">
                Tag: <%=string.Join(", ", Tags)%> 
            </div>
        <%} %>

        <%} %>
    </div>
    <%if (RelatiedArticles.Count > 0) {%>
    <aside class="my-3">
        <h3>Nội dung liên quan:</h3>
        <ul style="list-style:circle">
            <%foreach (var article in RelatiedArticles)
                { %>
            <li><a href="<%=HREF.LinkComponent(HREF.CurrentComponent, article.Title.ConvertToUnSign(), article.Id, SettingsManager.Constants.SendArticle, article.Id)%>" title="<%=article.Title %>"><%=article.Title %></a></li>
            <%} %>
        </ul>
    </aside>
    <%} %>
</section>

  <!-- end blog section -->

<%if (HREF.CurrentComponent == "article")
    { %>
<section class="section-all my-5">
    <div class="container">
        <p class="comment-box__title">Bình luận</p>
        <div class="comment-box__list">
            <%foreach (var review in Reviews)
                { %>
            <div class="comment-item">
                <div class="comment-item__top">
                    <div class="comment-item__info">
                        <div class="user-info__name">
                            <%=review.Name %>
                            <span class="rating" style="">
                                <i class="fa fa-star-o"></i>
                                <i class="fa fa-star-o"></i>
                                <i class="fa fa-star-o"></i>
                                <i class="fa fa-star-o"></i>
                                <i class="fa fa-star-o"></i>
                                <div class="rating--active" style="width:100%;">
                                    <%for (int i = 1; i <= review.Vote; i++)
                                        { %>
                                    <i class="fa fa-star"></i>
                                    <%} %>
                                </div>
                            </span>
                        </div>
                        <%if (review.IsBuyer)
                            { %>
                        <div class="d-flex align-items-center">
                            <div class="me-3">
                                <span class="check-buyked"><%=Language["UserBuy"] %> <%=Component.Company.NickName %></span>
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
                <%if (review.Images != null && review.Images.Count > 0)
                    { %>
                <div class="comment-item__imgs">
                    <%foreach (var image in review.Images)
                        { %>
                    <picture>
		                <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                        <img src="<%=HREF.DomainStore + image.FullPath%>"/>
                    </picture>
                    <%} %>
                </div>
                <%} %>
                <%if (review.Replies != null && review.Replies.Count > 0)
                    { %>
                <div class="comment-childs">
                    <%foreach (var reply in review.Replies)
                        { %>
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
                                        
        <div class="comment-box__form">
            <div class="formComment formValidation">
                <div class="row">
                    <div class="col-sm-6" style="margin-bottom:15px">
                        <div style="padding-right:30px">
                            <input id="reviewName" type="text" class="input_all" name="name" placeholder="Họ và tên" required>
                        </div>
                    </div>
                    <div class="col-sm-6" style="margin-bottom:15px">
                        <div style="padding-right:30px">
                            <input id="reviewPhone" type="text" class="input_all" name="phone" placeholder="Số điện thoại" required>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div style="padding-right:30px">
                        <textarea id="reviewComment" name="content" placeholder="Bình luận" m-required="Hãy để lại bình luận" cols="26" required></textarea>
                            </div>
                    </div>
                    <div class="col-sm-12 formComment__action">
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
</section>
<%} %>

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
                data: JSON.stringify({ itemId: '<%=dto.Id%>', name: reviewName, phone: reviewPhone, comment: reviewComment, vote: 5 }),
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

<%if(dto.Id != Guid.Empty){ %>
<script type="application/ld+json">
{
    "@context": "https://schema.org",
    "@type": "<%=dto.Type%>",
    "headline": "<%=dto.Title%>",
    "name" : "<%=dto.Title%>",
    <%if(dto.Image != null){ %>
    "image": [
    "<%=HREF.DomainStore + dto.Image.FullPath%>.webp"
    ],
    <%} %>
    "datePublished": "<%=String.Format("{0:dd/MM/yyyy}", dto.CreateDate)%>",
    "keywords":"<%= this.Page.MetaKeywords%>",
    "inLanguage":"<%= Config.Language%>",
    "interactionStatistic": [{
        "@type": "InteractionCounter",
        "interactionType": "https://schema.org/ReadAction",
        "userInteractionCount": "<%=dto.Views %>"
    }
    <%if(Reviews.Count > 0){ %>
        ,{
            "@type": "InteractionCounter",
            "interactionType": "https://schema.org/CommentAction",
            "userInteractionCount": "<%=Reviews.Count %>"
        }
    <%} %>
    ],
    "description": "<%=dto.Brief.Replace("\"","") %>",
    "abstract": "<%=dto.Brief.Replace("\"","") %>",
    "articleBody":"<%=dto.Content.DeleteHTMLTag().Replace("\"","")%>",
    "wordCount":"<%=dto.Content.DeleteHTMLTag().Replace("\"","").Length%>",
    "publisher" :{
        "@type": "<%=Component.Company.Type %>",
        "name": "<%=Component.Company.DisplayName %>",
        "legalName": "<%=Component.Company.FullName %>",
        <%if(!string.IsNullOrEmpty(Component.Company.NickName)){ %>
        "alternateName":"<%=Component.Company.NickName %>",
        <%} %>
        "url": "<%=HREF.DomainLink %>",
        "logo": "<%=HREF.DomainStore + Component.Company.Image.FullPath %>",
        <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
        "image": "<%=HREF.DomainStore + Config.WebImage.FullPath %>",
        <%} %>
        <%if(!string.IsNullOrEmpty(Component.Company.Slogan)){ %>
        "slogan":"<%=Component.Company.Slogan %>",
        <%} %>
        <%if(!string.IsNullOrEmpty(Component.Company.TaxCode)){ %>
        "taxID":"<%=Component.Company.TaxCode %>",
        <%} %>
        <%if(!string.IsNullOrEmpty(Component.Company.JobTitle)){ %>
        "keywords":"<%=Component.Company.JobTitle %>",
        <%} %>
        <%if(Component.Company.PublishDate != null){ %>
        "foundingDate":"<%=Component.Company.PublishDate %>",
        <%} %>
        <%if(Component.Company.Branches != null && Component.Company.Branches.Count > 0){ %>
            <%if(!string.IsNullOrEmpty(Component.Company.Branches[0].Email)){ %>
                "email": "<%=Component.Company.Branches[0].Email %>",
            <%} %>
            <%if(!string.IsNullOrEmpty(Component.Company.Branches[0].Phone)){ %>
                "telephone": "<%=Component.Company.Branches[0].Phone %>",
            <%} %>
            <%if(!string.IsNullOrEmpty(Component.Company.Branches[0].Address)){ %>
                "address": "<%=Component.Company.Branches[0].Address %>",
            <%} %>
        <%} %>
        <%if(!string.IsNullOrEmpty(Component.Company.Brief)){ %>
        "description": "<%=Component.Company.Brief.Replace("\"","") %>"
        <%} %>
    },
    <%if(Reviews.Count > 0){ %>
    "commentCount":"<%=Reviews.Count %>",
    "comment": [
        <%for(int i = 0; i < Reviews.Count; i++){ %>
            <%= i == 0 ? "" : "," %>
            {
                "@type": "Comment",
                "text": "<%=Reviews[i].Comment %>",
                "author": {
                    "@type": "Person",
                    "name": "<%=Reviews[i].Name %>"
                },
                <%if(Reviews[i].Replies != null && Reviews[i].Replies.Count > 0){ %>
                    "comment": [
                    <%for(int j = 0; j < Reviews[i].Replies.Count; j++){ %> 
                        <%= i == 0 ? "" : "," %>
                        {
                            "@type": "Comment",
                            "text": "<%= Reviews[i].Replies[j].Comment%>",
                            "author": {
                                "@type": "Person",
                                "name": "<%= Reviews[i].Replies[j].Name%>"
                            },
                            "dateCreated": "<%= Reviews[i].Replies[j].Date%>",
                            "parentItem": {
                                "@type": "Comment",
                                "text": "<%=Reviews[i].Comment %>"
                            }
                        }
                    <%} %> 
                    ],
                <%} %>
                "dateCreated": "<%=Reviews[i].Date %>"
            }
        <%} %>
    ], 
    <%} %>
    "isPartOf":{
        "@type": "WebPage",
        "name": "<%= this.Page.Title%>",
        "url": "<%=HREF.LinkComponent("Article", dto.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, dto.Id)%>",
        <%if(dto.Image != null){ %>
        "primaryImageOfPage": {
            "@type":"ImageObject",
            "url": "<%=HREF.DomainStore + dto.Image.FullPath%>.webp",
            "caption": "<%=Page.Title %>",
            "inLanguage":"<%= Config.Language%>"
        },
        <%} %>
        "isPartOf":{
            "@type": "WebSite",
            "name": "<%=Component.Company.DisplayName %>",
            "url": "<%=HREF.DomainLink %>",
            <%if(Component.Company.Image != null){ %>
                "image": "<%=HREF.DomainStore + Component.Company.Image.FullPath %>.webp"
            }
            <%} %>
    }
}
</script>
<%} %>