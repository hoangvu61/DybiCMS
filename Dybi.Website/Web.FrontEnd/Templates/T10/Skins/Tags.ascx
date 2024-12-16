<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Tags.ascx.cs" Inherits="Web.FrontEnd.Modules.Tags" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="tags">
<h3 class="tag_head" style="margin-bottom:15px; <%=string.IsNullOrEmpty(this.Skin.HeaderBackground) ? "" : ";background-color:" + this.Skin.HeaderBackground %><%=string.IsNullOrEmpty(this.Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h3>
	<ul class="tags_links">
        <%foreach (var link in ListTags)
            {%>
        <li class="gentag"><%=link %></li>
            <%} %>	
    </ul>
    <div class="clear"></div>
</div>