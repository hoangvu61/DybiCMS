<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <VIT:Position runat="server" ID="psContent"></VIT:Position>

    <section class="about_section">
      <div class="container">
        <div class="row">
          <div class="col-md-6">
            <div class="img-box">
                <%if(Config.WebImage != null){ %>
              <img src="<%=HREF.DomainStore + Config.WebImage.FullPath %>.webp" alt="<%=Company.FullName %>">
                <%} else { %>
                <%=Company.Brief %>
                <%} %>
            </div>
          </div>
          <div class="col-md-6">
            <div class="detail-box">
              <div class="heading_container">
                <h2>
                  <%=Company.FullName %>
                </h2>
              </div>
              <p>
                <%=Company.AboutUs %>
              </p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <VIT:Position runat="server" ID="psBottom"></VIT:Position>
</asp:Content>