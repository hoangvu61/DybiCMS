<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistWebsite.ascx.cs" Inherits="Web.FrontEnd.Modules.RegistWebsite" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<h4><%=Language["personalinfo"] %></h4>
    <div class="mb-3">
        <label for="txtFirstName" class="form-label"><%=Language["firstname"] %></label>
        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Phạm"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="txtLastName" class="form-label"><%=Language["lastname"] %></label>
        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Hoàng"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="username" class="form-label"><%=Language["username"] %></label>
        <asp:TextBox ID="txtUsertName" runat="server" CssClass="form-control" placeholder="Tai-Khoan"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="txtPassword" class="form-label"><%=Language["password"] %></label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="********"></asp:TextBox>
    </div>

    <h4><%=Language["websiteinfo"] %></h4>
    <div class="mb-3">
        <label for="Domain" class="form-label"><%=Language["domain"] %></label>
        <div class="input-group">
            <span class="input-group-text" id="basic-addon1">https://</span>
            <asp:TextBox ID="txtDomain" runat="server" CssClass="form-control" placeholder="tendangnhap"></>
        </div>
    </div>
    <div class="mb-3">
        <label for="FullName" class="form-label"><%=Language["websitename"] %></label>
        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Cửa hàng tiện lợi online"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="Phone" class="form-label"><%=Language["phone"] %></label>
        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="0987xxxxxx"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="Email" class="form-label">Email</label>
        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="Email@email.email"></asp:TextBox>
        <ValidationMessage For="()=> Request.Email"></ValidationMessage>
    </div>

<asp:Button ID="btnCreate" runat="server" CssClass="btn btn-primary" OnClick="btnCreate_Click" Text="Đăng ký"/>