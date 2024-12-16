<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Templates/T02/css/comment.css" rel="stylesheet" />

    <!-- FlexSlider -->
	<script defer src="/templates/T02/js/flexslider.min.js"></script>
    <link rel="stylesheet" href="/templates/T02/css/flexslider.css" type="text/css" media="screen" />

    <!-- Cloud Zoom -->
	<script src="/templates/T02/js/zoomsl-3.0.min.js"></script>

    <script>
        $(document).ready(function () {
            $(".mnuProducts").addClass("active");
        });
    </script>

    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
</asp:Content>