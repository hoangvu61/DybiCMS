<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <VIT:Position runat="server" ID="psTop"></VIT:Position>

    <!-- about section start -->
      <div class="about_section layout_padding">
         <div class="container">
            <div class="about_section_main">
               <div class="row">
                  <div class="col-md-6">
                     <div class="about_taital_main">
                        <h1 class="about_taital"><%=Company.Slogan %></h1>
                        <p class="about_text"><%=Company.AboutUs %></p>
                     </div>
                  </div>
                  <div class="col-md-6">
                     <div>
                         <%if(Template.Config.WebImage != null){ %>
                        <picture>
				            <source srcset="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>.webp" type="image/webp">
				            <source srcset="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>" alt="<%=Company.FullName %>" class="about_img"/>
                        </picture>
                        <%} %>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <!-- about section end -->
    
    <VIT:Position runat="server" ID="psBottom"></VIT:Position>
</asp:Content>