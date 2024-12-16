<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="service_section" style="margin-bottom:50px">
    <div class="container">
        <div style="margin-top:50px">
            <%if(HREF.CurrentComponent == "home"){ %>
            <h2 class="title text-center mb-md-4 mb-sm-3 mb-3 mb-2">
                <%=Title %>
            </h2>
            <%} else { %>
            <h1 class="title text-center mb-md-4 mb-sm-3 mb-3 mb-2">
                <%=Title %>
            </h1>
            <%} %>
        </div>
        <div class="title-wls-text text-center mb-lg-5 mb-md-4 mb-sm-4 mb-3">
            <p><%=Category.Brief  %> </p>
        </div>
        <div class="row">
        <%for(int i = 0; i < this.Data.Count; i++){%>  
            <%if(DisplayImage){ %>
                <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                    <div class="col-lg-3 col-md-6 col-sm-6" style="background: url('<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp')no-repeat 0px 0px;background-size:cover">
                    </div>
                <%} %>
                <div class="col-lg-3 col-md-6 col-sm-6 mid-text-info">
                    <div class="blog-info">
                        <ul>
                            <li style="border-bottom:1px solid"><%=string.Format("{0:dd}", Data[i].CreateDate) %></li>
                            <li><%=string.Format("{0:MM}", Data[i].CreateDate) %></li>
                        </ul>
                    </div>
                    <div class="mb-lg-4 mb-3">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                            <%=Data[i].Title %>
                        </a>
                    </div>
                    <p>
                        <%=!string.IsNullOrEmpty(Data[i].Brief) && Data[i].Brief.Length > 110 ? Data[i].Brief.Substring(0, 110) + "..." : Data[i].Brief %>
                    </p>
                </div>
            <%} else {%>
                <div class="col-lg-3 col-md-6 col-sm-6 mid-text-info">
                    <div class="mb-lg-4 mb-3">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                            <%=Data[i].Title %>
                        </a>
                    </div>
                    <p>
                        <%=!string.IsNullOrEmpty(Data[i].Brief) && Data[i].Brief.Length > 100 ? Data[i].Brief.Substring(0, 100) + "..." : Data[i].Brief %>
                    </p>
                </div>
                <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                    <div class="col-lg-3 col-md-6 col-sm-6" style="background: url('<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp')no-repeat 0px 0px;background-size:cover">
                        <div class="blog-info">
                            <ul>
                                <li style="border-bottom:1px solid"><%=string.Format("{0:dd}", Data[i].CreateDate) %></li>
                                <li><%=string.Format("{0:MM}", Data[i].CreateDate) %></li>
                            </ul>
                        </div>
                    </div>
                <%} %>
            <%} %>
            <%if(i % 2 == 1){ %>
                <%{ DisplayImage = !DisplayImage; }%>
                </div><div class="row">
            <%} %>
        <%} %>
        </div>
    </div>
</section>

<%if(HREF.CurrentComponent == "articles"){ %>
<script>
    $(document).ready(function () {
        $("#<%=Category.Id%>").addClass("active");
    });
</script>
<%} %>