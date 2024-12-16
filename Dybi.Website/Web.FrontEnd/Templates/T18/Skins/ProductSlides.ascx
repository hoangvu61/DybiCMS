<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- slider section -->
<aside class="slider_section ">
    <div id="customCarousel1" class="carousel slide pointer-event" data-ride="carousel">
        <div class="carousel-inner">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>  
                <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                <div class="container ">
                    <div class="row">
                        <div class="col-md-6 ">
                            <div class="detail-box">
                            <span class="title">
                                <%=Data[i].Title%>  
                            </span>
                            <p>
                                <%=Data[i].Brief.Replace("\n","<br />") %>
                            </p>
                            <div class="btn-box">
                                <a class="btn1" href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendProduct, Data[i].Id)%>" class="btn1">
                                    Xem chi tiết
                                </a>
                            </div>
                            </div>
                        </div>
                        <div class="col-md-6">
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
        <ol class="carousel-indicators">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                <li data-target="#customCarousel1" data-slide-to="<%=i %>" class="<%= i== 0 ? "active" : "" %>"></li>
            <%} %>
        </ol>
    </div>
</aside>
<!-- end slider section -->
