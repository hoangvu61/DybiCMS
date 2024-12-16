<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- why section -->
<section class="why_section layout_padding">
    <div class="container">
        <div class="row">
            <%foreach(var item in this.Data) 
            {%> 
            <div class="col-md-4">
                <div class="box ">
                    <div class="img-box">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                                <picture>
						            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						            <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                    <img style="width:100%" src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                </picture>
                            <%} %>
                        </a>
                    </div>
                    <div class="detail-box">
                    <h5>
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%=item.Title.Length > 35 ? item.Title.Substring(0, 35) + "..." : item.Title%>
                        </a>
                    </h5>
                    <p>
                        <%=item.Brief.Length > 75 ? item.Brief.Substring(0, 75) + "..." : item.Brief %>
                    </p>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
    </div>
</section>

<script>
    $(document).ready(function () {
        $('.why_section .img-box img').height($('.why_section .img-box img:first').width());
    });
</script>
<!-- end why section -->