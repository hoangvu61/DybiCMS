<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog section -->
<section class="blog_section layout_padding">
    <div class="container">
        <div class="heading_container">
            <h2>
                <%=Title %> 
            </h2>
        </div>
        <div class="row">
        <%foreach(var item in this.Data) 
        {%> 
            <div class="col-md-6">
                <div class="box">
                <div class="img-box">
                    <%if(GetValueParam<bool>("DisplayImage")){ %>
                        <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        </a>
                        <%} %>
                    <%} %>
                </div>
                <div class="detail-box">
                    <h3>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%=item.Title%>
                        </a>
                    </h3>
                    <p>
                        <%=item.Brief.Replace("\n", "<br />") %>
                    </p>
                </div>
                </div>
            </div>
        <%} %>
        </div>
    </div>
</section>
<!-- end blog section -->