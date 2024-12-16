<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="dv_spec">
        <div class="<%=HREF.CurrentComponent == "home" ? "min_wrap" : "" %>">
            <h2 class="t_h"><%=Title %></h2>
            <ul class="ul_dv_spec clearfix">
                <%foreach(var item in this.Data) 
                {%>  
                <li>
                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail,item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id)%>">
                        <figure class="img_dv_spec" style="margin:0px !important">
                             <picture>
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img class="img_object_fit" src="<%=HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                                 </figure>
                        <h3 class="n_dv_spec"><%=item.Title %></h3>
                    </a>
                </li>
                <%} %>
                            </ul><!-- End .ul_dv_spec -->
        </div><!-- End .min_wrap -->
    </section>