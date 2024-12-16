<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="media_service_section py-5" <%=!string.IsNullOrEmpty(Skin.HeaderBackground) ? "style='background:" + Skin.HeaderBackground + "'" : ""%>>
    <div class="container">
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

        <div class="row article-list pb-5">
            <%foreach (var item in this.Data)
                {%>
            <div class="col-12 col-lg-4 mx-auto py-3 wow animate__animated animate__zoomIn animated" data-wow-duration="0.8s"
                data-wow-delay="0.3s">
                <a href="<%=item.Url%>" title="<%=item.Title%>" target="<%=item.Target %>">
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
