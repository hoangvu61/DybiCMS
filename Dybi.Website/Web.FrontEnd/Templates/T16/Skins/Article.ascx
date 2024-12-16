<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Article.ascx.cs" Inherits="Web.FrontEnd.Modules.Article" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>


<!-- blog section -->

 <div class="blog">
         <div class="container">
            <div class="row">
               <div class="col-md-12">
                  <div class="titlepage text_align_center">
                     <h1><%=Title %></h1>
                  </div>
               </div>
            </div>
            <div class="row">
            <div class="col-md-4 col-lg-3 mx-auto box">
            <div class="img-box">
                <%if(DisplayImage){ %>
                <picture>
						  <source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>.webp" type="image/webp">
						  <source srcset="<%=HREF.DomainStore + dto.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=!string.IsNullOrEmpty(dto.ImageName) ? HREF.DomainStore + dto.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=Title %>"/>
                        </picture>
                <%} %>
                
            </div>
            </div>
            <div class="col-md-8 col-lg-9 mx-auto">
              <%if(DisplayBrief){ %>
            <div class="detail-box">
                <%if(DisplayDate){ %>
              <h4 class="blog_date">
                <%=string.Format("{0:dd/MM/yyyy}", dto.CreateDate) %>
              </h4>
                <%} %>  
              <p>
                  <%=dto.Brief %>
              </p> 
                <%if(RelatiedArticles.Count > 0) {%>
    <ul>
        <%foreach (var article in RelatiedArticles)
            { %>
        <li><a href="<%=HREF.LinkComponent("Article",article.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, article.Id )%>" title="<%=article.Title %>"><%=article.Title %></a></li>
        <%} %>
    </ul>
    <%} %>
            </div>
              <%} %>
            </div>
            <div class="col-md-12 col-lg-12" style="margin:20px">
                <%=dto.Content%> 
            

                  <%if(DisplayTag){ %>
                <div class="tag">
                    <%=string.Join(", ", Tags)%> 
                </div>
    <%} %>
            </div>
       
      </div>
    </div>
  </div>

  <!-- end blog section -->