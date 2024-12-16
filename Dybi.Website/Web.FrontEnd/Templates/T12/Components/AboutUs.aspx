<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuaboutus").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<section class="aboutus">
	<div class="container" style="padding:0px">	
        <h1 class="main-heading" title="Tiểu sử <%=Company.DisplayName%>">Tiểu sử <%=Company.DisplayName%></h1>
        <div class="row">
            <div class="grid_6">
                <table class="table">
                    <tbody>
                        <tr>
                            <td style="width:100px">Tên Thật: </td>
                            <td>
                                <%=Company.FullName %>
                            </td>
                        </tr>
                        <%if(Company.PublishDate != null){ %>
                        <tr>
                            <td>Sinh: </td>
                            <td>
                                <%=String.Format("{0:dd/MM/yyyy}", Company.PublishDate)%>
                            </td>
                        </tr>
                        <%} %>
                        <tr>
                            <td>Nghệ danh: </td>
                            <td>
                                <%=Company.DisplayName %>
                            </td>
                        </tr>
                        <tr>
                            <td>Tên khác: </td>
                            <td>
                                <%=Company.NickName %>
                            </td>
                        </tr>
                        <tr>
                            <td>Nghề nghiệp: </td>
                            <td>
                                <%=Company.JobTitle%>
                            </td>
                        </tr>
                        <tr>
                            <td>Cuộc đời 
                                <br />và Sự nghiệp: </td>
                            <td>
                                <%if(!string.IsNullOrEmpty(Company.Motto)){ %>
                                <%=Company.Motto.Replace("\n","<br />")%>
                                <%} %>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="grid_6">
                <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
                    <img src="<%=HREF.DomainStore + this.Config.WebImage.FullPath %>" alt="Company.DisplayName"/>
                <%} %>
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
            "name": "Tiểu sử <%=Company.DisplayName %>"
        }
        ]
    }
</script>

<script type="application/ld+json">
{
  "@context": "https://schema.org",
  "@type": "Person",
    <%if(Company.Image != null){ %>
    "image": "<%=HREF.DomainStore + this.Company.Image.FullPath %>",
    <%} %>
  "name": "<%=Company.DisplayName %>",
  "familyName": "<%=Company.FullName %>",
  "url": "<%=HREF.DomainLink %>",
    <%if(Company.PublishDate != null){ %>
    "birthDate":"<%=Company.PublishDate%>",
    <%} %>
    <%if(!string.IsNullOrEmpty(Company.JobTitle)){ %>
    "jobTitle": "<%=Company.JobTitle%>",
    <%} %>
    <%if(!string.IsNullOrEmpty(Company.Motto)){ %>
    "description": "<%=Company.Motto %>"
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