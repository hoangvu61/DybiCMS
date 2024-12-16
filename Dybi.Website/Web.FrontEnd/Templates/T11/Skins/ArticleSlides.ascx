<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- banner section start -->
<div class="banner_section layout_padding">
<div id="carouselExampleSlidesOnly" class="carousel slide" data-ride="carousel">
    <div class="carousel-inner">
    <%for(int i = 0; i < this.Data.Count; i++) 
    {%>  
        <div class='carousel-item <%= i == 0 ? "active" : ""%>'>
            <div class="container">
            <h1 class="banner_taital" style="font-size:<%=Skin.HeaderFontSize %>px">
                <%=Data[i].Title%>
            </h1>
            <p class="banner_text">
                <%=Data[i].Brief %>
            </p>
            <div class="read_bt"><a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>">View Detail</a></div>
            </div>
        </div>
        <%} %>
    </div>
</div>
</div>
<!-- banner section end -->

