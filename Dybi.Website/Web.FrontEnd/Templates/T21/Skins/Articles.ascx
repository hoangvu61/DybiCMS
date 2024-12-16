<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- section choose start -->
    <div class="choose_section layout_padding">
    	<div class="container">
    		<h1 class="choose_text"><span style="color: #54e4ba;"><%=Title %></h1>
    		 <p class="long_text">
                 <%=Category.Brief %>
     		</div>
     	</div>

 <div class="container">
     	<div class="choose_section_2">
     		<div class="row">
            <%foreach(var item in this.Data) 
            {%>  
                <div class="col-sm-4">
     			<div class="telephonr_icon">
                    <%if(!string.IsNullOrEmpty(item.ImageName)){ %>
                        <picture>
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + item.Image.FullPath%>" alt="<%=item.Title %>"/>
                        </picture>
                    <%} %>
     			</div>
     			<h2 class="emergency_text"><%=item.Title%></h2>
     			<p class="long_text_2"><%=item.Brief %></p>
     		</div>
            <%} %>
     		</div>
     	</div>
     </div>
<!-- section choose end -->
