<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<script>
    $("#<%=this.GetValueRequest<Guid>(SettingsManager.Constants.SendCategory) %>").addClass("active");
</script>

<section class="service_section">
    <div class="container">
        <div class="heading_container heading_center">
        <h1>
            <%=Title %>
        </h1>
        </div>
        <div class="row"> 
            <div class="col-md-10 col-md-offset-1">
                
            <%foreach(var item in this.Data) 
            {%>  
            <div class="row" style="margin-top:30px">
                <div class="col-xs-4 col-sm-4 col-md-3">
                    <a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, "spro", item.Id) %>">
                        <picture>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore +  item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                        </picture>
                    </a>
                </div>
                <div class="col-xs-8 col-sm-8 col-md-9">
                    <h2><%=item.Title %></h2>
                    <div class="brief"><%=item.Brief %></div>
                    <div class="btn btn-primary"><a href="<%=HREF.LinkComponent("Product", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendProduct, item.Id)%>">Chi tiết</a></div>
                </div>
            </div>
            <%} %>
                
            </div>
        </div>
    </div>
</section>