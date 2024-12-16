<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- client section -->
<section class="articles_section layout_padding">
    <div class="container">
        <div class="heading_container heading_center psudo_white_primary mb_45">
            <%if(HREF.CurrentComponent == "home"){ %>
                <h2 title="<%=Category.Title %>">
                    <%=Title %>
                </h2>
            <%} else {%>
                <h1 title="<%=Page.Title %>">
                    <%=Title %>
                </h1>
            <%} %>
            <p style="margin-bottom:40px">
                <%=Category.Brief %>
            </p>
        </div>

        <div class="row">
            <%for(int i = 0; i < Data.Count ; i++) 
            {%> 
            <div class="col-12 col-md-4 item">                       
                <div class="img-box">
                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                        <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                            <%if(Data[i].Image.FileExtension == ".webp"){ %>
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>" class="box-img"/>
                            <%} else { %>
                                <picture>
						            <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						            <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                    <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>" class="box-img"/>
                                </picture>
                            <%} %>
                        <%} %>
                    </a>
                </div>
                <a class="articletitle" href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                    <%=this.Data[i].Title.Length > 55 ? Data[i].Title.Substring(0, 55) + "..." : Data[i].Title %>
                </a>
                <p>
                    <%=Data[i].Brief %>
                </p>
            </div> 
            <%} %>
        </div>

        <%if(HREF.CurrentComponent != "home"){ %>
            <div class="category-contain">
                <%=Category.Content %>
            </div>
        <%} %>
    </div>
</section>
<!-- end client section -->