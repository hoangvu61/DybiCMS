<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(Data.Count > 0){ %>
<div class="category_section" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
        <%if(HREF.CurrentComponent == "home"){ %>
        <h2 class="text-center" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>" title="<%=Category.Title %>"><%=Title %></h2>
        <%} else { %>
        <h1 class="text-center" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>" title="<%=Category.Title %>"><%=Title %></h1>
        <%} %>
        <p class="text-center mb-5" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=Category.Brief %></p>

        <div class="row">
			<%for (int i = 0; i < Data.Count; i++)
			{%>
                <div class="team-grid-colum simpleCart_shelfItem col-lg-3 col-md-4 col-sm-4 mb-3">
                    <%if(Data[i].Image != null){ %>
                    <a href="<%=HREF.LinkComponent(Category.ComponentList,Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendCategory, Data[i].Id)%>">
                        <picture>
						    <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                        </picture>
                    </a>
                    <%} %>
                    <div class="text-grid-gried text-center">
                         <a href="<%=HREF.LinkComponent(Category.ComponentList,Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendCategory, Data[i].Id)%>" alt="Data[i].Title %>"><%=Data[i].Title %></a>
                    </div>
                </div>
			<%} %>
        </div>
    </div>
</div>

<script>
    $(document).ready(function () {
        if ($('.category_section img:first').height() == 0)
            $('.category_section img').height($('.category_section .team-grid-colum:first').width());
        else
            $('.category_section img').height($('.category_section img:first').height());
    });
</script>
<%} %>

