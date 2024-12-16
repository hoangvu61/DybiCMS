<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleMap.ascx.cs" Inherits="Web.FrontEnd.Modules.GoogleMap" %>
<%if (Component.Company.Branches.Count > 1) { %>
<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
<script type="text/javascript">
    google.charts.load('current', {
        'packages': ['map'],
        // Note: you will need to get a mapsApiKey for your project.
        // See: https://developers.google.com/chart/interactive/docs/basic_load_libs#load-settings
        "mapsApiKey": "<%= GoogleAPIKey%>"
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
            zoomLevel: <%= Zoom > 0 ? Zoom : 6 %>,
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
                    name: '<%=Component.Company.DisplayName%>', // This name will be displayed in the map type control.
                    styles: [
                        {
                            featureType: 'poi.attraction',
                            stylers: [{ color: '#fce8b2' }]
                        },
                        {
                            featureType: 'road.highway',
                            stylers: [{ hue: '#0277bd' }, { saturation: -50 }]
                        },
                        {
                            featureType: 'road.highway',
                            elementType: 'labels.icon',
                            stylers: [{ hue: '#000' }, { saturation: 100 }, { lightness: 50 }]
                        },
                        {
                            featureType: 'landscape',
                            stylers: [{ hue: '#259b24' }, { saturation: 10 }, { lightness: -22 }]
                        }
                    ]
                }
            }
            };
        var map = new google.visualization.Map(document.getElementById('map_div'));

        map.draw(data, options);
    }
</script>
<div id="map_div" style="height: 500px; width: 100%"></div>
<%} else { %>
<div id="gmap" class="contact-map">
      <iframe style="Width:100%" src="//www.google.com/maps/embed/v1/search?q=<%=Address%>
          &zoom=<%= Zoom %>
          &key=<%= GoogleAPIKey%>">
      </iframe> 
</div>
<%} %>