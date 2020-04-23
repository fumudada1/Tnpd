var App = angular.module('treeview', ['ui.tree', 'ngRoute', 'ui.bootstrap.contextMenu', 'angularModalService', 'ngCkeditor']);

App.controller('treeviewCtrl', ['toolService', 'treeApi', '$scope', '$http', 'ModalService', function(toolService, treeApi, $scope, $http, ModalService, $httpProvider) {
        $scope.editorOptions = { 
        };
        $scope.treeOptions = {
            accept: function(sourceNodeScope, destNodesScope, destIndex) {
                var srctype = sourceNodeScope.$element.attr('data-type');
                var dsttype = destNodesScope.$element.attr('data-type');
                if (srctype === dsttype) {
                    return true;
                } else {
                    return false;
                }
            },
            dragStop: function() {
                // var content = angular.toJson($scope.data);
                treeApi.UpdateSite($scope.thisSite.code, $scope.data);
            }

        };
        $scope.removes = function(scope) {
            if (confirm("你確定要刪除此筆資料嗎？子目錄也將一併刪除")) {

                scope.remove();
                treeApi.UpdateSite($scope.thisSite.code, $scope.data);
            } else {

            }

        };
        $scope.getClass =function(thisClass){
            if (thisClass == 'text-Edit'){
                return 'fa-file-text-o';    
            }
            else if(thisClass=='text-Link'){
                return 'fa-link';  
            }
            else if(thisClass=='text-Publish'){
                return 'fa-newspaper-o';  
            }
            else if(thisClass=='directory'){
                return '';  
            }
            
            else{
                return 'fa-database';  
            }

             
        };
        $scope.rootOption = [
            ['新增', function($itemScope) {
                // console.log( $scope.nodeType);
                // console.log($itemScope.$modelValue['@UnID']);
                // location.href="/new/"+$itemScope.$modelValue['@ModuleID'];
                // $scope.selected = $itemScope.item.name;
                var _self = $itemScope.$modelValue;

                ModalService.showModal({
                    templateUrl: "Scripts/treeview/views/Modal.html",
                    controller: "ModalRootController",
                    inputs: {

                        title: '新增網站項目',
                        type: $scope.nodeType,
                        metaIndex: $scope.metaIndex,
                        site: $scope.thisSite.code,
                        newsCatalog: $scope.newsCatalog
                    }
                }).then(function(modal) {
                    modal.element.modal();
                    modal.close.then(function(result) {
                        var tostr = JSON.stringify(result);
                        var req = {
                            method: 'POST',
                            url: _treeUrl+'WebSite/AddWebNode',
                            headers: {
                                'Content-Type': 'application/x-www-form-urlencoded'
                            },
                            data: result
                        };
                        $http(req).success(function(data) {
                            $scope.data = toolService.datahandle(data);

                        });
                    });
                });
            }]
        ];
        $scope.menuOptions = function($itemScope) {
            // console.log($itemScope.$modelValue);
            var str = [
                ['新增', function($itemScope) {
                    // console.log( $scope.nodeType);
                    // console.log($itemScope.$modelValue['@UnID']);
                    // location.href="/new/"+$itemScope.$modelValue['@ModuleID'];
                    // $scope.selected = $itemScope.item.name;
                    var _self = $itemScope.$modelValue;
                    ModalService.showModal({
                        templateUrl: "Scripts/treeview/views/Modal.html",
                        controller: "ModalController",
                        inputs: {
                            title: '網站項目：' + _self['@title'],
                            type: $scope.nodeType,
                            metaIndex: $scope.metaIndex,
                            guid: $itemScope.$modelValue['@UnID'],
                            site: $scope.thisSite.code,
                            newsCatalog: $scope.newsCatalog
                        }
                    }).then(function(modal) {
                        modal.element.modal();
                        modal.close.then(function(result) {
                            var tostr = JSON.stringify(result);
                            var req = {
                                method: 'POST',
                                url: _treeUrl+'WebSite/AddWebNode',
                                headers: {
                                    'Content-Type': 'application/x-www-form-urlencoded'
                                },
                                data: result
                            };
                            $http(req).success(function(data) {
                               $scope.data = toolService.datahandle(data);

                            });
                        });
                    });
                }],
                ['修改', function($itemScope, $element) {
                    var _self = $itemScope.$modelValue;
                    treeApi.updateWebNode($scope.thisSite.code, _self['@UnID']).then(function(data) {
                        ModalService.showModal({
                            templateUrl: "Scripts/treeview/views/Modal.html",
                            controller: "editModalController",
                            inputs: {
                                title: '網站項目：' + _self['@title'],
                                type: $scope.nodeType,
                                metaIndex: $scope.metaIndex,
                                guid: $itemScope.$modelValue['@UnID'],
                                site: $scope.thisSite.code,
                                newsCatalog: $scope.newsCatalog,
                                data: data
                            }
                        }).then(function(modal) {
                            modal.element.modal();
                            modal.close.then(function(result) {
                                var tostr = JSON.stringify(result);
                                var req = {
                                    method: 'POST',
                                    url: _treeUrl+'WebSite/UpdateWebNode',
                                    headers: {
                                        'Content-Type': 'application/x-www-form-urlencoded'
                                    },
                                    data: result
                                };
                                $http(req).success(function(data) {

                                    $scope.data = toolService.datahandle(data);
                                    alert('修改成功');

                                });
                            });
                        });
                    });
                }],
                
		['預覽',function($itemScope){
			console.log($scope.thisSite);
			console.log($itemScope);
			console.log($itemScope.$modelValue['@UnID'])
			if($scope.thisSite.code == 'tnpd'){
		
				window.open('https://www.tnpd.gov.tw/Article/'+$itemScope.$modelValue['@UnID'],'_blank');
			}else{
				window.open('https://www.tnpd.gov.tw/'+$scope.thisSite.code+'/Content/Article/'+$itemScope.$modelValue['@UnID'],'_blank');
			}
		}],
				['刪除', function($itemScope) {
                    $scope.removes($itemScope);
                }],
                ['預設', function($itemScope) {
                    var _self = $itemScope.$modelValue;
                    treeApi.getChildNode($scope.thisSite.code, _self['@UnID']).then(function(data) {
                        // console.log(data);
                        ModalService.showModal({
                            templateUrl: "Scripts/treeview/views/ModalDefault.html",
                            controller: "defaultModalController",
                            inputs: {
                                origin: _self,
                                title: '請選擇指定位置',
                                guid: $itemScope.$modelValue['@UnID'],
                                site: $scope.thisSite.code,
                                data: data
                            }
                        }).then(function(modal) {
                            modal.element.modal();
                            modal.close.then(function(result) {
                                var req = {
                                    method: 'POST',
                                    url: _treeUrl+'WebSite/UpdateDefaultValue',
                                    headers: {
                                        'Content-Type': 'application/x-www-form-urlencoded'
                                    },
                                    data: result
                                };
                                $http(req).success(function(data) {
                                    $scope.data = toolService.datahandle(data);
                                    alert('設定成功');

                                });
                            });
                        });
                    });
                }],
                ['複製', function($itemScope) {
                    // console.log($itemScope.$modelValue);
                    var thisId = $itemScope.$modelValue['@UnID'];
                    treeApi.copyWebNode($scope.thisSite.code, thisId, toolService.guid()).then(function(result) {
                        $scope.data = toolService.datahandle(result.data);
                        // treeApi.UpdateSite($scope.thisSite, $scope.data);
                    });
                }]
            ];
            var type =  $itemScope.$modelValue['@DataType'];
            if (type == 'directory') {
                str.splice(-1, 1);
                return str;
            }else{
                str.splice(0, 1);
                str.splice(-2,1);
                return str;
            }
            
        };
        $scope.toggle = function(scope) {

            scope.toggle();
        };
        $scope.decrement = function(scope) {

        };
        $scope.moveLastToTheBeginning = function() {
            var a = $scope.data.pop();
            $scope.data.splice(0, 0, a);
        };

        $scope.newSubItem = function(scope) {
            var nodeData = scope.$modelValue;
            nodeData.nodes.push({
                id: nodeData.id * 10 + nodeData.nodes.length,
                title: nodeData.title + '.' + (nodeData.nodes.length + 1),
                nodes: []
            });
        };

        $scope.collapseAll = function() {
            $scope.$broadcast('collapseAll');
        };

        $scope.expandAll = function() {
            $scope.$broadcast('expandAll');
        };
        $scope.thisSite='';
        $scope.Site='';
        $scope.data = '';
        var appUrl = _treeUrl;
        $scope.siteList = treeApi.getSite().then(function(data) {
            $scope.siteList = data;
            $scope.thisSite=$scope.siteList[0];
            // _treeUrl
            //取得列表
            $http.get(appUrl + "WebSite/WebDocJson?id=" + $scope.thisSite.code).success(function(data) {
                
                // console.log(data);
                $scope.data = toolService.datahandle(data);
            });
        });
        
        $scope.nodeType = treeApi.typeMenu().then(function(data) {
            $scope.nodeType = data;
        });
        $scope.siteChange = function(site){
            $scope.data='';
            $http.get(appUrl + "WebSite/WebDocJson?id=" + $scope.thisSite.code).success(function(data) {
            
            // console.log(data);
            $scope.data = toolService.datahandle(data);
        });
        };
        $scope.metaIndex = '';
        treeApi.metaIndex($scope.thisSite.code).then(function(data) {
            $scope.metaIndex = data;
        });
        $scope.newsCatalog = '';
        treeApi.newsCatalog().then(function(data) {
            $scope.newsCatalog = data;

        });
        $scope.reset = function() {
            treeApi.UpdateSite('Cypd', []);

        };
        
       
    }])
    .config(['$routeProvider', '$httpProvider', function($routeProvider, $httpProvider) {

        $routeProvider
            .when('/', {
                controller: 'treeviewCtrl',
                templateUrl: 'Scripts/treeview/views/treeview.html'
            });
        $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';

        /**
         * The workhorse; converts an object to x-www-form-urlencoded serialization.
         * @param {Object} obj
         * @return {String}
         */
        var param = function(obj) {
            var query = '',
                name, value, fullSubName, subName, subValue, innerObj, i;

            for (name in obj) {
                value = obj[name];

                if (value instanceof Array) {
                    for (i = 0; i < value.length; ++i) {
                        subValue = value[i];
                        fullSubName = name + '[' + i + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (value instanceof Object) {
                    for (subName in value) {
                        subValue = value[subName];
                        fullSubName = name + '[' + subName + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (value !== undefined && value !== null)
                    query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
            }

            return query.length ? query.substr(0, query.length - 1) : query;
        };

        // Override $http service's default transformRequest
        $httpProvider.defaults.transformRequest = [function(data) {
            return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
        }];
    }]);
