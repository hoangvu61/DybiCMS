<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        $(document).ready(function () {
            $("#mnuHome").addClass("active");
        });
    </script>
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- about section start -->
    <section class="about_section">
        <div class="container"> 
            <div class="row">
            <div class="col-md-6 wow animate__animated animate__fadeInLeft animated" data-wow-duration="0.8s" data-wow-delay="0.3s">
                <div class="image_2">
                    <picture>
				        <source srcset="<%=HREF.DomainStore + Template.Config.BackgroundImage.FullPath%>.webp" type="image/webp">
				        <source srcset="<%=HREF.DomainStore + Template.Config.BackgroundImage.FullPath%>" type="image/jpeg"> 
                        <img src="<%=HREF.DomainStore + Template.Config.BackgroundImage.FullPath%>" alt="<%=Company.FullName %>"/>
                    </picture>
                </div>
            </div>
            <div class="col-md-6 wow animate__animated animate__fadeInRight animated" data-wow-duration="0.8s" data-wow-delay="0.3s">
                <h2 class="makes_text">Quy mô nhà xưởng</h2>
                <p class="ipsum_text">
                    <%=Company.Brief.Replace("\n","<br />") %>
                </p>
            </div>
            </div>
        </div>
    </section>
    <!-- about section end -->

    <VIT:Position runat="server" ID="psContent"></VIT:Position>

    <!-- choose section start -->
    <section class="choose_section layout_padding">
        <div class="container wow animate__ animate__fadeInUp animated animated" data-wow-duration="1.2s" data-wow-delay="0.2s">
            <h2 class="news_text">Tại sao chọn chúng tôi? </h2>
            <%=Company.CoreValues.Replace("\n","<br />") %>
        </div>
    </section>
    <!-- choose section end -->

    <VIT:Position runat="server" ID="psBottom"></VIT:Position>

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