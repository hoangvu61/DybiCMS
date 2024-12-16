<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="section_feature"> 
    <%if(!string.IsNullOrEmpty(this.Skin.BodyBackground)){%>
        <style>
            .section_feature{background: <%=this.Skin.BodyBackground%> !important}
        </style>
    <%}%>
    <%if(!string.IsNullOrEmpty(this.Skin.BodyFontColor)){%>
        <style>
            .section_feature{color: <%=this.Skin.BodyFontColor%> !important}
        </style>
    <%}%>
	<div class="container"> 
		<div class="row">
			<div class="blue-pattern-background">  						
				<div class="col-md-12">
					<div class="row"> 
						<div class="col-md-12 text-center mb-4">
							<h2 title="<%=Title %>"><%=Title %></h2>
                            <p><%=Category.Brief %></p>
						</div>
                        <%for(int i = 0; i<this.Data.Count; i++) 
                        {%> 
                            <%if(!string.IsNullOrEmpty(Data[i].ImageName)){ %>
                            <div class="col-md-4 col-6 mx-auto icon-image-content">  
							    <picture>
			                        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp" type="image/webp">
			                        <source srcset="<%=HREF.DomainStore + Data[i].Image.FullPath%>" type="image/jpeg"> 
                                    <img style="width:100%" src="<%=HREF.DomainStore + Data[i].Image.FullPath%>" alt="<%=Data[i].Title %>"/>
                                </picture>
                                <div>
							        <strong><%= Data[i].Title%></strong>
                                </div>
                                <p class="mb-0"><%= Data[i].Brief%></p>
						    </div>
                            <%} %>
                        <%} %>
						
					</div>
				</div> 
			</div>
		</div>
	</div>  
</section>
