<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Article.ascx.cs" Inherits="Web.FrontEnd.Modules.Article" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>


<!-- blog section -->
  <section class="blog_section layout_padding" <%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : "style='background-color:" + this.Skin.BodyBackground + "'" %>">
    <div class="container">
        <%if(DisplayTitle){ %>
          <div class="heading_container">
            <h1>
              <%=Title %>
            </h1>
          </div>
        <%} %>

      <div class="blog_contain layout_padding2-top"> 
          <%if(DisplayDate){ %>
              <h4 class="blog_date">
                <%=string.Format("{0:dd/MM/yyyy}", dto.CreateDate) %>
              </h4>
                <%} %>  
              <%if(DisplayBrief){ %>
              <strong class="blog_brief">
                  <%=dto.Brief %>
              </strong> 
              <%} %>
            <div class="img-box">
                <picture>
					<source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>.webp" type="image/webp">
					<source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>" type="image/jpeg"> 
                    <img <%=DisplayImage ? "style='margin:20px 0px; width:75%;'" : "style='position:absolute; top:-1000px;'" %> src="<%=HREF.DomainStore + dto.Image.FullPath%>" alt="<%=Title %>"/>
                </picture>
                
            </div>
         
            <%if(RelatiedArticles.Count > 0) {%>
    <ul style="list-style:circle;margin:10px 0px 20px 20px">
        <%foreach (var article in RelatiedArticles){ %>
        <li><a href="<%=HREF.LinkComponent(HREF.CurrentComponent,article.Title.ConvertToUnSign(), article.Id, SettingsManager.Constants.SendArticle, article.Id)%>" title="<%=article.Title %>"><%=article.Title %></a></li>
        <%} %>
    </ul>
    <%} %>

    <%=dto.Content.Replace("[NAME]", Component.Company.Branches[0].Name).Replace("[ADDRESS]", Component.Company.Branches[0].Address).Replace("[PHONE]", Component.Company.Branches[0].Phone)%> 
        <%if(DisplayTag){ %>
    <div class="tag">
        Tag: <%=string.Join(", ", Tags)%> 
    </div>
    <%} %>
      </div>
    </div>
  </section>

  <!-- end blog section -->

<%if(HREF.CurrentComponent == "article"){ %>
<section class="section-all layout_padding2-bottom">
    <div class="container">
        <div class="heading_container">
            <h2 class="comment-box__title">Bình luận</h2>
        </div>
        <p class="comment-box__title"></p>
        <%if(Reviews.Count > 0){ %>
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
        <%} %>
                                        
        <div class="comment-box__form">
            <div class="formComment formValidation">
                <div class="row" style="margin-top:30px">
                    <div class="col-6" style="margin-bottom:15px">
                        <div style="padding-right:30px">
                            <input id="reviewName" type="text" class="input_all" name="name" placeholder="Họ và tên" required>
                        </div>
                    </div>
                    <div class="col-6" style="margin-bottom:15px">
                        <div style="padding-right:30px">
                            <input id="reviewPhone" type="text" class="input_all" name="phone" placeholder="Số điện thoại" required>
                        </div>
                    </div>
                    <div class="col-12">
                        <div style="padding-right:30px">
                        <textarea id="reviewComment" name="content" placeholder="Bình luận" m-required="Hãy để lại bình luận" cols="26" required></textarea>
                            </div>
                    </div>
                    <div class="col-12 formComment__action">
                        <button id="btnReview" class="btn btn-warning" type="button" onclick="AddReview()">Bình luận ngay</button>
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
<%} %>

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