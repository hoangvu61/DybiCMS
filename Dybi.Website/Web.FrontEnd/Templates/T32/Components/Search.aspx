<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
   <VIT:Position runat="server" ID="psContent"></VIT:Position>
        <div class="container">
   <div class="gcse-searchresults-only" data-queryParameterName="sq"></div>
        </div>
</asp:Content>