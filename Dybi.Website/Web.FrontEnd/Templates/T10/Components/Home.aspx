<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
    #psTop,.breadcrumb{display:none}
</style>
    
<VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-9">
            <VIT:Position runat="server" ID="psContent"></VIT:Position>
        </div>
        <div class="col-md-3 slideright">
            <VIT:Position runat="server" ID="psRight"></VIT:Position>
        </div>
    </div>
    <div class="alert alert-info" role="alert">
		<h1 style="font-size: 14pt;margin: 5px;" title="<%=Template.Company.Slogan %>">
			<strong style="font-weight: normal;"><%=Template.Company.Brief %><strong>
		</h1>
	</div>
</asp:Content>