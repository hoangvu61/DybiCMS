<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<section class="contact_section">
    <div class="container">
	    <h1 title="<%=Title %>"><%=Title %></h1>
        <VIT:Form ID="frmMain" runat="server">
            <input type="hidden" name="infoLable" value="Nội dung"/>
            <div style="display:none">
                Upload <asp:FileUpload ID="flu" runat="server" /><br />
            </div>
                <%if (!string.IsNullOrEmpty(Message.MessageString))
                {
                var type = Message.MessageType == "ERROR" ? "danger"
                        : Message.MessageType == "WARNING" ? "warning"
                        : "info";%>
                <div class="alert alert-<%=type %>" role="alert">
                    <h4 class="alert-heading"><%=Message.MessageString %></h4>
                </div>
                <%} %>                       
            <div class="row">
                <div class="col-12 col-md-6">
                    <%foreach(var branch in Component.Company.Branches){ %>
                        <div><%=branch.Name %>: <%=branch.Address %></div>
                    <%} %>
                    <div>Điện thoại: <a href="tel:<%=Component.Company.Branches[0].Phone %>"><%=Component.Company.Branches[0].Phone %></a></div>
                    <div>Email: <a href="mailto:<%=Component.Company.Branches[0].Email %>"><%=Component.Company.Branches[0].Email %></a></div>

                    <div class="map">
                        <%if (Component.Company.Branches.Count > 1) { %>
                        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                        <script type="text/javascript">
                            google.charts.load('current', {
                                'packages': ['map'],
                                // Note: you will need to get a mapsApiKey for your project.
                                // See: https://developers.google.com/chart/interactive/docs/basic_load_libs#load-settings
                                "mapsApiKey": "AIzaSyCZXdpCRgYzYNMLHoBnK_RcooQ8lwby_nc"
                            });
                            google.charts.setOnLoadCallback(drawMap);

                            function drawMap () {
                                var data = new google.visualization.DataTable();
                                data.addColumn('string', 'Address');
                                data.addColumn('string', 'Location');
                                data.addColumn('string', 'Marker')

                                data.addRows([
                                    <%foreach (var branch in Component.Company.Branches) { %>
                                        ['<%=branch.Address%>', '<%=branch.Name%>', ''],
                                    <%}%>
                                ]);
                                var url = 'https://icons.iconarchive.com/icons/icons-land/vista-map-markers/48/';

                                var options = {
                                    mapType: 'styledMap',
                                    zoomLevel: 8,
                                    showTooltip: true,
                                    showInfoWindow: true,
                                    useMapTypeControl: true,
                                    icons: {
                                        blue: {
                                        normal:   url + 'Map-Marker-Ball-Azure-icon.png',
                                        selected: url + 'Map-Marker-Ball-Right-Azure-icon.png'
                                        },
                                        green: {
                                        normal:   url + 'Map-Marker-Push-Pin-1-Chartreuse-icon.png',
                                        selected: url + 'Map-Marker-Push-Pin-1-Right-Chartreuse-icon.png'
                                        },
                                        pink: {
                                        normal:   url + 'Map-Marker-Ball-Pink-icon.png',
                                        selected: url + 'Map-Marker-Ball-Right-Pink-icon.png'
                                        }
                                    },
                                    maps: {
                                        // Your custom mapTypeId holding custom map styles.
                                        styledMap: {
                                            name: '<%=Component.Company.NickName%>', // This name will be displayed in the map type control.
                                            styles: [
                                                {featureType: 'poi.attraction',
                                                stylers: [{color: '#fce8b2'}]
                                                },
                                                {featureType: 'road.highway',
                                                stylers: [{hue: '#0277bd'}, {saturation: -50}]
                                                },
                                                {featureType: 'road.highway',
                                                elementType: 'labels.icon',
                                                stylers: [{hue: '#000'}, {saturation: 100}, {lightness: 50}]
                                                },
                                                {featureType: 'landscape',
                                                stylers: [{hue: '#259b24'}, {saturation: 10}, {lightness: -22}]
                                                }
                                            ]}}
                                    };
                                var map = new google.visualization.Map(document.getElementById('map_div'));

                                map.draw(data, options);
                            }
                        </script>
                        <div id="map_div" style="height: 270px; width: 100%"></div>
                    <%} else { %>
                            <div id="gmap" class="contact-map">
                                <iframe style="Width:100%;height:445px" src="//www.google.com/maps/embed/v1/search?q=<%=Component.Company.Branches[0].Address %>
                                    &zoom=16
                                    &key=AIzaSyCZXdpCRgYzYNMLHoBnK_RcooQ8lwby_nc">
                                </iframe> 
                            </div>
                        <%} %>
                    </div>   
                </div>
        
                <div class="col-12 col-md-6">
                    <h2 title="Gửi mail liên hệ">Gửi mail liên hệ</h2>	
		            <div class="form">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Họ và tên" required="required" MaxLength="300"></asp:TextBox>
                            <label>Họ và tên</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Số điện thoại" required="required" MaxLength="300"></asp:TextBox>
                            <label>Số điện thoại</label>
                        </div>
                        <div class="form-floating mb-3">
                            <textarea name="infoValue0" placeholder="Nội dung" class="form-control" style="height: 100px"></textarea>
                            <label>Nội dung</label>
                        </div>
				
                        <div class="form-group">
                            <asp:ScriptManager runat="server" ID="SptManager"></asp:ScriptManager>
                            <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional" Visible="false">
                                <ContentTemplate>
                                    <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                                        <ProgressTemplate>
                                            <%--Đang gửi...--%>
                                        </ProgressTemplate> 
                                    </asp:UpdateProgress>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Nhập mã xác nhận</span>
                                        </div> 
                                        <div class="input-group-prepend">
                                            <img id="imgCaptcha" runat="server" alt="Confirm Code" height="38"/>
                                        </div> 
                                        <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                        <div class="input-group-prepend">
                                            <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn btn-secondary" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>	
                    </div>
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn btn-light" Text="Gửi đi" />			
                </div>			
            </div>
        </VIT:Form>
    </div>
</section>