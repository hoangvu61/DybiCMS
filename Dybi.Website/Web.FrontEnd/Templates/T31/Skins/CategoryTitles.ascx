<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<!-- news section start -->
<div class="policy">
    <ul>
        <li>
            <a href="<%=HREF.LinkComponent("AboutUs", Component.Company.DisplayName.ConvertToUnSign(), true) %>"><%=Language["AboutUs"]%></a>
        </li>
    <%foreach(var item in this.Data) 
    {%>  
        <li>
            <a href="<%=HREF.LinkComponent(Category.ComponentList, item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, item.Id)%>" title="<%=item.Title%>" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>">
                <%=item.Title%>
            </a>
        </li>
    <%} %>
        <li><a href="<%=HREF.LinkComponent("Contact", "lien-he", true) %>"><%=Language["Contact"]%></a></li>
    </ul>
</div>
<!-- news section end -->
