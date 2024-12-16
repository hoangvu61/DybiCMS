<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="attribute-section">
    <div class="container">
        <div class="row">
        <%for(int i = 0; i < Data.Count; i++) 
        {%>
            <div class="col-12 col-md-4">
                <div class="attribute-box box<%=i %>">
                    <a href="<%=HREF.LinkComponent(Data[i].ComponentList, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendCategory, Data[i].Id) %>" title="<%=Data[i].Title %>">
                        <div class="row">
                            <div class="col-4 col-sm-4 col-md-12 col-lg-4 image-box">
                                <%if(Data[i].Image != null && !string.IsNullOrEmpty(Data[i].ImageName)){ %>
                                    <%if(Data[i].Image.FileExtension == ".webp"){ %>
                                        <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                    <%} else { %>
                                        <picture>
                                            <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
                                            <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                            <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                        </picture>
                                    <%} %>
                                <%} %>
                            </div>
                            <div class="col-8 col-sm-8 col-md-12 col-lg-8 detail-box">
                                <h2 title="<%=Data[i].Title %>"><%=Data[i].Title%></h2>
                                <p><%=Data[i].Brief %></p>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        <%} %>
        </div>
    </div>
</section>