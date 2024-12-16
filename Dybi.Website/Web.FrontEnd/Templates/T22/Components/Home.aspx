<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Templates/T22/css/timeline.css">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuhome").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <section id="AboutUs" class="design_section layout_padding-top">
            <div class="heading_container">
                <h2>
                    <%=Language["AboutUs"] %>
                </h2>
            </div>
            <div class="row aboutUs_content">
                    <div class="col" style="border-right:1px solid #ccc">
                        <div><label><%=Language["FullName"] %>:</label> <%=Company.FullName %></div>
                        <div><label><%=Language["Birthday"] %>:</label> <%=string.Format("{0: dd/MM/yyyy}", Company.PublishDate) %></div>
                        <%=Company.Brief.Replace("\n", "<br />") %>
                        <div><label><%=Language["JobTitle"] %>:</label> <%=Company.JobTitle %></div>
                    </div>
                    <div class="col">
                        <VIT:Position runat="server" ID="psTop"></VIT:Position>
                    </div> 
                    <div class="col-md-12">
                <div class="row aboutus">
                    <%if(Company.Image != null){ %>
                    <div class="col-md-2">
                        <picture>
					        <source srcset="<%=HREF.DomainStore + Company.Image.FullPath%>.webp" type="image/webp">
					        <source srcset="<%=HREF.DomainStore + Company.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Company.Image.FullPath%>" alt="<%=Company.FullName %>"/>
                        </picture>
                    </div>
                    <div class="col-md-10"><%=Company.AboutUs %></div>
                    <%} else { %>
                    <%=Company.AboutUs %>
                    <%} %>
                </div>
            </div>
                </div>
    </section>

    <VIT:Position runat="server" ID="psMiddle"></VIT:Position>

<!-- expand section -->
<section class="expand_section">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
                <div class="img-box">
                    <%if(Config.BackgroundImage != null){ %>
                        <picture>
					        <source srcset="<%=HREF.DomainStore + Config.BackgroundImage.FullPath%>.webp" type="image/webp">
					        <source srcset="<%=HREF.DomainStore + Config.BackgroundImage.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Config.BackgroundImage.FullPath%>" alt="<%=Company.FullName %>"/>
                        </picture>
                    <%} %>
                </div>
            </div>
            <div class="col-md-6">
                <div class="detail-box">
                    <h2>
                        <%=Company.Slogan %>
                    </h2>
                    <p>
                        <%=Company.Motto.Replace("\n","<br />") %>
                    </p>
                </div>
            </div>
            
        </div>
    </div>
</section>
<!-- end expand section -->
<div>
    <hr class="section_hr" />
</div>

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

<VIT:Position runat="server" ID="psBottom"></VIT:Position>
</asp:Content>