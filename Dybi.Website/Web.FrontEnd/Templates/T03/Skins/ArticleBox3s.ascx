<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<h6><%=Title %></h6>
							
<ul class="article">
    <li>
        <a class="simpletitle" href="<%=HREF.LinkComponent("AboutUs", "gioi-thieu-" + Component.Company.DisplayName.ConvertToUnSign(), true) %>">Giới thiệu</a>
    </li>
    <%foreach(var item in this.Data) 
    {%>
        <li>
            <a class="simpletitle" href="<%=HREF.LinkComponent(Category.ComponentDetail,item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle,item.Id)%>">
                <%=item.Title %>
            </a>
        </li>
    <%} %>
</ul>



                

