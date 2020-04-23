App.factory('treeApi', function($http) {
    return {
        UpdateSite: function(Site, data) {
            var str = {
                "siteMap": {
                    "siteMapNode": angular.copy(data)
                }
            };
            var tostr = JSON.stringify(str);
            var req = {
                method: 'POST',
                url: _treeUrl+'WebSite/UpdateWebDocJson',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: {
                    id: Site,
                    "webDoc": tostr
                }
            };
            $http(req).success(function(data) {
                
            });
        },
        typeMenu: function() {
            return $http.get(_treeUrl+'WebSite/GetSysMenuJson').then(function(result) {
                
                transactions = result.data;
               return transactions;
            });
        },
        copyWebNode: function(id,oldUnId,newUnId) {
            var req = {
                method: 'POST',
                url: _treeUrl+'WebSite/CopyWebNode',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: {
                    "Id":id,
                    "oldUnId": oldUnId,
                    "newUnId": newUnId
                }
            };
            return $http(req).success(function(data) {
                
            });
        },
        metaIndex: function(id) {
            return $http.get(_treeUrl+'MetaIndex/getjson?id='+id).then(function(result) {
                transactions = result.data;
               return transactions;
            });
        },
        getChildNode:function(id,unId){
            return $http.get(_treeUrl+'WebSite/GetChildNodes?Id='+id+'&unId='+unId).then(function(result){
                transactions = result.data;
                return transactions;
            });
        },
        updateWebNode: function(id,unId) {
            return $http.get(_treeUrl+'WebSite/GetWebNode?id='+id+'&unId='+unId).then(function(result) {
                transactions = result.data;
               return transactions;
            });
        },
        getSite: function() {
            return $http.get(_treeUrl+'WebSite/GetSiteListJson').then(function(result) {
                transactions = result.data;
               return transactions;
            });
        },
        newsCatalog: function() {
            return $http.get(_treeUrl+'NewsCatalog/getjson').then(function(result) {
                transactions = result.data;
               return transactions;
            });
        },

    };
});
