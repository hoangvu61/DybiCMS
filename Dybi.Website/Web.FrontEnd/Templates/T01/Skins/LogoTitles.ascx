<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="section-clients layout_padding-top"> 
	<div class="container"> 
		<div class="text-center mb-4">
			<h2 title="<%=Category.Title %>"><%=Title %></h2>
            <p class="mb4"><%=Category.Brief %></p>
		</div>
        
            <div id="carousel-customer" class="carousel slide" data-ride="carousel">
                <div class="carousel-inner">
                    <%for(int i = 0;i < Data.Count; i++) 
                    {%>  
                    <div class="carousel-item <%= i== 0? "active":"" %>">
                        <div class="row">
                            <%for(var j = 0; j < 4; j++){ %>
                                <%if(i < Data.Count){ %>
                                    <div class="col-3 mx-auto">
                                        <%if(Data[i].Image != null && !string.IsNullOrEmpty(Data[i].ImageName)){ %>
                                            <picture>
				                                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
				                                <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                                <img src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>" width="100%"/>
                                            </picture>
                                        <%} %>
                                    </div>
                                <%if(j < 3)i++; %>
                                <%} %>
                            <%} %>
                        </div>
                    </div>
                    <%} %>
                </div>
                <div class="carousel-control">
                    <button type="button" href="#carousel-customer" role="button" data-slide="prev" class="owl-prev">
                        <i class="fa fa-angle-left" aria-hidden="true"></i>
                    </button>
                    <button type="button" href="#carousel-customer" role="button" data-slide="next" class="owl-next">
                        <i class="fa fa-angle-right" aria-hidden="true"></i>
                    </button>
                </div>
            </div>
		</div>
        <hr />
	</div>  
    <script>
    $(document).ready(function () {
        //if ($('#carousel-customer img:first').height() == 0)
            $('#carousel-customer img').height($('#carousel-customer .col-3:first').width());
        //else
        //    $('#carousel-customer img').height($('#carousel-customer img:first').height());
    });
    </script>
</section>
