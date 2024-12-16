<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
    <style>
        .list-course{width:100%}
         .list-course .course-box-slider .img-course img {
    max-height: 130px;
    overflow: hidden;
    width: 100%;
    min-height: 130px;
}
         .form-group {
    margin-bottom: 15px;
    float:left}
         .course-box-slider {
    color: #000;
    display: inline-block;
    min-height: 240px;
    max-height: 240px;
    background: #fff;
    border-radius: 5px;
    margin-bottom: 20px;
    box-shadow: 0 2px 4px rgb(0 0 0 / 8%), 0 4px 12px rgb(0 0 0 / 8%);
}
         .sale-off {
    position: absolute;
    float: left;
    background-color: #f67052;
    z-index: 1;
    color: #ffffff;
    text-align: center;
    padding: 3px;
    left: 15px;
    top: 0px;
    border-radius: 3px;
    min-width: 42px;
}
         .img-course {
    position: relative;
}
         .content-course {
    margin: 5px 7px 0 7px;
}
         .title-course {
    min-height: 42px;
    margin-top: 6px;
    max-height: 42px;
    overflow: hidden;
    width: 100%;
    line-height: 21px;
}
         .title-course span {
    font-size: 15px;
}
         .name-gv, .des-gv {
    font-size: 12px;
    color: #555;
    min-height: 18px;
}
         .list-course .course-box-slider .price-course {
    display: inline-grid;
    width: 100%;
    text-align: right;
    position: relative;
    right: 12px;
}
         .list-course .course-box-slider .price-course .price-b {
    float: right;
    font-size: 14px;
}
         .price-b {
    text-decoration: line-through;
    float: right;
    color: #888;
    font-size: 13px !important;
}
            .list-course .course-box-slider .price-course .price-a {
                font-size: 16px;
            }
            .price-a {
    font-weight: 600;
    float: right;
    color: #333;
    font-size: 17px !important;
}
            #btn-load-more {
    background-color: #f26c4f;
    color: #FFFFFF;
}
    </style>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="videos">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
        </div>

    <h2 id="hocanhvan" title="Học anh văn online ở đâu rẻ mà hiệu quả?">Học anh văn online ở đâu rẻ mà hiệu quả?</h2>
    <p class="jumbotron" style="padding:20px">Khóa học tiếng Anh online cho người mới bắt đầu, cho người mất căn bản, giúp bạn lần đầu học tiếng Anh tự tin. Nhiều khóa học với phí phù hợp và đội ngũ giáo viên giỏi dạy Tiếng Anh</p>
    <div id="courses" class="row">

    </div>
    <div class="u-number-page text-center" style="margin-bottom:20px">
                        <a href="https://unica.vn/tag/tieng-anh-giao-tiep?aff=468109&aff_sid=QTEwMDA0NzExOXwyNzEzODgwMjEwMDAwNTZ8OTk5OXwzfDA=&page=2" type="button" id="btn-load-more" class="btn" target="_blank"><i class="fa fa-spinner fa-pulse fa-1x fa-fw" style="display: none;"></i> XEM TIẾP</a>
                    </div>
    <script>
        $.ajax({
            url: 'https://unica.vn/tag/tieng-anh-giao-tiep', success: function (data) {
                $('#courses').html("<div class='list-course'>" + $(data).find('.list-course').html() + "</div>");
                $(document).ready(function () {
                    var hrefs = document.querySelectorAll('#courses .list-course a');
                    for (var i = 0; i < hrefs.length; i++) {
                        if (hrefs[i].href.includes(window.location.hostname)) hrefs[i].href = hrefs[i].href.replace(window.location.hostname, "unica.vn");
                        else hrefs[i].href = "https://unica.vn" + hrefs[i].href;
                        hrefs[i].href = hrefs[i].href + "?aff=468109&aff_sid=QTEwMDA0NzExOXwyNzEzODgwMjEwMDAwNTZ8OTk5OXwzfDA=";
                    }
                });
                }
        });
    </script>
</asp:Content>