<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent"  %>

<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Templates/T01/css/comment.css" rel="stylesheet" />
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>

    <script src="/Templates/T01/js/jquery-1.9.1.min.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <header class="entry-header" style="background-image: url('<%= Config.WebImage != null ? HREF.DomainStore + Config.WebImage.FullPath + (Config.WebImage.FileExtension != ".webp" ? ".webp" : "") : ""%>');">
        <div class="container">
            <h1 class="entry-title" title="<%=Page.Title %>"><%=Page.Title %></h1>
            <p class="entry-brief"><%=Page.MetaDescription %></p>
        </div>
        <!-- .entry-header-inner -->
    </header>

    <VIT:Position runat="server" ID="psContent"></VIT:Position>
</asp:Content>
