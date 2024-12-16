<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Article.ascx.cs" Inherits="Web.FrontEnd.Modules.Article" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<%if(!string.IsNullOrEmpty(dto.Title)){ %>
<script>
    $("#<%=dto.CategoryId %>").addClass("active");
</script>
<!-- blog section -->
  <section class="blog_section layout_padding">
        <%if(DisplayTitle){ %>
      <div class="heading_container">
        <h1 title="<%=Page.Title %>">
          <%=Title %>
        </h1>
      </div>
    <%} %>

    <div class="box">
        <div class="img-box">
            <picture>
			    <source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>.webp" type="image/webp">
			    <source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>" type="image/jpeg"> 
                <img <%=DisplayImage ? "" : "style='position:absolute; top:-2000px;'" %> src="<%=!string.IsNullOrEmpty(dto.ImageName) ? HREF.DomainStore + dto.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=Title %>"/>
            </picture>
            <%if(DisplayDate){ %>
            <h4 class="blog_date">
            <%=string.Format("{0:dd/MM/yyyy}", dto.CreateDate) %>
            </h4>
            <%} %>  
        </div>
        <%if(DisplayBrief){ %>
        <div class="detail-box">
            <strong>
                <%=dto.Brief %>
            </strong>
        </div>
        <%} %>
    </div>

      <div class="blog_content">
        <%=dto.Content%> 
      </div>
        <%if(DisplayTag && Tags.Count > 0){ %>
            <div class="tag">
                <%=string.Join(", ", Tags)%> 
            </div>
        <%} %>
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
<%} else { %>
<div class="row"> 
    <div class="col-md-10 col-md-offset-1">
        <div class="alert alert-danger">
            The content does not exist
        </div>
    </div>
</div>
<%} %>