function Captcha(n) {
    var alpha = new Array('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        //'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
    var code = '';
    for (var i = 0; i < n; i++) {
        code = code + alpha[Math.floor(Math.random() * alpha.length)];
    }
    $('#DyBiCaptcha').text(code);
}
function ValidCaptcha() {
    var string1 = $('#DyBiCaptcha').text();
    var string2 = $('#DyBiCaptchaInput').val().toUpperCase();
    if (string1 == string2) {
        return true;
    } else {
        return false;
    }
}