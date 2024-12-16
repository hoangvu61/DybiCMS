<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="section-clients pt-0">
	<div class="col-md-12 text-center narrow-width">
		<h2><%=Title %></h2>
		<p><%=Category.Brief %></p>
	</div>
	<div class="row text-center">
	    <%for (int i = 0; i < Data.Count; i++)
		{%>
            <div class="col-6 col-md-3">
                <div class="category">
                    <h3>
                        <a href="<%=HREF.LinkComponent("Medias",Data[i].Title.ConvertToUnSign(), true, "scat", Data[i].Id)%>">
                            <%=Data[i].Title %>
                        </a>
                    </h3>
                    <p>
                        <%=Data[i].Brief %>
                    </p>
                </div>
            </div>
		<%} %>
	</div>
</section>