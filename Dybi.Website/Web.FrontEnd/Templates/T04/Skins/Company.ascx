<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../CompanyInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.CompanyInfo" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<!-- about sectuion start -->
<div class="about_section layout_padding" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="w3-container">
        <div class="news_title">
            <div class="w3-row" style="margin:10px 0px">
                <div class="w3-col l4 s5" style="padding-left:10px">
                    <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
                        <picture>
                            <source srcset="<%=HREF.DomainStore + this.Config.WebImage.FullPath%>.webp" type="image/webp">
                            <source srcset="<%=HREF.DomainStore + this.Config.WebImage.FullPath%>" type="image/jpeg"> 
                            <img style="margin-bottom:10px" src="<%=HREF.DomainStore + this.Config.WebImage.FullPath %>" alt="<%=Company.DisplayName %>"/>
                        </picture>
                    <%} %>
                </div>
                <div class="w3-col l8 s12">
                    <h1 class="info_title"><%=Company.DisplayName %></h1>
                    <strong>
                        <%=Company.Brief.Replace("\n","<br />") %>
                    </strong>
                </div>
            </div>
        </div>
        <div class="news_des" style="width:100%">
            <%=Company.Motto.Replace("\n","<br />") %>
        </div>
    </div>
</div>
<!-- about sectuion end -->

<script type="application/ld+json">
{
    "@context": "https://schema.org",
    "@type": "Organization",
    "name": "<%=Company.DisplayName %>",
    "legalName": "<%=Company.FullName %>",
    "url": "<%=HREF.DomainLink %>",
    "logo": "<%=HREF.DomainStore + this.Company.Image.FullPath %>",
    <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
    "image": "<%=HREF.DomainStore + Config.WebImage.FullPath %>",
    <%} %>
    <%if(!string.IsNullOrEmpty(Company.Slogan)){ %>
    "slogan":"<%=Company.Slogan %>",
    <%} %>
    <%if(!string.IsNullOrEmpty(Company.TaxCode)){ %>
    "taxID":"<%=Company.TaxCode %>",
    <%} %>
    <%if(!string.IsNullOrEmpty(Company.JobTitle)){ %>
    "keywords":"<%=Company.JobTitle %>",
    <%} %>
    <%if(Company.PublishDate != null){ %>
    "foundingDate":"<%=Company.PublishDate %>",
    <%} %>
    <%if(Company.Branches != null && Company.Branches.Count > 0){ %>
        <%if(!string.IsNullOrEmpty(Company.Branches[0].Email)){ %>
            "email": "<%=Company.Branches[0].Email %>",
        <%} %>
        <%if(!string.IsNullOrEmpty(Company.Branches[0].Phone)){ %>
            "telephone": "<%=Company.Branches[0].Phone %>",
        <%} %>
        <%if(!string.IsNullOrEmpty(Company.Branches[0].Address)){ %>
            "address": "<%=Company.Branches[0].Address %>",
        <%} %>
    <%} %>
    <%if(!string.IsNullOrEmpty(Company.Brief)){ %>
    "description": "<%=Company.Brief %>"
    <%} %>
}
</script>
