// Smooth scroll blocking
document.addEventListener( 'DOMContentLoaded', function() {
	if ( 'onwheel' in document ) {
		window.onwheel = function( event ) {
			if( typeof( this.RDSmoothScroll ) !== undefined ) {
				try { window.removeEventListener( 'DOMMouseScroll', this.RDSmoothScroll.prototype.onWheel ); } catch( error ) {}
				event.stopPropagation();
			}
		};
	} else if ( 'onmousewheel' in document ) {
		window.onmousewheel= function( event ) {
			if( typeof( this.RDSmoothScroll ) !== undefined ) {
				try { window.removeEventListener( 'onmousewheel', this.RDSmoothScroll.prototype.onWheel ); } catch( error ) {}
				event.stopPropagation();
			}
		};
	}

	try { $('body').unmousewheel(); } catch( error ) {}
});
function include(scriptUrl) {
    document.write('<script src="' + scriptUrl + '"></script>');
}

function isIE() {
    var myNav = navigator.userAgent.toLowerCase();
    return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
};

/* cookie.JS
 ========================================================*/
include('/Templates/T12/js/jquery.cookie.js');

/* Easing library
 ========================================================*/
include('/Templates/T12/js/jquery.easing.1.3.js');

/* Stick up menus
 ========================================================*/
;
(function ($) {
    var o = $('html');
    if (o.hasClass('desktop')) {
        include('/Templates/T12/js/tmstickup.js');

        $(document).ready(function () {
            $('#stuck_container').TMStickUp({})
        });
    }
})(jQuery);

/* ToTop
 ========================================================*/
;
(function ($) {
    var o = $('html');
    if (o.hasClass('desktop')) {
        include('/Templates/T12/js/jquery.ui.totop.js');

        $(document).ready(function () {
            $().UItoTop({easingType: 'easeOutQuart'});
        });
    }
})(jQuery);

/* EqualHeights
 ========================================================*/
;
(function ($) {
    var o = $('[data-equal-group]');
    if (o.length > 0) {
        include('/Templates/T12/js/jquery.equalheights.js');
    }
})(jQuery);

/* SMOOTH SCROLLIG
 ========================================================*/
;
(function ($) {
    var o = $('html');
    if (o.hasClass('desktop')) {
        include('/Templates/T12/js/jquery.mousewheel.min.js');
        include('/Templates/T12/js/jquery.simplr.smoothscroll.min.js');

        $(document).ready(function () {
            $.srSmoothscroll({
                step: 150,
                speed: 800
            });
        });
    }
})(jQuery);

/* Copyright Year
 ========================================================*/
;
(function ($) {
    var currentYear = (new Date).getFullYear();
    $(document).ready(function () {
        $("#copyright-year").text((new Date).getFullYear());
    });
})(jQuery);


/* Superfish menus
 ========================================================*/
;
(function ($) {
    include('/Templates/T12/js/superfish.js');    
})(jQuery);

/* Navbar
 ========================================================*/
;
(function ($) {
    include('/Templates/T12/js/jquery.rd-navbar.js');
})(jQuery);


/* Google Map
 ========================================================*/
;
(function ($) {
    var o = document.getElementById("google-map");
    if (o) {
        include('//maps.google.com/maps/api/js?sensor=false');
        include('/Templates/T12/js/jquery.rd-google-map.js');

        $(document).ready(function () {
            var o = $('#google-map');
            if (o.length > 0) {
                o.googleMap({styles: [{"featureType":"administrative","elementType":"labels.text.fill","stylers":[{"color":"#444444"}]},{"featureType":"administrative.country","elementType":"geometry.fill","stylers":[{"visibility":"on"}]},{"featureType":"administrative.province","elementType":"labels.icon","stylers":[{"hue":"#ff0000"},{"visibility":"on"}]},{"featureType":"landscape","elementType":"all","stylers":[{"color":"#f2f2f2"}]},{"featureType":"poi","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"road","elementType":"all","stylers":[{"saturation":-100},{"lightness":45}]},{"featureType":"road.highway","elementType":"all","stylers":[{"visibility":"simplified"}]},{"featureType":"road.arterial","elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"all","stylers":[{"color":"#46bcec"},{"visibility":"on"}]}]});
            }
        });
    }
})
(jQuery);

/* WOW
 ========================================================*/
;
(function ($) {
    var o = $('html');

    if ((navigator.userAgent.toLowerCase().indexOf('msie') == -1 ) || (isIE() && isIE() > 9)) {
        if (o.hasClass('desktop')) {
            include('/Templates/T12/js/wow.js');

            $(document).ready(function () {
                new WOW().init();
            });
        }
    }
})(jQuery);

/* Contact Form
 ========================================================*/
;
(function ($) {
    var o = $('#contact-form');
    if (o.length > 0) {
        include('/Templates/T12/js/modal.js');
        include('/Templates/T12/js/TMForm.js'); 

        if($('#contact-form .recaptcha').length > 0){
        	include('//www.google.com/recaptcha/api/js/recaptcha_ajax.js');
        }
    }
})(jQuery);

///* Orientation tablet fix
// ========================================================*/
//$(function () {
//    // IPad/IPhone
//    var viewportmeta = document.querySelector && document.querySelector('meta[name="viewport"]'),
//        ua = navigator.userAgent,

//        gestureStart = function () {
//            viewportmeta.content = "width=device-width, minimum-scale=0.25, maximum-scale=1.6, initial-scale=1.0";
//        },

//        scaleFix = function () {
//            if (viewportmeta && /iPhone|iPad/.test(ua) && !/Opera Mini/.test(ua)) {
//                viewportmeta.content = "width=device-width, minimum-scale=1.0, maximum-scale=1.0";
//                document.addEventListener("gesturestart", gestureStart, false);
//            }
//        };

//    scaleFix();
//    // Menu Android
//    if (window.orientation != undefined) {
//        var regM = /ipod|ipad|iphone/gi,
//            result = ua.match(regM);
//        if (!result) {
//            $('.sf-menus li').each(function () {
//                if ($(">ul", this)[0]) {
//                    $(">a", this).toggle(
//                        function () {
//                            return false;
//                        },
//                        function () {
//                            window.location.href = $(this).attr("href");
//                        }
//                    );
//                }
//            })
//        }
//    }
//});
//var ua = navigator.userAgent.toLocaleLowerCase(),
//    regV = /ipod|ipad|iphone/gi,
//    result = ua.match(regV),
//    userScale = "";
//if (!result) {
//    userScale = ",user-scalable=0"
//}
//document.write('<meta name="viewport" content="width=device-width,initial-scale=1.0' + userScale + '">');

/* Camera
 ========================================================*/
;(function ($) {
    var o = $('#camera');
    if (o.length > 0) {
        if (!(isIE() && (isIE() > 9))) {
            include('/Templates/T12/js/jquery.mobile.customized.min.js');
        }

        include('/Templates/T12/js/camera.js');

        $(document).ready(function () {
            o.camera({
                autoAdvance: true,
                height: '49.6410%',
                minHeight: '350px',
                pagination: false,
                thumbnails: false,
                playPause: false,
                hover: false,
                loader: 'none',
                navigation: true,
                navigationHover: true,
                mobileNavHover: false,
                fx: 'simpleFade'
            })
        });
    }
})(jQuery);

/* Subscribe Form
 ========================================================*/
;
(function ($) {
    var o = $('#subscribe-form');
    if (o.length > 0) {
        include('/Templates/T12/js/sForm.js');
    }
})(jQuery);


/* Audio Js
 ========================================================*/
;
(function ($) {
    var o = $('audio');
    if (o.length > 0) {
        include('/Templates/T12/js/audio.min.js');
        include('/Templates/T12/js/jquery.rd-audio.js');
        $(document).ready(function () {
            o.rdAudio({
                fixedBG: 'transparent',
                fixed: true,
                fixedClass: 'fixed-player',
                playlistClass: 'music-list'
            });
        });
    }
})(jQuery);

/* FancyBox
 ========================================================*/
;(function ($) {
    var o = $('.thumb');
    if (o.length > 0) {
        include('/Templates/T12/js/jquery.fancybox.js');
        include('/Templates/T12/js/jquery.fancybox-media.js');
        include('/Templates/T12/js/jquery.fancybox-buttons.js');
        $(document).ready(function () {
            o.fancybox();
        });
    }
})(jQuery);

/* Parallax
 =============================================*/
;(function ($) {
    include('/Templates/T12/js/jquery.rd-parallax.js');
})(jQuery);