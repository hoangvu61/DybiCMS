<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<h3 class="mod-center"><%=Title %></h3>
<h4 class="offset4"><%=Category.Brief %> </h4>
<div class="row offset3">
<%foreach(var item in this.Data) 
{%>  
                    <div class="grid_4">
                        <article>
						<a class="col-xs-3" href="<%=HREF.LinkComponent("Article",item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <img class="wow fadeInLeft animated" data-wow-duration="2s" src="<%=!string.IsNullOrEmpty(item.ImageName) ? HREF.DomainStore + item.Image.FullPath : "/templates/t02/img/no-image-available.png"%>.webp" alt="<%=item.Title%>" style="visibility: visible; animation-duration: 2s; animation-name: fadeInLeft;"/>
							</a>
                            <time datetime="<%=string.Format("{0:yyyy-MM-dd}",item.CreateDate)%>"><%=item.CreateDate.ToString("dddd, dd MMMM yyyy")%></time>
                            <h4 class="color-2"><a class="a__mod" href="<%=HREF.LinkComponent("Article", item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>"><%=item.Title%></a></h4>

                            <p class="p__mod1"><%=item.Brief.DeleteHTMLTag().Trim().Length > 200 ? item.Brief.DeleteHTMLTag().Trim().Substring(0, 200) + "..." : item.Brief.DeleteHTMLTag().Trim() %></p>
                        </article>
                    </div>      
                <%} %>
</div>
