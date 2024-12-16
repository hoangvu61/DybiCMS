<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Library" %>

<section class="well2 bg-content2 mod-center">
    <div class="container">	
	<div class="mod-clear">
        <div class="mod-position1">
			<h3 class="color-2"><%=Title %></h3>
            <h4 class="offset4"><%=Category.Brief %></h4>
        </div>
    </div>
	<div class="row">
		<%foreach (var item in Data)
		{%>
		<div class="grid_4 block1 wow fadeInLeft animated" style="visibility: visible; animation-name: fadeInLeft;margin-bottom: 20px;">
            <a href="<%=HREF.LinkComponent("Video",item.Title.ConvertToUnSign(), item.Id, "smid", item.Id )%>">
                <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                    <img src="<%=HREF.DomainStore + item.Image.FullPath%><%=item.Image.FileExtension == ".webp" ? "" : ".webp" %>" alt="<%=item.Title%>"/>
				<%} %>
                <div class="block1_overlay">
					<div class="block1_city">
						<p class="p__skin"><%=item.Title%></p>
					</div>
				</div>
			</a>
        </div>
		<%} %>
	</div>
</div>
</section>

<script type="application/ld+json">
{
    "@context": "https://schema.org",
    "@type": "ItemList",
    "numberOfItems": "<%=Data.Count %>",
    "itemListElement": [
        <%for (int i = 0; i < Data.Count; i++)
		{%>
        <%= i > 0 ? "," : "" %>
        {
            "@type": "VideoObject",
            "name": "<%=Data[i].Title %>",
            "thumbnailUrl": "<%=HREF.DomainStore + Data[i].Image.FullPath%><%=Data[i].Image.FileExtension == ".webp" ? "" : ".webp" %>",
            "image": "<%=HREF.DomainStore + Data[i].Image.FullPath%><%=Data[i].Image.FileExtension == ".webp" ? "" : ".webp" %>",
            <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("uploadDate"))){ %>
            "uploadDate": "<%=Data[i].GetAttribute("uploadDate") %>",
            <%} %>
            "url": "<%=HREF.LinkComponent("Video",Data[i].Title.ConvertToUnSign(), Data[i].Id, "smid", Data[i].Id )%>"
        }
        <%} %>
    ],
    <%if(!string.IsNullOrEmpty(Category.Brief)){ %>
    "description": "<%=Category.Brief.Replace("\"","")%>"
    <%} %>
}
</script>