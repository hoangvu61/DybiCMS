<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%{ Data = Data.Where(e => e.Type == "VID").ToList(); }%>
<%if(Data.Count > 0){ %>
	<%for (int i = 0; i < Data.Count; i++)
	{%>
        <div style="height:500px;">
			<%=Data[i].Embed %>
		</div>
        <p class="post-header">
			Nếu không tải được Video. Vui lòng vào link bên dưới để xem trên youtube: 
			<a target='_blank' href='<%=Data[i].Url %>'><%=Data[i].Url %></a>
		</p>
	<%} %>
    <%if(Top > 0 && TotalItems > Top) {%>
    <div style="text-align:center; margin:20px">
    <span>Tập: </span>
    <%for(int i = 1; i <= TotalPages; i++){ %>
        <%if(i == 1){ %>        
        <a style="padding: 5px;background: <%= i == CurrentPage ? "red" : "pink" %>;border: red 1px solid;border-radius: 10px;margin: 0px 3px;" href="<%=HREF.LinkComponent("Medias", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id)%>"><%=i %></a> 
                <%} else { %>
        <a style="padding: 5px;background: <%= i == CurrentPage ? "red" : "pink" %>;border: red 1px solid;border-radius: 10px;margin: 0px 3px;" href="<%=HREF.LinkComponent("Medias", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, i)%>"><%=i %></a> 
                <%} %>
    
    <%} %>
    </div>
    <%} %>
    
    <div class="container">
        <div class="video-title">
            <h1 style="font-size: 2rem; line-height: 2.8rem; font-weight: 700;margin:10px 0px; float:left">
		        <a href="<%=HREF.LinkComponent("Video",Data[0].Title.ConvertToUnSign(), Data[0].Id, "smid", Data[0].Id)%>">
			        <%=Data[0].Title %>
		        </a>
	        </h1>
            <section class="breacrum" style="margin:15px 0px; float:right">
                <a href="/"><i class="glyphicon glyphicon-home"></i> Trang chủ </a> \ 
                <%if(Category.ParentId == null){ %>
                    <%= Category.Title %> 
                <%} else { %>
                    <a href="<%=HREF.LinkComponent(HREF.CurrentComponent, Category.ParentTitle.ConvertToUnSign(), Category.ParentId.Value, SettingsManager.Constants.SendCategory, Category.ParentId) %>"><%= Category.ParentTitle %></a> \ <%= Category.Title %>
                <%} %>     
            </section>
            <div style="clear:both"></div>
        </div>

        <div class="video">
            <strong style="margin-bottom:20px"><%=Category.Brief.Replace("\n","<br />")%></strong>
            <%if(!string.IsNullOrEmpty(Category.Content.DeleteHTMLTag().Trim())){ %>
                <div class="content">
                    <%=Category.Content %>
                </div>
                <button id="btnViewMore" class="btn--orange" type="button" onclick="ViewMore()">Xem nội dung</button>
            <%} %>
        </div>
    </div>


    <script>
        $(document).ready(function () {
            $('#btnViewMore').css('display', 'block');
            $('.video .content').css('display', 'none');
        });
        function ViewMore() {
            $('#btnViewMore').css('display', 'none');
            $('.video .content').css('display', 'block');
        }
    </script>

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

    <script type="application/ld+json">
        {
          "@context":"https://schema.org",
          "@type":"ItemList",
          "itemListElement":[
            <%for(int i = 0; i < TotalPages; i++){ %>
            <%= i > 0 ? ",":"" %>
                {
                  "@type":"ListItem",
                  "position":<%=i+1 %>,
                   <%if(i == 0){ %>
                   "url":"<%=HREF.LinkComponent("Medias", Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>"
                    <%} else { %>
                  "url":"<%=HREF.LinkComponent("Medias", Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id, SettingsManager.Constants.SendPage, i+1)%>"
                    <%} %>
                }
            <%} %>
          ],
        <%if(!string.IsNullOrEmpty(Category.ImageName)){ %>
        "image": "<%=HREF.DomainStore + Category.Image.FullPath%><%=Category.Image.FileExtension == ".webp" ? "" : ".webp" %>",
        <%} %>
        <%if(!string.IsNullOrEmpty(Category.Brief)){ %>
        "description": "<%=Category.Brief.Replace("\"","")%>"
        <%} %>
        }
    </script>

<%} %>