<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Templates/T14/css/comment.css" rel="stylesheet" />
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuhome").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <VIT:Position runat="server" ID="psContent"></VIT:Position>

    <!-- subscribe section -->
      <section class="subscribe_section">
         <div class="container-fuild">
            <div class="box">
               <div class="row">
                  <div class="col-md-6 offset-md-3">
                     <div class="subscribe_form ">
                        <div class="heading_container heading_center">
                           <h3><%=Template.Company.Slogan %></h3>
                        </div>
                        <p style="text-align:justify"><%=Template.Company.Motto.Replace("\n","<br />") %></p>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </section>
      <!-- end subscribe section -->

    <VIT:Position runat="server" ID="psBottom"></VIT:Position>

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
        "description": "<%=Company.Brief.Replace("\"","") %>"
        <%} %>
    }
    </script>
</asp:Content>