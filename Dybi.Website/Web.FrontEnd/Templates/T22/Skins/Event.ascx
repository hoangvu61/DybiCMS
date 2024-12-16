<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Event.ascx.cs" Inherits="Web.FrontEnd.Modules.Event" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<section class="about_section layout_padding">
<div class="container">


<div class="row" style="margin-top:40px">
	<div class="col-3">
        <%if(!string.IsNullOrEmpty(dto.ImageName)){ %>
				<img src="<%=HREF.DomainStore + dto.Image.FullPath%>.webp" alt="<%=dto.Title %>" style="width:100%"/> 
        <%} %>
        <div class="map">
            <div id="googleMap">
                    <iframe style="Width:100%;height:445px" src="//www.google.com/maps/embed/v1/search?q=<%=dto.Place %>
                        &zoom=16
                        &key=AIzaSyCZXdpCRgYzYNMLHoBnK_RcooQ8lwby_nc">
                    </iframe> 
            </div>
        </div>
        
    </div>
    <div class="col-9" style="text-align:left">   
         <div class="heading_container">
            <h1 style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
        </div>

<div class="post-header">
    <span class="post-labels tag">
        📍 <%=dto.Place%>
    </span>
</div>
        <p class="brief">
            <%=dto.Brief.Replace("\n","<br />")%>
        </p>
        <%=dto.Content%>
    </div>
</div>
    </div>
    </section>
