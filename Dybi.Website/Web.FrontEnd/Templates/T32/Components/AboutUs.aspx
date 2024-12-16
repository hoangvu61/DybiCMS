<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
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

     <div class="about_section layout_padding">
         <div class="container">
            <div class="row">
               <div class="col-sm-12">
                  <h1 class="heading_container heading_center"><%=Company.FullName %></h1>
                  <p class="blog_text"><%=Company.Slogan %></p>
                   <div class="row">
                       <div class="col-sm-12" style="text-align:center">
                        <i class="fa fa-map-marker" aria-hidden="true"></i>
                        <span>
                            <%=Company.Branches[0].Address %>
                        </span>
                         </div><div class="col-sm-6" style="text-align:right">
                        <a href="tel:<%=Company.Branches[0].Phone %>">
                        <i class="fa fa-phone" aria-hidden="true"></i>
                        <span>
                            Call <%=Company.Branches[0].Phone %>
                        </span>
                        </a>
                         </div>
                         <div class="col-sm-6">
                        <a href="mailto:<%=Company.Branches[0].Email %>">
                        <i class="fa fa-envelope" aria-hidden="true"></i>
                        <span>
                            <%=Company.Branches[0].Email %>
                        </span>
                        </a></div>
                    </div>
               </div>
            </div>
             <div class="container-fluid">
             <p>
                <%=Company.Brief.Replace("\n","<br />") %>
              </p>
              <p>
                <%=Company.AboutUs %>
              </p>
             </div>
         </div>
         </div>
         

</asp:Content>