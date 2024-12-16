<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section>
    <div class="container">
<div class="t_cont">
                <p class="h_t_cont"><%=Title %></p>
            </div>

<div class="m_cont">

                <ul class="ul_dm_bv">
                    <%foreach (var article in Data)
        {%>
                    <li>
                        <%if(!string.IsNullOrEmpty(article.ImageName)){ %>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail,article.Title.ConvertToUnSign(),article.Id, "sArt",article.Id)%>" title="<%=article.Title%>">
                            <figure>
                                <picture>
						            <source srcset="<%=HREF.DomainStore + article.Image.FullPath%>.webp" type="image/webp">
						            <source srcset="<%=HREF.DomainStore + article.Image.FullPath%>" type="image/jpeg"> 
                                    <img class="img_object_fit" src="<%=HREF.DomainStore + article.Image.FullPath%>" alt="<%=article.Title %>"/>
                                </picture>
                            </figure>
                        </a>
                        <%} %>
                        <div class="m_ul_dm_bv">
                            <h3>
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail,article.Title.ConvertToUnSign(),article.Id, "sArt",article.Id)%>" title="<%=article.Title%>">
                                    <%=article.Title%>
                                </a>
                            </h3>

                            <ol class="sty_date">

                                <li><i class="glyphicon glyphicon-calendar"></i> <%=string.Format("{0:dd/MM/yyyy}", article.CreateDate) %></li>
                                <li><i class="glyphicon glyphicon-eye-open"></i> <%=article.Views%></li>

                            </ol>

                            <p><%=article.Brief%></p>

                            <span>
                                <a href="<%=HREF.LinkComponent(Category.ComponentDetail,article.Title.ConvertToUnSign(),article.Id, "sArt",article.Id)%>" title="<%=article.Title%>">
                                    Chi tiết <i class="glyphicon glyphicon-arrow-right"></i>
                                </a>
                            </span>

                        </div>
                    </li>
                    <%} %>
                    
                </ul><!-- End .ul_dm_bv -->

            </div>
    </div>
</section>