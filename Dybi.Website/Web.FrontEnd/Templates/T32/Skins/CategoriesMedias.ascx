<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CategoriesMedias.ascx.cs" Inherits="Web.FrontEnd.Modules.CategoriesMedias" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<!-- portfolio section -->
<section class="portfolio_section ">
    <div class="container">
        <div class="heading_container heading_center">
        <h2>
            <%=Title %>
        </h2>
        </div>
        <div class="carousel-wrap">
            <div class="filter_box">
                <nav class="owl-filter-bar">
                    <a href="#" class="item active" data-owl-filter="*"><%=Language["All"] %></a>
                    <%foreach (var category in Categories)
			        {%>
                        <a href="#" class="item" data-owl-filter=".<%=category.Id %>"><%=category.Title %></a>
                    <%} %>
                </nav>
            </div>
        </div>
    </div>
    <div class="owl-carousel portfolio_carousel">
        <%foreach(var category in Categories){ %>
            <%foreach(var item in Data[category.Id]){ %>
            <div class="item <%=item.CategoryId %>">
                <div class="box">
                    <div class="img-box">
                        <img src="<%=HREF.DomainStore + item.Image.FullPath %>.webp" alt="<%=item.Title %>">
                        <div class="btn_overlay">
                            <span onclick="SelectPicture('<%=HREF.DomainStore + item.Image.FullPath %>.webp')" data-toggle="modal" data-target="#myModal" >
                            <%=Language["View"] %>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <%} %>
        <%} %>
    </div>
</section>
<!-- end portfolio section -->
                

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
        $(".modal-body img").attr("src", image);
    }
</script>
