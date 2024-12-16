<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Article.ascx.cs" Inherits="Web.FrontEnd.Modules.Article" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>


<!-- blog section -->
  <div class="blog_section layout_padding">
    <div class="container">
        <%if(DisplayTitle){ %>
      <div class="heading_container heading_center">
        <h1 class="product_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
          <%=Title %>
        </h1>
      </div>
    <%} %>

      <div class="row">
        <div class="col-md-12 col-lg-12 mx-auto">
          <div class="box">
            <div class="img-box">
                <%if(DisplayImage && !string.IsNullOrEmpty(dto.ImageName)){ %>
                <picture>
                    <%if(dto.Image.FileExtension != ".webp"){ %>
						  <source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>.webp" type="image/webp">
                    <%} %>
						  <source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + dto.Image.FullPath%>" alt="<%=Title %>"/>
                        </picture>
                <%} %>
                <%if(DisplayDate){ %>
              <h4 class="blog_date">
                <%=string.Format("{0:dd/MM/yyyy}", dto.CreateDate) %>
              </h4>
                <%} %>  
            </div>
              <%if(DisplayBrief){ %>
            <div class="detail-box">
              <p>
                  <%=dto.Brief %>
              </p> 
            </div>
              <%} %>
          </div>
            <div>
            <%if(RelatiedArticles.Count > 0) {%>
    <ul style="list-style:circle;margin:10px 0px 20px 20px">
        <%foreach (var article in RelatiedArticles)
            { %>
        <li><a href="<%=HREF.LinkComponent("Article",article.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, article.Id )%>" title="<%=article.Title %>"><%=article.Title %></a></li>
        <%} %>
    </ul>
    <%} %>

                <%=dto.Content.PageArticleLink()%> 
                  <%if(DisplayTag){ %>
                <div class="tag">
                    <%=string.Join(", ", Tags)%> 
                </div>
    <%} %>
            </div>
        </div> 
      </div>
    </div>
  </div>

  <!-- end blog section -->