<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<!-- news section start -->
<div class="policy">
    <h6 title="<%=Title %>"><%=Title %></h6>
    <ul>
    <%foreach(var item in this.Data) 
    {%>  
        <li>
            <a href="<%=HREF.LinkComponent("Articles", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, item.Id)%>" title="<%=item.Title%>" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>">
                <%=item.Title%>
            </a>
        </li>
    <%} %>
    </ul>
</div>
<!-- news section end -->
