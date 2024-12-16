<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog section -->
<section class="blog_section layout_padding-bottom <%=HREF.CurrentComponent == "home" ? "" : "layout_padding-top"%>">
    <div class="container">
    <div class="heading_container">
        <%if(HREF.CurrentComponent == "home"){ %>
            <h2 style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
                <%=Title %>
            </h2>
        <%} else {%>
            <h1 title="<%=Page.Title %>"  style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
                <%=Title %>
            </h1>
        <%} %>
    </div>

    <%if(HREF.CurrentComponent != "home"){ %>
        <%if (!string.IsNullOrEmpty(Category.Brief)) { %>
        <p class="my-5">
            <%=Category.Brief.Replace("\n","<br />") %>
        </p>
        <%} %>
        <div class="category-contain mb-5">
            <%=Category.Content %>
        </div>
    <%} %>

    <div class="row">
        <%foreach(var item in this.Data) 
        {%> 
        <div class="col-md-4">
        <div class="box">
            <div class="img-box">
                <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                    <picture>
						<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                        <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                    </picture>
                </a>
                <%} %>
            </div>
            <div class="detail-box">
            <h4>
                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                    <%=item.Title%>
                </a>
            </h4>
            <p>
                <%=item.Brief %>
            </p>
            </div>
        </div>
        </div>
        <%} %>
    </div>
    </div>
</section>
<!-- end blog section -->