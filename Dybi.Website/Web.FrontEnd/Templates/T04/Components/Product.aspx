<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="/Templates/T04/data/comment.css" rel="stylesheet" />

    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>

</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contain detail-none">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
    </div>
</asp:Content>