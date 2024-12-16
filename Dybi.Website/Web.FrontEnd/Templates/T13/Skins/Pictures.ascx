<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%if(Data.Count > 0){ %>
<!-- gallery section start -->
<div class="gallery_section" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">

        <h2 class="gallery_taital wow animate__ animate__fadeInUp  animated animated" data-wow-duration="1.2s" data-wow-delay="0.2s" style="padding:30px;<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h2>
        <p class="gallery_text wow animate__ animate__fadeInUp  animated animated" data-wow-duration="1.2s" data-wow-delay="0.2s" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=Category.Brief %></p>
    
        <div class="gallery_section">
        <div class="row">
			<%for (int i = 0; i < Data.Count; i++)
			{%>
                <div class="col-6 col-sm-3 wow animate__animated animate__zoomIn animated" data-wow-duration="0.8s" data-wow-delay="0.3s" style="<%=Skin.Height > 0 ?"height:" + Skin.Height + "px" : ""%>">
                    <div class="container_main">
                        <img onclick="SelectPicture(this)" data-toggle="modal" data-target="#myModal" src="<%=HREF.DomainStore + Data[i].Image.FullPath %>.webp" alt="<%=Data[i].Title %>" class="image" style="width:100%; cursor:pointer; margin-bottom:30px">
                    </div>
                </div>
			<%} %>
        </div>
    </div>
    </div>
</div>
<!-- gallery section end -->

<!-- The Modal -->
<div class="modal" id="myModal">
  <div class="modal-dialog modal-lg">
    <div class="modal-content"> 
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
            $('.gallery_section img').height($('.gallery_section .container_main:first').width());
        else
            $('.gallery_section img').height($('.gallery_section img:first').height());
    });
</script>
<%} %>

