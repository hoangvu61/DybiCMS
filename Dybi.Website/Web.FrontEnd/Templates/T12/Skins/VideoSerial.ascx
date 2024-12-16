<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Library"%>

 <div class="container" style="margin-top:-120px;<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>" >	
	<div class="row">
		<%foreach (var cat in Data) {%>
		<div class="grid_4 block1 wow fadeInLeft animated" style="visibility: visible; animation-name: fadeInLeft;margin-bottom: 20px;">
            <a href="<%=HREF.LinkComponent("medias", cat.Title.ConvertToUnSign(), cat.Id, "scat", cat.Id)%>">
                <img src="<%=HREF.DomainStore + cat.Image.FullPath%>.webp" alt="<%=cat.Title%>"/>
				<div class="block1_overlay">
					<div class="block1_city">
						<p class="p__skin"><%=cat.Title%></p>
					</div>
				</div>
			</a>
        </div>
		<%} %>
	</div>
</div>