<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- service section -->

    <section class="service_section layout_padding">
      <div class="container py_mobile_45">
        <div class="heading_container heading_center">
          <h2>
              <%=Title %>
          </h2>
        </div>
        <div class="row">
        <%foreach(var item in this.Data) 
        {%>  
        <div class="col-md-<%= this.Data.Count % 4 == 0 ? 3 : 4%>">
            <div class="box ">
                <div class="img-box">
                    <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                    <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <picture>
						      <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						      <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        <%} %>
                    </a>
                </div>
                <div class="detail-box">
                    <h5>
                        <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%=item.Title%>
                        </a>
                    </h5>
                    <p>
                        <%=item.Brief %>
                    </p>
                </div>
            </div>
        </div>
        <%} %>
        </div>
      </div>
    </section>