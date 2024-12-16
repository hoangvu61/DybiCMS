<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(Data.Count > 0){ %>
<!-- services section start -->
      <div class="services_section layout_padding">
         <div class="container">
             <%if(GetValueParam<bool>("DisplayImage")){ %>
            <h1 class="services_taital">
                <%=Category.Title %>
            </h1>
            <p class="services_text">
                <%=Category.Brief.Replace("\n","<br />") %>
            </p>
             <%} else { %>
             <hr />
             <%} %>
            <div class="services_section_2">
                       <div class="row">
                   <%foreach(var item in this.Data) 
                    {%>
                      <div class="col-md-3" style="margin-bottom:30px">
                         <div>
                             <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                             <a href="<%=HREF.LinkComponent("Article",item.Title.ConvertToUnSign(), true, "sart", item.Id)%>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="services_img" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture> 
                                 </a>
                             <%} %>
                         </div>
                             <div style="clear:both;height:80px;padding: 10px; background:<%=Skin.BodyBackground%>;font-size:<%=Skin.BodyFontSize%>px"><a href="<%=HREF.LinkComponent("Article",item.Title.ConvertToUnSign(), true, "sart", item.Id)%>" style="color:<%=Skin.BodyFontColor%>"><%=item.Title %></a></div>
                      </div>
                    <%} %>
                       </div>
            </div>
         </div>
      </div>
      <!-- services section end -->
<%} %>
