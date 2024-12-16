<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div class="documents">
    <h3 class="tag_head" style="margin-bottom: 15px; <%=string.IsNullOrEmpty(Skin.HeaderBackground) ? "": ";background-color:" + this.Skin.HeaderBackground %><%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
        <a href="/Tai-lieu-tieng-Anh" title="<%=Title %>"><%=Title %></a>
    </h3>
    <%foreach (var item in this.Data)
        {%>
    <div style="margin-bottom: 10px;">
        <a href="<%=HREF.LinkComponent("Document", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendMedia, item.Id)%>" title="<%=item.Title %>">
            <img src="<%=HREF.DomainStore + item.Image.FullPath %>" alt="<%=item.Title %>" style="width: 30%; float: left; margin: 0px 10px 10px 0px" />
        </a>

        <h5><a style="font-size: 14px" href="<%=HREF.LinkComponent("Document", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendMedia, item.Id)%>" title="<%=item.Title %>"><%=item.Title%></a></h5>
        <div class="clear" style="clear: both"></div>
    </div>

    <%} %>
</div>

