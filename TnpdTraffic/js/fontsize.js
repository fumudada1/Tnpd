//     function LoadFont() {
//         var fontsize;
//         if (readCookie('fontsizeA') == null) {
//             createCookie('fontsizeA', 'font', 30);
//         } else {
            
//             fontsize = readCookie('fontsizeA');
//             $('.bigfont').removeClass('ative');
//             $('.font').removeClass('ative');
//             $('.smallfont').removeClass('ative');
//             if (fontsize == "bigfont")
//             {
//                 $('.bigfont').addClass('ative');
//                 $('#printblock').css({ "font-size": "120%" });                
//             }
//             if (fontsize == "font") {
//                 $('.font').addClass('ative');
//                 $('#printblock').css({ "font-size": "100%" });
//             }
//             if (fontsize == "smallfont") {
//                 $('.smallfont').addClass('ative');
//                 $('#printblock').css({ "font-size": "90%" });

//             }
//         }

//     }

// function createCookie(name, value, days) {
//     if (days) {
//         var date = new Date();
//         date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
//         var expires = "; expires=" + date.toGMTString();
//     }
//     else expires = "";
//     document.cookie = name + "=" + value + expires + ";domain=.president.gov.tw;path=/";
//     //document.cookie = name + "=" + value + expires + ";path=/";

// }

// function readCookie(name) {
//     var nameEQ = name + "=";
//     var ca = document.cookie.split(';');
//     for (var i = 0; i < ca.length; i++) {
//         var c = ca[i];
//         while (c.charAt(0) == ' ')
//             c = c.substring(1, c.length);
//         if (c.indexOf(nameEQ) == 0)
//             return c.substring(nameEQ.length, c.length);
//     }


//     return null;
// }

// function SetFont(size) {
//     createCookie('fontsizeA', size, 30);
//     LoadFont();
// }
// $(document).ready(function () {
//     // 使用者字型大小   
//     LoadFont();
       
// });



$(function () {
    if ($("a[class*=con01]").length > 0)
        $("a[class*=con01]").attr("href", "javascript: void(window.open('http://www.facebook.com/share.php?u='.concat(encodeURIComponent(location.href)) ));");
    if ($("a[class*=con02]").length > 0)
        $("a[class*=con02]").attr("href", "https://plus.google.com/share?url=" + encodeURIComponent(location.href));
    
    if ($("#aSFont").length > 0)
    {
        $("#aSFont").bind("click", function () {
            SetFontSize("S");
            changeFontSize("S");
        });
    }
    if($("#aMFont").length > 0)
    {
        $("#aMFont").bind("click", function () {
            SetFontSize("M");
            changeFontSize("M");
        });
    }
    if($("#aLFont").length > 0)
    {
        $("#aLFont").bind("click", function () {
            SetFontSize("L");
            changeFontSize("L");
        });
    }
    var fontCookieStyle = GetFontSize();
    if (fontCookieStyle != "")
        changeFontSize(fontCookieStyle);
});
var fontCookieKey = "WebFontSize";
function SetFontSize(fontSizeStyle)
{
    $.cookie(fontCookieKey, fontSizeStyle);
}
function GetFontSize() {
    var fonCookie = $.cookie(fontCookieKey);
    if (fonCookie == null)
        return "";
    return fonCookie;
}
function changeFontSize(fontSizeStyle) {
    var divID = "printblock";
    var fontSize = $("#" + divID).css("font-size");
    switch (fontSizeStyle)
    {
        case "L":
            fontSize = "1.2em";
            break;
        case "M":
            fontSize = "1em";
            break;
        case "S":
            fontSize = "0.9em";
            break;
    }
    $("#" + divID).css("font-size", fontSize);
}

