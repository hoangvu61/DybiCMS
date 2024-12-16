<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<div class="col-lg-3 col-md-6">
    <div class="headimg_fooh3">
        <h3>Liên hệ</h3>
        <ul class="conta">
            <li>
                <i class="fa fa-phone" aria-hidden="true"></i> 
                <a href="tel:<%=Component.Company.Branches[0].Phone %>"><%=Component.Company.Branches[0].Phone %></a>
            </li>
            <li> 
                <i class="fa fa-envelope" aria-hidden="true"></i>
                <a href="mailto:<%=Component.Company.Branches[0].Email %>"><%=Component.Company.Branches[0].Email %></a>
            </li>
        </ul>   
        <ul class="social_icon text_align_left">
        <%foreach(var item in this.Data) 
            {%>  
            <li>
                <a href="<%=item.Url%>" target="<%=item.Target%>">
                    <i class="fa fa-<%=item.Title%>" aria-hidden="true"></i>
                </a>
            </li>
            <%} %>
        </ul>   
    </div>
</div>

    