<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="articles-section">
    <div class="container">
        <h1 class="t_h" title="<%=Category.Title%>"><%=Title%></h1>
        <p class="subtitle">
            <%=Category.Brief %>
        </p>

        <ul class="ul_dm_bv">
            <%foreach(var item in this.Data) 
            {%>  
            <li class="col-md-6">
                <%if(!string.IsNullOrEmpty(item.ImageName)){%>
                <figure>
                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                        <picture>
					        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
					        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img class="img_object_fit" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    </a>
                </figure>
                <%} %>
                <div class="m_ul_dm_bv">
                    <h3> 
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%=item.Title%>
                        </a>
                    </h3>
                    
                    <ol class="sty_date">
                        <li> <i class="glyphicon glyphicon-calendar"></i> <%=string.Format("{0:dd/MM/yyyy}", item.CreateDate) %> </li>
                    </ol>
                    <p><%=item.Brief %></p>
                    <span>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            Chi tiết <i class="glyphicon glyphicon-arrow-right"></i>
                        </a>
                    </span>
                </div>
            </li>
            <%} %>
                            </ul>
    </div>
</section>
