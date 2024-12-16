<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="blog_w3ls py-5">
    <div class="container py-lg-5">
        <div class="text-center wthree-title pb-5">
            <%if(HREF.CurrentComponent == "home"){ %>
            <h2 class="w3l-sub pb-5" title="<%=Title %>">
                <%=Title %>
            </h2>
            <%} else { %>
            <h1 class="w3l-sub pb-5" title="<%=Title %>">
                <%=Title %>
            </h1>
            <%} %>
            <p class="mx-auto"><%=Category.Brief %></p>
        </div>
        <div class="row py-sm-5">
            <%foreach(var item in this.Data) 
            {%>  
            <div class="col-lg-3 col-md-4 col-sm-6 mb-5">
                <div class="card border-0">
                    <div class="card-header p-0">
                        <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>">
                            <picture>
					            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="card-img-bottom" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        </a>
                        <%} %>
                    </div>
                    <div class="card-body p-0 border-0">
                        <div class="pt-2">
                            <h4 class="blog-title card-title font-weight-bold">
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>"><%=item.Title%></a>
                            </h4>
                            <p><%=item.Brief.Trim().Length > 120 ? item.Brief.Trim().Substring(0, 120) + "..." : item.Brief.Trim() %></p>
                        </div>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" class="blog-btn mt-2 d-inline-block">Đọc tiếp</a>
                    </div>
                </div>
            </div>     
            <%} %>
            <%if(Top > 0 && TotalItems > Top){ %>
            <a href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="Xem tất cả <%= Category.Title%>" class="d-block mx-auto border btn-lg blog_more">
                Xem tất cả
            </a>
            <%} %>
        </div>
    </div>
</section>

<%if(HREF.CurrentComponent == "article"){ %>
<script>
    $(document).ready(function () {
        $("#mnu<%=Category.Id%>").addClass("active");
    });
</script>
<%} %>

<script>
    $(document).ready(function () {
        if ($('.blog_w3ls .card-header img:first').height() == 0)
            $('.blog_w3ls .card-header img').height($('.blog_w3ls .card-header:first').width());
        else
            $('.blog_w3ls .card-header img').height($('.blog_w3ls .card-header img:first').height());
    });
</script>