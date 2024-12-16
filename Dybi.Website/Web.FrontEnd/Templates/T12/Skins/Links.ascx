<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div class="container">
    <div class="row offset3" style="padding-top: 30px;"> 
    <%foreach(var item in this.Data) 
    {%>  
        <div class="grid_4"> 
            <article>
		    <a class="col-xs-3" href="<%=item.Url%>" title="<%=item.Title%>" target="_blank" rel="nofollow">
                <%if(item.Image.FileExtension == ".webp"){ %>
                    <img class="wow fadeInLeft animated" style="visibility: visible; animation-duration: 2s; animation-name: fadeInLeft;" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                <%} else { %>
                    <picture>
                        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
                        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                        <img class="wow fadeInLeft animated" style="visibility: visible; animation-duration: 2s; animation-name: fadeInLeft;" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                    </picture>
                <%} %>
			    </a>
                <time datetime="<%=string.Format("{0:yyyy-MM-dd}",item.CreateDate)%>"><%=item.CreateDate.ToString("dddd, dd MMMM yyyy")%></time>
                <h4 class="color-2"><a class="a__mod" href="<%=item.Url%>" target="_blank" rel="nofollow"><%=item.Title%></a></h4>

                <p class="p__mod1"><%=item.Brief.DeleteHTMLTag().Trim().Length > 200 ? item.Brief.DeleteHTMLTag().Trim().Substring(0, 200) + "..." : item.Brief.DeleteHTMLTag().Trim() %></p>
            </article>
        </div>      
    <%} %>
    </div>
</div>
