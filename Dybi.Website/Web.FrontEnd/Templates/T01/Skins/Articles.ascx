    <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="articles_section py-5" <%=!string.IsNullOrEmpty(Skin.HeaderBackground) ? "style='background:" + Skin.HeaderBackground + "'" : ""%>>
    <div class="container">
        <%if(!string.IsNullOrEmpty(Category.ImageName)){%>
        <style>
            .entry-header{background-image: url('<%= HREF.DomainStore + Category.Image.FullPath%>') !important;}
        </style>
        <%}%>
        <div class="container">
            <div class="row text-center" <%=!string.IsNullOrEmpty(Skin.HeaderFontColor) ? "style='color:" + Skin.HeaderFontColor + "'" : ""%>>
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
        </div>

        <div class="row article-list py-5">
            <%for(int i = 0; i < this.Data.Count && i < 2; i++) {%>
            <div class="col-<%=i == 0 ? 12 : 6 %> col-md-6 mx-auto py-3">
                <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                    <div class="article-item" style="background:<%=Skin.BodyBackground%>;color:<%=Skin.BodyFontColor%>">
                        <div class="row">
                            <div class="col-12 col-sm col-lg-5">
                                <div class="cards-thumbnail article-image">
                                    <%if(Data[i].Image != null && !string.IsNullOrEmpty(Data[i].Image.FullPath)){ %>
                                        <%if(Data[i].Image.FullPath.EndsWith(".webp")){ %>
                                            <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                        <%} else { %>
                                            <picture>
				                                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
				                                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                            </picture>
                                        <%} %>
                                    <%} %>
                                </div>
                            </div>
                            <div class="col-12 col-sm-12 col-lg-7">
                                <div class="articel-content py-2">
                                    <h3 title="<%=Data[i].Title %>"><%=Data[i].Title %></h3>
                                    <div class="blog-first-para" title="<%=Data[i].Brief.DeleteHTMLTag() %>">
                                        <%=Data[i].Brief.Length > 140 ? Data[i].Brief.Substring(0, 140).DeleteHTMLTag() + "..." : Data[i].Brief.DeleteHTMLTag() %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
            <%} %>

            <%for(int i = 2; i < this.Data.Count && i < 5; i++) {%>
            <div class="col-<%=i == 0 ? 12 : 6 %> col-md-4 mx-auto py-3">
                <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                    <div class="article-item" style="background:<%=Skin.BodyBackground%>;color:<%=Skin.BodyFontColor%>">
                        <div class="articel-content p-3">
                            <h3 title="<%=Data[i].Title %>"><%=Data[i].Title %></h3>
                            <div class="blog-first-para" title="<%=Data[i].Brief.DeleteHTMLTag() %>">
                                <%=Data[i].Brief.Length > 175 ? Data[i].Brief.Substring(0, 175).DeleteHTMLTag() + "..." : Data[i].Brief.DeleteHTMLTag() %>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
            <%} %>

            <%for(int i = 5; i < this.Data.Count; i++) {%>
            <div class="col-6 col-lg-3 mx-auto py-3">
                <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                    <div class="article-item" style="background:<%=Skin.BodyBackground%>;color:<%=Skin.BodyFontColor%>">
                        <div class="article-image">
                            <%if(Data[i].Image != null && !string.IsNullOrEmpty(Data[i].Image.FullPath)){ %>
                                <%if(Data[i].Image.FullPath.EndsWith(".webp")){ %>
                                    <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                <%} else { %>
                                    <picture>
		                                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
		                                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                    </picture>
                                <%} %>
                            <%} %>
                        </div>
                        <div class="p-2">
                            <h3 title="<%=Data[i].Title %>"><%=Data[i].Title %></h3>
                            <div class="blog-first-para" title="<%=Data[i].Brief.DeleteHTMLTag() %>">
                                <%=Data[i].Brief.Length > 140 ? Data[i].Brief.Substring(0, 140).DeleteHTMLTag() + "..." : Data[i].Brief.DeleteHTMLTag() %>
                            </div>
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
<section class="articles_section m-5">
    <%=Category.Content %>
</section>
<%} %>