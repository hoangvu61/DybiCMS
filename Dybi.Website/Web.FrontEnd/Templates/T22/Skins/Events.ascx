<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Events.ascx.cs" Inherits="Web.FrontEnd.Modules.Events" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section id="Event" class="layout_padding-top layout_padding-bottom sestime">
    <div class="container">	
        <div class="heading_container">
            <h2>
                <%=Title %>
            </h2>
        </div>

        <div class="timelineframe">
            <div class="timeline">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>  
                <div class="timelinecontainer <%= i % 2 == 0 ? "left" : "right"%>">
                    <div class="content">
                        <h4 style="color:black"><%=string.Format("{0:MM/yyyy}",this.Data[i].StartDate)%></h4>
                            <h5>
                                <strong><a href="<%=HREF.LinkComponent("Event",this.Data[i].Title.ConvertToUnSign(), this.Data[i].Id, SettingsManager.Constants.SendEvent, this.Data[i].Id)%>" title="<%=this.Data[i].Title%>">
                                    <%=this.Data[i].Title%>
                                </a></strong>
                            </h5>
                            <ul class="detail_box">
                                <li><span>📍</span>: <%=this.Data[i].Place%></li>
                                <li><span>👉</span> <a href="<%=HREF.LinkComponent("Event",this.Data[i].Title.ConvertToUnSign(), this.Data[i].Id, SettingsManager.Constants.SendEvent, this.Data[i].Id)%>" title="<%=this.Data[i].Title%>"><%=Language["ViewDetail"] %></a></li>
		                    </ul>
                    </div>
                </div> 
            <%} %>
            </div>
        </div>
    </div>
</section>


                

