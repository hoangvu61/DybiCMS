<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- us section -->
<section class="us_section layout_padding">
    <div class="container">
        <div class="row">
        <div class="col-md-5">
            <div class="img-box">
                <%if(Category.Image != null){ %>
                <picture>
					<source srcset="<%=HREF.DomainStore + Category.Image.FullPath%>.webp" type="image/webp">
					<source srcset="<%=HREF.DomainStore + Category.Image.FullPath%>" type="image/jpeg"> 
                    <img src="<%=HREF.DomainStore + Category.Image.FullPath%>" alt="<%=Category.Title %>"/>
                </picture>
                <%} %>
            </div>
        </div>
        <div class="col-md-7">
            <div class="detail-box">
            <div class="heading_container">
                <h2>
                <%=Title %>
                </h2>
            </div>
            <div class="box">
                <%for(int i = 0; i < this.Data.Count; i++){ %>
                <div class="text-box">
                    <div class="number-box">
                    <h5>
                    0<%=i+1 %>
                    </h5>
                </div>
                    <h6>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                            <%=Data[i].Title%>
                        </a>
                    </h6>
                </div>
                <%} %>
            </div>
            </div>
        </div>
        </div>
    </div>
</section>
<!-- end us section -->

