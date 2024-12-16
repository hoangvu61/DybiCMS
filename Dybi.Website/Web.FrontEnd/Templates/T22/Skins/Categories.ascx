<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="design_section layout_padding-top">
    <div class="container">
        <div class="heading_container">
            <h2>
            <%=Title %>
            </h2>
        </div>
        <div class="design_container layout_padding2">
        <%for (int i = 0; i < Data.Count; i++)
	    {%>
            <div class="box b-<%=i+1 %>">
            <h5>
                <a href="<%=HREF.LinkComponent(ComponentName,Data[i].Title.ConvertToUnSign(), true, "scat", Data[i].Id)%>">
                <%=Data[i].Title %>
                </a>
            </h5>
            </div>
        <%} %>
        </div>
    </div>
</section>

<!-- end design section   -->
<div>
    <hr class="section_hr" />
</div>
<!-- about section -->

<section class="about_section layout_padding-bottom">
    <div class="container">
    <div class="heading_container">
        <h2>
            <%=Category.Title %>
        </h2>
    </div>
    <div class="box layout_padding2-top layout_padding-bottom">
        <div class="detail-box">
            <%=Category.Content %>
        </div>
    </div>
    </div>
</section>
