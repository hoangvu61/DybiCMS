<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- bestseller section -->
<section class="bestseller_section">
    <div class="w3-container">
        <div class="box">
            <div class="w3-row">
                <div class="w3-col m8">
                    <div class="heading_container remove_line_bt">
                    <h2>
                        #BestSeller 
                        <a href="<%=HREF.LinkComponent("Product", Data.Title.ConvertToUnSign(), Data.Id, SettingsManager.Constants.SendCategory, Data.Id) %>" title="<%=Data.Title %>">
                            <%=Title %>
                        </a>
                    </h2>
                    </div>
                    <p style="margin-top: 20px;margin-bottom: 30px;">
                    <%=Data.Brief %>
                    </p>
                    <a class="w3-btn w3-ripple w3-green" href="<%=HREF.LinkComponent("Product", Data.Title.ConvertToUnSign(), Data.Id, SettingsManager.Constants.SendCategory, Data.Id) %>" title="<%=Data.Title %>">
                        <%=Language["BuyNow"] %>
                    </a>
                </div>
                <div class="w3-col m4">
                    <a href="<%=HREF.LinkComponent("Product", Data.Title.ConvertToUnSign(), Data.Id, SettingsManager.Constants.SendCategory, Data.Id) %>" title="<%=Data.Title %>">
                        <picture>
						    <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
                            <img data-large="<%=HREF.DomainStore + Data.Image.FullPath%>" src="<%=!string.IsNullOrEmpty(Data.ImageName) ? HREF.DomainStore + Data.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=Title %>"/>
                        </picture> 
                    </a>
                </div>
            </div>
        </div>
    </div>
</section>
<!-- end arrival section -->