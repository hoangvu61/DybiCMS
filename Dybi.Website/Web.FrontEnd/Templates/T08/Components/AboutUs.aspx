<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuAboutUs").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/"><%=Language["Home"] %></a></li>
            <li class="breadcrumb-item active" aria-current="page">
                <%=Language["AboutUs"] %>
            </li>   
        </ol>
    </nav>
    <script type="application/ld+json">
    {
        "@context": "https://schema.org",
        "@type": "BreadcrumbList",
        "itemListElement": [{
            "@type": "ListItem",
            "position": 1,
            "name": "<%=Language["Home"] %>",
            "item": "<%=HREF.DomainLink %>"
        },
        {
            "@type": "ListItem",
            "position": 2,
            "name": "<%=Language["AboutUs"] %>",
            "item": "<%=HREF.CurrentURL %>"
        }
        ]
    }
    </script>

<section class="aboutus_section mb-5 mt-5">
    <div class="container">
        <div class="row">
            <div class="col-12 col-md-6">
                <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
                    <picture>
                        <source srcset="<%=HREF.DomainStore + this.Config.WebImage.FullPath%>.webp" type="image/webp">
                        <source srcset="<%=HREF.DomainStore + this.Config.WebImage.FullPath%>" type="image/jpeg"> 
                        <img style="margin-bottom:10px; max-width:100%" src="<%=HREF.DomainStore + this.Config.WebImage.FullPath %>" alt="<%=Company.DisplayName %>"/>
                    </picture>
                <%} %>
            </div>
            <div class="col-12 col-md-6">
                <div class="alert alert-secondary" role="alert">
                    <h1><%=Company.FullName %></h1>
                    <div><strong>Mã số doanh nghiệp:</strong> <%=Company.TaxCode %></div>
                    <div><strong>Ngày thành lập:</strong> <%=string.Format("{0:dd/MM/yyyy}", Company.PublishDate == null ? Company.CreateDate : Company.PublishDate)%></div>
                    <div class="blog_jobtitle"><strong>Lĩnh vực hoạt động</strong>: <%=Company.JobTitle %></div>
                    <div class="blog_motto">
                        <%=Company.Motto %>
                    </div>
                </div>
            </div>
            <div class="col-12">
                <div class="text-center mb-md-4 mb-sm-3 mb-3 mb-3 mt-5">
                    <h2 class="mb-3"><%=Company.Slogan %></h2>
                </div>
                <div class="blog_brief">
                    <%=Company.Brief %>
                </div>
                <div class="blog_content">
                    
                    <%=Company.AboutUs %>
                </div>
            </div>
        </div>

        <div class="text-center mb-md-4 mb-sm-3 mb-3 mb-3 mt-5">
            <h2 class="mb-3"><%=Language["Contact"] %></h2>
            <div><i class="fa fa-phone"></i> <a href="tel:Company.Branches<%=Company.Branches[0].Phone %>"><%=Company.Branches[0].Phone %></a></div>
			<div><i class="fa fa-envelope"></i><a href="mailto:<%=Company.Branches[0].Email %>"> <%=Company.Branches[0].Email %></a></div>
        </div>
        <div class="row"> 
            <div class="col-md-10 mx-auto">
                <div class="row">
                    <%foreach(var branch in Company.Branches){ %>
                    <div class="col-12 col-md-6" style="margin-bottom:30px">
                        <div class="box">
                            <p><%= branch.Name%></p>
                            <i class="fa fa-map"></i> <%=branch.Address %>
                        </div>  
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</section>

<script>
    $(document).ready(function () {
        if ($('.aboutus_section img:first').height() > 0)
            $('.alert').height($('.aboutus_section img:first').height());
    });
</script>
    
<script type="application/ld+json">
{
    "@context": "https://schema.org",
    "@type": "<%=Company.Type %>",
    "name": "<%=Company.DisplayName %>",
    "legalName": "<%=Company.FullName %>",
    <%if(!string.IsNullOrEmpty(Company.NickName)){ %>
    "alternateName":"<%=Company.NickName %>",
    <%} %>
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
    "description": "<%=Company.Brief.Replace("\"","") %>"
    <%} %>
}
</script>
    <script type="application/ld+json">
    {
        "@context": "https://schema.org",
        "@type": "AboutPage",
        "name": "<%=Page.Title %>",
        "datePublished": "<%=Company.PublishDate == null ? Company.CreateDate : Company.PublishDate %>",
        "inLanguage" : "<%=Config.Language %>",
        "url": "<%=HREF.CurrentURL %>",
        <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
        "primaryImageOfPage": {
            "@type":"ImageObject",
            "url": "<%=HREF.DomainStore + Config.WebImage.FullPath %>.webp",
            "caption": "<%=Page.Title %>",
            "inLanguage":"<%=Config.Language%>"
        },
        <%} %>
      "isPartOf":{
            "@type": "WebSite",
            "name": "<%=Company.DisplayName %>",
            "url": "<%=HREF.DomainLink %>",
            "inLanguage" : "<%=Config.Language %>",
            <%if(Company.Image != null){ %>
                "image": "<%=HREF.DomainStore + Company.Image.FullPath %>.webp"
            <%} %>
            }
    }
    </script>
</asp:Content>