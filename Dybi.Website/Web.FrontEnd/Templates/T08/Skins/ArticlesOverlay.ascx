<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<%if(Data.Count > 0){ %>
<section class="service_section" style="margin-bottom:50px">
    <div class="container">
        <div class="row"> 
            <div class="col-md-10 mx-auto">
                <div class="row">
                    <div class="col-12 col-lg-4" style="margin-bottom:30px">
                        <div class="box">
                            <div class="row">
                                <div class="col-lg-4 col-2">
                                    <%if(!string.IsNullOrEmpty(Data[0].ImageName)){ %>
                                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[0].Title.ConvertToUnSign(), Data[0].Id, SettingsManager.Constants.SendArticle, Data[0].Id)%>" title="<%=Data[0].Title%>">
                                            <picture>
						                        <source srcset="<%=HREF.DomainStore + Data[0].Image.FullPath%>.webp" type="image/webp">
						                        <source srcset="<%=HREF.DomainStore + Data[0].Image.FullPath%>" type="image/jpeg"> 
                                                <img src="<%=HREF.DomainStore + Data[0].Image.FullPath%>" alt="<%=Title %>" style="width:100%"/>
                                            </picture>
                                        </a>
                                    <%} %>
                                </div>
                                <div class="col-lg-8 col-10 box-text1">
                                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[0].Title.ConvertToUnSign(), Data[0].Id, SettingsManager.Constants.SendArticle, Data[0].Id)%>" title="<%=Data[0].Title%>">
                                        <%if(!string.IsNullOrEmpty(Data[0].GetAttribute("SubTitle"))){ %>   
                                            <%=Language[Data[0].GetAttribute("SubTitle")] %>
                                        <%} else {%>
                                            <%=Data[0].Title %>
                                        <%} %>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%for(int i = 1; i < this.Data.Count; i++) 
                    {%>  
                        <div class="col-6 col-lg-4" style="margin-bottom:30px">
                            <div class="box">
                                <div class="row">
                                    <div class="col-lg-4 d-none d-lg-block">
                                        <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                                            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                                                <picture>
						                            <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						                            <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                                    <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Title %>" style="width:100%"/>
                                                </picture>
                                            </a>
                                        <%} %>
                                    </div>
                                    <div class="col-lg-8 col-md-12 box-text2">
                                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                                            <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("SubTitle"))){ %>   
                                                <%=Language[Data[i].GetAttribute("SubTitle")] %>
                                            <%} else {%>
                                                <%=Data[i].Title %>
                                            <%} %>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</section>
<%} %>