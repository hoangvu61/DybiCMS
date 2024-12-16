<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleBilinguals.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleBilinguals" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="articles">
  <%if(this.Data.Count > 0) { %>
<%for(int i = 0; i< this.Data.Count; i++) 
  {%> 
  <hr />
  <div id="article<%=this.Data[i].Id %>">
  <h2 class="bd-title" title="<%=this.Data[i].Details[0].Title %>"><a href="<%=HREF.LinkComponent("article",this.Data[i].Details[0].Title.ConvertToUnSign(), true, "sart", this.Data[i].Id)%>"><%=this.Data[i].Details[0].Title %></a></h2>
	<%if (!string.IsNullOrEmpty(Data[i].ImageName))
        { %>
    <a href="<%=HREF.LinkComponent("article",this.Data[i].Details[0].Title.ConvertToUnSign(), true, "sart", this.Data[i].Id)%>" title="<%=this.Data[i].Details[0].Title %>">
        <img alt="<%=this.Data[i].Details[0].Title %>" src="<%=HREF.DomainStore + Data[i].Image.FullPath %>" style="width:250px; float:left; margin: 0px 20px 20px 0px"/>
    </a>
    <%} %>
      <div class="articlecontent1"><%=this.Data[i].Details[0].Content %></div>
<button type="button" class="btn btn-secondary" onclick="$('#Translate<%=this.Data[i].Details[0].Id %>').toggle(500);"><%=TextTranslate %></button>
  <div class="jumbotron mt-3" id="Translate<%=this.Data[i].Id %>" style="display:none">
      <%if (this.Data[i].Details.Count > 1){ %>
    <h3 title="<%=this.Data[i].Details[1].Title %>"><%=this.Data[i].Details[1].Title %></h3>
    <div class="articlecontent2">
        <%=this.Data[i].Details[1].Content %>
	</div>
      <div style="text-align:right; margin-top:20px">
	        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#sendTranslate" onclick="SetData('<%=this.Data[i].Id %>', 1)"><%=TextFixTranslationYet %></button>
	    </div>
      <%} else { %>
      <div><%=TextNoTranslationYet %></div>
        <div style="text-align:right; margin-top:20px">
	        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#sendTranslate" onclick="SetData('<%=this.Data[i].Id %>', 0)"><%=TextSendTranslationYet %></button>
	    </div>
	
      <%} %>
  </div>
      </div>
  <%} %>

<div id="LoadErea"></div>

<input type="hidden" value="<%= Total%>" id="articlesTotal"/>
<input type="hidden" value="<%= this.Top%>" id="articlesCurrent"/>

<div style="margin: 40px;display:block; text-align:center" id="btnLoad">
  <button type="button" class="btn btn-primary" onclick="LoadMore();"><%=Language["LoadMore"] %></button>
</div>
 <%} %>

<!-- Modal -->
<div class="modal fade" id="sendTranslate" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel"><%=TextTranslate %></h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
      <input type="hidden" id="articleid"/>
      <h3></h3>
      <div class="articlecontent"></div>
          <hr style="margin:15px 0px"/>
       <div class="form-group row">
    <label for="sendName" class="col-sm-2 col-form-label"><%=Language["YourName"] %></label>
    <div class="col-sm-10">
      <input class="form-control" id="sendName" placeholder="<%=Language["YourName"] %>" required="required">
    </div>
  </div>
      <textarea id="w3review" name="w3review" rows="10" style="width:100%"> </textarea>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal"><%=Language["Close"] %></button>
        <button type="button" class="btn btn-primary" onclick="SendTranlation()"><%=Language["Send"] %></button>
      </div>
    </div>
  </div>
</div>
</div>
<script src='<%=HREF.BaseUrl%>Includes/bbeditor/jquery.bbcode.js' type='text/javascript'></script>
<script>
    checkload();
    function checkload()
    {
        $("#sendTranslate #w3review").bbcode({ tag_bold: true, tag_italic: true, tag_underline: true, tag_link: true, tag_image: true, button_image: true });
        $(document).ready(function () {
            var total = $("#articlesTotal").val();
            var current = $("#articlesCurrent").val();
            if (parseInt(current, 10) >= parseInt(total, 10)) $("#btnLoad").hide();
        });
    }
</script>
<script>
    function SetData(id, type)
    {
        var title1 = $("#article" + id + " h2 a").html();
        var content1 = $("#article" + id + " .articlecontent1").html();
        $("#sendTranslate h3").html(title1);
        $("#sendTranslate .articlecontent").html(content1);
        $("#sendTranslate #articleid").val(id);
        //if(type == 1)
        //{
            var title2 = $("#article" + id + " h3").html();
            var content2 = $("#article" + id + " .articlecontent2").html();
            //$("#sendTranslate #w3review").html(title2 + "<br/>" + content2);
        //}
    }

    function LoadMore()
    {
        var total = $("#articlesTotal").val();
        var currentbefore = $("#articlesCurrent").val();
		var key = '<%=this.GetRequestThenParam<string>(SettingsManager.Constants.SendTag, "Tag")%>';
		var orderby = '<%=this.GetRequestThenParam<string>("sort", "OrderBy")%>';
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/LoadMoreArticleBilinguals",
            data: JSON.stringify({key: '<%= Session.SessionID%>', cat: '<%= CategoryId%>', skip: currentbefore, take: <%= this.Top%>, order: orderby, tag: key}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    var currentafter = parseInt(currentbefore, 10) + <%= this.Top%>;
                    $("#articlesCurrent").val(currentafter); 
                    if(currentafter >=  parseInt(total, 10)) $("#btnLoad").hide();

                    var content = "";
                    for (var i = 0; i < data.d.length; i++) {
                        content +="<hr />";
                        content +="<div id='"+data.d[i].Id+"'>";
                        content +="<h2 class='bd-title' title='" + data.d[i].Details[0].Title + "'><a href='/article/" + data.d[i].Details[0].TitleUnSign + "/sart/" + data.d[i].Id + "' title='" + data.d[i].Details[0].Title +"'>"+data.d[i].Details[0].Title+"</a></h2>";
                        if (data.d[i].ImagePath)
                        {
                            content +="<a href='/article/" + data.d[i].Details[0].TitleUnSign + "/sart/" + data.d[i].Id + "' title='" + data.d[i].Details[0].Title +"'>";
                            content +="<img alt='" + data.d[i].Details[0].Title +"' src='<%=HREF.DomainStore%>"+data.d[i].Image.FullPath+"' style='width:250px; float:left; margin: 0px 20px 20px 0px'/>";
                            content +="</a>";
                        }
                        content +="<div class='articlecontent1'>" + data.d[i].Details[0].Content + "</div>";
                        content +="<button type='button' class='btn btn-secondary' onclick='$(\"#Translate" + data.d[i].Id + "\").toggle();'><%=TextTranslate %></button>";
                        content +="<div class='jumbotron mt-3' id='Translate"+data.d[i].Id+"' style='display:none'>";
                        if(data.d[i].Details.length > 1)
                        {
							content +="<h3 title='" + data.d[i].Details[1].Title + "'>" + data.d[i].Details[1].Title + "</h3>";
                            content +="<div class='articlecontent2'>";
                            content +="<div class='articlecontent2'>" + data.d[i].Details[1].Content + "</div>";
                            content +="<div style='text-align:right; margin-top:20px'>";
                            content +="<button type='button' class='btn btn-success' data-toggle='modal' data-target='#sendTranslate' onclick='SetData('"+data.d[i].Id+"', 1)'><%=TextFixTranslationYet %></button>";
                            content +="</div>";
                        }
                        else
                        {
                             content +="<div><%=TextNoTranslationYet %></div>";
                            content +="<div style='text-align:right; margin-top:20px'>";
                            content +="<button type='button' class='btn btn-success' data-toggle='modal' data-target='#sendTranslate' onclick='SetData('"+data.d[i].Id+"', 0)'><%=TextSendTranslationYet %></button>";
                            content +="</div>";
                        }
                        content +="</div></div>";
                    }
                    $("#LoadErea").append(content);
                    $('html,body').animate({
                        scrollTop: $("#" + data.d[0].Id).offset().top
                    }, 'fast');
                }
            }
        });
    }

    function SendTranlation()
    {
        var aid = $("#sendTranslate #articleid").val();
        var sendname = $("#sendTranslate #sendName").val();
        var sendcontent = $("#sendTranslate #w3review").val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/Comment",
            data: JSON.stringify({ id: aid, name: sendname, content: sendcontent, captcha:'' }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == -1) alert('Sai mã xác nhận !');
                else if (data.d == 0) alert('<%=Language["ErrorSend"] %>');
                else alert('<%=Language["ThankSent"] %>');
            }
        });
    }
</script>