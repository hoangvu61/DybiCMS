<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuhome").addClass("active");
            $("#mnuhome2").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">


    <!-- banner -->
      <section class="banner_main">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
      </section>
      <!-- end banner -->

      <!-- about -->
      <div class="about">
         <div class="container-fluid">
            <div class="row">
               <div class="col-md-5">
                  <div class="titlepage">
                     <h2><%=Template.Company.Slogan %></h2>
                      <p>
                     <%=Template.Company.Brief %>
                      </p>
                      <a class="read_more" href="<%=HREF.LinkComponent("AboutUs", "gioi-thieu", true) %>"><%=Language["readmore"] %></a>
                  </div>
               </div>
               <div class="col-md-7">
                  <div class="about_img">
                     <figure>
                         <%if(Template.Config.BackgroundImage != null){ %>
                         <picture>
				            <source srcset="<%=HREF.DomainStore + Template.Config.BackgroundImage.FullPath%>.webp" type="image/webp">
				            <source srcset="<%=HREF.DomainStore + Template.Config.BackgroundImage.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Template.Config.BackgroundImage.FullPath%>" alt="<%=Template.Company.FullName %>"/>
                        </picture>
                         <%} %>
                     </figure>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <!-- end about -->
<VIT:Position runat="server" ID="psBottom"></VIT:Position>
</asp:Content>