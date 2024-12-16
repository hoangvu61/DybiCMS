<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuaboutus").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

<nav aria-label="breadcrumb">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
        <li class="breadcrumb-item active" aria-current="page">Giới thiệu</li>
    </ol>
</nav>

<section class="blog_section layout_padding">
    <div class="container">
      <div class="heading_container">
        <h1>
            <%=Company.FullName %>
        </h1>
      </div>

      <div class="row">
        <div class="col-md-12 col-lg-12 mx-auto">
            <%if(!string.IsNullOrEmpty(Company.Slogan)){ %>
            <h2><%=Company.Slogan %></h2>
            <%} %>
            <%if(!string.IsNullOrEmpty(Company.JobTitle)){ %>
            <h3><%=Company.JobTitle %></h3>
            <%} %>
            <div class="row contact-session">
                <div class="col-sm-12 contact-address">
                <i class="fa fa-map-marker" aria-hidden="true"></i>
                <span>
                    <%=Company.Branches[0].Address %>
                </span>
                    </div><div class="col-sm-6 contact-phone">
                <a href="tel:<%=Company.Branches[0].Phone %>">
                <i class="fa fa-phone" aria-hidden="true"></i>
                <span>
                    Call <%=Company.Branches[0].Phone %>
                </span>
                </a>
                    </div>
                    <div class="col-sm-6 contact-email">
                <a href="mailto:<%=Company.Branches[0].Email %>">
                <i class="fa fa-envelope" aria-hidden="true"></i>
                <span>
                    <%=Company.Branches[0].Email %>
                </span>
                </a></div>
            </div>
        </div>

        <div class="blog_contain">
            <p class="blog_brief">
            <%=Company.Brief.Replace("\n","<br />") %>
            </p>
            <p>
            <%=Company.Motto.Replace("\n","<br />") %>
            </p>
            <p>
            <%=Company.AboutUs %>
            </p>
            <VIT:Position runat="server" ID="psBottom"></VIT:Position>
        </div>
    </div>
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