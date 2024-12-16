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
     
    <section class="about py-lg-5 py-md-3 py-sm-3 py-3">
         <div class="container">
            <div class="row">
               <div class="col-lg-12 col-md-12">
                  <div class="wthree-about-txt mb-lg-5 mb-md-4 mb-3">
                     <h2><%=Language["CompanySize"] %></h2>
                  </div>
                  <div class="about-para-txt">
                        <div class="row">
                            <div class="col-lg-7 col-md-6">
                                <div style="margin:0px 0px 30px">
                                    <%=Company.AboutUs %>
                                </div>
                            </div>
                            <div class="col-lg-5 col-md-6">
                                <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
                                    <picture>
                                        <source srcset="<%=HREF.DomainStore + Config.WebImage.FullPath%>.webp" type="image/webp">
                                        <source srcset="<%=HREF.DomainStore + Config.WebImage.FullPath%>" type="image/jpeg"> 
                                        <img style="max-width:100%" src="<%=HREF.DomainStore + this.Config.WebImage.FullPath %>" alt="<%=Company.DisplayName %>"/>
                                    </picture>
                                <%} %>
                            </div>
                            <div class="col-md-12">
                                <h3><%=Language["Contact"] %></h3>
                                <div><i class="fa fa-phone"></i> <a href="tel:Company.Branches<%=Company.Branches[0].Phone %>"><%=Company.Branches[0].Phone %></a></div>
							    <div><i class="fa fa-envelope"></i><a href="mailto:<%=Company.Branches[0].Email %>"> <%=Company.Branches[0].Email %></a></div>
                                <div class="branch">
                                <%foreach(var branch in Company.Branches){ %>
                                     <div>
                                         <i class="fa fa-map"></i>
                                         <strong><%= branch.Name%>: </strong>
                                         <%=branch.Address %>
                                     </div>  
                                <%} %>
                                </div>
                            </div>
                        </div>
                  </div>
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