<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<section class="blog_section">
        <div class="container ">
        <div class="heading_container heading_center">
        <h2><%=Title %></h2>
        </div>
        <div class="row">
                    <%foreach(var item in Data) 
  {%>  
<div class="col-md-6 col-sm-12 col-xs-12"  style="margin-top:20px">
                        <a href="<%=HREF.LinkComponent("Article",item.Title.ConvertToUnSign(),true,"sart", item.Id)%>">
                            <%if(item.Image != null){ %>
                            <figure class="img_news_h">
                                <picture>
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore + item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                                    </picture>
                            </figure>
                            <%} %>
                            <div class="info_news_h">
                                <h3><%=item.Title %></h3>
                                <p><%=item.Brief %></p>
                                <span><%=Language["ReadMore"] %> <i class="glyphicon glyphicon-arrow-right"></i></span>
                            </div>
                        </a></div>
                <%} %>
            </div>
        </div><!-- End .min_wrap -->
    </section>