<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Category.ascx.cs" Inherits="Web.FrontEnd.Modules.Category" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider" %>

<section class="category<%=Data.Id %> section image-banner height-background-image rocket-lazyload entered lazyloaded">
    <%if(Skin.BodyBackgroundFile != null){ %>
        <style>
            .category<%=Data.Id %>{background: url('<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>')no-repeat top; background-size:cover}
        </style>
    <%} else if(!string.IsNullOrEmpty(this.Skin.BodyBackground)){%>
        <style>
            .category<%=Data.Id %>{background: <%=this.Skin.BodyBackground%> !important}
        </style>
    <%}%>
    <div class="container">
		<div class="row">
			<div class="col-md-5 col-11">	 
				<h2 class="text-left"><%=Title %></h2> 
				<div class="content-text">
					<%=Data.Content %>
				</div>
				<div class="btn-link py-3">
					<a class="btn" href="<%=HREF.LinkComponent("Articles",Data.Title.ConvertToUnSign(), true, "scat", Data.Id)%>"><%=Language["LearnMore"] %></a>
				</div>  					
			</div> 
		</div>
	</div>
</section>