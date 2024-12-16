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

     <!-- inner page section -->
      <section class="inner_page_head">
         <div class="container_fuild">
            <div class="row">
               <div class="col-md-12">
                  <div class="full">
                     <h1>
                         <%=Company.FullName %>
                     </h1>
                  </div>
               </div>
            </div>
         </div>
      </section>
      <!-- end inner page section -->

    <section class="article_section layout_padding">
        <div class="container">
            <div class="heading_container heading_center">
               <h2>
                  <span>
                      <%=Company.Slogan %>
                  </span>
               </h2>
            </div>
        <div class="row">
          <div class="col-md-12">
            <div class="box">
                <%=Company.AboutUs %>
            </div>
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