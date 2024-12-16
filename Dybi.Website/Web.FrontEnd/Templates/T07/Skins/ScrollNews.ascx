<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<script src="/Includes/auto-scrolling-data-list/jquery.autoscroll.js"></script>
<div class="news_index col-md-6 col-sm-12 col-xs-12">
    <div class="title_main">
        <h3><%=Title %></h3>
    </div>

                        <ul data-autoscroll class="scroll_tin">
                             <%foreach(var item in this.Data) {%>
                            <li class="item_tin">
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail,item.Title.ConvertToUnSign(), item.Id, "sArt", item.Id)%>">
                                    <div class="img_tin yktin transition">
                                        <picture>
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						  <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img class="hover_opacity" src="<%=HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                            </picture>
                                    </div>
                                    <h4><%=item.Title %></h4>
                                </a>
                                <p class="destin">
                                    <%= string.IsNullOrEmpty(item.Brief) ? string.Empty : item.Brief.Replace("\n", "<br />") %>
                                </p>
                            </li>
                <%} %>
                        </ul>
</div>