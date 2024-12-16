<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="service_section">
    <div class="container">
        <div class="heading_container heading_center">
            <h2 class="h-title">
                <%=Title %>
            </h2>
        </div>
        <div class="row"> 
            <div class="col-md-10 col-md-offset-1">
                <div class="row">
                    <%foreach(var item in this.Data) 
                    {%>  
                    <div class="col-xs-6 col-sm-4 col-md-3">
                        <div class="box">
                            <a class="b-title" href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                                <%=item.Title %>
                            </a>
                            <p class="article-brief"><%=item.Brief %></p>
                        </div>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</section>