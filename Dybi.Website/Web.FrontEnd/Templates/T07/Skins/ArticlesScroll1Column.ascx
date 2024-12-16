<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<script src="/Includes/auto-scrolling-data-list/jquery.autoscroll.js"></script>

<div class="block_sb">
        <div class="t_sb"><%=Title %></div>
        <div class="m_sb">
            <ul data-autoscroll class="news_sb">
                <%foreach(var item in this.Data) 
                {%>  
                <li>
					<a title="<%=item.Title %>" href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, "sart", item.Id)%>">
                        <%if(item.Image != null){ %>
                        <figure>
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                
                                <img class="img_object_fit" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        </figure>
                        <%} %>
						<h4><%=item.Title %></h4>
					</a>
				</li>
                <%} %>
				            </ul>
        </div><!-- End .m_sb -->
    </div>