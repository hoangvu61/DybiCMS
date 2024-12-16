<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<script>
    $("#<%=Category.Id %>").addClass("active");
</script>
<section class="service_section">
    <div class="container">
        <div class="heading_container heading_center">
            <h1>
                <%=Title %>
            </h1>
        </div>
        <div class="row"> 
            <div class="col-md-10 col-md-offset-1">
                <div class="row">
                    <%foreach(var item in this.Data) 
                    {%>  
                    <div class="col-xs-6 col-sm-4 col-md-3 box">
                        <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>">
                            <%=item.Title %>
                        </a>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</section>