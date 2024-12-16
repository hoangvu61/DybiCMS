<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Events.ascx.cs" Inherits="Web.FrontEnd.Modules.Events" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="well2 mod-center sestime">
    <div class="container">	
        <%if(HREF.CurrentComponent == "home"){ %>
		<h3 class="timelinetitle"><%=Title %></h3>
        <%} else { %>
        <h1 class="timelinetitle"><%=Title %></h1>
        <%} %>

        <div class="timelineframe">
            <div class="timeline">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>  
                <div class="timelinecontainer <%= i % 2 == 0 ? "left" : "right"%>">
                    <%if(this.Data[i].StartDate.Date < System.DateTime.Now.Date){ %>
                    <div class="content" style="color: #ccc">
                        <h4 style="color:#ccc"><%=string.Format("{0:dd/MM/yyyy}",this.Data[i].StartDate)%></h4>
                        <p> 
                            <h5>
                                <strong><a href="<%=HREF.LinkComponent("Event",this.Data[i].Title.ConvertToUnSign(), this.Data[i].Id, SettingsManager.Constants.SendEvent, this.Data[i].Id)%>" title="<%=this.Data[i].Title%>">
                                    <%=this.Data[i].Title%>
                                </a></strong>
                            </h5>
                            <ul class="detail_box">
                                <li><span>🕓</span>: <%=string.Format("{0:HH:mm}",this.Data[i].StartDate)%></li>
                                <li><span>📍</span>: <%=this.Data[i].Place%></li>
		                    </ul>
		                </p>
                    </div>
                    <%} else if(this.Data[i].StartDate.Date == System.DateTime.Now.Date) { %>
                    <div class="content" style="color:blue">
                        <h4 style="color:blue"><%=string.Format("{0:dd/MM/yyyy}",this.Data[i].StartDate)%></h4>
                        <p> 
                            <h5>
                                <strong><a href="<%=HREF.LinkComponent("Event",this.Data[i].Title.ConvertToUnSign(), this.Data[i].Id, SettingsManager.Constants.SendEvent, this.Data[i].Id)%>" title="<%=this.Data[i].Title%>">
                                    <%=this.Data[i].Title%>
                                </a></strong>
                            </h5>
			                <ul class="detail_box">
                                <li><span>🕓</span>: <%=string.Format("{0:HH:mm}",this.Data[i].StartDate)%></li>
                                <li><span>📍</span>: <%=this.Data[i].Place%></li>
                                <li><span>👉</span> <a href="<%=HREF.LinkComponent("Event",this.Data[i].Title.ConvertToUnSign(), this.Data[i].Id, SettingsManager.Constants.SendEvent, this.Data[i].Id)%>" title="<%=this.Data[i].Title%>">Xem chi tiết</a></li>
		                    </ul>
		                </p>
                    </div>
                    <%} else {%>
                    <div class="content" style="color:black">
                        <h4 style="color:black"><%=string.Format("{0:dd/MM/yyyy}",this.Data[i].StartDate)%></h4>
                            <h5>
                                <strong><a href="<%=HREF.LinkComponent("Event",this.Data[i].Title.ConvertToUnSign(), this.Data[i].Id, SettingsManager.Constants.SendEvent, this.Data[i].Id)%>" title="<%=this.Data[i].Title%>">
                                    <%=this.Data[i].Title%>
                                </a></strong>
                            </h5>
                            <ul class="detail_box">
                                <li><span>🕓</span>: <%=string.Format("{0:HH:mm}",this.Data[i].StartDate)%></li>
                                <li><span>📍</span>: <%=this.Data[i].Place%></li>
                                <li><span>👉</span> <a href="<%=HREF.LinkComponent("Event",this.Data[i].Title.ConvertToUnSign(), this.Data[i].Id, SettingsManager.Constants.SendEvent, this.Data[i].Id)%>" title="<%=this.Data[i].Title%>">Xem chi tiết</a></li>
		                    </ul>
                    </div>
                    <%} %>
                </div> 
            <%} %>
            </div>
        </div>
    </div>
</section>


                

