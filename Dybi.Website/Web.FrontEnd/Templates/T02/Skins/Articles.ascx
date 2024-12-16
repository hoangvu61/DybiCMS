<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="service_section">
    <div class="container">
        <div class="heading_container heading_center">
            <%if(HREF.CurrentComponent == "home"){ %>
            <h2 class="h-title" title="<%=Category.Title %>">
                <%=Title %>
            </h2>
            <%} else {%>
            <h1 class="h-title" title="<%=Category.Title %>">
                <%=Title %>
            </h1>
            <%} %>
            <p><%=Category.Brief %></p>
        </div>
        <div class="row"> 
            <div class="col-md-10 col-md-offset-1">
                <div class="row">
                    <%if(DisplayImage){ %>
                        <%foreach(var item in this.Data) 
                        {%>  
                        <div class="col-xs-12 col-sm-4">
                            <div class="box">
                                <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
					            <a class="image img-scaledown" href="<%=HREF.LinkComponent(Category.ComponentDetail,item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title %>%>">
                                    <picture>
					                    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					                    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%= HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                    </picture>
                                </a>
                                <%} %>
                                <a class="b-title" href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                                    <%=item.Title %>
                                </a>
                                <p class="article-brief"><%=item.Brief %></p>
                            </div>
                        </div>
                        <%} %>
                        <script>
                            $(document).ready(function () {
                                if ($('.service_section img:first').height() == 0)
                                    $('.service_section img').height($('.service_section .box:first').width());
                                else
                                    $('.service_section img').height($('.service_section img:first').height());
                            });
                        </script>
                    <%} else { %>
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
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</section>