<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Templates.ascx.cs" Inherits="Web.FrontEnd.Modules.Templates" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

 <div class="row">
<%foreach (var template in Data)
{ %>
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 temItem">
        <div class="row">
            <div class="col-xs-12 center temImage">
                <div style='background-image:url(<%=HREF.DomainStore + template.Image.FullPath%>.webp);'></div>
            </div>
            <div class="col-xs-12 center">
                <div class="temText"> 
                    <div class="row"> 
                        <div class="col-xs-12 center">
                    <span class="temName"><%=template.TemplateName %></span>
                    <%if(template.IsPublic){ %>
                    <a class="temSelect" href="<%=HREF.LinkComponent("CreateWebsite",template.TemplateName, false, SettingsManager.Constants.SendTemplate, template.TemplateName)%>" title="<%=Language["registfree"] %>"><%=Language["select"]%></a>
                    <%} %>
                    <div class="dropdown" style="float:right; text-align:left">
                        <%if(template.Demos.Count == 1){ %>
                            <a class="dropbtn temDemo" href="https://<%=template.Demos[0]%>" title="<%=Language["viewdemo"]%> <%#Eval("TemplateName") %>" target="_blank"><%=Language["demo"]%></a>
                        <%} else { %>
                            <a class="dropbtn temDemo" href="#" title="<%=Language["viewdemo"]%> <%#Eval("TemplateName") %>"><%=Language["demo"]%></a>
                        <%} %> 
                                <ul class="dropdown-content list-group">
                                    <%foreach(var domain in template.Demos){ %>
                                        <li class="list-group-item">
                                            <a href="https://<%=domain %>" title="<%=domain %>" target="_blank">
                                                <%=domain %>
                                                <span class="badge"><%=Language["demo"] %></span>
                                            </a>
                                            <span style="clear:both"></span>
                                        </li>
                                    <%} %>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<%} %>
</div>

<script>
$(document).ready(function() {
    $(".temImage img").hover(
    function () {
        $(this).stop().animate({
            top: '0px'
        }, 'slow');
    },
    function () {
        $(this).stop().animate({
            top: '-100'
        }, 'slow');
    });
});
</script>