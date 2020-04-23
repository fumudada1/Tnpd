var app = angular.module('nmmbaGame', []);

var Joiner = function (clientId, name) {
    this.name = name;
    this.clientId = clientId;
    this.scope = 0;
    this.ans = null;
};

function masterGameCtrl($scope, $http) {
    $scope.customers = [];
    $scope.count = 0;
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.nmmba;
    // Create a function that the hub can call back to display messages.

    //載入題目
    $http({ method: 'GET', url: '/nmmba/GetNmmbaQuestionsJson' }).success(function (data) {


        $scope.NmmbaQuestions = data; // response data 


    });

    //接收姓名
    chat.client.receiveName = function (clientId, name) {
        if ($scope.count == 0) {
            var customer = new Joiner(clientId, name);

            $scope.customers.push(customer);
            console.log($scope.customers[0].name);
            $scope.$apply(); //把異動同步到頁面上
        }

    };
    
    //接收答案
    chat.client.receiveAns = function (clientId, ans) {
        console.log(ans);
        angular.forEach($scope.customers, function (customer) {
            console.log(customer.clientId);
            if (clientId == customer.clientId) {
                customer.ans = ans;
                if (ans == $scope.NmmbaQuestions[$scope.count].Ans) {
                    customer.scope += 1;
                }
            }

        });

        $scope.$apply(); //把異動同步到頁面上
    };
    //離線處理
    chat.client.disconnected = function () {
        $(".sessions").hide();
        $("#Disconnected").show();
    };

    $scope.StartGameCtrl = function () {
        console.log('StartGameCtrl click');
        chat.server.startGame();
    };

    chat.client.startGameCtrl = function () {
        console.log('StartGameCtrl_r');
        $scope.StartGame();
    };

    $scope.StartGame = function () {
        $(".sessions").hide();
        $("#questionMessage").show();
        chat.server.showQuestion($scope.count);

    };
   

    //計算解答
    $scope.AnsResultCtrl = function () {
        chat.server.ansResult();
    };

    chat.client.ansResultCtrl = function () {
        $scope.AnsResult();
    };

    $scope.AnsResult = function () {

        var ans = $scope.NmmbaQuestions[$scope.count].Ans;
        var rightCustomer = "";
        var rightCounter = 0;
        angular.forEach($scope.customers, function (customer) {

            if (customer.ans == ans) {
                //customer.scope += 1;
                rightCustomer += "<span style='color:blue'>" + customer.name + "</span>&nbsp;&nbsp;";
                rightCounter++;
            }
            customer.ans = null;
        });
        $(".sessions").hide();
        $("#AnsResult").show();
        if (rightCounter == 0) {
            $("#resultMessage").text("沒有人答對喔!");
        } else {
            $("#resultMessage").html("恭喜" + rightCustomer + "答對了!");
        }

    };
    
    //下一題
    $scope.NextQuestionCtrl = function () {
        chat.server.nextQuestion();
    };

    chat.client.nextQuestionCtrl = function () {
        $scope.NextQuestion();
    };
    $scope.NextQuestion = function () {


        if ($scope.NmmbaQuestions.length == $scope.count + 1) {
            $(".sessions").hide();
            $("#EndResult").show();
        } else {
            $scope.count = $scope.count + 1;
            $(".sessions").hide();
            $("#questionMessage").show();

            chat.server.showQuestion($scope.count);
            console.log($scope.NmmbaQuestions.length);
        }
        $scope.$apply(); //把異動同步到頁面上
    };
    
    $scope.GoQRcodeCtrl = function () {
        chat.server.goQRcode();
    };

    chat.client.goQRcodeCtrl = function () {
        $scope.GoQRcode();
    };
    $scope.GoQRcode = function () {
        angular.forEach($scope.customers, function (customer) {
            console.log(customer.scope);
            chat.server.sendResult(customer.clientId, customer.scope);

        });


        $scope.customers = [];
        $scope.count = 0;
        $(".sessions").hide();
        $("#qrcode").show();
        $scope.$apply(); //把異動同步到頁面上
    };


    $.connection.hub.start().done(function () {

    });
}