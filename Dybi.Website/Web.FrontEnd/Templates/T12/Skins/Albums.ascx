<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="well bg-content2"> 
            <div class="container">

                <div class="mod-clear">
                    <div class="mod-position1">
                        <h3 class="color-2"><%=Title%></h3>
                        <h4 class="offset4"><%=Category.Brief %></h4>
                    </div>
                </div>
				
                        
                <div class="row offset4" style="margin-top:90px">
				<%for (int i = 0; i < this.Data.Count; i++)
					{%>
                    <div class="grid_3 wow fadeIn animated" data-wow-duration="1s" style="visibility: visible; animation-duration: 1s; animation-name: fadeIn;">
					<a href="<%=HREF.LinkComponent("album",this.Data[i].Title.ConvertToUnSign(), this.Data[i].Id, "scat", this.Data[i].Id)%>">
					<img class="circle-img" src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp" alt="<%=this.Data[i].Title %>" /> </a>
                        <h4><a href="<%=HREF.LinkComponent("album", this.Data[i].Title.ConvertToUnSign(), this.Data[i].Id, "scat", this.Data[i].Id)%>"><%=this.Data[i].Title %></a></h4>

                        <p style="min-height: 100px;margin-bottom:20px"><%=this.Data[i].Brief %></p>
                    </div>
					<%} %>
                </div>
            </div>
        </section>
    
