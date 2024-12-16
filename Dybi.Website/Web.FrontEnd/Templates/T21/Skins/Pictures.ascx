<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- section Professional start -->
<div class="Professional_section layout_padding">
    <div class="container">
    	<h2 class="professional_text"><%=Title %></h2>
        <h2 class="our_text"><%=Category.Title %> </h2>
        <p class="long_text"><%=Category.Brief %></p>
        <div class="Professional_section_2">
            <div class="row">
                <%{ Data = Data.Where(e => e.Type == "IMG").ToList(); }%>
			    <%for (int i = 0; i < Data.Count; i++)
			    {%>
                    <div class="col-sm-4">
            		    <div class="images"><img src="<%=HREF.DomainStore + Data[i].Image.FullPath %>.webp" alt="<%=Data[i].Title %>"></div>
            		    <h2 class="design_text"><%=Data[i].Title %></h2>
            	    </div>
			    <%} %>
            </div>
        </div>
    </div>
</div>
<!-- section Professional end -->


