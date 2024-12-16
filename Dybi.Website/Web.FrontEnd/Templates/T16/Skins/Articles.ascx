<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- blog -->
      <div class="blog">
         <div class="container">
            <div class="row">
               <div class="col-md-12">
                  <div class="titlepage text_align_left ">
                     <h2><%=Title %></h2>
                     <p><%=Category.Brief %></p>
                  </div>
               </div>
            </div>
            <div class="row">
                <%foreach(var item in this.Data) 
                {%>  
                <div class="col-md-12">
                  <div class="fist_sec">
                     <div class="row">
                        <div class="col-lg-6 col-md-12">
                           <div class="blog_box" >
                              <div class="fer text_align_left">
                                 <span>
                                     <%=item.CreateDate.ToShortDateString() %>
                                 </span>
                              </div>
                              <h3><a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                                    <%=item.Title%>
                                </a></h3>
                              <p><%=item.Brief.Replace("\n","<br />") %></p>
                           </div> 
                        </div>
                        <div class="col-lg-6 col-md-12">
                           <div class="blog_img">
                               <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                                   <figure>
                                   <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                                        <picture>
						                  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						                  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                        </picture>
                                    <%} %>
                                   </figure> 
                                </a>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
                <%} %>
               <div class="col-md-12">
                  <a class="read_more" href="<%=HREF.LinkComponent("Articles", Title.ConvertToUnSign(), true) %>">Xem thêm</a>
               </div>
            </div>
         </div>
      </div>
      <!-- end blog -->