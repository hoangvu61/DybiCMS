<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<!-- gallery section start -->
<div class="gallery_section layout_padding" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
    <div class="row">
        <div class="col-sm-12">
            <h2 class="gallery_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h2>
            <p class="gallery_text" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=Category.Brief %></p>
        </div>
    </div>
    <div class="gallery_section">
        <div class="gallery_section_2">
            <div class="row">
			    <%for (int i = 0; i < Data.Count; i++)
			    {%>
                    <div class="col-md-4">
                        <div class="container_main">
                            <%if(Data[i].Image != null){ %>
                            <a href="<%=HREF.LinkComponent(Category.ComponentList,Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendCategory, Data[i].Id)%>">
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath %>.webp" alt="<%=Data[i].Title %>" class="image">
                                 <div class="overlay">
                                      <div class="text">
                                         <h4 class="interior_text" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=Data[i].Title %></h4>
                                         <p class="ipsum_text" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=Data[i].Brief %></p>
                                      </div>
                                   </div>
                            </a>
                            <%} %>
                        </div>
                    </div>
			    <%} %>
            </div>
        </div>
    </div>
    </div>
</div>
<!-- gallery section end -->


<script>
    $(document).ready(function () {
        if ($('.gallery_section img:first').height() == 0)
            $('.gallery_section img').height($('.gallery_section .container_main:first').width());
        else
            $('.gallery_section img').height($('.gallery_section img:first').height());
    });
</script>

