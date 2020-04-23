﻿$(document).ready(function($) {

(function (){
  var url = window.location.href;
  var thispage = function(name){
    var testing = url.match(name)!=null;
    return testing;
  };

if(thispage('Product') == true)
{
  $('.menu-1').addClass('active');
}
else if(thispage('OnlinePrice') == true || thispage('FastPrice') == true || thispage('FastPrice') == true )
{
  $('.menu-2').addClass('active');
}
else if(thispage('Case') == true)
{
  $('.menu-3').addClass('active');
}  
else if(thispage('QA') == true)
{
  $('.menu-4').addClass('active');
} 
else if(thispage('Forum') == true)
{
  $('.menu-5').addClass('active');
} 
})();


 // $(".iframe").colorbox({iframe:true,rel:'group2', width:"500px", height:"400px"});
 $('.iframe').click(function(e){
   e.preventDefault();
var w = 510;
            var h = 500;
        var left = (screen.width/2)-(w/2) - 20;
        var top = (screen.height/2)-(h/2) - 60;
var targetWin = window.open('/home/message', 'newwin', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w +', height=' + h + ', top='+top+', left='+left);
 })
 

$('.oncall').attr('style','right: -280px;');
$(".jq-call").click(function (e) {
    e.preventDefault();
if($('.oncall').attr('style') == 'right: -280px;'){
$('.oncall').animate({
        right: "-115px"}
      ,200);
 
  }

else{
$('.oncall').animate({
        right: "-280px"}
      ,200);
 
  }
}
    );
$(".del").click(function () {

            $.post('/shoppingcar/RemoveShoppingCar', { id: $(".del").index($(this)) }, function (data) {
$('.jq-num').text(data);
            });
   
    $(this).parent().parent().remove();
    var sumPrice = 0;
    var totalPrice = 0;
    $(".trCar").each(function () {
      
        sumPrice += parseInt($(this).find(".Price").val()) ;
        totalPrice += parseInt($(this).find(".totalPrice").val());
    });
    $('.jq-shopping-smallcount').text(sumPrice);
    $('.jq-shopping-bigcount').text(totalPrice);

if($('.cart tr').length == 1){
$('.jq-shopping-smallcount').text('0');
$('.jq-shopping-bigcount').text('0');
}
alert('已刪除');
        });



  $('.addCar').click(function(){

  //抓出當前section區塊
  var block = $(this).parent().parent().parent();
  //抓出產品名稱
  var _subject = $(block).find('h3').text() ;
  //抓出當前表格
  var table = $(block).find('table');
  var tr = $(table).find('tbody tr');
  var tr_total = $(table).find('tbody tr').length;
  var valid_number = $(block).find('.number_acount');

  //確認此產品是否有數量產品，如無則彈跳視窗警告
  var i = 0;
  $(valid_number).each(function(){
    if($(this).val() == 0){
    i = i+ 1;
    };
  });
  if (i == tr_total){
    alert('您尚未購買任何產品');
    return false;
  }

 $(tr).each(function(index){
    var _Quantity = $(this).find('td:last').find('input').val();
    if(_Quantity > 0){

        var _Productkey = $(this).find('.jq-productkey').attr('value');
        var _ProductId = $(this).find('.jq-ProductId').attr('value');
        var _SpacID = $(this).find('.jq-SpacID').attr('value');
        var _OnSaleType = $(this).find('.jq-OnSaleType').attr('value');
        

      var _ModelNumber = $(this).find('td').eq(0).text();
      var _Spac = $(this).find('td').eq(1).text();

        
      $(this).find('.jq-productall').each(function(index){
        //var _start = $(this).find('.jq-productstart').attr('value');
        //var _end = $(this).find('.jq-productend').attr('value');
        var _Price = $(this).find('.jq-price').text();
        var _TotalPrice = _Price * _Quantity;
      
       // if(parseInt(_end) >= parseInt(_Quantity) && parseInt(_Quantity) >= parseInt(_start) ){
            //alert(_subject+','+_Productkey+','+_ModelNumber+','+_Spac+','+_Price+','+_Quantity+','+_TotalPrice);
            $.ajaxSetup({ async: false });
            $.post('/Product/AddShoppingCar', { Subject: _subject, Productkey: _Productkey, ModelNumber: _ModelNumber, Spac: _Spac, Price: _Price, Quantity: _Quantity, TotalPrice: _TotalPrice, ProductId: _ProductId, SpacID: _SpacID, OnSaleType: _OnSaleType }, function(data) {
                $('.jq-num').text(data);
                //alert($(".jq-message").val() + "已加入" + _subject + "(型號:" + _ModelNumber + "規格:" + _Spac + ") 價格:" + _Price + "共" + _Quantity + "個\n");
                
            });
           
        //};


      }) ;     
      
    };
  });
      alert("商品已經加入購物車");
      return false;
  });



  










  //$('.search-box').attr('value', '請輸入關鍵字..');
  //$(".search").delegate(".search-box", "click", function() {
  //  $('.search-box').attr('value', '');
  //});
  //$(".search").delegate(".search-box", "blur", function() {
  //  $('.search-box').attr('value', '請輸入關鍵字..');
  //});
  //Q&A輪播
  $(".qa-hot h3:eq(0)").addClass("active");
  $(".qa-hot div:eq(0)").show();
  $(".qa-hot").delegate("h3:not(h3.active)", "click", function() {
    $(".qa-hot h3").removeClass("active");
    $(this).addClass("active");
    $(this).next('div').slideDown().siblings("div").slideUp();
    $('.trunk').trunk8();
  });
  
  //QA列表
  $('.qa-section .answer').eq(0).show();
  $('.qa-section h3').eq(0).addClass('active');
  $(".qa-section section").delegate("h3", "click", function () {
      var postnum = $(this).attr('data-anchor');
       $.post('/QA/QACounter', { id: postnum }, function (data) {
console.log(data);
            });
      var thissection = $(this);
      $(".qa-section section h3").each(function () {
          if ($(this) != thissection) {
              $(this).removeClass("active");
              var qa_answer = $(this).parent().parent().find('.answer');
              $(qa_answer).hide();
          }
      });

      if ($(this).attr('class') == 'active') {
      $(this).removeClass("active");
    }else{
      $(this).addClass("active");
    }
    var qa_answer = $(this).parent().parent().find('.answer');
    $(qa_answer).slideToggle(500);
    setTimeout(tar(), 3000 )
    function tar(){
      $('html,body').animate({ scrollTop: thissection.offset().top }, 1500);
    } 

  });
  //trunk

  //會員表頭彈跳
  $(".member").delegate(".member-name", "click", function() {
    if($(this).attr("class") ==  'member-name'){
      $(this).attr('class', 'member-name active');
    $('.member-bar').slideDown();
  }else{
    $(this).attr('class', 'member-name');
    $('.member-bar').hide();
  }
  });

  //選單hover
   $("#product-menu ul li:not(#product-menu ul li li)").hover(function(){
    $(this).find('ul:first').stop(true, true).slideDown("");
    $(this).find('a:first').addClass("hover");
     }, function(){
        $(this).find('ul:first').stop(true, true).hide();
        $(this).find('a:first').removeClass("hover");
    });
  //youtube
  var youtube_sub = $('.youtube-sub input:first').attr('value')
  $('.youtube-main').html('').html(youtube_sub);
$(".youtube-sub li").delegate("a", "click", function(e) {


var youtube_click = $(this).children('input').attr('value');
$('.youtube-main').html('').html(youtube_click)
e.preventDefault();
});
  //last margin right
$('.dindan-sub #pab1 li:last,.dindan-sub #pab2 li:last,#case .tab a:last').addClass("last");
$('.promote-inner section').last().addClass("last");
$('#footer-inner li:nth-child(3n)').addClass("last");
$('.p-box section:nth-child(3n+1)').addClass("last");
$('.p-box-2 section:nth-child(3n)').addClass("last");
$('.p-box-2 section:nth-child(3n+1)').removeClass("last");
$('.case-category:nth-child(3n+1)').addClass("last");

//論壇列表tab

  //header fixed
  //var $cart = $('#top'),
  //  _top = $cart.offset().top;
  //var $win = $(window).scroll(function() {
  //  if ($win.scrollTop() > _top) {
  //    if ($cart.css('position') != 'fixed') {
  //      $cart.css({
  //        position: 'fixed',
  //        top: 0
  //      });
  //    }
  //  } else {
  //    $cart.css({
  //      position: 'static'
  //    });
  //  }
  //});
  // ind tab slide
$('.partner img').eq(4).hide();
//forum tab
  var _showTab = 0;
  $('.forum-tab li:eq(0)').addClass("active")
  $('.forum-tab').each(function() {
    var $tab = $(this);
    $('.tab_content', $tab).hide().eq(_showTab).show();
    $('.tab li', $tab).hover(function() {
      var $this = $(this),
        _clickTab = $this.find('a').attr('href');
      $this.addClass('active').siblings('.active').removeClass('active');
      $(_clickTab).stop(false, true).fadeIn().siblings().hide();
    });
  });
  var _showTab = 0;
  $('.court_tab').each(function() {
    var $tab = $(this);
    $('.tab_content', $tab).hide().eq(_showTab).show();
    $('.tab li', $tab).hover(function() {
      var $this = $(this),
        _clickTab = $this.find('a').attr('href');
      $this.addClass('active').siblings('.active').removeClass('active');
      $(_clickTab).stop(false, true).fadeIn().siblings().hide();
    });
  });
//index promote
var _showTab = 0;
  $('.dindan-sub li:eq(0)').addClass("active")
  $('.dindan-sub').each(function() {
    var $tab = $(this);
    $('.tab_content', $tab).hide().eq(_showTab).show();
    $('.tab li', $tab).hover(function() {
      var $this = $(this),
        _clickTab = $this.find('a').attr('href');
      $this.addClass('active').siblings('.active').removeClass('active');
      $(_clickTab).stop(false, true).fadeIn().siblings().hide();
    });
  });

//product tab
var _showTab = 0;
  $('.pd-tab-box').each(function() {

    var $tab = $(this);
    $('.tab_content', $tab).hide().eq(_showTab).show();
    $('.pd-tab li', $tab).hover(function() {
      var $this = $(this),
        _clickTab = $this.find('a').attr('href');
      $this.addClass('active').siblings('.active').removeClass('active');
      $(_clickTab).stop(false, true).fadeIn().siblings().hide();
    });
  }); 
  $('.choose-box .number').each(function(index){
    
    $(this).text(index+1)
  });
  $(".choose-box").delegate(".jq-cell", "change", function () {
      var _select_box = $(this).parent();
      var SelectProductSpac = $(this).next();
    var _select_word = $(this).find(":selected").val();
      $.getJSON("../GetFastPriceProductSpacJson", {
        id: _select_word
      }).done(function (data) {
          SelectProductSpac.children().remove();
          $.each(data, function (key, val) {
              SelectProductSpac.append($("<option></option>").attr("value", val.id).text(val.Subject));
          });
          //if (SelectProductSpac.children().size() > 1) {
          //    SelectProductSpac.children().eq(0).remove();
          //}
      }).fail(function (jqxhr, textStatus, error) {
        //var err = textStatus + ", " + error;
          SelectProductSpac.children().remove();
          SelectProductSpac.append($("<option></option>").attr("value", 0).text("不搭配"));
      });
   
  
    if(_select_word != 0){
      $(_select_box).addClass("active");
    }
    else{
       $(_select_box).removeClass("active");
    }
  });
});

 
