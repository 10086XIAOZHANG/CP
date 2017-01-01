var t = n = 0, count;
$(document).ready(function () {
    $('.wp_menu').append('<li class="wp_li"><a href="http://www.shvtc.com"><span class="w_line">学校首页</span></a></li>');
    count = $("#wheel_list a").length;
    $("#wheel_list span:not(:first-child)").hide();
    $("#wheelwen span:not(:first-child)").hide();
    // var Wwen=$("#wheelwen").width()+35;
    //$(".tmc").css({"width":Wwen});
    $("#wheelyu li").click(function () {
        var i = $(this).text() - 1; //获取Li元素内的值，即1，2，3，4      
        n = i;
        if (i >= count) return;
        $("#wheel_list span").filter(":visible").fadeOut(1000).parent().children().eq(i).fadeIn(1500);
        $("#wheelwen span").filter(":visible").hide().parent().children().eq(i).show();
        document.getElementById("wheelyu").style.background = "";
        $(this).toggleClass("on");
        $(this).siblings().removeAttr("class");
        //var Wwen=$("#wheelwen").width()+50;
        //$(".tmc").css({"width":Wwen});

    });
    t = setInterval("showAuto()", 8000);
    $("#wheelyu").hover(function () { clearInterval(t) }, function () { t = setInterval("showAuto()", 4000); });
    $("#wheel>.prev,#wheel>.next").hover(function () { clearInterval(t) }, function () { t = setInterval("showAuto()", 4000); });
    $("#wheel>.prev").click(function () {

        n = n <= 0 ? (count - 1) : --n;
        alert(n);
        $("#wheelyu li").eq(n).trigger('click');

    });
    $("#wheel>.next").click(function () {
        n = n >= (count - 1) ? 0 : ++n;
        $("#wheelyu li").eq(n).trigger('click');

    });

});
function showAuto() {
    n = n >= (count - 1) ? 0 : ++n;
    $("#wheelyu li").eq(n).trigger('click');
}

 
//1
