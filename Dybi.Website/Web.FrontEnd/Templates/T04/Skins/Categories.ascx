<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="cate_left_title" style="<%=string.IsNullOrEmpty(this.Skin.HeaderBackground) ? "" : ";background-color:" + this.Skin.HeaderBackground %>">
    <strong style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></strong>
</div>
<div class="cate_left_list">
    <ul class="module_colors">
        <%foreach(var item in this.Data) 
        {%>
        <li> 
            <a href="<%=HREF.LinkComponent(Category.ComponentList,item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendCategory, item.Id)%>" title="<%=item.Title%>" class="w3-row">
                <div class="w3-col w3-center" style="width:20%">
                    <input type="radio" id="<%=item.Id%>" name="<%=Category.Id %>" value="60" class="input-checkbox filter" style="margin-top:22px" <%=item.Id == this.GetValueRequest<Guid>(SettingsManager.Constants.SendCategory) ? "checked='checked'" : "" %>/>
                </div>
                <div class="w3-col" style="width:80%">
                    <%=item.Title %>
                </div>
            </a>
        </li>
	    <%} %>
    </ul>
</div>

