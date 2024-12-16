<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductAttributes.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductAttributes" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>


<!-- category section -->
<style>
.cat_section .detail-box h2{color:<%= Skin.BodyFontColor%>}
.cat_section .detail-box h2{font-size:<%= Skin.HeaderFontSize%>px !important}
.cat_section .detail-box a{color:<%= Skin.BodyFontColor%> !important}
.cat_section .detail-box a{font-size:<%= Skin.BodyFontSize%>px !important}
</style>

  <section class="cat_section">
    <div class="container-fluid">
      <div class="row">
        <%foreach(var item in Data) 
        {%>
        <div class="col-6 col-sm-6 col-md-<%=12/Data.Count %> mx-auto">
          <div class="box cat-box1">
            <img src="/Templates/T13/images/<%=item.Key %>.jpg" alt="<%=item.Value %>">
            <div class="detail-box">
              <h2>
                <%=item.Value %>
              </h2>
              <a href="<%=HREF.LinkComponent(ComponentProducts, Title.ConvertToUnSign(), true, SettingsManager.Constants.SendAttributeId, item.Key) %>" title="<%=item.Value %>">
                Xem sản phẩm <i class="fa fa-long-arrow-right" aria-hidden="true"></i>
              </a>
            </div>
          </div>
        </div>
        <%} %>
      </div>
    </div>
  </section>