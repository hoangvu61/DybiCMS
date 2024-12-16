<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%if(HREF.CurrentComponent == "home"){ %>
    <section class="well2 bg-content2 mod-center">
<%} else { %>
    <section class="well3 bg-content2 mod-center">
<%} %>
    <div class="container">	
        <%if(HREF.CurrentComponent != "home"){ %>
            <div class="breacrum">
                <a href="/"><i class="glyphicon glyphicon-home"></i> Trang chủ </a> \ 
                <%if(Category.ParentId == null){ %>
                    <%= Category.Title %> 
                <%} else { %>
                    <a href="<%=HREF.LinkComponent(HREF.CurrentComponent, Category.ParentTitle.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.ParentId) %>"><%= Category.ParentTitle %></a> \ <%= Category.Title %>
                <%} %>       
            </div>
        <%} %>
	<div class="mod-clear">
        <div class="mod-position1">
            <%if(HREF.CurrentComponent == "home"){ %>
			<h3 class="color-2"><%=Title %></h3>
            <%} else { %>
            <h1 class="color-2"><%=Title %></h1>
            <%} %>
            <h4 class="offset4"><%=Category.Brief %></h4>
        </div>
    </div>
	<div class="row">
		<%foreach (var item in Data)
		{%>
		<div class="grid_6 block1 wow fadeInLeft animated" style="visibility: visible; animation-name: fadeInLeft;margin-bottom: 20px;">
            <div style="height:350px;">
			<%=item.Embed %>
			</div>
					<div style="width: 100%;text-align: center;padding: 20px;background: #ccc;">
						<p class="p__skin">
                            <a href="<%=HREF.LinkComponent("Video",item.Title.ConvertToUnSign(), true, "smid", item.Id )%>">
                            <%=item.Title%>
                            </a>
						</p>
					</div>
			<p class="post-header">
				Nếu không tải được Video. Vui lòng vào link bên dưới để xem trên youtube: 
				<a target='_blank' href='<%=item.Url %>'><%=item.Url %></a>
			</p>
                
        </div>
		<%} %>
	</div>

    <div style="text-align:center; margin:20px">
    <%if(Top > 0 && TotalItems > Top) {%>
            <%if(Top < 7) {%>
                <a href="<%=HREF.LinkComponent("TV", Title.ConvertToUnSign(), true) %>" style="background:<%=Skin.HeaderBackground%>;color:<%=Skin.HeaderFontColor%>;border-radius:15px;padding:15px">
                  Xem tất cả video
                </a>
            <%} else {%>
                <a href="<%=HREF.LinkComponent("TV", Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, 1)%>">Trang đầu</a> 
                <%for(int i = 1; i <= TotalPages; i++){ %>
                <a style="padding: 5px;background: <%= i == CurrentPage ? "red" : "pink" %>;border: red 1px solid;border-radius: 10px;margin: 0px 3px;" href="<%=HREF.LinkComponent("TV", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, i)%>"><%=i %></a> 
                <%} %>
                <a href="<%=HREF.LinkComponent("TV", Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, TotalPages)%>">Trang cuối</a> 
            <%} %>
    <%} %>
</div>
</div>
</section>

<script type="application/ld+json">
    {
        "@context": "https://schema.org",
        "@type": "BreadcrumbList",
        "itemListElement": [{
            "@type": "ListItem",
            "position": 1,
            "name": "Trang chủ",
            "item": "<%=HREF.DomainLink %>"
        },
            
        <%if(Category.ParentId == null){ %>
        {
            "@type": "ListItem",
            "position": 2,
            "name": "<%=Category.Title%>"
        }
        <%} else {%>
        {
            "@type": "ListItem",
            "position": 2,
            "name": "<%=Category.ParentTitle%>",
            "item": "<%=HREF.LinkComponent(HREF.CurrentComponent, Category.ParentTitle.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.ParentId ) %>"
        },
        {
            "@type": "ListItem",
            "position": 3,
            "name": "<%=Category.Title%>"
        }   
        <%} %>
        ]
    }
</script>

<%var series = Data.Select(o => o.GetAttribute("TVSeries")).Distinct().Where(o => !string.IsNullOrEmpty(o)).ToList();
foreach(var serial in series){ %>
    <script type="application/ld+json">
    {
    "@context": "https://schema.org",
    "@type": "TVSeries",
    "actor": {
        "@type": "Person",
        "name": "<%=Component.Company.DisplayName %>"
    },
    "name": "<%=serial %>",
    "containsSeason": [
        <%
        var i = 0;
        foreach(var item in Data){ 
        if (item.GetAttribute("TVSeries") != serial) continue;
        %>
        <%= i > 0 ? "," : "" %>
        {
            "@type": "TVSeason",
            "datePublished": "<%=item.GetAttribute("uploadDate") %>",
            "name": "<%=item.Title%>",
            "numberOfEpisodes": "<%=item.GetAttribute("numberOfEpisodes") %>"
        }
        <% i++; } %>
        ]
    }
    </script>
<%} %>