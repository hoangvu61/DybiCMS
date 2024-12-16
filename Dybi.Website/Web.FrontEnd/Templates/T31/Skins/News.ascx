<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="articles_section m-5">
    <%if(!string.IsNullOrEmpty(Category.ImageName)){%>
    <style>
        .entry-header{background-image: url('<%= HREF.DomainStore + Category.Image.FullPath%>') !important;}
    </style>
    <%}%>
    <div class="container">
        <div class="row text-center">
            <h2 title="<%=Category.Title %>">
                <%=Title %>
            </h2>
            <p class="category_brief text-center"><%=Category.Brief %></p>
        </div>

        <div class="row article-list py-5">
            <%foreach(var item in this.Data) {%>
            <div class="col-6 col-md-4 mx-auto py-3">
                <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                    <div class="article-item">
                        <div class="cards-thumbnail">
                            <%if(item.Image != null && !string.IsNullOrEmpty(item.Image.FullPath)){ %>
                                <%if(item.Image.FullPath.EndsWith(".webp")){ %>
                                    <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                <%} else { %>
                                    <picture>
						                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						                <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                    </picture>
                                <%} %>
                            <%} %>
                        </div>
                        <div class="articel-content">
                            <div class="w-100">
                                <h3><%=item.Title %></h3>
                                <div class="blog-first-para">
                                    <%=item.Brief.Length > 220 ? item.Brief.Substring(0, 220).DeleteHTMLTag() + "..." : item.Brief.DeleteHTMLTag() %>
                                </div>
                            </div>
                            <p class="readmore"><%=Language["LearnMore"] %></p>
                        </div>
                    </div>
                </a>
            </div>
            <%} %>
            <%if(Top > 0 && TotalItems > Top) {%>
            <div class="btn-box">
                <%if(HREF.CurrentComponent == "home") {%>
                    <a class="alm-btn-wrap" href="<%=HREF.LinkComponent("Articles", Category.Title.ConvertToUnSign(), true, "scat", Category.Id)%>" title="<%=Category.Title %>">
                        <%=Language["viewall"] %>
                    </a>
                <%} else {%>
                    <a class="btn btn-warning" href="<%=LinkPage(1)%>"><%=Language["firstpage"] %></a> 
                    <%for(int i = 1; i <= TotalPages; i++) { %>
                    <a class="btn btn-warning" style="padding: 10px;<%= i == CurrentPage ? "background:" + Config.Background + "75 !important" : "" %>;" href="<%=LinkPage(i)%>"><%=i %></a> 
                    <%} %>
                    <a class="btn btn-warning" href="<%=LinkPage(TotalPages)%>"><%=Language["lastpage"] %></a> 
                <%} %>
            </div>
            <%} %>
        </div>
    </div>
</section> 

<%if(HREF.CurrentComponent != "home"){ %>
<section class="bg25">
    <div class="container py-5" style="text-align:justify">
        <%=Category.Content %>
    </div>
</section>
<%} %>