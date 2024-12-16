<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- team section start -->
<div class="team_section layout_padding">
    <%if(Skin.BodyBackgroundFile != null){ %>
        <style>
            .team_section{background: url('<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>')no-repeat top; background-size:cover}
        </style>
    <%} else if(!string.IsNullOrEmpty(this.Skin.BodyBackground)) {%>
        <style>
            .team_section{background: <%=this.Skin.BodyBackground%> !important}
        </style>
    <%}%>
    <div class="container">
        <%if(HREF.CurrentComponent == "home"){ %>
        <h2 class="highly_text"><%=Title %></h2>
        <%} else {%>
        <h1 class="highly_text"><%=Title %></h1>
        <%} %>
        <p class="who_text"><%=Category.Brief %></p>
        
        <div class="team_section_2_2">
            <div class="row">
                <div class="col-12 col-sm-12 col-lg-6 wow animate__animated animate__fadeInLeft animated" data-wow-duration="0.8s" data-wow-delay="0.3s" style="margin-bottom:20px">
                    <div class="row">
                        <%if(Category.Image != null && !string.IsNullOrEmpty(Category.Image.FullPath)) {%>
                        <div class="col-sm-6 col-lg-6">
                            <div class="image_4">                            
                                <picture>
					                <source srcset="<%=HREF.DomainStore + Category.Image.FullPath%>.webp" type="image/webp">
					                <source srcset="<%=HREF.DomainStore + Category.Image.FullPath%>" type="image/jpeg"> 
                                    <img src="<%=HREF.DomainStore + Category.Image.FullPath%>" alt="<%=Category.Title %>"/>
                                </picture>
                            </div>
                        </div>
                        <%} %>
                        <div class="col-sm-6 col-lg-6">
                            <p class="lorem_ipsum_text"><%=Category.Content.DeleteHTMLTag() %></p>
                            <%if(Top > 0 && TotalItems > Top){ %>
                            <div class="read_bt_main">
                                <div class="read_bt_2">
                                    <a href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="Xem tất cả <%= Category.Title%>" class="d-block mx-auto border btn-lg blog_more">Xem tất cả</a>
                                </div>
                            </div>
                            <%} %>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-12 col-lg-6 wow animate__animated animate__fadeInRight animated" data-wow-duration="0.8s" data-wow-delay="0.3s">
                    <div class="row">
                        <%for(int i = 0; i < Data.Count && i < 4; i++){ %>
                        <div class="col-6">
                            <div class="article-item">
                                <div class="article-item-box">
                                    <div class="image_5">
                                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>">
                                            <picture>
					                            <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
					                            <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                            </picture>
                                        </a>
                                    </div>
                                    <h3 class="johanson_text">
                                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>">
                                            <%= Data[i].Title%>
                                        </a>
                                    </h3>
                                </div>
                            </div>
                        </div>
                        <%} %>
                    </div>
                </div>
                <%for(int i = 4; i < Data.Count; i++){ %>
                    <div class="col-6 col-sm-6 col-lg-3 wow animate__animated animate__zoomIn animated" data-wow-duration="0.8s" data-wow-delay="0.3s">
                        <div class="article-item">
                            <div class="article-item-box">
                                <div class="image_5">
                                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>">
                                        <picture>
					                        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
					                        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                            <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                        </picture>
                                    </a>
                                </div>
                                <h3 class="johanson_text">
                                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>">
                                        <%= Data[i].Title%>
                                    </a>
                                </h3>
                            </div>
                        </div>
                    </div>
                <%} %>
            </div>
        </div>
    </div>
</div>
<!-- team section end -->

<%if(HREF.CurrentComponent == "article"){ %>
<script>
    $(document).ready(function () {
        $("#mnu<%=Category.Id%>").addClass("active");
    });
</script>
<%} %>