<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Media.ascx.cs" Inherits="Web.FrontEnd.Modules.Media" %>

<%if (Data.Image != null)
{ %>
<a href="<%=Data.Url%>" title="<%=Title %>" target="<%=Data.Target %>">
    <picture>
        <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>.webp" type="image/webp">
        <source srcset="<%=HREF.DomainStore + Data.Image.FullPath%>" type="image/jpeg"> 
        <img src="<%=HREF.DomainStore + Data.Image.FullPath%>" alt="<%=Title %>" style="max-width:75%; width:150px"/>
    </picture>
</a>
<%} %>