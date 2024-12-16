<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">

    <style>
        .home-button {margin: 4em auto 0;color: #000000;font-weight: bold;text-transform: uppercase;background: #fff;font-size: 1em;letter-spacing: 2px;}
    </style>
    <script>
        $(document).ready(function () {
            $("#mnuHome").addClass("active");
        });
    </script>
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
     
    <section class="about-w3ls py-md-5 py-3" id="about">
        <div class="container">
            <div class="row py-sm-5 py-3">
                <div class="col-lg-8">
                    <div class="order-lg-0 order-1">
                        <h2 class="w3l-sub pb-lg-5 pb-3 mr-lg-5"><%=Company.Slogan %></h2>
                        <p class="lead pb-lg-5 pb-3 mr-lg-5 about-text-wthree">
                            <%=Company.Motto.Replace("\n","<br />") %></p>
                    </div>
                        <div class="lead about-text-wthree">
                            <div><i class="fa fa-phone"></i> <a href="tel:<%=this.Company.Branches[0].Phone %>"><%=this.Company.Branches[0].Phone %></a></div>
									<div><i class="fa fa-envelope"></i><a href="mailto:<%=this.Company.Branches[0].Email %>"> <%=this.Company.Branches[0].Email %></a></div>
                           <div><i class="fa fa-map"></i> <%=this.Company.Branches[0].Address %></div>
                        </div>
                </div>
                <div class="col-lg-4 mt-lg-0 mt-5">
                    <VIT:Position runat="server" ID="psTopRight"></VIT:Position>
                </div>
            </div>
        </div>
    </section>
    
    <VIT:Position runat="server" ID="psContent"></VIT:Position>

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
</asp:Content>