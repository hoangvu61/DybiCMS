<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<%if(Data.Count > 0){ %>
<section class="blog_w3ls py-3">
    <aside class="container">
        <div class="row">
            <%foreach(var item in this.Data) 
            {%>  
            <div class="col-lg-3 col-md-4 col-sm-6 mb-5">
                <div class="card border-0">
                    <div class="card-header p-0">
                        <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>">
                            <picture>
					            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="card-img-bottom" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        </a>
                        <%} %>
                    </div>
                    <div class="card-body p-0 border-0">
                        <div class="pt-2">
                            <h4 class="blog-title card-title font-weight-bold">
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>"><%=item.Title%></a>
                            </h4>
                            <p><%=item.Brief.Trim().Length > 120 ? item.Brief.Trim().Substring(0, 120) + "..." : item.Brief.Trim() %></p>
                        </div>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" class="blog-btn mt-2 d-inline-block">Đọc tiếp</a>
                    </div>
                </div>
            </div>     
            <%} %>
        </div>
    </aside> 
</section>
<%} %>