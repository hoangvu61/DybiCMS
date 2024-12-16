<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Templates/T12/css/timeline.css">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>

    <script>
        $(document).ready(function () {
            $("#mnuhome").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

<section class="well mod-center">
            <div class="container">
                <div class="row">

                    <div class="grid_4">
                        <img class="right circle-img" src="<%=HREF.DomainStore + Company.Image.FullPath %>.webp" alt="<%=Company.FullName %>" >
                    </div>

                    <div class="grid_8">
                        <%=Company.AboutUs %>
                    </div>

                </div>
            </div>
        </section>

<div class="homecomponent">
<VIT:Position runat="server" ID="psTop"></VIT:Position> 

<section class="center" style="padding:100px 0px">
<div class="container">
                <div class="row">
<VIT:Position runat="server" ID="psContent"></VIT:Position>
</div>
</div>
</section>

<VIT:Position runat="server" ID="psBottom"></VIT:Position>
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
</asp:Content>