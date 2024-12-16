<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent"  %>

<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="breadcrumb-section py-4">
        <nav class="container" aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="/"><%=Language["Home"] %></a></li>
                <li class="breadcrumb-item active" aria-current="page"><%=Language["AboutUs"] %></li>
            </ol>
        </nav>
    </section>

    <section class="aboutus_section layout_padding">
        <div class="container">
            <h1 class="mb-5"><%=Company.FullName %></h1>

            <div class="company_content">
                <%=Company.AboutUs %>
            </div>

            <div class="blog_contain py-5">
                <div class="row">
                    <%if (!string.IsNullOrEmpty(Company.Vision))
                        { %>
                    <div class="col-6">
                        <div class="pb-3" style="height: 100%">
                            <div class="alert alert-primary" style="height: 100%" role="alert">
                                <h2>
                                    <%=Language["Vision"] %>
                                </h2>
                                <p>
                                    <%=Company.Vision %>
                                </p>
                            </div>
                        </div>
                    </div>
                    <%} %>
                    <%if (!string.IsNullOrEmpty(Company.Mission))
                        { %>
                    <div class="col-6">
                        <div class="pb-3" style="height: 100%">
                            <div class="alert alert-success" style="height: 100%" role="alert">
                                <h2>
                                    <%=Language["Mission"] %>
                                </h2>
                                <p>
                                    <%=Company.Mission %>
                                </p>
                            </div>
                        </div>
                    </div>
                    <%} %>
                    <div class="col-12">
                        <%if (!string.IsNullOrEmpty(Company.CoreValues))
                            { %>
                        <div class="alert alert-danger" role="alert">
                            <h2><%=Language["CoreValue"] %></h2>
                            <p>
                                <%=Company.CoreValues.Replace("\n","<br />") %>
                            </p>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>
            <%if (!string.IsNullOrEmpty(Company.Motto))
                { %>
            <%=Company.Motto %>
            <%} %>
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
                "name": "<%=Language["Home"] %>",
                "item": "<%=HREF.DomainLink %>"
            },
            {
                "@type": "ListItem",
                "position": 2,
                "name": "<%=Language["Contact"] %>",
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
        <%if (!string.IsNullOrEmpty(Company.NickName))
        { %>
        "alternateName":"<%=Company.NickName %>",
        <%} %>
        "url": "<%=HREF.DomainLink %>",
        "logo": "<%=HREF.DomainStore + Company.Image.FullPath %>",
        <%if (Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath))
        { %>
        "image": "<%=HREF.DomainStore + Config.WebImage.FullPath %>",
        <%} %>
        <%if (!string.IsNullOrEmpty(Company.Slogan))
        { %>
        "slogan":"<%=Company.Slogan %>",
        <%} %>
        <%if (!string.IsNullOrEmpty(Company.TaxCode))
        { %>
        "taxID":"<%=Company.TaxCode %>",
        <%} %>
        <%if (!string.IsNullOrEmpty(Company.JobTitle))
        { %>
        "keywords":"<%=Company.JobTitle %>",
        <%} %>
        <%if (Company.PublishDate != null)
        { %>
        "foundingDate":"<%=Company.PublishDate %>",
        <%} %>
        <%if (Company.Branches != null && Company.Branches.Count > 0)
        { %>
            <%if (!string.IsNullOrEmpty(Company.Branches[0].Email))
        { %>
                "email": "<%=Company.Branches[0].Email %>",
            <%} %>
            <%if (!string.IsNullOrEmpty(Company.Branches[0].Phone))
        { %>
                "telephone": "<%=Company.Branches[0].Phone %>",
            <%} %>
            <%if (!string.IsNullOrEmpty(Company.Branches[0].Address))
        { %>
                "address": "<%=Company.Branches[0].Address %>",
            <%} %>
        <%} %>
        <%if (!string.IsNullOrEmpty(Company.Brief))
        { %>
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
        <%if (Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath))
        { %>
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
            <%if (Company.Image != null)
        { %>
                "image": "<%=HREF.DomainStore + Company.Image.FullPath %>.webp"
            <%} %>
            }
    }
    </script>
</asp:Content>
