<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- arrival section -->
<section class="arrival_section">
    <div class="container">
        <div class="box">
            <div class="arrival_bg_box">
                <%if(Skin.BodyBackgroundFile != null){ %>
                    <img src="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" alt="<%=Title %>">
                <%} %>
            </div>
            <div class="row">
                <div class="col-md-6 ml-auto">
                    <div class="heading_container remove_line_bt">
                    <h2>
                        #BestSeller <%=Title %>
                    </h2>
                    </div>
                    <p style="margin-top: 20px;margin-bottom: 30px;">
                    <%=Data.Brief %>
                    </p>
                    <a href="<%=HREF.LinkComponent(HREF.CurrentComponent, Data.Title.ConvertToUnSign(), Data.Id, SettingsManager.Constants.SendProduct, Data.Id) %>" title="<%=Data.Title %>">
                    Mua ngay
                    </a>
                </div>
            </div>
        </div>
    </div>
</section>
<!-- end arrival section -->