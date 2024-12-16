<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>


<div class="dt_h">
<%foreach(var item in this.Data) 
{%>  
    <a href="<%=item.Url%>">
        <picture>
		    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
		    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
            <img class="swiper-lazy img_object_fit swiper-lazy-loaded" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore + item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>" style="height:75px;margin-left:10px"/>
        </picture>                   
    </a>
<%} %>
<div style="clear:both"></div>
</div>
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