<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../CompanyInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.CompanyInfo" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<!-- about sectuion start -->
    <section class="section-company py-5">
        <div class="container">
            <div class="heading_container mb-4">
                <h1 class="wow animate__animated animate__fadeInDown animated"
                    data-wow-duration="0.8s"
                    data-wow-delay="0.3s" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "": ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>" title="<%=Company.FullName %>">
                    <%=Company.DisplayName %>
                </h1>
            </div>
            <%if (!string.IsNullOrEmpty(Company.Brief))
            { %>
            <div class="row">
                <%if (Skin.HeaderBackgroundFile == null)
                    { %>
                <div class="col-12">
                    <%=Company.Brief.Split('\n')[0] %>
                </div>
                <%}
                else
                { %>
                <div class="col-12 col-md-4 wow animate__animated animate__fadeInLeft animated"
                    data-wow-duration="0.8s"
                    data-wow-delay="0.3s">
                    <div class="about_img-box">
                        <div class="about_img" <%=Skin.Width > 0 ? "style='border-radius:"+Skin.Width+"% " + (Skin.Height > 0 ? Skin.Height + "%":"") + "'":"" %>>
                            <picture>
                                <source srcset="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>.webp" type="image/webp">
                                <source srcset="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>" type="image/jpeg">
                                <img src="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>" alt="<%=Company.FullName %>" />
                            </picture>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-md-8 pt-3 wow animate__animated animate__fadeInRight animated"
                    data-wow-duration="0.8s"
                    data-wow-delay="0.3s">
                    <p class="p-0 p-md-3 p-sm-2">
                        <%=Company.Brief.Split('\n')[0] %>
                    </p>
                </div>
                <%} %>
            </div>
                <%if (Company.Brief.Split('\n').Length > 1)
                    { %>
            <div class="row mt-3">
                <%if (string.IsNullOrEmpty(this.Skin.BodyBackground))
                    { %>
                <div class="col-12">
                    <%=Company.Brief.Split('\n')[1] %>
                </div>
                <%}
                else
                {%>
                <div class="col-12 col-md-4 order-md-2 wow animate__animated animate__fadeInRight animated"
                    data-wow-duration="0.8s"
                    data-wow-delay="0.3s">
                    <div class="about_img-box">
                        <div class="about_img" <%=Skin.Width > 0 ? "style='border-radius:"+Skin.Width+"% " + (Skin.Height > 0 ? Skin.Height + "%":"") + "'":"" %>>
                            <picture>
                                <source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>.webp" type="image/webp">
                                <source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" type="image/jpeg">
                                <img src="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" alt="<%=Company.DisplayName %>" />
                            </picture>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-md-8 pt-3 order-md-1 wow animate__animated animate__fadeInLeft animated"
                    data-wow-duration="0.8s"
                    data-wow-delay="0.3s">
                    <p class="p-0 p-md-3 p-sm-2">
                        <%=Company.Brief.Split('\n')[1] %>
                    </p>
                </div>
                <%} %>
            </div>
                <%} %>
            <%} %>
        </div>
    </section>
    <!-- about sectuion end -->
