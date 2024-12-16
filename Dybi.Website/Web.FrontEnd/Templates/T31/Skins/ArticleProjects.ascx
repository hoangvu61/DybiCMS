<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Article.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="media_service_section py-5" <%=!string.IsNullOrEmpty(Skin.HeaderBackground) ? "style='background:" + Skin.HeaderBackground + "'" : ""%>>
    <div class="container">
        <%if (HREF.CurrentComponent == "home") { %>
        <div class="text-center py-5" <%=!string.IsNullOrEmpty(Skin.HeaderFontColor) ? "style='color:" + Skin.HeaderFontColor + "'" : ""%>>
            <div class="wow animate__animated animate__fadeInUp animated"
                data-wow-duration="0.8s"
                data-wow-delay="0.3s">
                <h2 title="<%=Title %>"><%=Title %></h2>
                <p class="category_brief">
                    <%= Category.Brief%>
                </p>
            </div>
        </div>
        <%} else { %>
        <div class="pb-5" style="border-bottom: 1px solid #ccc;">
            <div class="row" style="align-items:center">
                <%if (Category.Image != null) { %>
                <div class="col-12 col-md-6 order-md-2 wow animate__animated animate__fadeInRight animated"
                    data-wow-duration="0.8s"
                    data-wow-delay="0.3s">
                    <div class="img-box" <%=Skin.Width > 0 ? "style='border-radius:" + Skin.Width + "% " + (Skin.Height > 0 ? Skin.Height + "%" : "") + "'" : "" %>>
                        <picture>
	                        <source srcset="<%=HREF.DomainStore + Category.Image.FullPath%>.webp" type="image/webp">
	                        <source srcset="<%=HREF.DomainStore + Category.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Category.Image.FullPath%>" alt="<%=Category.Title %>"/>
                        </picture>
                    </div>
                </div>
                <%} %>
                <div class="col-12 col-md-6 pt-3 mx-auto order-md-1 wow animate__animated animate__fadeInLeft animated"
                    data-wow-duration="0.8s"
                    data-wow-delay="0.3s">
                    <h1 class="wow animate__animated animate__fadeInDown animated"
                        data-wow-duration="0.8s"
                        data-wow-delay="0.3s" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "": ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>" title="<%=Category.Title %>">
                        <%=Page.Title %>
                    </h1>
                    <p class="text-justify">
                        <%=Category.Brief %>
                    </p>
                    <a class="btn mt-3" href="<%=HREF.LinkComponent("Contact", "lien-he", true) %>"><%=Language["Contact"]%></a>
                </div>
            </div>

            <div class="mt-5 wow animate__animated animate__zoomIn animated">
                <%=Category.Content %>
            </div>
        </div>
        <%} %>
        <div class="row article-list pb-5">
            <%foreach (var item in this.Data)
                {%>
            <div class="col-12 col-lg-3 mx-auto py-3 wow animate__animated animate__zoomIn animated" data-wow-duration="0.8s"
                data-wow-delay="0.3s">
                <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                    <div class="article-item" <%=!string.IsNullOrEmpty(Skin.BodyBackground) ? "style='background:" + Skin.BodyBackground + "'" : ""%>>
                        <div class="article-image">
                            <%if (item.Image != null && !string.IsNullOrEmpty(item.Image.FullPath))
                                { %>
                            <%if (item.Image.FullPath.EndsWith(".webp"))
                                { %>
                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>" />
                            <%}
                            else
                            { %>
                            <picture>
                                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
                                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg">
                                <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>" />
                            </picture>
                            <%} %>
                            <%} %>
                        </div>
                        <div class="p-3">
                            <h3 title="<%=item.Title %>" class="text-center py-2 font-weight-bold"><%=item.Title %></h3>
                            <div class="blog-first-para text-justify" title="<%=item.Brief.DeleteHTMLTag() %>">
                                <%=item.Brief.DeleteHTMLTag() %>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
            <%} %>
        </div>
    </div>
</section>