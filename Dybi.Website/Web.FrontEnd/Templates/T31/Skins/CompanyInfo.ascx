<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../CompanyInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.CompanyInfo" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<!-- about sectuion start -->
<section class="section-company py-5">
    <div class="container">
        <div class="row">
            <div class="col-12 col-md-4 order-md-2 wow animate__animated animate__fadeInRight animated"
                data-wow-duration="0.8s"
                data-wow-delay="0.3s">
                <div class="about_img-box">
                    <div class="about_img" <%=Skin.Width > 0 ? "style='border-radius:"+Skin.Width+"% " + (Skin.Height > 0 ? Skin.Height + "%":"") + "'":"" %>>
                        <picture>
	                        <source srcset="<%=HREF.DomainStore + Config.WebImage.FullPath%>.webp" type="image/webp">
	                        <source srcset="<%=HREF.DomainStore + Config.WebImage.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Config.WebImage.FullPath%>" alt="<%=Company.FullName %>"/>
                        </picture>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-8 pt-3 order-md-1 wow animate__animated animate__fadeInLeft animated"
                data-wow-duration="0.8s"
                data-wow-delay="0.3s">
                <h1 class="wow animate__animated animate__fadeInDown animated"
                    data-wow-duration="0.8s"
                    data-wow-delay="0.3s" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "": ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>" title="<%=Company.FullName %>">
                    <%=Company.DisplayName %>
                </h1>
                <p class="text-justify">
                    <%=Company.Brief %>
                </p>
                <a class="btn mt-3" href="<%=HREF.LinkComponent("Contact", "lien-he", true) %>"><%=Language["Contact"]%></a>
            </div>
        </div>
    </div>
</section>
<!-- about sectuion end -->
