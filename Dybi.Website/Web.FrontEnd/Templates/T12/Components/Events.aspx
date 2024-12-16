<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Templates/T12/css/timeline.css">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <script>
        $(document).ready(function () {
            $("#mnuevents").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <VIT:Position runat="server" ID="psContent"></VIT:Position>
    <script type="application/ld+json">
    {
        "@context": "https://schema.org",
        "@type": "BreadcrumbList",
        "itemListElement": [{
            "@type": "ListItem",
            "position": 1,
            "name": "Trang chủ",
            "item": "<%=HREF.DomainLink %>"
        },
        {
            "@type": "ListItem",
            "position": 2,
            "name": "Sự kiện",
            "item": "<%=HREF.LinkComponent("Events", "su-kien", true) %>"
        }
        ]
    }
    </script>
</asp:Content>