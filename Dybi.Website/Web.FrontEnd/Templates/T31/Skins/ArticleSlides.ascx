<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- client section -->
<section class="articles_section py-5">
    <div class="container">
        <div class="heading_container heading_center psudo_white_primary mb_45">
            <%if (HREF.CurrentComponent == "home")
            { %>
            <h2 title="<%=Category.Title %>">
                <a href="<%=HREF.LinkComponent("Articles", Category.Title.ConvertToUnSign(), true, "scat", Category.Id)%>"><%=Category.Title %></a>
            </h2>
            <%} else { %>
                <h2 title="<%=Category.Title %>"><%=Category.Title %></h2>
            <%} %>
            <p class="category_brief"><%=HREF.CurrentComponent != "home" ? Category.Brief : Title %></p>

        </div>
        <div id="carousel<%=Id %>" class="carousel slide" data-ride="carousel">
            <div class="carousel-inner">
            <%
                var page = Data.Count / 3;
                if (Data.Count % 3 != 0) page++;
                for (int j = 0; j < page; j++)
                {%> 
                <div class="carousel-item <%= j == 0 ? "active" : "" %>">
                    <div class="row">
                    <%for (int i = j * 3; i < Data.Count && i < (j * 3) + 3; i++) { %>
                        <div class="col-<%= i == j * 3 ? 12 : 6 %> col-md-4 my-3 my-lg-5">  
                            <div class="article-box">
                                <div class="article-item">
                                    <div class="article-image">
                                        <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                                            <%if (!string.IsNullOrEmpty(Data[i].ImageName))
                                                { %>
                                                <%if (Data[i].Image.FileExtension == ".webp")
                                                    { %>
                                                    <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %> class="box-img"/>
                                                <%}
                                                    else
                                                    { %>
                                                    <picture>
						                                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						                                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                                        <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>" class="box-img"/>
                                                    </picture>
                                                <%} %>
                                            <%} %>
                                        </a>
                                    </div>
                                    <div class="articel-content px-3">
                                        <a class="articletitle" href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                                            <%=Data[i].Title %>
                                        </a>
                                        <p class="mb-3">
                                            <%=this.Data[i].Brief.Length > 125 ? Data[i].Brief.Substring(0, 125) + "..." : Data[i].Brief %>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    <%} %>
                    </div>
                </div> 
            <%} %>
            </div>
            <a class="carousel-control-prev" href="#carousel<%=Id %>" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#carousel<%=Id %>" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>
</section>

<script>
    $('.carousel').carousel();
</script>