<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Article.ascx.cs" Inherits="Web.FrontEnd.Modules.Article" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="skill_box">
    <h3 style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h3>
    <div class="row">
        <%foreach(var attribute in dto.Attributes){ %>
        <div class="col-3">
            <%=attribute.Name %>:
        </div>
        <div class="col-9">
            <div class="progress">
                <div class="progress-bar" role="progressbar" style="width: <%=attribute.ValueName%>%" aria-valuenow="<%=attribute.ValueName%>" aria-valuemin="0" aria-valuemax="100"></div>
            </div>
        </div>
        <%} %>
    </div>
</div>