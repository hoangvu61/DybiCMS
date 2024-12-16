<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductAttributes.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductAttributes" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="attribute-section">
    <div class="container">
        <div class="row">
        <%foreach(var item in Data.Reverse()) 
        {%>
            <div class="col-12 col-md-4">
                <div class="attribute-box <%=item.Key %>">
                    <a href="<%=HREF.LinkComponent(ComponentProducts, item.Value.Split('|')[0].ConvertToUnSign(), true, SettingsManager.Constants.SendAttributeId, SourceId, SettingsManager.Constants.SendAttributeValue, item.Key) %>" title="<%=item.Value %>">
                        <div class="row">
                            <div class="col-4 col-sm-4 col-md-12 col-lg-4 image-box">
                                <img src="/Templates/T03/images/<%=item.Key %>.webp" alt="<%=item.Value %>" />
                            </div>
                            <div class="col-8 col-sm-8 col-md-12 col-lg-8 detail-box">
                                <h2 title="<%=item.Value %>"><%=item.Value.Split('|')[0] %></h2>
                                <p><%=item.Value.Split('|')[1] %></p>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        <%} %>
        </div>
    </div>
</section>