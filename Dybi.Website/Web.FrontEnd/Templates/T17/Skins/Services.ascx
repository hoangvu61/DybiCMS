<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- services section start -->
<div class="services_section layout_padding" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
    <div class="row">
        <div class="col-md-12">
            <%if(HREF.CurrentComponent == "home"){ %>
            <h2 class="services_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h2>
            <%} else {%>
            <h1 class="services_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
            <%} %>
        </div>
    </div>
    <div class="services_section_2">
        <div class="row">
            <%foreach(var item in this.Data) 
            {%>  
            <div class="col-sm-<%=12/Data.Count %>">
                <div class="services_box" style="<%=string.IsNullOrEmpty(this.Skin.HeaderBackground) ? "" : ";background-color:" + this.Skin.HeaderBackground %>">
                <div class="services_icon">
                    <%if(!string.IsNullOrEmpty(item.ImageName) && GetValueParam<bool>("DisplayImage")){ %>
                        <picture>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    <%} %>
                </div>
                <h3 class="retina_text">
                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>">
                        <%=item.Title%>
                    </a>
                </h3>
                <p class="services_text" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %>"><%=item.Brief %></p>
                </div>
            </div>
            <%} %>
        </div>
    </div>
    </div>
</div>
<!-- services section end -->
