var app = angular.module('treeview');

app.controller('ModalController', [
  '$scope', '$element', 'title','type','guid','site','metaIndex','newsCatalog', 'close', 
  function($scope, $element, title,type,guid,site,metaIndex,newsCatalog, close) {
  // api data
  $scope.nodeType = type;
  $scope.title = title;
  $scope.metaIndex = metaIndex;
  $scope.newsCatalog= newsCatalog;
  $scope.guid = guids();
  $scope.name = null;
  $scope.site = site;
  $scope.ParentId =guid;
  $scope.type = type[0];
  $scope.group = metaIndex[0];
  $scope.visible = 1;
  $scope.ckContent='';
  $scope.Catalog =newsCatalog[0];
  $scope.TargetAry = [{'name':'本地開啟','value':'_self'},{'name':'外部超連結','value':'_blank'}];
  $scope.Link='';
  $scope.Target=$scope.TargetAry[0];
  //  This close function doesn't need to use jQuery or bootstrap, because
  //  the button has the 'data-dismiss' attribute.
  $scope.close = function() {

 	  close({
      Guid:$scope.guid,
      Subject: $scope.name,
      WebSiteName: $scope.site,
      ParentId:$scope.ParentId,
      MetaId: $scope.group.Id,
      Visible:   $scope.visible,
      DataType: $scope.type.DataType,
      Category: $scope.Catalog.Id,
      // $scope.Catalog.Id,
      Article: $scope.ckContent,
      Link :$scope.Link,
      Target :$scope.Target.value,
      upFile :''
    }, 500); // close, but give 500ms for bootstrap to animate
  };

  //  This cancel function must use the bootstrap, 'modal' function because
  //  the doesn't have the 'data-dismiss' attribute.
  $scope.cancel = function() {

    //  Manually hide the modal.
    $element.modal('hide');
    
    //  Now call close, returning control to the caller.
    close({
      name: $scope.name,
      age: $scope.age
    }, 500); // close, but give 500ms for bootstrap to animate
  };
  function guids() {
    function s4() {
      return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
      s4() + '-' + s4() + s4() + s4();
  }
}]).controller('ModalRootController', [
  '$scope', '$element', 'title','type','site','metaIndex','newsCatalog', 'close', 
  function($scope, $element, title,type,site,metaIndex,newsCatalog, close) {
  // api data
  $scope.nodeType = type;
  $scope.title = title;
  $scope.metaIndex = metaIndex;
  $scope.newsCatalog= newsCatalog;
  $scope.guid = guids();
  $scope.name = null;
  $scope.site = site;
  $scope.ParentId ='';
  $scope.type = type[0];
  $scope.group = metaIndex[0];
  $scope.visible = 1;
  $scope.ckContent='';
  $scope.Catalog =newsCatalog[0];
  $scope.TargetAry = [{'name':'本地開啟','value':'_self'},{'name':'外部超連結','value':'_blank'}];
  $scope.Link='';
  $scope.Target=$scope.TargetAry[0];
  //  This close function doesn't need to use jQuery or bootstrap, because
  //  the button has the 'data-dismiss' attribute.
  $scope.close = function() {
    close({
      Guid:$scope.guid,
      Subject: $scope.name,
      WebSiteName: $scope.site,
      ParentId:$scope.ParentId,
      MetaId: $scope.group.Id,
      Visible:   $scope.visible,
      DataType: $scope.type.DataType,
      Category: $scope.Catalog.Id,
      // $scope.Catalog.Id,
      Article: $scope.ckContent,
      Link :$scope.Link,
      Target :$scope.Target.value,
      upFile :''
    }, 500); // close, but give 500ms for bootstrap to animate
  };

  //  This cancel function must use the bootstrap, 'modal' function because
  //  the doesn't have the 'data-dismiss' attribute.
  $scope.cancel = function() {

    //  Manually hide the modal.
    $element.modal('hide');
    
    //  Now call close, returning control to the caller.
    close({
      name: $scope.name,
      age: $scope.age
    }, 500); // close, but give 500ms for bootstrap to animate
  };
  function guids() {
    function s4() {
      return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
      s4() + '-' + s4() + s4() + s4();
  }
}]).controller('editModalController', [
  '$scope', '$element','data', 'title','type','guid','site','metaIndex','newsCatalog', 'close', 
  function($scope, $element,data, title,type,guid,site,metaIndex,newsCatalog, close) {
  // api data
  $scope.nodeType = type;
  $scope.title = '修改頁面名稱：'+data.Subject;
  $scope.metaIndex = metaIndex;
  $scope.newsCatalog= newsCatalog;
  //
// var thismetaId = list;

  //
  $scope.guid = guid;
  $scope.name = data.Subject;
  $scope.site = site;

  //網站類型
  $scope.type = '';
  $scope.editData='true';
  type.forEach(function(element, index, array) {
     if(element.DataType == data.DataType){
      $scope.type = type[index];
     }
    });
  //分類群組
  $scope.group = '';
  metaIndex.forEach(function(element, index, array) {
     if(element.Id == data.MetaId){
      $scope.group = metaIndex[index];
     }
    });
  $scope.TargetAry = [{'name':'本地開啟','value':'_self'},{'name':'外部超連結','value':'_blank'}];
  $scope.Link = data.Link;
  $scope.Target='';
  $scope.TargetAry.forEach(function(element, index, array) {
     if(element.value == data.Target){
      $scope.Target=$scope.TargetAry[index];
     }
    });
  //訊息子類別
  $scope.Catalog ='';
  newsCatalog.forEach(function(element, index, array) {
     if(element.Id == data.Category){
      $scope.Catalog = newsCatalog[index];
     }
    });
  $scope.visible = 1;
  $scope.ckContent=data.Article;
  
  //  This close function doesn't need to use jQuery or bootstrap, because
  //  the button has the 'data-dismiss' attribute.
  $scope.close = function() {
    close({
      Guid:$scope.guid,
      Subject: $scope.name,
      WebSiteName: $scope.site,
      MetaId: $scope.group.Id,
      Visible:   $scope.visible,
      DataType: $scope.type.DataType,
      Category: $scope.Catalog.Id,
      // $scope.Catalog.Id,
      Article: $scope.ckContent,
      Link :$scope.Link,
      Target :$scope.Target.value,
      upFile :''
    }, 500); // close, but give 500ms for bootstrap to animate
  };

  //  This cancel function must use the bootstrap, 'modal' function because
  //  the doesn't have the 'data-dismiss' attribute.
  $scope.cancel = function() {

    //  Manually hide the modal.
    $element.modal('hide');
  };
  function guids() {
    function s4() {
      return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
      s4() + '-' + s4() + s4() + s4();
  }
}]).controller('defaultModalController', [
  '$scope', '$element','data', 'title','guid','site','origin', 'close', 
  function($scope, $element,data, title,guid,site,origin, close) {
  // api data
  //
// var thismetaId = list;
  $scope.title=title;
  //
  $scope.guid = guid;
  $scope.site = site;


  //訊息子類別
  $scope.default = data;
  // $scope.childDefault = data[0];
  data.forEach(function(element, index, array) {
     if(element.siteMapNode['@UnID'] == origin['@Default']){
      $scope.childDefault = data[index];
     }
    });
  
  //  This close function doesn't need to use jQuery or bootstrap, because
  //  the button has the 'data-dismiss' attribute.
  $scope.close = function() {
    var thisDefault = $scope.childDefault;
   
    if(thisDefault === null){
      thisDefault = '';
    }else{
      thisDefault = $scope.childDefault.siteMapNode['@UnID'];
    }
    close({
      unId:$scope.guid,
      Id: $scope.site,
      defaultValue: thisDefault

    }, 500); // close, but give 500ms for bootstrap to animate
  };

  //  This cancel function must use the bootstrap, 'modal' function because
  //  the doesn't have the 'data-dismiss' attribute.
  $scope.cancel = function() {

    //  Manually hide the modal.
    $element.modal('hide');
  };
 
}]);