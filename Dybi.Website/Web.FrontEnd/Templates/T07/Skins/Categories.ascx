<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(HREF.CurrentComponent == "home"){ %>
<section class="dv_spec">
    <div class="container">
        <h1 class="t_h" title="<%=Category.Title %>"><%=Title %></h1>
        <div class="row ul_dv_spec clearfix">
        <%foreach(var item in this.Data) 
        {%>  
        <div class="col-xs-6 col-sm-6 col-md-<%=Data.Count % 4 == 0 ? 3 : 4 %>">
            <a href="<%=HREF.LinkComponent(Category.ComponentList,item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendCategory, item.Id)%>">
                <figure class="img_dv_spec" style="margin:0px !important">
                    <%if(item.Image != null){ %>
                    <picture>
					    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                        <img class="img_object_fit" src="<%=HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                    </picture>
                    <%} %>
                </figure>
            </a>
            <div class="cat_h">
                <a href="<%=HREF.LinkComponent(Category.ComponentList,item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendCategory, item.Id)%>">
                    <h3 class="n_dv_spec"><%=item.Title %></h3>
                </a>
            </div>
        </div>
        <%} %>
        </div><!-- End .ul_dv_spec -->
    </div><!-- End .min_wrap -->
</section>
<%} else {%>
<div class="row">
    <h2 class="t_h" <%=HREF.CurrentComponent == "products" ? "style='margin:50px 0px 60px'" : "" %>><%=Title %></h2>
        <%foreach(var item in this.Data) 
        {%>  
        <div class="col-md-12" style="margin-bottom:40px">
            <a href="<%=HREF.LinkComponent(Category.ComponentList,item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendCategory, item.Id)%>">
                <figure class="img_dv_spec" style="margin:0px !important">
                    <%if(item.Image != null){ %>
                    <picture>
					    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                        <img class="img_object_fit" src="<%=HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                    </picture>
                    <%} %>
                </figure>
            </a>
            <div class="cat_h">
                <a href="<%=HREF.LinkComponent(Category.ComponentList,item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendCategory, item.Id)%>">
                    <h3 class="n_dv_spec"><%=item.Title %></h3>
                </a>
            </div>
        </div>
        <%} %>
        </div>
    <%}%>
