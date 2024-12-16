<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="info_link-box">
                <h5>
                    <%=Title %>
                </h5>   
        <ul class="customerservice">
            <%for(int i = 0; i < this.Data.Count; i++){ %>
            <li>
                <a href="<%=HREF.LinkComponent("Article", Data[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendArticle, Data[i].Id)%>" title="<%=Data[i].Title%>">
                    <%=Data[i].Title%>
                </a>
            </li>
            <%} %>
        </ul>
    </div>

