<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>


<%{ Data = Data.Where(e => e.Type == "IMG").ToList(); }%>
<%if(Data.Count > 0){ %>
    <section class="breacrum">
        <a href="/"><i class="glyphicon glyphicon-home"></i> Trang chủ </a> \ 
        <%if(Category.ParentId == null){ %>
            <%= Category.Title %> 
        <%} else { %>
            <a href="<%=HREF.LinkComponent("Album", "hinh-anh", true) %>">Hình ảnh</a> \ <%= Category.Title %>
        <%} %>       
    </section>

	<h1 class="mod-center"><%=Category.Title%></h1>
    <p><%=Category.Brief%></p>
	<div class="row">
        <div class="column">
		<%for (int i = 0; i < Data.Count; i++)
		{%>
			<a class="thumb" data-fancybox-group="1" href="<%=HREF.DomainStore + Data[i].Image.FullPath %>.webp">
				<img src="<%=HREF.DomainStore + Data[i].Image.FullPath %>.webp" alt="<%=Data[i].Title %>">
			</a>
			<%if((i+1) % 6 == 0){%>
			</div><div class="column">
			<%} %>
		<%} %>
		<%if(Data.Count % 6 != 1){%>
		</div>
		<%} %>
    </div>

    <%if(Top > 0 && TotalItems > Top) {%>
    <div style="text-align:center; margin:20px">
        <a href="<%=HREF.LinkComponent("Album", Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, 1)%>">Trang đầu</a> 
        <%for(int i = 1; i <= TotalPages; i++){ %>
        <a style="padding: 5px;background: <%= i == CurrentPage ? "red" : "pink" %>;border: red 1px solid;border-radius: 10px;margin: 0px 3px;" href="<%=HREF.LinkComponent("Album", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, i)%>"><%=i %></a> 
        <%} %>
        <a href="<%=HREF.LinkComponent("Album", Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, TotalPages)%>">Trang cuối</a> 
    </div>
    <%} %>

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
<%} %>

