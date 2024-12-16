<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- client section -->
<section class="client_section layout_padding">
    <div class="container">
        <div class="heading_container heading_center psudo_white_primary mb_45">
            <h2 title="<%=Title %>">
                <%=Title %>
            </h2>
            <p style="margin-bottom:40px">
                <%=Category.Brief %>
            </p>
        </div>
        <div class="carousel-wrap ">
            <div class="owl-carousel client_owl-carousel">
                <%for(int i = 0; i < Data.Count ; i++) 
                {%> 
                <div class="item">
                    <div class="box">
                        <div class="detail-box">                        
                            <div class="img-box">
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                                    <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                                        <%if(Data[i].Image.FileExtension == ".webp"){ %>
                                            <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %> class="box-img""/>
                                        <%} else { %>
                                            <picture>
						                        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						                        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>" class="box-img"/>
                                            </picture>
                                        <%} %>
                                    <%} %>
                                </a>
                            </div>
                            <a class="articletitle" href="<%=HREF.LinkComponent(Category.ComponentDetail, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                                <%=this.Data[i].Title.Length > 55 ? Data[i].Title.Substring(0, 55) + "..." : Data[i].Title %>
                            </a>
                            <p>
                                <%=Data[i].Brief %>
                            </p>
                        </div>
                    </div>
                </div> 
                <%} %>
            </div>
        </div>
    </div>
</section>

<script>
// client section owl carousel
$(".client_owl-carousel").owlCarousel({
    loop: true,
    margin: 20,
    dots: false,
    nav: true,
    navText: [],
    autoplay: true,
    autoplayHoverPause: true,
    navText: [
        '<i class="fa fa-angle-left" aria-hidden="true"></i>',
        '<i class="fa fa-angle-right" aria-hidden="true"></i>'
    ],
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1000: {
            items: 3,
            center: true
        }
    }
});
</script>
<!-- end client section -->