<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../CompanyInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.CompanyInfo" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<!-- section about start -->
<div class="about_section">
    <div class="container-fluid">
        <div class="row">
    	    <div class="col-md-6 ">
    		    <div class="right_text" style="color:#fff">
    			    <%=Company.AboutUs %>
    		    </div>
    	    </div>
    	    <div class="col-md-6 padding_0">
    		    <div class="about_img">
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
<!-- section about end -->