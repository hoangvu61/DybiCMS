<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(Data.Count > 0){ %>
<!-- services section start -->
      <div class="services_section layout_padding">
         <div class="container">
            <h1 class="services_taital">
                <%=Category.Title %>
            </h1>
            <p class="services_text">
                <%=Category.Brief.Replace("\n","<br />") %>
            </p>
            <div class="services_section_2">
               <div class="row">
                   <%if(!string.IsNullOrEmpty(Category.ImageName)){ %>
                   <div class="col-md-4">
                        <picture>
						    <source srcset="<%=HREF.DomainStore + Category.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + Category.Image.FullPath%>" type="image/jpeg"> 
                            <img class="services_img" src="<%=HREF.DomainStore + Category.Image.FullPath%>" alt="<%=Category.Title %>" style="width:100%"/>
                        </picture> 
                   </div> 
                   <div class="col-md-8">
                       <div class="row">
                   <%foreach(var item in this.Data) 
                    {%>
                      <div class="col-md-6" style="margin-bottom:30px">
                         <div>
                             <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                             <a href="<%=HREF.LinkComponent("Articles",item.Title.ConvertToUnSign(), true, "scat", item.Id)%>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="services_img" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture> 
                                 </a>
                             <%} %>
                             <div style="width:100%;position:absolute;text-align:center;bottom:40px">
                             <div class="btn_main" style="width:75%;opacity:0.7"><a href="<%=HREF.LinkComponent("Articles",item.Title.ConvertToUnSign(), true, "scat", item.Id)%>"><%=item.Title %></a></div>
                         </div>
                         </div>
                         
                      </div>
                    <%} %>
                       </div>
                   </div>
                   <%} else { %>
                    <%foreach(var item in this.Data) 
                    {%>
                      <div class="col-md-4" style="margin-bottom:30px">
                         <div>
                             <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                             <a href="<%=HREF.LinkComponent("Articles",item.Title.ConvertToUnSign(), true, "scat", item.Id)%>">
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img class="services_img" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture> 
                                 </a>
                             <%} %>
                         </div>
                         <div class="btn_main" style="width:75%"><a href="<%=HREF.LinkComponent("Articles",item.Title.ConvertToUnSign(), true, "scat", item.Id)%>"><%=item.Title %></a></div>
                      </div>
                    <%} %>
                   <%} %>
                   <div class="col-md-12">
                        <%=Category.Content %>
                   </div>
               </div>
            </div>
         </div>
      </div>
      <!-- services section end -->
<%} %>
