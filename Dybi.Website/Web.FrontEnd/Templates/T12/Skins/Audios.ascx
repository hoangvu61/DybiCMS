<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="well well__ins">
	<div class="container">
    
<h3 class="mod-center"><%=Title%></h3>
			<div class="row">
				<div class="block2 grid_4  mod-center">
				<h4 class="color-2 h4__mod"><%=Category.Description%></h4>
					<p class="p__mod1"><%=Category.URL%></p>
                    <a class="btn2 pulse" href="/audios-vit-scat-<%=Category.ID%>-album"><span>Xem tất cả</span></a>
                </div>
				<div class="fixed-player" style="display: none; position: fixed; bottom: 0px; left: 0px; right: 0px; background: transparent;">
                    <div class="audiojs " classname="audiojs" id="audiojs_wrapper0">
						<audio id="player" preload="auto" src=""></audio></div>
                </div>
					
				<div class="block2 grid_4 mg-add2">
                    <ul class="music-list" data-player-id="player">
					<%for (int i = 0; i < this.Data.Count / 2; i++)
					{%>
                        <li data-src="<%=this.Data[i].ImagePath%>"><h4 class="h4__mod" style="height:50px"><%=this.Data[i].Title %></li>
					<%} %>
                    </ul>
                </div>
				
				<div class="block2 grid_4 mg-add2">
                    <ul class="music-list" data-player-id="player">
					<%for (int i = this.Data.Count / 2; i < this.Data.Count; i++)
					{%>
                        <li data-src="<%=this.Data[i].ImagePath%>"><h4 class="h4__mod" style="height:50px"><%=this.Data[i].Title %> </li>
					<%} %>
                    </ul>
                </div>
			</div>
</div>
</section>
