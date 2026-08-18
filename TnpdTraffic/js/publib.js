$(document).ready(function () {

    $("#tabNewHeader").show();
    initTabBlurEvent();


});




function initTabBlurEvent() {
    //$("a[id*=ui-id-]").each(function () {
    //    $(this).blur(function () {
    //        // console.log("blur");
    //        var index = $(this).index();
    //        $("div[id*=tabs-news-" + (index + 1).tostr() + "]").children("div").children("div").children("a").first().focus();
    //    });
    //});
    $("div[id*=tabs-news-]").each(function () {
        var index = parseInt($(this).attr("id").replace("tabs-news-", "")) - 1;
        if (index != $("div[id*=tabs-news-]").length - 1) {
            var target = $(this).children("div").children("div").children("div").children("div").last().children("a");
            console.log(target);
            $(target).blur(function () {
                console.log("blur");
                console.log("#ui-id-" + (index + 2).toString() + " focus");
                $("#ui-id-" + (index + 2).toString()).focus();
                $("#tabNewHeader").children("li").attr("class", "ui-state-default ui-corner-top");
                $("#ui-id-" + (index + 2).toString()).parent().attr("class", "ui-state-default ui-corner-top ui-tabs-active ui-state-active");
                $("div[id*=tabs-news-]").hide();
                $("#tabs-news-" + (index + 2).toString()).show();
            });
        }
    });
   
}




   