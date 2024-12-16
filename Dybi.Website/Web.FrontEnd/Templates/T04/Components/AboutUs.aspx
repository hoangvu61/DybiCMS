<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuaboutus").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <ul class="breadcrumb">
        <li>
            <a href="/">
                <%=Language["Home"] %>
            </a>
        </li>
        <li>
            <%=Language["AboutUs"] %>
        </li>     
    </ul>
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
            "item": "<%=HREF.LinkComponent("AboutUs", Language["AboutUs"].ConvertToUnSign(), true) %>"
        }
        ]
    }
    </script>

<section class="aboutus_section">
    <div class="w3-container">
        <h1>
            <%=Company.FullName %>
        </h1>
        <h2>
            <%=Company.Slogan %>
        </h2>
        <h3><%=Company.JobTitle %></h3>
        <div class="w3-row contact">
            <div class="w3-col m12">
                <i class="fa fa-map-marker" aria-hidden="true"></i>
                <span>
                    <%=Company.Branches[0].Address %>
                </span>
            </div>
            <div class="w3-col m6 i12 contact-phone">
                <a href="tel:<%=Company.Branches[0].Phone %>">
                <i class="fa fa-phone" aria-hidden="true"></i>
                <span>
                    Call <%=Company.Branches[0].Phone %>
                </span>
                </a>
            </div>
            <div class="w3-col m6 i12 contact-email">
                <a href="mailto:<%=Company.Branches[0].Email %>">
                <i class="fa fa-envelope" aria-hidden="true"></i>
                <span>
                    <%=Company.Branches[0].Email %>
                </span>
                </a>
            </div>
        </div>

        <div class="container-fluid" style="text-align:justify; margin-top:50px">
            <strong>
            <%=Company.Brief.Replace("\n","<br />") %>
            </strong>
            <div style="margin: 20px 0px">
                <%=Company.Motto.Replace("\n","<br />") %>
            </div>
            <%=Company.AboutUs %>
            <VIT:Position runat="server" ID="psBottom"></VIT:Position>
        </div>
    </div>
</div>

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
            "item": "<%=HREF.LinkComponent("AboutUs", "gioi-thieu", true) %>"
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
    "foundingDate":"<%=Company.PublishDate%>",
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