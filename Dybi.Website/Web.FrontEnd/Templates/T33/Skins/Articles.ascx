<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>


<%if (Data.Count > 0)
    { %>
<!-- blog section -->
<section class="blogs_section">
    <div class="container">
        <div class="heading_container">
            <%if (HREF.CurrentComponent == "home")
                {%>
                <h2>
                    <%=Title %>
                </h2>
            <%}
                else
                { %>
                <h1>
                    <%=Title %>
                </h1>
            <%} %>
            <p><%=Category.Brief %></p>
        </div>
        <div class="row">
        <%foreach (var item in this.Data)
            {%>  
        <div class="col-6 mx-auto">
            <div class="box">
                <%if (GetValueParam<bool>("DisplayImage"))
                    { %>
                    <div class="img-box"> 
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                        <%if (!string.IsNullOrEmpty(item.ImageName))
                            { %>
                            <picture>
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						        <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                            </picture>
                        <%} %>
                        </a>
                    </div>
                <%} %>
            <div class="detail-box">
                <h5>
                    <%=item.Title%>
                </h5>
                <p>
                    <%=item.Brief.Length > 200 ? item.Brief.Substring(0, 200) + "..." : item.Brief %>
                </p>
            </div>
            </div>
        </div> 
        <%} %>
        </div>
    </div>
</section>
<!-- end blog section -->

<script>
    $('.blogs_section img').height($('.blogs_section img:first').width());
</script>
<%} %>
