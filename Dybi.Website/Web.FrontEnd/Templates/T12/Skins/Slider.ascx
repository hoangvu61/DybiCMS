<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Library" %>

<div class="camera_container">
	<div id="camera" class="camera_wrap" style="display: block; height: 627px;">
		<%for(int i = 0; i< this.Data.Count; i++) 
        {%>
		    <div data-src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp"></div>			
        <%} %>		    
    </div>
		
    <div class="slogan  wow fadeIn animated" data-wow-duration="2s" style="visibility: visible; animation-duration: 2s; animation-name: fadeIn;">
        <p><%=Category.Brief %></p>
    </div>
</div>

       