var app = angular.module('nmmbaClientGame', []);

function masterClientGameCtrl($scope, $http) {
    
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.nmmba;
    // Create a function that the hub can call back to display messages.

    //載入題目
    $http({ method: 'GET', url: '/nmmba/GetNmmbaQuestionsJson' }).success(function (data) {


        $scope.NmmbaQuestions = data; // response data 


    });

    // Reference the auto-generated proxy for the hub.
    chat = $.connection.nmmba;
    $scope.count = 0;
    $scope.ResultMessage = "";
    // Create a function that the hub can call back to display messages.
    chat.client.showQuestion = function (index) {
        
        $scope.count = index;
        $(".sessions").hide();
        $("#questionMessage").show();
        console.log($scope.count);
        $scope.$apply(); //把異動同步到頁面上
        
    };
    chat.client.sendCustomerResult = function (scope) {
        console.log("scope:" +scope);
        if (scope == 0) {
            $scope.ResultMessage = "您答對0題喔，請再接再厲!";
        } else {
            $scope.ResultMessage = "恭喜您答對" + scope + "題";
        }
        $(".sessions").hide();
        $("#gameScope").show();
        $scope.$apply(); //把異動同步到頁面上

    };
    chat.client.disconnected = function () {
        $(".sessions").hide();
        $("#Disconnected").show();
    };
    $scope.SendName = function () {
            chat.server.sendName($.connection.hub.id, $("#userName").val());
            $(".sessions").hide();
            $("#waitStart").show();

    };
    $scope.sendAns = function (ListNum) {
        //console.log(ListNum);
        chat.server.sendAns($.connection.hub.id, ListNum);
        if (ListNum == $scope.NmmbaQuestions[$scope.count].Ans) {
            $(".sessions").hide();
            $("#ansRight").show();
        } else {
            $(".sessions").hide();
            $("#ansWrong").show();
        }

    };
    $.connection.hub.start().done(function () {
        $(".sessions").hide();
        $("#askUserName").show();

        //$("#btnName").click(function () {
        //    if ($("#userName").val() == "") {
        //        return;
        //    }


        //    chat.server.sendName($.connection.hub.id, $("#userName").val());
        //    $(".sessions").hide();
        //    $("#waitStart").show();
        //});
    });
}