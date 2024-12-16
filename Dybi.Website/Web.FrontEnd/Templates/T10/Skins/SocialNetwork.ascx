<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<nav class="navbar navbar-expand-sm navbar-dark bg-dark">
    <a class="navbar-brand" href="#" title="English" onclick="changelang('en')">
        <img alt="English" src="/Templates/T10/images/usa-flag.jpg" /></a>
    <a class="navbar-brand" href="#" title="VietNamies" onclick="changelang('vi')">
        <img alt="VietNamies" src="/Templates/T10/images/vn-flag.png" /></a>
    <ul class="mr-auto social">
        <li class="nav-item dropup">
            <a class="nav-link dropdown-toggle" href="#" id="dropdown10" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Categories</a>
            <div class="dropdown-menu" aria-labelledby="dropdown10">
                <a class="dropdown-item" href="/aboutus/truyen-cuoi-hay-moi-nhat">Về chúng tôi</a>
                <a class="dropdown-item" href="#">Funny Pictures</a>
                <a class="dropdown-item" href="<%=HREF.LinkComponent("Videos","",true) %>">Funny Videos</a>
                <a class="dropdown-item" href="/Tai-lieu-tieng-Anh">Tài liệu tiếng Anh</a>
                <a class="dropdown-item" href="/Tai-lieu-tieng-Anh#hocanhvan">Khóa học anh văn hiệu quả giá rẻ</a>
                <a class="dropdown-item" href="https://có-nên-mua.vn">Săn hàng khuyến mãi</a>
            </div>
        </li>
        <%foreach(var link in Data){ %>
            <%if (link.Title == "Facebook")
            {%>
            <li class="nav-item"><a class="nav-link" href="<%=link.Url %>" target="<%=link.Target %>" aria-label="<%=link.Title %>"><i class="fb"></i></a></li>
            <%} %>
            <%if (link.Title == "Youtube")
            {%>
            <li class="nav-item"><a class="nav-link" href="<%=link.Url %>" target="<%=link.Target %>" aria-label="<%=link.Title %>"><i class="utube"></i></a></li>
            <%} %>
        <%} %>
        <li class="nav-item"><a class="nav-link" href="/vit-rss" target="_blank" aria-label="RSS">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-rss" viewBox="0 0 16 16">
                <path d="M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z" />
                <path d="M5.5 12a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm-3-8.5a1 1 0 0 1 1-1c5.523 0 10 4.477 10 10a1 1 0 1 1-2 0 8 8 0 0 0-8-8 1 1 0 0 1-1-1zm0 4a1 1 0 0 1 1-1 6 6 0 0 1 6 6 1 1 0 1 1-2 0 4 4 0 0 0-4-4 1 1 0 0 1-1-1z" />
            </svg></a></li>
    </ul>
    <div class="form-inline">
        <div class="gcse-searchbox-only" enableautocomplete="true" data-resultsurl="/search/tim-kiem" data-queryparametername="<%=SettingsManager.Constants.SendGcseSearch.ToLower() %>"></div>
        <%--<input id="searchkey" class="form-control mr-sm-2" placeholder="Search" aria-label="Search" />
                    <button class="btn btn-outline-info my-2 my-sm-0" onclick="search();">Search</button>--%>
        <script async src="https://cse.google.com/cse.js?cx=9263f7de3323cdf20"></script>
    </div>
</nav>
