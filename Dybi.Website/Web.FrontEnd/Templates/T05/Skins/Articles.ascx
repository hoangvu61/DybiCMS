<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- services section start -->
<div class="news_section layout_padding">
    <%if(Skin.BodyBackgroundFile != null){ %>
        <style>
            .services_section{background: url('<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>')no-repeat top; background-size:cover}
        </style>
    <%} else if(!string.IsNullOrEmpty(this.Skin.BodyBackground)){%>
        <style>
            .services_section{background: <%=this.Skin.BodyBackground%> !important}
        </style>
    <%}%>
    <div class="container">
        <%if(HREF.CurrentComponent == "home"){ %>
        <h2 class="news_text"><%=Title %></h2>
        <%} else {%>
        <h1 class="news_text"><%=Title %></h1>
        <%} %>
        <p class="news_smile_text"><%=Category.Brief %></p>
        <div class="services_section_2_2">
            <div class="row">
                <%foreach(var item in this.Data) {%>
                <div class="col-6 col-md-3 wow animate__animated animate__zoomIn animated" data-wow-duration="0.8s" data-wow-delay="0.3s">
                    <div class="article-item">
                        <div class="article-item-box">
                            <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                            <div class="icon_2">
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>">
                                    <picture>
					                    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					                    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                    </picture>
                                </a>
                            </div>
                            <%} %>
                            
                            <h3 class="dental_text" title="<%=item.Title %>">
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>"><%=item.Title%></a>
                            </h3>
                            <p class="many_text"><%=item.Brief.Trim().Length > 65 ? item.Brief.Trim().Substring(0, 65) + "..." : item.Brief.Trim() %></p>
                        </div>
                    </div>
                </div>
                <%} %>
                <%if(Top > 0 && TotalItems > Top) {%>
                <div class="btn-box">
                    <%if(HREF.CurrentComponent == "home") {%>
                        <a class="read_bt_1" href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
                            Xem thêm
                        </a>
                    <%} else {%>
                        <nav aria-label="Page navigation example">
                            <ul class="pagination pagination-lg">
                                <li class="page-item"><a class="page-link" href="<%=LinkPage(1)%>">Trang đầu</a></li>
                                <%for(int i = 1; i <= TotalPages; i++){ %>
                                <li><a class="page-link <%= i == CurrentPage ? "active" : ""%>" href="<%=LinkPage(i)%>"><%=i %></a></li> 
                                <%} %>
                                <li class="page-item"><a class="page-link" href="<%=LinkPage(TotalPages)%>">Trang cuối</a></li>
                            </ul>
                        </nav>
                    <%} %>
                </div>
                <%} %>
            </div>
        </div>

        <%if(HREF.CurrentComponent != "home" && !string.IsNullOrEmpty(Category.Content.DeleteHTMLTag().Trim())){ %>
        <div class="alert alert-primary" style="margin-top:50px" role="alert">
            <div class="category-contain">
                <%=Category.Content %>
            </div>
        </div>
        <%} %>
    </div>
</div>
<!-- services section end -->

<%if(HREF.CurrentComponent == "article"){ %>
<script>
    $(document).ready(function () {
        $("#mnu<%=Category.Id%>").addClass("active");
    });
</script>
<%} else { %>
<%if(HREF.CurrentComponent == "articles"){ %>
<script>
    $(document).ready(function () {
        $("#mnu<%=Category.ParentId == null ? Category.Id : Category.ParentId %>").addClass("active");
    });
</script>
<%} }%>
