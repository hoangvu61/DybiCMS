<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- slider section -->
        <div class="slider_section">
        <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
            <div class="carousel-inner">
            <%for(int i = 0; i<this.Data.Count; i++) 
            {%>  
            <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-6 offset-lg-1">
                    <div class="detail-box">
                        <div class="heading_box">
                        <h2 style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>">
                            <%=Data[i].Title%> 
                        </h2>
                        <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("SubTitle"))){ %>
                            <h1 style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
                                <%=Data[i].GetAttribute("SubTitle") %>
                            </h1>
                        <%} %>
                        </div>
                        <div>
                            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>">
                                Tìm hiểu thêm
                            </a>
                        </div>
                    </div>
                    </div>
                    <div class="col-lg-5 px-0">
                    <div class="img-box">
                        <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                            <picture>
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                            </picture>
                        <%} %>
                    </div>
                    </div>
                </div>
                </div>
            </div>
            <%} %>
            </div>
        </div>
        <div class="slider_btn-container">
            <a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev" > <span class="sr-only">Previous</span> </a>
            <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next"><span class="sr-only">Next</span></a>
        </div>
        </div>
        <!-- end slider section -->