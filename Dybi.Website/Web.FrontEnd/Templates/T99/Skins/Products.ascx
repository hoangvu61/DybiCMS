﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<script>
    $("#<%=this.GetValueRequest<Guid>(SettingsManager.Constants.SendCategory) %>").addClass("active");
</script>


<div class="heading_container heading_center">
<h2 title="<%=Title %>">
    <%=Title %>
</h2>
</div>
<div class="row"> 
    <div class="col-md-10 col-md-offset-1">
        <div class="row">
    <%foreach(var item in this.Data) 
    {%>  
    <div class="col-xs-4 col-sm-4 col-md-4 box">
            <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                <%=item.Title %>
            </a>
    </div>
    <%} %>
        </div>
    </div>
</div>
