<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog section -->
<section class="section_other layout_padding-bottom">
    <div class="container">
    <div class="row">
        <%foreach(var item in this.Data) 
        {%> 
        <div class="col-6 col-md-4 mx-auto">
        <div class="box mt-5">
            <div class="img-box">
                <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                    <%if(item.Image.FullPath.EndsWith(".webp")){ %>
                        <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                    <%} else { %>
                        <picture>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    <%} %>
                </a>
                <%} %>
            </div>
            <div class="detail-box">
            <h4>
                <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                    <%=item.Title%>
                </a>
            </h4>
            <p>
                <%=item.Brief.Length > 140 ? item.Brief.Substring(0, 140).DeleteHTMLTag() + "..." : item.Brief.DeleteHTMLTag() %>
            </p>
            </div>
        </div>
        </div>
        <%} %>
    </div>
    </div>
</section>
<!-- end blog section -->