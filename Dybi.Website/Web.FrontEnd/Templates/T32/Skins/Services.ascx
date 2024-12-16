<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- service section -->
<section class="service_section layout_padding" style="<%=Skin.BodyBackgroundFile != null ? "background-image:url("+ HREF.DomainStore + Skin.BodyBackgroundFile.FullPath +")" : ""%>; background-size:cover">
    <div class="container ">
        <div class="heading_container heading_center">
        <h2><%=Title %></h2>
        </div>
        <div class="row">
            <%foreach(var item in this.Data) {%> 
            <div class="col-6 col-sm-6 col-md-<%=Skin.Width > 0 ? 12/Skin.Width : 4 %>" style="margin:20px 0px">
                <div class="box">
                <div class="img-box">
                    <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                    <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">   
                        <picture>
                            <%if(item.Image.FileExtension != ".webp"){ %>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <%} %>
                            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    </a>
                    <%} %>
                </div>
                <div class="detail-box">
                    <h5>
                        <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
                            <%=item.Title%>
                        </a>
                    </h5>
                    <p style="<%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>">
                        <%=item.Brief.Length > 200 ? item.Brief.Substring(0, 200) + "..." : item.Brief %>
                    </p>
                </div>
                </div>
            </div>
            <%} %>
        </div>
    </div>
</section>
<!-- end service section -->
