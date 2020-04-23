App.factory('toolService', function($http) {
    return {
        guid: function() {
                function s4() {
                    return Math.floor((1 + Math.random()) * 0x10000)
                        .toString(16)
                        .substring(1);
                }
                return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                    s4() + '-' + s4() + s4() + s4();
        },
        datahandle:function(data){
            data = data.siteMap.siteMapNode;
            function forData(obj){
                for(var j=0;obj.siteMapNode.length>j;j++){
                     var thisobj=obj.siteMapNode[j];
                    if(thisobj['@DataType']=='directory'){
                         if(thisobj.siteMapNode===undefined ){
                            thisobj.siteMapNode=[];
                          }
                        if(thisobj.siteMapNode instanceof Array === false){
                            thisobj.siteMapNode = [thisobj.siteMapNode];
                        }
                        if(thisobj.siteMapNode.length !== 0){
                            forData(thisobj);
                        }
                    }
                }
            }
            for(var i=0;data.length>i;i++){
                 if(data[i].siteMapNode===undefined ){
                    data[i].siteMapNode=[];
                  }
                if(data[i].siteMapNode instanceof Array === false){
                    data[i].siteMapNode = [data[i].siteMapNode];
                }
                if(data[i]['@DataType']=='directory'){
                    forData(data[i]);
                }
            }
            if(data instanceof Array === false){
                data = [data];
                return data;
            }
            else{
                return data;
            }
        }
    };
});
