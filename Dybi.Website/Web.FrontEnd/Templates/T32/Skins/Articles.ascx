<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="service_section layout_padding">
        <div class="container ">
        <div class="heading_container heading_center">
        <h2><%=Title %></h2>
        </div>

            <div class="m_news_h col-md-12 col-sm-12 col-xs-12">
                <ul>
                    <%foreach(var item in Data.Take(4)) 
  {%>  
                	 <li>
                        <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, "sart", item.Id)%>">
                            <figure class="img_news_h">
                                <picture>
						      <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						      <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore + item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                            </picture>
                                    </figure>
                            <div class="info_news_h">
                                <h3><%=item.Title %></h3>
                                <p><%=item.Brief %></p>
                                <span><%=Language["ReadMore"] %> <i class="glyphicon glyphicon-arrow-right"></i></span>
                            </div>
                        </a>
                    </li>
                <%} %>
                    </ul>
            </div>
            
                    <%foreach(var item in Data.Skip(4)) 
  {%>  
<div class="col-md-6 col-sm-12 col-xs-12"  style="margin-bottom:20px">
                        <a href="<%=HREF.LinkComponent("Article",item.Title.ConvertToUnSign(),true,"sart", item.Id)%>">
                            <figure class="img_news_h">
                                <picture>
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore + item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                                    </picture>
                            </figure>
                            <div class="info_news_h">
                                <h3><%=item.Title %></h3>
                                <p><%=item.Brief %></p>
                                <span><%=Language["ReadMore"] %> <i class="glyphicon glyphicon-arrow-right"></i></span>
                            </div>
                        </a></div>
                <%} %>
            
        </div><!-- End .min_wrap -->
    </section>
<script>
function animatethis(targetElement, speed) {
    var scrollWidth = $(targetElement).get(0).scrollWidth;
    var clientWidth = $(targetElement).get(0).clientWidth;
    $(targetElement).animate({ scrollLeft: scrollWidth - clientWidth },
    {
        duration: speed,
        complete: function () {
            targetElement.animate({ scrollLeft: 0 },
            {
                duration: speed,
                complete: function () {
                    animatethis(targetElement, speed);
                }
            });
        }
    });
};
animatethis($('.dt_h'), 10000);
</script>