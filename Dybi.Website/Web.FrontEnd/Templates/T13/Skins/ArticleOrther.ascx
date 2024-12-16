<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog section -->

<section class="blogs_section layout_padding">
    <div class="container">
        <div class="row">
        <%foreach(var item in this.Data) 
        {%>  
        <div class="col-12 col-md-4 col-lg-4 mx-auto">
            <div class="box">
                <div class="detail-box">
                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                        <h4 title="<%=item.Title%>" class="pt-3 px-3 text-justify"><%=item.Title%></h4>
                    </a>
                    <p class="p-3 text-justify">
                        <%=item.Brief %>
                    </p>
                    <div class="img-box"> 
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                                <picture>
						            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                    <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                </picture>
                            <%} %>
                        </a>
                    </div>
                </div>
            </div>
        </div> 
        <%} %>
        </div>
    </div>
</section>

  <!-- end blog section -->