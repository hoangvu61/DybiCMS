<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- slider section -->
<section class="slider_section ">
    <div class="slider_bg_box">
        <%if(Skin.BodyBackgroundFile != null){ %>
            <img src="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" alt="<%=Title %>">
        <%} %>
    </div>
    <div id="customCarousel1" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <%for(int i = 0; i<this.Data.Count; i++) 
            {%>  
            <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                <div class="container ">
                <div class="row">
                    <div class="col-md-7 col-lg-6 ">
                        <div class="detail-box">
                            <h1 style="<%=Skin.HeaderFontSize > 0 ? "font-size:" + Skin.HeaderFontSize + "px" : ""%>">
                                    <span>
                                    <%=Data[i].Title%> 
                                    </span>
                                    <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("SubTitle"))){ %>
                                    <br /> <%=Data[i].GetAttribute("SubTitle") %>
                                    <%} %>
                            </h1>
                            <p>
                                <%=Data[i].Brief.Replace("\n","<br />") %>
                            </p>
                            <div class="btn-box">
                                <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>" class="btn1">
                                    Chi tiết
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
            <%} %>
        </div>
        <div class="container">
            <ol class="carousel-indicators"> 
                <%for(int i = 0; i < this.Data.Count; i++) 
                {%>
                    <li data-target="#customCarousel1" data-slide-to="<%=i %>" class="<%= i== 0 ? "active" : "" %>"></li>
                <%} %>
            </ol>
        </div>
    </div>
</section>
<!-- end slider section -->