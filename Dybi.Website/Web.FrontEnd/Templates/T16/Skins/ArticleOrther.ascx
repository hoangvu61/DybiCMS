<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog section -->

  <section class="blog_section layout_padding">
    <div class="container">
      <div class="row">
          <%foreach(var item in this.Data) 
        {%>  
        <div class="col-6 col-md-4 col-lg-4 mx-auto">
          <div class="box">
            <div class="img-box">
                <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                    <picture>
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    </a>
                <%} %>
              <h4 class="blog_date">
                <%=string.Format("{0:dd/MM/yyyy}", item.CreateDate) %>
              </h4>
            </div>
            <div class="detail-box">
              <h5>
                  <%=item.Title%>
              </h5>
              <p>
                  <%=item.Brief %>
              </p>
              <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                Xem thêm <i class="fa fa-long-arrow-right" aria-hidden="true"></i>
              </a>
            </div>
          </div>
        </div> 
        <%} %>
      </div>
    </div>
  </section>

  <!-- end blog section -->