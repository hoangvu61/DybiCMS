<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuhome").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <VIT:Position runat="server" ID="psTop"></VIT:Position>

    <!-- about section -->
    <section class="about_section layout_padding">
    <div class="container  ">
        <div class="row">
        <div class="col-md-6">
            <div class="detail-box">
            <div class="heading_container">
                <h2>
                    <%=Company.DisplayName %>
                </h2>
            </div>
            <p>
                <%=Company.Brief.Replace("\n","<br />") %>
            </p>
            <a class="nav-link" href="<%=HREF.LinkComponent("AboutUs", "gioi-thieu", true) %>">
                <%=Language["ReadMore"] %>
            </a>
            </div>
        </div>
        <div class="col-md-6 ">
            <div class="img-box">
                <%if(Company.Image != null){ %>
                <picture>
                    <%if(Company.Image.FileExtension != ".webp"){ %>
				    <source srcset="<%=HREF.DomainStore + Company.Image.FullPath%>.webp" type="image/webp">
				    <%} %>
                    <source srcset="<%=HREF.DomainStore + Company.Image.FullPath%>" type="image/jpeg"> 
                    <img src="<%=HREF.DomainStore + Company.Image.FullPath%>" alt="<%=Company.FullName %>" class="about_img"/>
                </picture>
                <%} %>
            </div>
        </div>
        </div>
    </div>
    </section>
    <!-- end about section -->
    
    <VIT:Position runat="server" ID="psBottom"></VIT:Position>
</asp:Content>