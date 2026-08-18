$(document).ready(function () {

    $("#tabNewHeader").show();

    // 無障礙修正 GN1240300E / GN1210100E：確保 roving tabindex 隨頁籤焦點正確更新
    // jQuery UI tabs 方向鍵切換只更新 aria-selected，未更新 tabindex=0，此處補正
    // 採文件層級委派（document → #tabNewHeader li[role='tab']）
    // 選擇器在事件觸發時才評估，不受 jQuery UI 初始化時序影響
    $(document).on("focusin", "#tabNewHeader li[role='tab']", function () {
        $("#tabNewHeader li[role='tab']").attr("tabindex", "-1");
        $(this).attr("tabindex", "0");
    });

});


