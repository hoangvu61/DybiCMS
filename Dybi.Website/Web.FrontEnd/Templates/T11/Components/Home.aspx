<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        #home{color: #ffffff;background-color: #2b2278;}
    </style>
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

<VIT:Position runat="server" ID="psContent"></VIT:Position>
        
    <!-- about section start -->
      <div class="about_section layout_padding">
         <div class="container-fluid">
            <div class="row">
               <div class="col-md-7">
                  <div class="about_taital_main">
                     <h1 class="about_taital">
                         <%=Template.Company.FullName %>
                     </h1>
                     <div class="about_text">
                         <%=Template.Company.AboutUs %>
                     </div>
                  </div>
               </div>
               <div class="col-md-5 padding_right_0">
                  <div>
                      <%if(Template.Config.BackgroundImage != null){ %>
                      <picture>
				        <source srcset="<%=HREF.DomainStore + Template.Config.BackgroundImage.FullPath%>.webp" type="image/webp">
				        <source srcset="<%=HREF.DomainStore + Template.Config.BackgroundImage.FullPath%>" type="image/jpeg"> 
                        <img class="about_img" src="<%=HREF.DomainStore + Template.Config.BackgroundImage.FullPath%>" alt="<%=Template.Company.FullName %>"/>
                    </picture> 
                      <%} %>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <!-- about section end -->
</asp:Content>