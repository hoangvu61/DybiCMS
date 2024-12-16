<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <style>
        .hero_area{min-height:790px}
        @media (min-width: 1280px) {
            .hero_area {min-height: 950px;}
        }
    </style>
     <!-- owl slider -->
    <script type="text/javascript" src="/Templates/T18/js/owl.carousel.min.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- about section -->
    <section class="about_section layout_padding">
        <div class="container-fluid  ">
            <div class="row">
                <div class="col-12">
                    <VIT:Position runat="server" ID="psTop"></VIT:Position>
                </div>
            <div class="col-md-6 pl-md-0">
                <div class="img-box">
                    <picture>
				        <source srcset="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>.webp" type="image/webp">
				        <source srcset="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>" type="image/jpeg"> 
                        <img src="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>" alt="<%=Company.FullName %>"/>
                    </picture>
                </div>
            </div>
            <div class="col-md-5">
                <div class="detail-box">
                <div class="heading_container ">
                    <h1 title="<%=Company.DisplayName %>">
                        <%=Company.DisplayName %>
                    </h1>
                </div>
                <p>
                    <%=Company.Brief %>
                </p>
                </div>
            </div>
            </div>
        </div>
    </section>
    <!-- end about section -->

    <VIT:Position runat="server" ID="psContent"></VIT:Position>

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
</asp:Content>