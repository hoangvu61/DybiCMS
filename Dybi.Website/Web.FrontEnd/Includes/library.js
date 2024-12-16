function OnNumber(sender, e, text) {

    var str = text.GetValue();
    if (str != null) {
        var nStr = str.split(',');
        if ((nStr.length) > 1) {
            for (var i = 1; i <= (nStr.length - 1) ; i++) {
                str = str.replace(",", '');
            }
        }
        if (str.length > 30) {
            text.SetText(tocoma(str.substring(0, 30)));
        }

    }
}

function tocoma(amount, kytuNgan, kytuThapPhan) {
    if (!amount) return '';
    var num_parts = amount.toString().split(".");

    if (kytuNgan) num_parts[0] = num_parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, kytuNgan);
    else num_parts[0] = num_parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

    if (num_parts.length > 1) {
        if (kytuThapPhan) return num_parts.join(kytuThapPhan);
        else return num_parts.join(".");
    }
    else return num_parts[0];
}

function UnComa(text, kytuNgan, kytuThapPhan) {
    if (text != null) {
        var str = text;

        var num_parts = [];
        if (kytuThapPhan) num_parts = text.toString().split(kytuThapPhan);
        else num_parts = text.toString().split(".");

        if (kytuNgan) {
            var arr = num_parts[0].split(kytuNgan);
            if ((arr.length) > 1) {
                for (var i = 1; i <= (arr.length - 1) ; i++) {
                    num_parts[0] = num_parts[0].replace(kytuNgan, '');
                }
            }
        }
        else num_parts[0] = num_parts[0].replace(/,/g, '');

        return num_parts.join(".");
    }
    else {
        str = 0;
    }
    return text;
}

function ToRound(number, index) {
    number = parseFloat(number) || 0;
    if (index < 0) {
        index = index * -1;
        var n = Math.pow(10, index)
        var le = number % n;
        if (le > 0) {
            var dau = le.toString()[0];
            if (dau >= 5) number = parseFloat(number) + parseFloat(n);
        }
        number = number - le;
        return number;
    }
    else if (0 <= index && index <= 100) return number.toFixed(index);
    else return number;

}

function FormatMoney(number, index, kytuNgan, kytuThapPhan) {
    if (window.location.href.toLowerCase().includes("invoice41") && !number)
        return '';
    else 
        return tocoma(ToRound(parseFloat(number), index), kytuNgan, kytuThapPhan);
}

function FormatMoneyFromText(text, index, kytuNgan, kytuThapPhan) {
    if (text) {
        var number = 0;
        if (text.includes(kytuNgan)) {
            var numberText = UnComa(text, kytuNgan, kytuThapPhan);
            number = parseFloat(numberText);
        }
        else {
            number = parseFloat(text);
        }
        
        return tocoma(ToRound(number, index), kytuNgan, kytuThapPhan);
    }
    return text;
}

function SetLink() {
    var currentPath = window.location.pathname;
    var links = [];
    if (sessionStorage.getItem("linkHistories"))
        links = JSON.parse(sessionStorage.getItem("linkHistories"));
    else links = [];
    var lastOne = links[links.length - 1];
    if (currentPath != lastOne && !window.location.search.includes("action=coppy")) {
        links.push(currentPath);
        DeleteSessionStorage('details')
    }
    sessionStorage.setItem("linkHistories", JSON.stringify(links));
    
}
function GoBack() {
    var links = JSON.parse(sessionStorage.getItem("linkHistories"));
    links.pop();
    sessionStorage.setItem("linkHistories", JSON.stringify(links));
    if (links.length > 0) {
        var linkGo = links[links.length - 1];
        window.location.href = linkGo;
    }
    else { console.log('Không có trang trước') }
}

var mangso = ['không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín'];

function dochangchuc(so, daydu) {
    var chuoi = "";
    chuc = Math.floor(so / 10);
    donvi = so % 10;
    if (chuc > 1) {
        chuoi = " " + mangso[chuc] + " mươi";
        if (donvi == 1) {
            chuoi += " mốt";
        }
    } else if (chuc == 1) {
        chuoi = " mười";
        if (donvi == 1) {
            chuoi += " một";
        }
    } else if (daydu && donvi > 0) {
        chuoi = " lẻ";
    }

    if (donvi == 5 && chuc >= 1) {
        chuoi += " lăm";
    } else if (donvi > 1 || (donvi == 1 && chuc == 0)) {
        chuoi += " " + mangso[donvi];
    }
    return chuoi;
}

function docblock(so, daydu) {
    var chuoi = "";
    tram = Math.floor(so / 100);
    so = so % 100;
    if (daydu || tram > 0) {
        chuoi = " " + mangso[tram] + " trăm";
        chuoi += dochangchuc(so, true);
    } else {
        chuoi = dochangchuc(so, false);
    }
    return chuoi;
}

function dochangtrieu(so, daydu) {
    var chuoi = "";
    trieu = Math.floor(so / 1000000);
    so = so % 1000000;
    if (trieu > 0) {
        chuoi = docblock(trieu, daydu) + " triệu";
        daydu = true;
    }
    nghin = Math.floor(so / 1000);
    so = so % 1000;
    if (nghin > 0) {
        chuoi += docblock(nghin, daydu) + " nghìn";
        daydu = true;
    }
    if (so > 0) {
        chuoi += docblock(so, daydu);
    }
    return chuoi;
}

function docso(so) {
    if (so == 0) return mangso[0];
    var chuoi = "",
        hauto = "";

    do {
        ty = so % 1000000000;
        so = Math.floor(so / 1000000000);
        if (so > 0) {
            chuoi = dochangtrieu(ty, true) + hauto + chuoi;
        } else {
            chuoi = dochangtrieu(ty, false) + hauto + chuoi;
        }
        hauto = " tỷ";
    } while (so > 0);

    return chuoi.trim();
}

function DocTien(so, donvi, ck) {
    var dcgiam = "";
    if(so < 0 && ck != 3){
        dcgiam = "Điều chỉnh giảm ";        
    }
    so = so < 0 ? -so : so;
    var donViNguyen = "",
        donViLe = "";
    if (donvi == 'VND') {
        donViNguyen = 'đồng';
        donViLe = ''
    }
    else if (donvi == 'USD') {
        donViNguyen = 'đô la Mỹ';
        donViLe = 'cent'
    }
    else if (donvi == 'EUR') {
        donViNguyen = 'Euro';
        donViLe = ''
    }
    else if (donvi == 'JPY') {
        donViNguyen = 'yên Nhật';
        donViLe = ''
    }
    else if (donvi == 'CAD') {
        donViNguyen = 'đô la Canada';
        donViLe = 'cent'
    }
    else if (donvi == 'AUD') {
        donViNguyen = 'đô la Úc';
        donViLe = 'cent'
    }
    else if (donvi == 'POUND') {
        donViNguyen = 'bảng Anh';
        donViLe = 'pence'
    }

    var le = "";
    if (so.toString().includes('.')) {
        var arr = so.toString().split('.');
        so = arr[0];
        le = arr[1];
    }

    var chu = docso(so) + " " + donViNguyen;
    if (le && parseFloat(le) != 0) chu = chu + ' và ' + docso(le) + ' ' + donViLe;
    return CapitalizeFirstLetter(`${dcgiam}${chu}`);
}

function CapitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

function MoneyToWords (num, currency){
    var result = part1 = part2 = leftSingular = leftPlural = rightSingular = rightPlural = "";
    var splitngan = ".";
    if (currency !== "VND"){
        leftSingular= " dollar"; leftPlural= " dollars"; rightSingular= " cent"; rightPlural= " cents";
    }
    else {
        leftSingular =  leftPlural= " dong";
    }
    
    numparts = num.toString().split(".");
    part1 = numToWords(numparts[0]);
    if (numparts[1]) part2 =  numToWords(numparts[1]);
    if (part2 && part2.trim() === 'one') part2 = part2 + rightSingular;
    else if (part2 && part2.trim() !== 'one') part2 = part2 + rightPlural;

    if (part1 && part1.trim() !== 'one' && part1.trim() !== 'zero') part1 = part1 + leftPlural;
    else if (part1) part1 = part1 + leftSingular;
    if (part1 != "") {
        if (part2 != "") result = part1 + " and " + part2;
        else result = part1;
    }
    result = result.replaceAll(/\s\s+/g, ' ');
    result = CapitalizeFirstLetter(result);
    return result;
}
const arr = x => Array.from(x);
const num = x => Number(x) || 0;
const str = x => String(x);
const isEmpty = xs => xs.length === 0;
const take = n => xs => xs.slice(0,n);
const drop = n => xs => xs.slice(n);
const reverse = xs => xs.slice(0).reverse();
const comp = f => g => x => f (g (x));
const not = x => !x;
const chunk = n => xs =>
    isEmpty(xs) ? [] : [take(n)(xs), ...chunk (n) (drop (n) (xs))];
let numToWords = n => {
    let a = [
      '', 'one', 'two', 'three', 'four',
      'five', 'six', 'seven', 'eight', 'nine',
      'ten', 'eleven', 'twelve', 'thirteen', 'fourteen',
      'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'
    ];
    let b = [
      '', '', 'twenty', 'thirty', 'forty',
      'fifty', 'sixty', 'seventy', 'eighty', 'ninety'
    ];
    let g = [
      '', 'thousand', 'million', 'billion', 'trillion', 'quadrillion',
      'quintillion', 'sextillion', 'septillion', 'octillion', 'nonillion'
    ];
    // this part is really nasty still
    // it might edit this again later to show how Monoids could fix this up
    let makeGroup = ([ones,tens,huns]) => {
        return [
          num(huns) === 0 ? '' : a[huns] + ' hundred ',
          num(ones) === 0 ? b[tens] : b[tens] && b[tens] + '-' || '',
          a[tens+ones] || a[ones]
        ].join('');
    };
    // "thousands" constructor; no real good names for this, i guess
    let thousand = (group,i) => group === '' ? group : `${group} ${g[i]}`;
    // execute !
    if (typeof n === 'number') return numToWords(String(n));
    if (n === '0')             return 'zero';
    return comp (chunk(3)) (reverse) (arr(n))
      .map(makeGroup)
      .map(thousand)
      .filter(comp(not)(isEmpty))
      .reverse()
      .join(' ');
};

jQuery(function ($) {
    $.datepicker.regional["vi-VN"] =
      {
          closeText: "Đóng",
          prevText: "Trước",
          nextText: "Sau",
          currentText: "Hôm nay",
          monthNames: ["Tháng một", "Tháng hai", "Tháng ba", "Tháng tư", "Tháng năm", "Tháng sáu", "Tháng bảy", "Tháng tám", "Tháng chín", "Tháng mười", "Tháng mười một", "Tháng mười hai"],
          monthNamesShort: ["Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín", "Mười", "Mười một", "Mười hai"],
          dayNames: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"],
          dayNamesShort: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"],
          dayNamesMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
          weekHeader: "Tuần",
          dateFormat: "dd/mm/yy",
          firstDay: 1,
          isRTL: false,
          showMonthAfterYear: false,
          yearSuffix: ""
      };

    $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);
});

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for(var i = 0; i <ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    var expires = "expires="+ d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function DeleteCookie(name) {
    document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:01 GMT;path=/";
}

function GetHSMPassword(callback, reject) {
    var signingType = parseInt(getCookie("SigningType"));
    if (signingType > 1){
        var inputform = '<label style="float:left">Mật khẩu</label>';
        inputform += '<input id="HSMpass" name="HSMpass" type="password" class="form-control"/>';
        Swal.fire({
            icon: 'info',
            title: 'Vui lòng nhập mật khẩu cho chữ ký số HSM',
            html: inputform,
            showCancelButton: true,
            confirmButtonText: 'Xác nhận',
            showLoaderOnConfirm: true,
            cancelButtonText: 'Quay về',
            allowOutsideClick: () => {
                !Swal.isLoading();
                reject();
            }
        }).then((result) => {
            if (result.isConfirmed) {
                var pass = $("#HSMpass").val();

                if (pass && pass.length > 5)
                    callback(pass)
                else  {
                    Swal.fire({
                        icon: 'warning',
                        title: "<span style='color:#F28C30'>Thông báo</span>",
                        html: "Mật khẩu không hợp lệ",
                        confirmButtonText: 'OK',
                    }).then(()=>{
                        reject();
                    })
                }
            }
            else reject();
        });
    }
    else reject();
}

function GetSessionStorage(name){
    var storage = window.sessionStorage;
    return storage.getItem(name);
}
function SetSessionStorage(name, value){
    var storage = window.sessionStorage;
    if(!name || !value) return;
    if (typeof value != 'string')
        value = JSON.stringify(value)

    storage.setItem(name, value);
}
function DeleteSessionStorage(name){
    var storage = window.sessionStorage;
    storage.removeItem(name)
}


$(document).ready(function() {
    $(".btn-once").on('click',function() {
        //$(this).attr("disabled","disabled")
        //$(this).prop("disabled", true)
        //$(this).css("disable",true)
        $(this).css("cursor","default");
    }) 
});