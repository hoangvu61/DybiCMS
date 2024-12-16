    <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="articles_news_section py-5" <%=!string.IsNullOrEmpty(Skin.HeaderBackground) ? "style='background:" + Skin.HeaderBackground + "'" : ""%>>
    <div class="container">
        <div class="row">
            <div class="col-12 col-md-7 wow animate__animated animate__fadeInLeft animated" <%=!string.IsNullOrEmpty(Skin.HeaderFontColor) ? "style='color:" + Skin.HeaderFontColor + "'" : ""%>>
                <h2 title="<%=Category.Title %>">
                    <a href="<%=HREF.LinkComponent("Articles", Category.Title.ConvertToUnSign(), true, "scat", Category.Id)%>"><%=Category.Title %></a>
                </h2>
                <p class="text-justify"><%=Category.Brief %></p>
            </div> 
            <div class="col-12 col-md-5 pt-3 wow animate__animated animate__fadeInRight animated"
                 data-wow-duration="0.8s"
                 data-wow-delay="0.3s">
                <div class="listnews">
                <%for(int i = 0; i < this.Data.Count && i < 3; i++) {%>
                    <div class="article_new mb-3">
                        <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                            <%if(Data[i].Image != null && !string.IsNullOrEmpty(Data[i].Image.FullPath)){ %>
                                <%if(Data[i].Image.FullPath.EndsWith(".webp")){ %>
                                    <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>" style="height:75px!important; width:unset; float:left"/>
                                <%} else { %>
                                    <picture>
	                                    <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
	                                    <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>" style="height:75px!important; width:unset; float:left"/>
                                    </picture>
                                <%} %>
                            <%} %> 
                            <%--<div style="">--%>
                                <span class="new_title" title="<%=Data[i].Title %>"><%=Data[i].Title %></span>
                            <hr />
                                <%--<div class="blog-first-para" title="<%=Data[i].Brief.DeleteHTMLTag() %>">
                                    <%=Data[i].Brief.Length > 130 ? Data[i].Brief.Substring(0, 130).DeleteHTMLTag() + "..." : Data[i].Brief.DeleteHTMLTag() %>
                                </div>
                            </div>--%>
                        </a>
                    </div>
                 <%} %>
                 </div>
             </div>
        </div>
    </div>
</section>