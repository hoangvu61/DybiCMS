<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Article.ascx.cs" Inherits="Web.FrontEnd.Modules.Article" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog section -->
<%=dto.HTML %>
<header class="entry-header has-text-align-center header-footer-group" style="background-image: url('https://www.ippworld.com/wp-content/uploads/2023/08/ippworld-transcreation.webp');"> 


<div class="container">
		<div class="row align-items-center justify-content-center">
			<h1 class="entry-title"><%=Page.Title %></h1>
	</div> 
	</div><!-- .entry-header-inner -->
</header>
  <section class="blog_section">
      <div class="container">
        <%if(DisplayTitle){ %>
      <div class="heading_container">
        <h1>
          <%=Title %>
        </h1>
          <%if(DisplayBrief){ %>
            <div class="detail-box">
                <strong>
                  <%=dto.Brief %>
                </strong>
            </div>
              <%} %>
      </div>
    <%} %>

          <div class="box">
            <div class="img-box">
                <%if(DisplayImage){ %>
                    <picture>
						<source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>.webp" type="image/webp">
						<source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>" type="image/jpeg"> 
                        <img src="<%=!string.IsNullOrEmpty(dto.ImageName) ? HREF.DomainStore + dto.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=Title %>"/>
                    </picture>
                <%} %>
                
                <%if(DisplayDate){ %>
                    <h4 class="blog_date">
                    <%=string.Format("{0:dd/MM/yyyy}", dto.CreateDate) %>
                    </h4>
                <%} %>  
            </div>
              
          </div>

            <%if(RelatiedArticles.Count > 0) {%>
    <ul style="list-style:circle;margin:10px 0px 20px 20px">
        <%foreach (var article in RelatiedArticles)
            { %>
        <li><a href="<%=HREF.LinkComponent("Article",article.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, article.Id )%>" title="<%=article.Title %>"><%=article.Title %></a></li>
        <%} %>
    </ul>
    <%} %>

      <div class="blog_content">
        <%=dto.Content%> 
      </div>
    <%if(DisplayTag){ %>
        <div class="tag">
            Tag: <%=string.Join(", ", Tags)%> 
        </div>
    <%} %>
          </div>
  </section>

  <!-- end blog section -->

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