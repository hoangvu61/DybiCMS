<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
        

<%for(int i = 0; i< this.Data.Count; i++) 
{%>
    <!-- hình 1920x400 5s đổi 1 lần -->
    <picture>
		<source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
		<source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
        <img class="nature" <%= i> 0? "style='display: none' " : ""%> src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" class="d-block w-100" alt="<%=Data[i].Title %>"/>
    </picture>
<%} %>		

<script>
    w3.slideshow(".nature", 10000);
</script>