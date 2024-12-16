<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- gallery section start -->
<section class="sp_spec">
    <div class="<%=HREF.CurrentComponent == "home" ? "min_wrap" : "" %>">
        <h2 class="t_h" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h2>
        <p class="subtitle" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=Category.Brief %></p>
        
        <div class="gallery_section">
            <div class="gallery_section_2">
                <div class="row">
                    <%{ Data = Data.Where(e => e.Type == "IMG").ToList(); }%>
			        <%for (int i = 0; i < Data.Count; i++)
			        {%>
                        <%if(i > 3 && i < 6){ %>
                            <div class="col-xs-6"> 
                        <%} else if(i > 5 && i < 9){ %>
                            <div class="col-xs-6 col-sm-4 col-md-4">
                        <%} else {%>
                            <div class="col-xs-6 col-sm-4 col-md-3">
                        <%} %>
                            <div class="container_main">
                                <img onclick="SelectPicture(this)" data-toggle="modal" data-target="#myModal" src="<%=HREF.DomainStore + Data[i].Image.FullPath %>.webp" alt="<%=Data[i].Title %>" class="image">
                            </div>
                        </div>
			        <%} %>
                </div>
            </div>
        </div>
        <div class="seemore_bt">
        <%if(Top > 0 && TotalItems > Top){ %>
            <%if(HREF.CurrentComponent == "home"){ %>
                <a href="<%=HREF.LinkComponent("Medias",Category.Title.ConvertToUnSign(), true)%>">Xem thêm</a>
            <%} else {%>
                <a href="<%=HREF.LinkComponent("Medias", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, 1)%>">Trang đầu</a> 
                <%for(int i = 1; i <= TotalPages; i++){ %>
                <a href="<%=HREF.LinkComponent("Medias", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, i)%>"><%=i %></a> 
                <%} %>
                <a href="<%=HREF.LinkComponent("Medias", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, TotalPages)%>">Trang cuối</a> 
            <%} %>
        <%} %>
        </div>

    </div>
</section>
<!-- gallery section end -->

<!-- The Modal -->
<div class="modal" id="myModal">
  <div class="modal-dialog modal-lg">
    <div class="modal-content"> 

      <!-- Modal Header -->
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>

      <!-- Modal body -->
      <div class="modal-body">
        <img style="width:100%"/>
      </div>
    </div>
  </div>
</div>

<script type="text/javascript">
    function SelectPicture(image) {
        $(".modal-body img").attr("src", image.src);
    }
</script>

<script>
    $(document).ready(function () {
        if ($('.gallery_section img:first').height() == 0)
            $('.gallery_section img').height($('.gallery_section .row>div:first').width());
        else
            $('.gallery_section img').height($('.gallery_section img:first').width());
    });
</script>

