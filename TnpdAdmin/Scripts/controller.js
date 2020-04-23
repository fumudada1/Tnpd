var app = angular.module('chatMessage', []);

app.directive('datepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function(scope, element, attrs, ngModelCtrl) {
            $(function() {
                element.datepicker({
                    dateFormat: 'yy/mm/dd',
                    onSelect: function(date) {
                        ngModelCtrl.$setViewValue(date);
                        scope.$apply();
                    }
                });
            });
        }
    };
});

function GetPersonListJsonCtrl($scope, $http) {
    //載入線上人數
    $http({ method: 'GET', url: '/customer/GetPersonListJson' }).success(function (data) {

        angular.forEach(data, function (item) {
            item.lastDate = tranDateFormate(item.lastDate);
            
        });


        $scope.posts = data; // response data 
        

    });
    
    //載入罐頭訊息
    $http({ method: 'GET', url: '/customer/GetMessageCanJson' }).success(function (data) {

       
        $scope.messageCans = data; // response data 


    });
    
  


    //載入訊息
    $scope.loadMessage = function (id) {

        var perondata = $scope.posts;
        var sessionid = perondata[id].id;
        $scope.sessionID = sessionid;
        perondata[id].readCount = 0;

       
        $http({ method: 'GET', url: '/customer/GetMessageJson/' + sessionid }).success(function (data) {

            angular.forEach(data, function (item) {
                item.initDate = tranDateFormate(item.initDate);
            });


            $scope.messages = data; // response data 

        });
    };
    
    $scope.pushMessage = function (id) {

        
        var cans = $scope.messageCans;
        $scope.Messagebox = cans[id].message;
    };


    //轉換日期格式
    function tranDateFormate(tDate) {
        var value = new Date
                (
                     parseInt(tDate.replace(/(^.*\()|([+-].*$)/g, ''))
                );
        var dat = value.getFullYear() + "/" + (value.getMonth() + 1) + "/" + value.getDate() + "/ " + value.getHours() + ":" + value.getMinutes() + " " + value.getSeconds();
        return dat;
    }

    //送訊息出去
    $scope.sendMessage = function (message) {
        if ($scope.sessionID != null) {
            chat.server.send($scope.sessionID, '客服人員', message, 1);
            $scope.Messagebox = ""; //清空
        } else {
            alert('請選擇客戶!!');
        }


    };



    //宣告signalR 
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.chatHub;
    // Create a function that the hub can call back to display messages.

    //接收來自客戶 主動送來的資訊
    chat.client.addNewMessageToPage = function (clientId, name, message, initDate, directions) {

        
        var isNew = 1;//判斷是否為新使用者
        var postsDate = $scope.posts;
        var index = 0; //紀錄目前是第幾個資料

        for (var i = 0; i <= postsDate.length - 1; i++) {
            if (postsDate[i].id == clientId) { //找到舊使用者資料
                if ($scope.sessionID != clientId) {
                    postsDate[i].readCount++;
                } else {
                    var isRead = 0;
                    //剛好跟使用者聊天
                    if (directions == 1) { //管理者
                        postsDate[i].readCount = 0;
                        isRead = 1;
                        //清空所有已讀
                        angular.forEach($scope.messages, function (item) {
                            item.isRead = 1;
                        });
                        //清空Server已讀
                        $http({ method: 'GET', url: '/customer/ClearMessageIsRead/' + clientId });

                    } else {
                        postsDate[i].readCount ++;
                    }


                    //直接加入訊息
                    $scope.messages.splice(0, 0, { //插入到最上面，使用push可到最下面
                        Name: name,
                        message: message,
                        initDate: initDate,
                        isRead: isRead
                    });
                }
                postsDate[i].messageCount++;
                postsDate[i].lastDate = initDate; //更新日期
                isNew = 0;
                index = i;
                $scope.currentItem = angular.copy(postsDate[i]); //複製自己資料一份起來
                
            }
        }


        if (isNew) { //判斷是否為新使用者
            $scope.posts.splice(0, 0, {
                //插入到最上面，使用push可到最下面
                id: clientId,
                Name: name,
                lastDate: initDate,
                readCount: 1,
                messageCount: 1,
                onLine: '線上'
            });
        }
        else { //移到最上方
            $scope.posts.splice(index, 1); //把自己移除
            $scope.posts.splice(0, 0, $scope.currentItem); //把複製出來的放到最上方

        }


        $scope.$apply(); //把異動同步到頁面上

    };
    
    chat.client.receiveState = function (clientId) {
        //alert("here");
        angular.forEach($scope.posts, function (item) {
            if (clientId == item.id) {
                item.onLine = '線上';
            }
        });
        $scope.$apply(); //把異動同步到頁面上
    };
    
    //通知離線
    chat.client.clientOffline = function (clientId) {
        //alert("here");
        angular.forEach($scope.posts, function (item) {
            if (clientId == item.id) {
                item.onLine = '離線';
            }
        });
        $scope.$apply(); //把異動同步到頁面上
    };

    //載入歷史訊息
    $scope.GetSearchPersonListJson = function (startDate, endDate,name) {
        if (startDate == null && endDate == null && name == null) {
            alert("請輸入查詢條件!");
            return;
        }
       
        $http({ method: 'GET', url: '/customer/GetSearchPersonListJson?startDate=' + startDate + "&endDate=" + endDate + "&name=" + name  }).success(function (data) {
            
            angular.forEach(data, function (item) {
                item.lastDate = tranDateFormate(item.lastDate);
            });


            $scope.oldposts = data; // response data 
            $scope.oldmessages = null;
        });
    };
    
    //載入訊息
    $scope.loadHistoryMessage = function (id) {
      
        var perondata = $scope.oldposts;
        var sessionid = perondata[id].id;
        $scope.sessionID = sessionid;
        perondata[id].readCount = 0;
        $http({ method: 'GET', url: '/customer/GetMessageJson/' + sessionid }).success(function (data) {

            angular.forEach(data, function (item) {
                item.initDate = tranDateFormate(item.initDate);
            });


            $scope.oldmessages = data; // response data 

        });
    };

    //啟動時執行S
    $.connection.hub.start().done(function () {
        
        //通知大家客服人員上線了
        chat.server.sendAllOnLine($.connection.hub.id);
        $scope.checkstate();
        //setInterval(loadState, 5000);

    });
    
    $scope.checkstate = function () {
        loadState();
    };

  
    //送出偵測是否離線
    function loadState() {
        angular.forEach($scope.posts, function (item) {
           
            item.onLine = '離線';
            chat.server.askClientState(item.id);
            $scope.$apply(); //把異動同步到頁面上
        });
    }
    //自動回應在線上
    chat.client.serverreceive = function (clientId) {
        
        chat.server.serverResponseState(clientId, "客服人員在線中");
    };

    $("#chat").show();
    
}
