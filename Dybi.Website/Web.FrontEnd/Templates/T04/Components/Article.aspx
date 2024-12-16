<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Templates/T04/data/comment.css" rel="stylesheet" />
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <VIT:Position runat="server" ID="psTop"></VIT:Position>
    <div class="w3-container">
        <div class="w3-row">
            <div class="w3-quarter w3-hide-small">  
                <VIT:Position runat="server" ID="psLeft"></VIT:Position>
            </div>
            <div class="w3-threequarter"> 
                <VIT:Position runat="server" ID="psContent"></VIT:Position> 
            </div>
        </div>
    </div>
</asp:Content>