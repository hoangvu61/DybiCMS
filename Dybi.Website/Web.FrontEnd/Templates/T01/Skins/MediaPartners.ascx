<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="partners_section py-5" <%=!string.IsNullOrEmpty(Skin.HeaderBackground) ? "style='background:" + Skin.HeaderBackground + "'" : ""%>>
    <div class="container">
        <%if (!string.IsNullOrEmpty(Category.ImageName))
            {%>
        <style>
            .entry-header {
                background-image: url('<%= HREF.DomainStore + Category.Image.FullPath%>') !important;
            }
        </style>
        <%}%>

        <div class="row text-center py-5" <%=!string.IsNullOrEmpty(Skin.HeaderFontColor) ? "style='color:" + Skin.HeaderFontColor + "'" : ""%>>
            <div class="wow animate__animated animate__fadeInUp animated"
                data-wow-duration="0.8s"
                data-wow-delay="0.3s">
                <h2 title="<%=Title %>"><%=Title %></h2>
                <p class="category_brief"><%= Category.Brief%></p>
            </div>
        </div>

        <div class="row article-list pb-5">
            <%foreach (var item in this.Data)
                {%>
            <div class="col-12 col-md-4 col-lg-6 mx-auto py-3 wow animate__animated animate__zoomIn animated"
                data-wow-duration="0.8s" data-wow-delay="0.3s">
                <a href="<%=item.Url%>" title="<%=item.Title%>" target="<%=item.Target %>">
                    <div class="article-item"
                        <%=!string.IsNullOrEmpty(Skin.BodyBackground) ? "style='background:" + Skin.BodyBackground + "'" : "" %>
                        style="border-radius: 15px; overflow: hidden; box-shadow: 0px 4px 8px rgba(0,0,0,0.1); transition: all 0.3s ease-in-out;">

                        <div class="d-flex flex-column flex-md-row align-items-center justify-content-center text-center p-3"
                            style="min-height: 200px; gap: 10px;">

                            <!-- Cột Hình Ảnh -->
                            <div class="col-12 col-md-4 d-flex align-items-center justify-content-center">
                                <% if (item.Image != null && !string.IsNullOrEmpty(item.Image.FullPath))
                                    { %>
                                <% if (item.Image.FullPath.EndsWith(".webp"))
                                    { %>
                                <img src="<%=HREF.DomainStore + item.Image.FullPath%>"
                                    alt="<%=item.Title %>"
                                    style="width: 100%; border-radius: 50%; object-fit: cover;" />
                                <% }
                                else
                                { %>
                                <picture class="article-image" style="border-radius: 50%; overflow: hidden; display: flex; align-items: center; justify-content: center; background-color: #fff;">
                                    <source srcset="<%=HREF.DomainStore + item.Image.FullPath %>.webp" type="image/webp">
                                    <source srcset="<%=HREF.DomainStore + item.Image.FullPath %>" type="image/jpeg">
                                    <img src="<%=HREF.DomainStore + item.Image.FullPath %>"
                                        alt="<%=item.Title %>"
                                        style="width: 100%; object-fit: cover;" />
                                </picture>
                                <% } %>
                                <% } %>
                            </div>

                            <!-- Cột Nội Dung -->
                            <div class="col-12 col-md-8 text-md-left text-center article-container">
                                <div class="article-title font-weight-bold py-2" style="font-size: 18px; color: #333;">
                                    <%=item.Title %>
                                </div>
                                <div class="articel-content pt-2 text-justify px-2 pb-2"
                                    title="<%=item.Brief.DeleteHTMLTag() %>"
                                    style="font-size: 16px; color: #666;">
                                    <%=item.Brief.DeleteHTMLTag() %>
                                </div>
                            </div>
                        </div>



                    </div>
                </a>
            </div>

            <%} %>
        </div>
    </div>

</section>
