<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="service_section" style="margin-bottom:50px">
    <div class="container">
        <div class="row">
        <%for(int i = 0; i < this.Data.Count; i++){%>  
            <%if(Top == 4){ %>
                <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                    <div class="col-lg-3 col-md-6 col-sm-6">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Title %>" style="width:100%"/>
                            </picture>
                        </a>
                    </div>
                <%} %>
                <div class="col-lg-3 col-md-6 col-sm-6 mid-text-info">
                    <div class="blog-info">
                        <ul>
                            <li style="border-bottom:1px solid"><%=string.Format("{0:dd}", Data[i].CreateDate) %></li>
                            <li><%=string.Format("{0:MM}", Data[i].CreateDate) %></li>
                        </ul>
                    </div>
                    <div class="mb-lg-4 mb-3">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                            <%=Data[i].Title %>
                        </a>
                    </div>
                    <p>
                        <%=!string.IsNullOrEmpty(Data[i].Brief) && Data[i].Brief.Length > 100 ? Data[i].Brief.Substring(0, 100) + "..." : Data[i].Brief %>
                    </p>
                </div>
            <%} else {%>
                <div class="col-lg-3 col-md-6 col-sm-6 mid-text-info">
                    <div class="mb-lg-4 mb-3">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                            <%=Data[i].Title %>
                        </a>
                    </div>
                    <p>
                        <%=!string.IsNullOrEmpty(Data[i].Brief) && Data[i].Brief.Length > 100 ? Data[i].Brief.Substring(0, 100) + "..." : Data[i].Brief %>
                    </p>
                </div>
                <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                    <div class="col-lg-3 col-md-6 col-sm-6">
                        <div class="blog-info">
                            <ul>
                                <li style="border-bottom:1px solid"><%=string.Format("{0:dd}", Data[i].CreateDate) %></li>
                                <li><%=string.Format("{0:MM}", Data[i].CreateDate) %></li>
                            </ul>
                        </div>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Title %>" style="width:100%"/>
                            </picture>
                        </a>
                    </div>
                <%} %>
            <%} %>
            <%if(i % 2 == 1){ %>
                <%{ Top = Top == 4 ? 5 : 4; }%>
                </div><div class="row">
            <%} %>
        <%} %>
        </div>
    </div>
</section>