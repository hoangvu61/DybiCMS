<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%if(Data.Count > 0){ %>
<!-- gallery section start -->
<section class="gallery mt-5" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container py-lg-5 py-md-4 py-sm-4 py-3">
        <%if(HREF.CurrentComponent == "home"){ %>
        <h2 class="text-center mb-4" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>" title="<%=Category.Title %>"><%=Title %></h2>
        <%} else { %>
        <h1 class="text-center mb-4" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>" title="<%=Category.Title %>"><%=Title %></h1>
        <%} %>
        <div class="title-wls-text text-center mb-lg-5 mb-md-4 mb-sm-4 mb-3">
            <%=Category.Brief %>
        </div>
        <div class="row gallery-info">
            <%{ Data = Data.Where(e => e.Type == "IMG").ToList(); }%>
			<%for (int i = 0; i < Data.Count; i++)
			{%>
                <%if(i > 3 && i < 6){ %>
                <div class="col-lg-6 col-md-6 col-sm-6 gallery-img-grid gallery-mid-two p-0">
                <%} else if(i > 5 && i < 9){ %>
                    <div class="col-lg-4 col-md-4 col-sm-12 gallery-img-three p-0">
                <%} else {%>
                    <div class="col-lg-3 col-md-3 col-sm-6 gallery-img-grid p-0">
                <%} %>
                    <div class="gallery-grids">
                        <a href="<%=HREF.DomainStore + Data[i].Image.FullPath %>.webp" rel="lightbox-mygallery" title="<%=Title %>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Title %>" class="img-fluid"/>
                            </picture>
                        </a>
                    </div>
                </div>
			<%} %>
        </div>
        <div style="margin:30px 0px; text-align:justify">
            <%=Category.Content %>
        </div>
    </div>
</section>

<script type="text/javascript">
    $(document).ready(function () {
        $('.gallery_section a:has(img)').lightbox();
    });
</script>
<!-- gallery section end -->
<%} %>