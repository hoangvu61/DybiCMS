<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Web.FrontEnd.Error" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider" %>

<!DOCTYPE>
<html lang="<%=Config.Language %>">
<head>
	<title>Thông báo</title>
    <meta name="robots" content="noindex, nofollow"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>

    <!-- Latest compiled and minified JavaScript -->
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="Includes/Error.css" type="text/css" />
</head>
<body>
     <%if(!string.IsNullOrEmpty(Config.Background)){%>
        <style>
            .navbar, .about_section, .btn{background:<%= Config.Background%>}
        </style>
    <%} %>
    <%if(!string.IsNullOrEmpty(Config.FontColor)){%>
        <style>
            .navbar-default .navbar-brand, .navbar-default .navbar-brand:hover ,.navbar-brand a,.navbar-brand a:hover,.navbar-brand a:active,
            .navbar-default .navbar-nav>li>a,.navbar-default .navbar-nav>li>a:hover,.navbar-default .navbar-nav>li>a:active, 
            .about_section,.btn{color:<%= Config.FontColor%> !important}
        </style>
    <%} %>
    <!-- Fixed navbar -->
    <nav class="navbar navbar-default navbar-fixed-top">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="/" title="<%=this.Company.FullName %>">
              <img src="<%=HREF.DomainStore + this.Company.Image.FullPath %>" alt="<%=this.Company.FullName %>" style="height:50px"/>
          </a>
          <ul class="navbar-brand" style="font-size: 14px;padding: 5px 0px 0px 15px; list-style-type:none; margin:0px">
            <li><span class="hidden-sm hidden-xs">Holine:</span> <a href="tel:<%=this.Company.Branches[0].Phone %>"><%=this.Company.Branches[0].Phone %></a></li>
            <li><span class="hidden-sm hidden-xs">Email:</span> <a href="mailto:<%=this.Company.Branches[0].Email %>"><%=this.Company.Branches[0].Email %></a></li>
          </ul>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <ul class="nav navbar-nav navbar-right">
            <li id="mnuHome"><a href="/">Trang chủ</a></li>
                <%foreach (var item in this.Menu)
				{%>
                    <%if(item.Children != null && item.Children.Count > 0) {%>
                        <li class="dropdown">
                            <a href="<%=item.Url%>" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><%=item.Title %> <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <%foreach (var child in item.Children)
						        {%>
                                    <li><a href="<%=child.Url%>"><%=child.Title%></a></li>
						        <%} %>
                            </ul>
                        </li>
                      <%} else { %>
                        <li>
                            <a href="<%=item.Url%>"><%=item.Title%></a>
                        </li>
                      <%} %> 
                  <%} %>
                        
          </ul>
        </div><!--/.nav-collapse -->
      </div>
    </nav>

	<div align="center" style="padding:100px">
			    <h1>
                    Thông báo
                </h1>
        <h3>Xảy ra sự cố trong quá trình xử lý dữ liệu</h3>
        <div class="alert alert-danger" role="alert">
            <%=Message %>
        </div>
        <div class="alert alert-info" role="alert">
            Sự cố này có thể là vì địa chỉ không đúng.
        </div>
			    <h3>Xin hãy thử một trong những cách sau</h3>
			    <a class="btn btn-primary btn-lg" href="javascript:history.back();">Quay lại trang trước</a>
				<a class="btn btn-primary btn-lg" href="/" title="Đến trang chủ">Về trang chủ</a>
        <br /><br />
        <%foreach (var item in this.Menu)
		{%>
            <%if(item.Children != null && item.Children.Count > 0) {%>
                <a href="<%=item.Url%>" class="btn btn-primary"><%=item.Title %></a>
                <%foreach (var child in item.Children)
				{%>
                    <a class="btn btn-primary btn-sm" href="<%=child.Url%>"><%=child.Title%></a>
				<%} %>
            <%} else { %>
                <a class="btn btn-primary btn-sm" href="<%=item.Url%>"><%=item.Title%></a>
            <%} %> 
        <%} %>
    </div>


    <section class="about_section">
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-5">
          <div class="img-box">
              <%if (!string.IsNullOrEmpty(Config.WebImage.FullPath))
              {%>
                <picture>
	                <source srcset="<%=HREF.DomainStore + Config.WebImage.FullPath%>.webp" type="image/webp">
	                <source srcset="<%=HREF.DomainStore + Config.WebImage.FullPath%>" type="image/jpeg"> 
                    <img src="<%=HREF.DomainStore + Config.WebImage.FullPath%>" alt="<%=Company.FullName %>"/>
                </picture> 
              <%}
                else
                { %>
                    <img src="<%=HREF.DomainStore + this.Company.Image.FullPath %>" alt="<%=this.Company.FullName %>"/>
                <%} %>
          </div>
        </div>
        <div class="col-md-7 detail_container">
          <div class="detail-box">
            <div class="heading_container">
              <h2>
                <%=Company.DisplayName %>
              </h2>
            </div>
            <p>
                <%=Company.Brief.Replace("\n","<br />") %>
            </p>
          </div>
        </div>
      </div>
    </div>
  </section>
    <footer class="footer2">
        <div class="container">
            © Copyright <%=String.Format("{0:yyyy}", Company.CreateDate)%> <a href="http://<%=HREF.Domain %>"><%=HREF.Domain %></a> - 
            Develop by <a target="_blank" href="<%=SettingsManager.AppSettings.DomainPublic %>" class="link"><%=SettingsManager.AppSettings.Copyright %></a>
        </div>
    </footer>
</body>
</html>
