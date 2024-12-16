<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/RegistWebsite.ascx.cs" Inherits="Web.FrontEnd.Modules.RegistWebsite" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class="row regist">
    <%if(Message.IsEmpty){ %>
<h3>Đăng ký ngay giao diện <%=TemplateName.ToUpper() %></h3>
<div>
    <div class="col-md-6 center">
        <%if(!string.IsNullOrEmpty(Template.ImageName)){ %>
        <picture>
			<source srcset="<%=HREF.DomainStore + Template.Image.FullPath%>.webp" type="image/webp">
			<source srcset="<%=HREF.DomainStore + Template.Image.FullPath%>" type="image/jpeg"> 
            <img src="<%=HREF.DomainStore +  Template.Image.FullPath%>" alt="<%=Template.TemplateName %>"/>
        </picture>
        <%} %>
    </div>
    <div class="col-md-6">
        <VIT:Form ID="frmMain" runat="server">
            <table width="100%">
                <tbody>
                   <tr>
                        <th colspan="2"><%=Language["personalinfo"] %></th>
                    </tr>
                    <tr>
                        <td><%=Language["firstname"] %></td>
                        <td><asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Phạm"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><%=Language["lastname"] %></td>
                        <td><asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Hoàng"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><%=Language["username"] %></td>
                        <td><asp:TextBox ID="txtUsertName" runat="server" CssClass="form-control" placeholder="Tai-Khoan"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><%=Language["password"] %></td>
                        <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="********"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th colspan="2"><%=Language["websiteinfo"] %></th>
                    </tr>
                    <tr>
                        <td><%=Language["domain"] %></td>
                        <td class="input-group" style="100%">
                            <span class="input-group-addon" id="basic-addon1">https://</span>
                            <asp:TextBox ID="txtDomain" runat="server" CssClass="form-control" placeholder="tendangnhap"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><%=Language["websitename"] %></td>
                        <td><asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Cửa hàng tiện lợi online"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><%=Language["phone"] %></td>
                        <td><asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="0987xxxxxx"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Email</td>
                        <td><asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="Email@email.email"></asp:TextBox></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnCreate" runat="server" CssClass="btn btn-primary" OnClick="btnCreate_Click" Text="Đăng ký"/>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </VIT:Form>
    </div>
</div>
    <%} else { %>
    <div class="alert alert-success" role="alert">
        <%=Message.MessageString %>
    </div>
    <%} %>
</section>