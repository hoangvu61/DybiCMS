<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%if(this.Data.Count > 0) { %>
<!-- slider section -->
<aside class="slider_section py-5">
    <div id="carouselBeseller" class="carousel slide" data-bs-ride="carousel">
        <div class="carousel-inner">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>  
            <div class="carousel-item <%= i== 0 ? "active" : "" %>">
                <div class="container">
                    <div class="row">
                        <div class="col-12 col-md-4 order-md-2">
                            <div class="img-box">
                                <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendProduct, Data[i].Id)%>">
                                    <picture>
						                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                    </picture>
                                </a>
                                <%} %>
                            </div>
                        </div>
                        <div class="col-12 col-md-8 order-md-1">
                            <div class="detail-box p-0 p-lg-4 p-md-3 p-sm-2">
                                <span class="title">
                                    <%=Data[i].Title%>  
                                </span>
                                <p>
                                    <%=Data[i].Brief.Replace("\n","<br />") %>
                                </p>
                                <div class="btn-box">
                                    <a class="btn btn-light btn-lg" href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendProduct, Data[i].Id)%>">
                                        Xem chi tiết
                                    </a>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
            <%} %>
        </div>
        <div class="carousel-indicators">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                <button type="button" data-bs-target="#carouselBeseller" data-bs-slide-to="<%=i %>" class="<%= i== 0 ? "active" : "" %>" aria-current="true" aria-label="<%=i + 1 %>"><%=i + 1 %></button>
            <%} %>
        </div>
    </div>
</aside>
<!-- end slider section -->
<%} %>
