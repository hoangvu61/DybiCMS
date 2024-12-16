<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Templates/T07/style/comment.css" rel="stylesheet" />
    <script src="/Templates/T10/js/moment.min.js"></script>
    <link href="/Templates/T10/css/newpub.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Templates/T10/css/simplePagination.css">
    <link rel="stylesheet" href="/Templates/T10/css/adpia_minishop_style.css">
    <style>
	.newpub-discount-code-data-table {
        font-size:14px;
	}
	.list-event {
		border-bottom: 1px solid #dddddd !important;
		margin-top: 15px;
		overflow-y: hidden;
	}
	.tb-coupoun tbody td {
		color: black;
		vertical-align: middle !important;
		text-align: center;
	}
	.tb-coupoun tbody td.text-left {
		text-align: left;
	}
	.tb-coupoun tbody td a {
		color: black;
	}
	.dt-buttons {
		text-align: right;
	}
	.btn-sm {
		padding: 1px 3px;
	}
	
	table .input-group {
		margin-bottom: 0;
	}
	table button {
		margin-bottom: 0 !important;
		margin-right: 0 !important;
		border-radius: 0 !important;
	}
	table input {
		color: rgb(183, 183, 183) !important;
	}
	table tr td:nth-child(1) span:nth-child(2) {
		background-color: rgb(249, 104, 62); color: #fff; padding: 5px;
		position: relative;
		text-transform: uppercase;
	}
	table tr td:nth-child(1) span:nth-child(1) {
		border-top: 13px solid transparent;
		border-bottom: 13px solid transparent;
		border-right:8px solid rgb(249, 104, 62);
		width: 0;
		height: 0;
		display: inline-block;
		transform: translateY(8px);
	}
	table tr td:nth-child(1) span:nth-child(3) {
		border-top: 13px solid transparent;
		border-bottom: 13px solid transparent;
		border-left:8px solid rgb(249, 104, 62);
		width: 0;
		height: 0;
		display: inline-block;
		transform: translateY(8px);
	}
	.export-file-button-row:hover {
		background:  olivedrab !important;
	}
</style>
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <VIT:Position runat="server" ID="psContent"></VIT:Position>

</asp:Content>