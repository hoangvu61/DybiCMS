<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- client section -->
<section class="articles_section p-5 m-3">
            <%if(HREF.CurrentComponent == "home"){ %>
                <h2 title="<%=Category.Title %>">
                    <%=Title %>
                </h2>
            <%} else {%>
                <h1 title="<%=Title %>">
                    <%=Page.Title %>
                </h1>
            <%} %>
            <p class="py-4">
                <%=Category.Brief %>
            </p>

        <%if(HREF.CurrentComponent != "home"){ %>
            <div class="pb-5">
                <%=Category.Content %>
            </div>
        <%} %>

        <div class="row">
            <%for(int i = 0; i < Data.Count ; i++) 
            {%> 
            <div class="col-12 col-md-4 col-lg-3 item">                       
                <div class="img-box">
                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                        <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                            <%if(Data[i].Image.FileExtension == ".webp"){ %>
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %> class="box-img""/>
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
                    <%= Data[i].Title %>
                </a>
                <p>
                    <%=Data[i].Brief %>
                </p>
            </div> 
            <%} %>
        </div>
</section>
<!-- end client section -->