﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="service_section1">
          <%foreach(var item in this.Data) 
          {%>  
            <div class="box" style="text-align:left; padding:20px">
                <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                    <%=item.Title %>
                </a>
            </div>
          <%} %>
</section>