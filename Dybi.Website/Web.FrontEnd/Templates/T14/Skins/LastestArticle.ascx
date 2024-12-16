<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- care section -->
<section class="care_section layout_padding">
    <div class="container">
        <div class="row">
        <%foreach(var item in this.Data) 
        {%>
            <div class="col-md-6">
                <div class="detail-box">
                    <div class="heading_container">
                        <h2>
                            <%=item.Title%>
                        </h2>
                    </div>
                    <p>
                    <%=item.Brief %>
                    </p>
                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                        Xem thêm
                    </a>
                </div>
            </div>
            <div class="col-md-6">
                <div class="img-box">
                    <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <picture>
					        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    <%} %>
                </div>
            </div>
        <%} %>
        </div>
    </div>
</section>
<!-- end care section -->