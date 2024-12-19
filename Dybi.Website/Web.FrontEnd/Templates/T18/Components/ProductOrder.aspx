<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <link href="/Templates/T18/css/comment.css" rel="stylesheet" />

    <link href="/Templates/T18/js/jquery-flexslider/flexslider.css" rel="stylesheet" />
    <script src="/Templates/T18/js/jquery-flexslider/jquery.flexslider-min.js"></script>

    <!-- Cloud Zoom -->
    <link rel="stylesheet" href="/templates/t18/js/cloud-zoom/cloud-zoom.css">
    <script src="/templates/t18/js/cloud-zoom/zoomsl-3.0.min.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
</asp:Content>