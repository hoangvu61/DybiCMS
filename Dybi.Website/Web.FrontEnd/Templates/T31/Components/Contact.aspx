﻿<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent"  %>

<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="breadcrumb-section py-4">
        <nav class="container" aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="/"><%=Language["Home"] %></a></li>
                <li class="breadcrumb-item active" aria-current="page"><%=Language["Contact"] %></li>
            </ol>
        </nav>
    </section>

    <VIT:Position runat="server" ID="psContent"></VIT:Position>

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
        "@type": "ContactPage",
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