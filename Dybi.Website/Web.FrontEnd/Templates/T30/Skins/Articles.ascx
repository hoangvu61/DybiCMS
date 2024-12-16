<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog section -->
<section class="blog_section layout_padding">
    <div class="container">
        <%if(HREF.CurrentComponent == "home"){ %>
        <div class="heading_container">
            <h2>
                <a href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
                    <%=Title %> 
                </a>
            </h2>
        </div>
        <%} else {%>
            <h1 title="<%=Category.Title %>"><%=Title %></h1>
            <div class="row">
                <div class="col-10 mx-auto" style="text-align:center"><%=Category.Brief %></div>
            </div>
        <%} %>
        <div class="row">
        <%foreach(var item in this.Data) 
        {%> 
            <div class="col-md-6">
                <div class="box">
                <div class="img-box">
                    <%if(GetValueParam<bool>("DisplayImage")){ %>
                        <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        </a>
                        <%} %>
                    <%} %>
                </div>
                <div class="detail-box">
                    <h3>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%=item.Title%>
                        </a>
                    </h3>
                    <p>
                        <%=item.Brief.Replace("\n", "<br />") %>
                    </p>
                </div>
                </div>
            </div>
        <%} %>
        </div>
        <%if(Top > 0 && TotalItems > Top) {%>
        <div class="btn-box">
            <%if(HREF.CurrentComponent == "home") {%>
                <a class="button2" href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
                Xem tất cả vài viết
                </a>
            <%} else {%>
                <a class="button2" href="<%=LinkPage(1)%>">Trang đầu</a> 
                <%for(int i = 1; i <= TotalPages; i++){ %>
                <a class="button2" style="margin: 0px 10px;<%= i == CurrentPage ? "background:" + Config.Background + "75 !important" : "" %>;" href="<%=LinkPage(i)%>"><%=i %></a> 
                <%} %>
                <a class="button2" href="<%=LinkPage(TotalPages)%>">Trang cuối</a> 
            <%} %>
        </div>
        <%} %>
    </div>
</section>
<!-- end blog section -->
