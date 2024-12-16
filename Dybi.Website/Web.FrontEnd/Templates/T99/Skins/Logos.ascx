<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%for(int i = 0; i<this.Data.Count; i++) 
{%> 
    <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
        <picture>
			<source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
			<source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
            <img style="width:100%" src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
        </picture>
    <%} %>
<%} %>