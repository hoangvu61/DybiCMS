<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../CompanyInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.CompanyInfo" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<div class="container">
    <div class="banner_section layout_padding">
    	<div class="row">
    		<div class="col-md-6">
    			<h2 class="landing_text"><%=Company.DisplayName %></h2>
    			<h1 class="interios_text"><%=Company.FullName %></h1>
    			<p class="lorem_text"><%=Company.Brief.Replace("\n","<br />") %></p>
            <%--div class="read_bt"><a href="<%=HREF.LinkComponent("AboutUs", "gioi-thieu", true) %>">Xem thêm</a></div>--%>
    		</div>
    		<div class="col-md-6">
    			<div class="images_1">
                <%if(Skin.BodyBackground != null){ %>
                <picture>
				    <source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>.webp" type="image/webp">
				    <source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" type="image/jpeg"> 
                    <img src="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" alt="<%=Company.FullName %>"/>
                </picture>
                <%} %>
    			</div>
    		</div>
    	</div>
    </div>
</div>

