<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CategoryBackLink.ascx.cs" Inherits="Web.FrontEnd.Modules.CategoryBackLink" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>


<div class="breadcrumb" style="padding:0">
    <div class="container">
        <ul class="breadcrumb">
            <li>
                <a href="/">
                    <i class="glyphicon glyphicon-home"></i> <%=this.GetValueParam<string>("BeginName") %>
                </a>
            </li>
            <%for (int i = 0 ; i< Categories.Count; i++) { %>
            <li>
                <%if(string.IsNullOrEmpty(ItemTitle) && i == Categories.Count - 1){ %>
                    <%=Categories[i].Title %>
                <%} else { %>
                <a href="<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(),Categories[i].Id, SettingsManager.Constants.SendCategory, Categories[i].Id) %>">
                    <%=Categories[i].Title %>
                </a>
                <%} %>
            </li>
            <%} %>      
            <%if(!string.IsNullOrEmpty(ItemTitle))  { %>
            <li>
                <%= ItemTitle %>
            </li>    
            <%} %>     
        </ul>
    </div>
</div>