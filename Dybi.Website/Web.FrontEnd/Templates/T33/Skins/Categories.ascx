<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<!-- gallery section start -->
<%if(Data.Count > 0 && !(Data.Count == 1 && Data[0].Id == Category.Id)){ %>
<div class="gallery_section" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div style="padding:0px 20px">
        <%if(HREF.CurrentComponent != "home"){%>
            <div class="row">
                <div class="col-sm-12">
                    <h1 class="gallery_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
                    <p class="gallery_text" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=Category.Brief %></p>
                </div>
            </div>
        <%} %>
        <div class="gallery_section_2">
            <div class="row">
			    <%for (int i = 0; i < Data.Count; i++)
			    {%>
                    <div class="col-6 col-sm-4 col-md-3">
                        <div class="img-box">
                            <%if(Data[i].Image != null){ %>
                            <a href="<%=HREF.LinkComponent(Category.ComponentList,Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendCategory, Data[i].Id)%>">
                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath %>.webp" alt="<%=Data[i].Title %>" class="image">
                            </a>
                            <%} %>
                            <div class="box-text">
                                <div class="box-text-inner">
                                    <h6 class="interior_text" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>">
                                        <a href="<%=HREF.LinkComponent(Category.ComponentList,Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendCategory, Data[i].Id)%>">
                                            <%=Data[i].Title %>
                                        </a>
                                    </h6>
                                </div>
                            </div>
                        </div>
                    </div>
			    <%} %>
            </div>
        </div>
    </div>
</div>
<!-- gallery section end -->

<script>
    $(document).ready(function () {
        if ($('.gallery_section img:first').height() == 0)
            $('.gallery_section img').height($('.gallery_section .img-box:first').width());
        else
            $('.gallery_section img').height($('.gallery_section img:first').height());
    });
</script>
<%} %>

