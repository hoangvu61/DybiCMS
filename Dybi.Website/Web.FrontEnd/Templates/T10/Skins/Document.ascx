<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Media.ascx.cs" Inherits="Web.FrontEnd.Modules.Media" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<%if(this.DisplayTitle) {%>
<h1 title="<%=Data.Title%>"><%=Data.Title%></h1>
<%} %>

<div class="row">
    <div class="col-md-3 hidden-4 hidden-xs-down">
        <img src="<%=HREF.DomainStore + Data.Image.FullPath%>" alt="<%=Data.Title%>" style="width:100%"/>
    </div>
    <div class="col-md-9 hidden-8 hidden-xs-12">
        <table class="table table-success table-striped" style="width:100%;text-align:left">
            <tr>
                <td style="width:150px">Lượt xem</td>
                <td><%=Data.Views %></td>
            </tr>
            <%foreach(var attribute in Data.Attributes){ %>
                <tr>
                    <td>
                        <label><%=attribute.Name %></label>
                    </td>
                    <td>
                        <%=attribute.ValueName == "True" ? "Có" : attribute.ValueName == "False" ? "Không" : attribute.ValueName %>
                    </td>
                </tr>
            <%} %>
            <tr>
                <td>Tải về</td>
                <td><a href="<%=Data.Url %>" target="_blank">Click vào đây để tải về</a></td>
            </tr>
        </table>
        <strong><%=Data.Brief%></strong>
    </div>
    
</div> 


<%if(this.Config.Language == "en") { %>
<a href="https://link1s.com/ref/104476169309621930641" target="_blank"><img src="//link1s.com/img/refbanner/728x90-en.png" title="Shorten URLs and EARN money" style="width:100%; margin-top:20px"/></a>
<%} else { %>
<a href="https://link1s.com/ref/104476169309621930641" target="_blank"><img src="//link1s.com/img/refbanner/728x90.png" title="Rút gọn link kiếm tiền uy tín số 1 Việt Nam" style="width:100%; margin-top:20px"/></a>
<%} %>

<div style="margin-top:20px"><%=Data.Content%></div>

<script type="application/ld+json">
{
  "@context": "https://schema.org",
  "@type": "Book",
  "name": "<%=Data.Title%>",
  "description": "<%=Data.Brief%>",
    <%if(!string.IsNullOrEmpty(Data.ImageName)){ %>
    "image": "<%=HREF.DomainStore + Data.Image.FullPath%>.webp",
    <%} %> 
  <%if(!string.IsNullOrEmpty(Data.GetAttribute("author"))){ %>
    "author": "<%=Data.GetAttribute("author") %>",
<%} %>
<%if(!string.IsNullOrEmpty(Data.GetAttribute("genre"))){ %>
  "genre": "<%=Data.GetAttribute("genre") %>",
<%} %>
<%if(!string.IsNullOrEmpty(Data.GetAttribute("inLanguage"))){ %>
  "inLanguage": "<%=Data.GetAttribute("inLanguage") %>",
  <%} %>
    "bookFormat": "EBook"
}
</script>