<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="grid_4">
<h3 class="mod-center"><%=Title %></h3>
<%foreach(var item in this.Data) 
{%>  

                            <div class="post">
                                <h4 class="color-2 h4__mod">
									<a class="col-xs-3" href="<%=HREF.LinkComponent("Article",item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>"><%=item.Title%>
									</a></h4>
                                <p class="p__mod1"><%=item.Brief.DeleteHTMLTag().Trim().Length > 200 ? item.Brief.DeleteHTMLTag().Trim().Substring(0, 200) + "..." : item.Brief.DeleteHTMLTag().Trim() %></p>
                                <time datetime="<%=string.Format("{0:yyyy-MM-dd}",item.CreateDate)%>"><span><%=string.Format("{0:dd}",item.CreateDate)%></span><br> <%=item.CreateDate.ToString("MMM")%></time>
                            </div>
                <%} %>
</div>
