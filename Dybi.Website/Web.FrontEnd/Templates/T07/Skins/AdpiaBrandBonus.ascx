<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/AdpiaBonus.ascx.cs" Inherits="Web.FrontEnd.Modules.AdpiaBonus" %>

<%if(Title == "Lazada" || Title == "Shopee"){ %>
    <%if(!string.IsNullOrEmpty(Data)){ %>
        <%=Data.Replace("Copy","Đến Shop").Replace("\n","") %>
    <%} %>
<%} else { %>
<section class="brands">
    <h2 class="t_h"><%=Title %></h2>
    <div class="container">
        <div class="row">
            <%if(!string.IsNullOrEmpty(Data)){ %>
                <%=Data.Replace("Copy","Đến Shop").Replace("\n","") %>
            <%} %>
        </div>
    </div>
</section>
<%} %>

<script>
    $(document).ready(function () {
        $(".brand-bonus-share-btn").remove();
    });

    function copyToClipboardText(url) {
        var win = window.open(url, '_blank');
        if (win) {
            //Browser has allowed it to be opened
            win.focus();
        } else {
            //Browser has blocked it
            alert('Please allow popups for this website');
        }
    }

    var adpiaBrandBonus = $('table.leading-normal.w-full tr').get().map(function (row) {
        return $(row).find('td').get().map(function (cell) {
            return $(cell).html();
        });
    });

    $("table.leading-normal.w-full").remove();

    //console.log(adpiaBrandBonus);
    $.each(adpiaBrandBonus, function () {
        if (this.length > 0) {
            $('section.brands>div.container>div.row').append(
                '<div class="col-xs-6 col-md-2">'
                + this[1]
                + this[2]
                + this[6]
                + '</div>'
            );
        }
    });
    $('.brands img').height($('.brands img:first').width());

</script>