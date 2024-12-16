<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSingle.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSingle" %>
<%@ Import Namespace="Library" %>

<div class="media">
<h1 title="<%=Data.Title %>" class="text-center"><%=Data.Title %></h1>
<div style="height:500px" id="videocontent">
	<%=Data.Content %>
</div>
<p class="post-header" style="margin: 20px 0px">
	Nếu không tải được Video. Vui lòng vào link sau để xem tại kênh youtube "<a class='youtubechannel' target='_blank'></a>": 
	<strong><a class='youtubevideo' target='_blank' href='<%=this.Data.Url %>'><%=this.Data.Url %></a></strong>
</p>
<div style="margin: 20px 0px">
<%=Data.Brief != null ? Data.Brief.Replace("\n","<br /><br />") : "" %>
</div>

<div id="goyoutube" style="display:none">
		<div class="row">
			<div class="grid_6">
				<img alt="<%=Data.Title %>" src="<%=HREF.DomainStore + Data.ImagePath%>.webp" style="max-height: 500px" /> 
			</div>
			<div class="grid_6">
                <div style="padding:50px">
					<h4>Video không thể tự tải</h4> <br />
					Vui lòng vào link bên dưới để xem tại kênh youtube "<a class='youtubechannel' target='_blank'></a>": <br/><br/>
					<strong><a class='youtubevideo' href='<%=Data.URL %>' target='_blank'></a></strong>
				</div>
			</div>
		</div>
</div>
</div>
