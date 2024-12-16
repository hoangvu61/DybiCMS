<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog section -->
<section class="blogs_section layout_padding-top">
    <div class="container">
        <div class="heading_container mb-5 wow animate__ animate__fadeInUp animated" data-wow-duration="1.2s" data-wow-delay="0.2s">
        <%if(HREF.CurrentComponent == "home"){%>
                <h2 title="<%=Title %>">
                    <%=Title %>
                </h2>
            <%} else { %>
                <h1 title="<%=Title %>">
                    <%=Title %>
                </h1>
            <%} %>
        </div>
        <div class="row">
        <%foreach(var item in this.Data) 
        {%>  
        <div class="col-12 col-md-4 col-lg-4 mx-auto wow animate__ animate__fadeInUp animated" data-wow-duration="1.2s" data-wow-delay="0.2s">
            <div class="box">
                <div class="detail-box">
                    <%if(GetValueParam<bool>("DisplayImage")){ %>
                        <div class="img-box"> 
                            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                                <picture>
						            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                    <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                </picture>
                            <%} %>
                            </a>
                    
                        </div>
                    <%} %>
                    <div style="position: relative; padding: 30px 7px;text-align: justify;">
                        <span class="blog_date">
                            <%=string.Format("{0:dd/MM/yyyy}", item.CreateDate) %>
                        </span>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <h4 title="<%=item.Title%>"><%=item.Title%></h4>
                        </a>
                        <p><%=item.Brief %></p>
                    </div>
                </div>
            </div>
        </div> 
        <%} %>
        </div>
    </div>
</section>
<!-- end blog section -->


