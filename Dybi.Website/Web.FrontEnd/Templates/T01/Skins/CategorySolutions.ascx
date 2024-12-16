<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="section-services layout_padding">
    <div class="container">
	    <div class="col-md-12 text-center narrow-width">
			<h2 title="<%=Title %>"><%=Title %></h2>
			    <%--<p><%=Category.Brief %></p>--%>
	    </div>
	    <div class="row text-center">
	        <%for (int i = 0; i < Data.Count; i++)
		    {%>
                <div class="col-6 col-md-4 mx-auto">
                    <div class="box">
                        <a href="<%=HREF.LinkComponent("Articles",Data[i].Title.ConvertToUnSign(), true, "scat", Data[i].Id)%>">
                            <div class="box_odd">
                                <h3 title="<%=Data[i].Title %>">
                                    <%=Data[i].Title %> 
                                </h3>
                                <p>
                                    <%=Data[i].Brief %>
                                </p>
                            </div>
                        </a>
                    </div>
                </div>
		    <%} %>
	    </div>
    </div>
</section>
