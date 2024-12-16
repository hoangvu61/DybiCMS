<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<%if (!DisplayImage)
{ %>
    <section class="section-services py-5" <%=!string.IsNullOrEmpty(Skin.HeaderBackground) ? "style='background:" + Skin.HeaderBackground + " !important'" : ""%>>
        <div class="container">
            <%if(!string.IsNullOrEmpty(Category.ImageName)){%>
            <style>
                .entry-header{background-image: url('<%= HREF.DomainStore + Category.Image.FullPath%>') !important;}
            </style>
            <%}%>

	        <div class="heading_container text-center">
			    <h2 title="<%=Category.Title %>">
                    <a href="<%=HREF.LinkComponent("Articles", Category.Title.ConvertToUnSign(), true, "scat", Category.Id)%>" title="<%=Category.Title %>">
                        <%=Category.Title %>
                    </a>
                </h2>
			    <p class="category_brief"><%=Title %></p>
	        </div>
	        <div class="row text-center mt-5">
	            <%for (int i = 0; i < Data.Count && i < 2; i++)
                    {%>
                    <div class="col-12 col-xs-12 col-md-6 mx-auto mb-3">
                        <div class="box box_odd">
                            <h3>
                                <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, "sart", Data[i].Id)%>" title="<%=Data[i].Title %>">
                                    <%=Data[i].Title %>
                                </a>
                            </h3>
                            <p>
                                <%=Data[i].Brief %>
                            </p>
                        </div>
                    </div>
		        <%} %>
                <%for (int i = 2; i < Data.Count; i++)
                    {%>
                    <div class="col-12 col-xs-12 col-md-4 mx-auto mt-3">
                        <div class="box box_odd">
                            <h3>
                                <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, "sart", Data[i].Id)%>" title="<%=Data[i].Title %>">
                                    <%=Data[i].Title %>
                                </a>
                            </h3>
                            <p>
                                <%=Data[i].Brief %>
                            </p>
                        </div>
                    </div>
		        <%} %>
	        </div>
        </div>
    </section>
<%} else { %>
    <section class="articles_section py-5" <%=!string.IsNullOrEmpty(Skin.HeaderBackground) ? "style='background:" + Skin.HeaderBackground + "'" : ""%>>
        <div class="container">
            <div class="text-center py-5" <%=!string.IsNullOrEmpty(Skin.HeaderFontColor) ? "style='color:" + Skin.HeaderFontColor + "'" : ""%>>
                <h2 title="<%=Title %>"><%=Title %></h2>
                <p class="category_brief"><%= Category.Brief%></p>
            </div>

            <div class="row article-list pb-5">
                <%foreach(var item in this.Data) {%>
                <div class="col-12 col-lg-4 mx-auto py-3">
                    <a href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), true, "sart", item.Id)%>" title="<%=item.Title %>">
                        <div class="article-item">
                            <div class="article-image" style='<%=!string.IsNullOrEmpty(Skin.BodyBackground) ? "background:" + Skin.BodyBackground : ""%><%=Skin.Width > 0 ? ";border-radius:"+Skin.Width+"%":"" %>'>
                                <%if(item.Image != null && !string.IsNullOrEmpty(item.Image.FullPath)){ %>
                                    <%if(item.Image.FullPath.EndsWith(".webp")){ %>
                                        <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                    <%} else { %>
                                        <picture>
                                            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
                                            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                        </picture>
                                    <%} %>
                                <%} %>
                            </div>
                            <div class="p-3" <%=!string.IsNullOrEmpty(Skin.BodyBackground) ? "style='background:" + Skin.BodyBackground + ";margin-top:15px;height:100%'" : ""%>>
                                <h3 title="<%=item.Title %>" class="text-center py-2 font-weight-bold"><%=item.Title %></h3>
                                <div class="blog-first-para text-justify" title="<%=item.Brief.DeleteHTMLTag() %>">
                                    <%=item.Brief.DeleteHTMLTag() %>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <%} %>
            </div>
        </div>
</section>
<%} %>
