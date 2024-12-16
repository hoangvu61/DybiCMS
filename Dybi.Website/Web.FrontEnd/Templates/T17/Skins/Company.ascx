<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../CompanyInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.CompanyInfo" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<!-- about sectuion start -->
    <div class="about_section layout_padding" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
        <div class="container">
        <div class="row">
            <div class="col-md-6">
                <h1 class="about_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
                <p class="about_text" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=Company.AboutUs %></p>
            </div>
            <div class="col-md-6">
                <div class="about_img">
                    <%if(Skin.HeaderBackgroundFile != null){ %>
                    <picture>
				        <source srcset="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>.webp" type="image/webp">
				        <source srcset="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>" type="image/jpeg"> 
                        <img src="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>" alt="<%=Company.FullName %>" class="about_img"/>
                    </picture>
                    <%} %>
                </div>
            </div>
        </div>
        </div>
    </div>
    <!-- about sectuion end -->
