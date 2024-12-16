<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="products">
    <div class="pro_list_title">
        <%if(HREF.CurrentComponent == "home"){ %>
        <h2 class="cate_title" title="<%=Title %>"><%=Title %></h2>
        <%} else { %>
        <h1 class="cate_title" title="<%=Title %>"><%=Title %></h1>
        <%} %>
    </div>
    <div class="w3-row-padding spham">
        <%foreach(var item in this.Data) 
        {%>  
            <div class="w3-col m4 s6 i6">
                <div class="w3-row" style="height:110px; overflow:hidden">
                    <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                    <a class="w3-col m3 s12 i12" href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                        <picture>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    </a>
                    <%} %>
                    <div class="w3-col m9 s12 i12" style="padding:0px 20px 0px 10px">
                        <h2 title="<%=item.Title%>" class="article_title">
                            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>"><%=item.Title%></a>
                        </h2>
                        <p class="article_brief"><%=item.Brief.Trim().Length > 120 ? item.Brief.Trim().Substring(0, 120) + "..." : item.Brief.Trim() %></p>
                    </div>
                </div>
            </div>       
        <%} %>
    </div>
</div>
