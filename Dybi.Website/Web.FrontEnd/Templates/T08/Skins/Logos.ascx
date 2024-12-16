<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%if(Data.Count > 0){ %>
<%if(Skin.BodyBackgroundFile != null){ %>
    <style>
        .info-matter{background: url('<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>')no-repeat top; background-size:cover}
    </style>
<%} else if(!string.IsNullOrEmpty(this.Skin.BodyBackground)){%>
    <style>
        .info-matter{background: <%=this.Skin.BodyBackground%> !important}
    </style>
<%}%>

<section class="info-matter py-lg-4 py-md-3 py-sm-3 py-3">
    <div class="container">
        <h2 class="text-center mb-4" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>" title="<%=Category.Title %>"><%=Title %></h2>
        <div class="stats-info row ">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>  
                <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                <div class="col-2 col-sm-1 stats-grid">
                    <div class="register-left-num">
                        <a href="<%=Data[i].Url %>" title="<%=Data[i].Title%>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Title %>" style="width:100%"/>
                            </picture>
                        </a>
                    </div>
                </div>
                <%} %>
            <%} %>
        </div>
    </div>
</section>
<%} %>