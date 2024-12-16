<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../CompanyInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.CompanyInfo" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<!-- about -->
      <div class="about">
         <div class="container">
            <div class="row d_flex">
               <div class="col-md-6">
                  <div class="titlepage text_align_left">
                     <h2>
                         <%=Company.FullName %>
                     </h2>
                     <p>
                         <%=Company.AboutUs %> 
                     </p>
                  </div>
               </div>
               <div class="col-md-6">
                  <div class="about_img" data-aos="fade-left">
                      <%if(Skin.BodyBackground != null){ %>
                     <figure>
                         <picture>
				            <source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>.webp" type="image/webp">
				            <source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" alt="<%=Company.FullName %>"/>
                        </picture>
                     </figure>
                      <%} %>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <!-- end about -->

