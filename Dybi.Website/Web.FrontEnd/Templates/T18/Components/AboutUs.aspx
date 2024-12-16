<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<nav aria-label="breadcrumb">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
        <li class="breadcrumb-item active" aria-current="page">Giới thiệu</li>
    </ol>
</nav>

<section class="aboutus_section layout_padding">
    <div class="container">
        <h1 class="text-center" title="<%=Company.FullName %>">
            <%=Company.FullName %>
        </h1>

        <div class="row">
            <div class="col-md-12 col-lg-12 mx-auto">
                <%if(!string.IsNullOrEmpty(Company.Slogan)){ %>
                <h2 style="text-align:center"><%=Company.Slogan %></h2>
                <%} %>
                <%if(!string.IsNullOrEmpty(Company.JobTitle)){ %>
                <h3 style="text-align:center"><%=Company.JobTitle %></h3>
                <%} %>
            </div>
            <div class="col-12">
                <!-- about section start -->
                <div class="about_section layout_padding2">
                    <div class="container">
                        <div class="row">
                        <div class="col-md-6">
                            <div class="image_2">
                                <picture>
				                    <source srcset="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>.webp" type="image/webp">
				                    <source srcset="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>" type="image/jpeg"> 
                                    <img src="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>" alt="<%=Company.FullName %>"/>
                                </picture>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <h2 style="font-size:36px;padding-bottom:20px">Về chúng tôi</h2>
                            <p class="ipsum_text">
                                <%=Company.Brief.Replace("\n","<br />") %>
                                <br/>
                                <%--<a href="tel:<%=Company.Branches[0].Phone %>">
                                    <i class="fa fa-phone" aria-hidden="true"></i>: 
                                    <span><%=Company.Branches[0].Phone %></span>
                                </a>
                                <br/>
                                <a href="mailto:<%=Company.Branches[0].Email %>">
                                    <i class="fa fa-envelope" aria-hidden="true"></i>: 
                                    <span>
                                        <%=Company.Branches[0].Email %>
                                    </span>
                                </a>--%>
                            </p>
                        </div>
                        </div>
                    </div>
                </div>
                <!-- about section end -->
            </div>
        </div>

        <div class="blog_contain">
            <div class="row layout_padding2">
                <div class="col-md-6">
                    <%if(!string.IsNullOrEmpty(Company.Vision)){ %>
                    <div class="alert alert-primary" role="alert">
                        <h2>Tầm nhìn</h2>
                        <p>
                            <%=Company.Vision %>
                        </p>
                    </div>
                    <%} %>
                    <%if(!string.IsNullOrEmpty(Company.Mission)){ %>
                    <div class="alert alert-success" role="alert">
                        <h2>Sứ mệnh</h2>
                        <p>
                            <%=Company.Mission %>
                        </p>
                    </div>
                    <%} %>
                </div>
                <div class="col-md-6">
                    <%if(!string.IsNullOrEmpty(Company.CoreValues)){ %>
                    <div class="alert alert-danger" role="alert">
                        <h2>Giá trị cốt lõi</h2>
                        <p>
                            <%=Company.CoreValues.Replace("\n","<br />") %>
                        </p>
                    </div>
                    <%} %>
                </div>
                <%if(!string.IsNullOrEmpty(Company.Motto)){ %>
                <div class="col-md-12">
                        <div class="alert alert-info" role="alert">
                            <h2>Phương châm</h2>
                            <p>
                                <%=Company.Motto %>
                            </p>
                        </div>
                </div>
                <%} %>
            </div>
            <div class="company_content">
                <%=Company.AboutUs %>
            </div>
        </div>
        <VIT:Position runat="server" ID="psBottom"></VIT:Position>
    </div>
</section>

<script type="application/ld+json">
    {
        "@context": "https://schema.org",
        "@type": "BreadcrumbList",
        "itemListElement": [{
            "@type": "ListItem",
            "position": 1,
            "name": "Trang chủ",
            "item": "<%=HREF.DomainLink %>"
        },
        {
            "@type": "ListItem",
            "position": 2,
            "name": "Giới thiệu",
            "item": "<%=HREF.CurrentURL %>"
        }
        ]
    }
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