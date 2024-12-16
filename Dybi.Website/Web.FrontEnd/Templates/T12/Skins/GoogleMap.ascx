<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/GoogleMap.ascx.cs" Inherits="Web.FrontEnd.Modules.GoogleMap" %>
<div class="grid_4 center"> 
<h3 class="left mod-center" style="margin-bottom:20px"><%=Title%></h3>
	<div id="gmap" class="contact-map">
				<iframe title="Bản đồ" style="Width:100%;" height="540" src="//www.google.com/maps/embed/v1/search?q=<%=Address%>
			  &zoom=<%= Zoom %>
			  &key=<%= GoogleAPIKey%>">
		  </iframe> 
	</div>
</div>
