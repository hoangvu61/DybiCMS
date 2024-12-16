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
    <section class="breacrum">
        <div class="min_wrap">
            <ul class="ul_breacrum">
                <li>
                    <a href="/">
                        <i class="glyphicon glyphicon-home"></i> Trang chủ
                    </a>
                </li>     
                <li>
                    Giới thiệu
                </li>         
            </ul><!-- End .ul-breacrum -->
        </div><!-- End .min_wrap -->
    </section>

    <div class="container" style="padding:50px 20px">
        <section class="aboutus_section layout_padding">
            <div class="heading_container">
                <h1 style="text-align:center; font-size:36px; margin:20px !important">
                    <strong>
                        <%=Company.FullName %>
                    </strong>
                </h1>
            </div>

            <div class="row" style="margin-bottom:30px">
                <div class="col-sm-6 mst">
                    <%if(!string.IsNullOrEmpty(Company.TaxCode)){ %>
                        <strong>Mã số doanh nghiệp:</strong> <%=Company.TaxCode %>
                    <%} %>
                </div>
                <div class="col-sm-6 createdate">
                    <strong>Ngày thành lập:</strong> <%=string.Format("{0:dd/MM/yyyy}", Company.PublishDate == null ? Company.CreateDate : Company.PublishDate)%>
                </div>
            </div>

            <div class="row">
                <div class="col-md-8">
                    <%if(!string.IsNullOrEmpty(Company.Slogan)){ %>
                    <h2 style="text-align:center; font-size:24px; margin:20px !important">
                        <strong><%=Company.NickName %></strong> - <%=Company.Slogan %>
                    </h2>
                    <%} %>

                    <h2 class="blog_jobtitle">
                        <strong><%=Company.DisplayName %></strong> - <strong><%=Company.JobTitle %></strong> 
                    </h2>
                    <div class="blog_brief">
                        <%=Company.Brief.Replace("\n", "<br />") %>
                    </div>
                    
                    <div class="blog_value">
                        <%if(!string.IsNullOrEmpty(Company.Vision)){ %>
                            <h3>Tầm nhìn</h3>
                            <p>
                                <%=Company.Vision %>
                            </p>
                        <%} %>
                        <%if(!string.IsNullOrEmpty(Company.Mission)){ %>
                            <h3>Sứ mệnh</h3>
                            <p>
                                <%=Company.Mission %>
                            </p>
                        <%} %>
                        <%if(!string.IsNullOrEmpty(Company.CoreValues)){ %>
                            <h3>Giá trị cốt lõi</h3>
                            <p>
                                <%=Company.CoreValues.Replace("\n","<br />") %>
                            </p>
                        <%} %>
                    </div>
                    
                </div>
                <div class="col-md-4">
                    <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
                        <picture>
                            <source srcset="<%=HREF.DomainStore + this.Config.WebImage.FullPath%>.webp" type="image/webp">
                            <source srcset="<%=HREF.DomainStore + this.Config.WebImage.FullPath%>" type="image/jpeg"> 
                            <img style="margin-bottom:10px" src="<%=HREF.DomainStore + this.Config.WebImage.FullPath %>" alt="<%=Company.DisplayName %>"/>
                        </picture>
                    <%} %>
                    <%if(!string.IsNullOrEmpty(Company.Motto)){ %>
                    <div class="blog_motto">
                        <%=Company.Motto %>
                    </div>
                    <%} %>

                    <div class="branches">
                        <h3>Liên hệ</h3>
                        <%foreach(var branch in Company.Branches){ %>
                        <div class="branch">
                            <h4><strong><%= branch.Name%></strong></h4>
                            <div><b>Điện thoại:</b> <a href="tel:<%=branch.Phone %>"><%=branch.Phone %></a> <a href="https://zalo.me/<%=branch.Phone %>" rel="nofollow" target="_blank">(Có zalo)</a></div>
                            <div><b>Email:</b> <a href="mailto:<%=branch.Email %>"><%=branch.Email %></a></div>
                            <div><b>Địa chỉ:</b> <%=branch.Address %></div> 
                        </div>
                        <%} %>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="blog_content">
                        <%=Company.AboutUs %>
                    </div>
                </div>
            </div>
            
      </section>
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
    "url": "<%=HREF.DomainLink %>",
    "logo": "<%=HREF.DomainStore + Company.Image.FullPath %>",
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