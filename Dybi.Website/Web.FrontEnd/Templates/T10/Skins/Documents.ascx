<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="documents">
    <h1 title="<%=Title%>"><strong> <%=Title%></strong></h1>
    <p class="jumbotron" style="padding:20px">
        <%=Category.Brief%>
        <br />
        <strong>Xem thêm hướng dẫn tải file tại:</strong> <a href="https://truyện-cười.vn/news/huong-dan-tai-file-tai-link-rut-gon-link1scom" title="Hướng dẫn tải file" target="_blank">https://truyencuoi.top/news/huong-dan-tai-file-tai-link-rut-gon-link1scom</a>
    </p>

    <div class="row">
    <%foreach (var item in this.Data)
        {%>
    <div style="max-height:<%=Skin.Height%>px;overflow: hidden;margin-bottom: 30px;" class="document col-lg-4 col-md-4 col-sm-3 col-xs-12" >
        <div class="row">
            <div class="col-lg-5 col-md-5 col-sm-5 hidden-xs-down">
                <a href="<%=HREF.LinkComponent("Document",item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendMedia,item.Id)%>" title="<%=item.Title %>">
                    <img src="<%=HREF.DomainStore + item.Image.FullPath %>" alt="<%=item.Title %>" style="width:100%;max-height:100%"/>
                </a>
            </div>
            <div class="col-md-7 col-sm-12 col-xs-12">
                <h5><a href="<%=HREF.LinkComponent("Document", item.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendMedia, item.Id)%>" title="<%=item.Title %>"><%=item.Title%></a></h5>
                <%=item.Brief.Length > 150 ? item.Brief.Substring(0, 150) + "..." : item.Brief%>
            </div>
        </div>
    </div>
    <%} %>
	<div style="max-height:<%=Skin.Height%>px;overflow: hidden;margin-bottom: 30px;" class="document col-lg-3 col-md-3 col-xs-12" >
<%--            <style>.fb_iframe_widget, .fb_iframe_widget span, .fb_iframe_widget span iframe[style] 
{
    width: 100% !important;
}</style>
        <script src="https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&amp;version=v2.5"></script>
<div class="fb-like-box" data-href="https://www.facebook.com/Sach.TaiLieu.TengAnh" data-width="" data-show-facepile="true" data-show-faces="true" data-stream="" data-header="true"></div>--%>
    </div>
        </div>
    </div>

