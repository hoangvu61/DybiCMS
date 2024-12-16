<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuAboutUs").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

<section class="about_section">
    <div class="container">
        <h1>
            <%=Company.NickName %> - <%=Company.FullName %>
        </h1>

        <div class="row company-info">
            <div class="col-md-8 col-md-offset-2">
                <div class="row">
                    <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2">
                        <img src="<%=HREF.DomainStore + this.Company.Image.FullPath %>.webp" alt="<%=this.Company.FullName %>"/>
                    </div>
                    <div class="col-xs-12 col-sm-5 col-md-6 col-lg-6">
                        <p class="company-brief">
                            <%=Company.Brief.Replace("\n","<br />") %>
                        </p>
                    </div>
                    <div class="col-xs-12 col-sm-5 col-md-4 col-lg-4">
                        <div class="company-contact">
                            <div>
                                <label><%=Language["TaxCode"] %>:</label><span> <%=Company.TaxCode%></span>
                            </div>
                            <div>
                                <label><%=Language["publishdate"] %>:</label><span> <%=string.Format("{0:dd/MM/yyyy}", Company.PublishDate == null ? Company.CreateDate : Company.PublishDate)%></span>
                            </div>
                            <div>
                                <label><%=Company.Branches[0].Name %>: </label>
                                <ul>
                                    <li>
                                        <i class="fa fa-phone" aria-hidden="true"></i>
                                        <span>
                                            Call <a href="tel:<%=Company.Branches[0].Phone %>"><%=Company.Branches[0].Phone %></a>
                                        </span>
                                    </li>
                                    <li>
                                        <i class="fa fa-envelope" aria-hidden="true"></i>
                                        <span>
                                            <a href="mailto:<%=Company.Branches[0].Email %>"><%=Company.Branches[0].Email %></a>
                                        </span>
                                    </li>
                                    <li>
                                        <i class="fa fa-map-marker" aria-hidden="true"></i>
                                        <span>
                                            <%=Company.Branches[0].Address %>
                                        </span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%if(!string.IsNullOrEmpty(Company.Slogan)){ %>
        <h2>
            <%=Company.Slogan %>
        </h2>
        <%} %>

        <div class="row layout_padding2">
            <div class="col-md-8">
                <%if(!string.IsNullOrEmpty(Company.Vision)){ %>
                <div class="alert" role="alert">
                    <h3><%=Language["vision"] %></h3>
                    <p>
                        <%=Company.Vision %>
                    </p>
                </div>
                <%} %>
                <%if(!string.IsNullOrEmpty(Company.Mission)){ %>
                <div class="alert" role="alert">
                    <h3><%=Language["mission"] %></h3>
                    <p>
                        <%=Company.Mission %>
                    </p>
                </div>
                <%} %>
            </div>
            <div class="col-md-4">
                <%if(!string.IsNullOrEmpty(Company.CoreValues)){ %>
                <div class="alert" role="alert">
                    <h3><%=Language["corevalues"] %></h3>
                    <p>
                        <%=Company.CoreValues.Replace("\n","<br />") %>
                    </p>
                </div>
                <%} %>
            </div>
            <%if(!string.IsNullOrEmpty(Company.Motto)){ %>
            <div class="col-md-12">
                    <div class="alert" role="alert">
                        <h3><%=Language["motto"] %></h3>
                        <p>
                            <%=Company.Motto %>
                        </p>
                    </div>
            </div>
            <%} %>
        </div>

        <div class="col-md-12>
            <%if(!string.IsNullOrEmpty(Company.JobTitle)){ %>
            <h3><%=Company.NickName %> : <%=Company.JobTitle %></h3>
            <%} %>
        </div>

        <div class="company_content">
            <%=Company.AboutUs %>
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
            "name": "<%=Language["home"] %>",
            "item": "<%=HREF.DomainLink %>"
        },
        {
            "@type": "ListItem",
            "position": 2,
            "name": "<%=Language["aboutus"] %>"
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