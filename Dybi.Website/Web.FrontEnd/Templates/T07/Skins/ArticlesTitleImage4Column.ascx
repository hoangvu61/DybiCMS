<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="dv_spec">
    <div class="min_wrap">
        <h2 class="t_h"><%=Title %></h2>
        <p class="subtitle">
            <%=Category.Brief %>
        </p>
        <div class="row">
            <%foreach(var item in this.Data) 
            {%>  
            <div class="col-md-3 col-sm-12 col-xs-12">
                <a href="<%=HREF.LinkComponent(Category.ComponentDetail,item.Title.ConvertToUnSign(), item.Id, "sart", item.Id)%>">
                    <picture>
						<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						<source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                        <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore + item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                    </picture>
                    <h3 class="post4title" title="<%=item.Title %>"><%=item.Title %></h3>
                </a>
            </div>
            <%} %>
        </div><!-- End .ul_dv_spec -->
    </div><!-- End .min_wrap -->
</section>