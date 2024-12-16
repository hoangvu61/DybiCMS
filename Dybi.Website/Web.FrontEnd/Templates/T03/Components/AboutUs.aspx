<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

<article class="abouts_article">
	<h1 title="Giới thiệu <%=Company.FullName %>">Giới thiệu <%=Company.FullName %></h1>
    <div class="article_content">
            <h2><%=Company.JobTitle %></h2>

            <%foreach(var branch in Company.Branches){ %>
                <div class="col-sm-12" style="text-align:center">
                <i class="fa fa-map-marker" aria-hidden="true"></i>
                <span>
                    <%=branch.Name %>: <%=branch.Address %>
                </span>
                </div>
            <%} %>
            <div class ="row">
                <div class="col" style="text-align:right">
                    <a href="tel:<%=Company.Branches[0].Phone %>">
                        <i class="fa fa-phone" aria-hidden="true"></i> <%=Company.Branches[0].Phone %>
                    </a>
                </div>
                <div class="col">
                    <a href="mailto:<%=Company.Branches[0].Email %>">
                        <i class="fa fa-envelope" aria-hidden="true"></i> <%=Company.Branches[0].Email %>
                    </a>
                </div>
            </div>

        <div class="brief">
            <%=Company.Brief.Replace("\n","<br />") %>
        </div>
        <div class="content">
            <%=Company.AboutUs %>
        </div>
        
    </div>
</article>

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
            "name": "Giới thiệu"
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