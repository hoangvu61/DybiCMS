<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="cate_left_title" style="<%=string.IsNullOrEmpty(this.Skin.HeaderBackground) ? "" : ";background-color:" + this.Skin.HeaderBackground %>">
    <strong style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
        <%=Title %>
    </strong>
</div>
<div class="art_left_list">
    <ul class="module_colors">
        <%foreach(var item in this.Data) 
        {%>
        <li> 
            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendArticle, item.Id)%>" title="<%=item.Title%>" class="w3-row">
                <div class="w3-col w3-center" style="width:20%">
                    <input type="radio" id="<%=item.Id%>" name="<%=ArticleId %>" value="60" class="input-checkbox filter" style="margin-top:22px"/>
                </div>
                <div class="w3-col" style="width:80%">
                    <%=item.Title %>
                </div>
            </a>
        </li>
	    <%} %>
    </ul>
</div>