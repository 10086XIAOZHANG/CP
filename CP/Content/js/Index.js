var AdminJumpUrl = "";
var state = true;

$(document).ready(function () {
    $("#liGrzx").css("display", "none");
    IndexLogin();
});
$(function () {

    document.onkeydown = function (e) {
        var ev = document.all ? window.event : e;
        if (ev.keyCode == 13 && ev.srcElement.id != "txts") {
            var loginState = $("#loginIdentify").val();
            switch (loginState) {
                case 0:
                    StudentLogin();
                    break;
                case 1:
                    AdminLogin();
                    break;
                default:
            }
        }
    }

    $("#head").addClass("selected");
    //新生/管理员登录后信息显示隐藏控制
    $(".kind span").click(function () {
        if ($("#loginIdentify").val() == 0||$("#loginIdentify").val() == 1) {
            $(this).addClass("alt").siblings().removeClass("alt");
            var word = $(".alt").text();
            $(".LoginTile").text(word);
            if (word == "普通用户登录") {
                $(".dis").css("display", "block");
                $(".dis1").css("display", "none");
                $("#loginIdentify").val(0)
            } else {
                $(".dis").css("display", "none");
                $(".dis1").css("display", "block");
                $("#loginIdentify").val(1)
            }
        }
    });


    var strs = new Array();
    $('.date').each(function () {
        var time = $(this).text();
        $(this).text("");
        strs = time.split("/");
        var temp = "<span>" + strs[0] + "</span><br/><span>" + strs[1] + '-  ' + strs[2] + "</span>";
        $(this).append(temp);
    });
});
//首页登陆认证
function IndexLogin() {
    var loginState = $("#loginIdentify").val();//定义了常量
    if (loginState == 3) {
        $.ajax({
            type: "Get",
            url: "/Home/GetUserinfoModel",
            dataType: "json",
            success: function (data) {
                if (data != null) {                  
                    stuLoginShow(data);
                    stuLoginSuccess();
                } else {
                    $.MsgBox.Alert("提示消息", "请重新登录！");
                  
                }
            }

        });
          
            }
        }


function adminPublic(json) {
    var data = json.Data.result;
    if (data != "") {
        adminLoginShow(data);
        state = false;
        loginState = 4;
        $("#stuJmp").html("进入管理员界面");
        $("#adminLogin").click();
        $(".Afterlogin").css("display", "block");
        $(".Beforelogin").css("display", "none");
        $("#loginState").attr("value",1);
    }
}



学生登陆
function StudentLogin() {
    if ($("#StuLoginSubmit").html() == "登陆中")
        return;
    $("#StuLoginSubmit").html('登陆中');
    var xm = $("#stuXm").val();
    var psd = $("#stuPsd").val();
    if (xm == "") {
        $(".saveStyle").removeClass("finish");
        $("#alert").css('display', 'block');
        $.MsgBox.Alert("提示消息", "用户名为空，请输入用户名！");
        $("#StuLoginSubmit").html('登陆');
        return;
    }
    if (psd == "") {
        $(".saveStyle").removeClass("finish");
        $("#alert").css('display', 'block');
        $.MsgBox.Alert("提示消息", "密码为空，请输入密码！");
        $("#StuLoginSubmit").html('登陆');
        return;
    }
    var o = new Object();
    o["Xm"] = xm;
    o["Psd"] = psd;
    $.ajax({
        type: "POST",
        url: "/Home/StudentLogin",
        data: { userNum: xm,userPsd:psd },
        dataType: "json",
        success: function (data) {
             if (data != null) {
            state = true;
            //window.location.href = "/PersonalCentre/Index";
                 //$("#stuJmp").html("进入个人界面");
                 //alert(data[0].qq + "11");
            $("#loginIdentify").val(3);
            stuLoginShow(data);
            stuLoginSuccess();
        } else {
            $(".saveStyle").removeClass("finish");
            $.MsgBox.Alert("提示消息", "您输入的信息不正确");
            $("#StuLoginSubmit").html('登陆');
        }
        }
    });
   
}

//管理员登陆信息显示
function adminLoginShow(data) {
    $("#stuTop").html("<span class='blue'>" + data.Question + "</span>管理员欢迎您登陆迎新服务系统");
    var src = (data.Answer == null) ? "/Content/img/pic.jpg" : data.Answer;
    $("#stuImg").attr('src', src);
    $("#stuShowXm").html("姓名：" + data.Question);
    $("#stuShowXb").html("部门：" + data.Email);
    $("#stuShowYx").html("管理员编号：" + data.SafePassword);
    AdminJumpUrl = data.Note;
    loginState=4;
}

//学生登陆信息显示
function stuLoginShow(data) {
    var photoinit = "/Content/img/num0000.png";
    $("#stuTop").html("<span class='blue'>" + data.userName + "</span>同学欢迎您来到江西师大");
    var logo = (data.photoAddress == null) ? photoinit : data.photoAddress+"";
    $("#stuImg").attr('src', logo);
    $("#stuShowXh").html("学号：" + data.userId);
    $("#stuShowXm").html("姓名：" + data.userName);
    $("#stuShowXb").html("性别：" + data.sex);
   
    loginState = 3;
}

//登陆成功
function stuLoginSuccess() {
    $(".Afterlogin").css("display", "block");
    $(".Beforelogin").css("display", "none");
    $("#liGrzx").css("display", "block");
    //$("#stuJmp").click();
    
}
//登出
function LoginOut() {
    $("#liGrzx").css("display", "none");
    $.ajax({
        type: "Get",
        url: "/Home/Logout",
        dataType: "html",
        success: function (data) {
            if (data = "ok") {
                $("#stuXm").val('');
                $("#stuPsd").val('');
                $("#UserName").val('');
                $("#Password").val('');
                $(".Beforelogin").css("display", "block");
                $(".Afterlogin").css("display", "none");
                if ($("#loginIdentify") .val()== 3) {
                    $("#loginIdentify").val(0);
                    $("#StuLoginSubmit").html('登入');
                } else {
                    $("#loginIdentify").val(1);
                    $("#StuLoginSubmit").html('登入');
                }
            }
        }
    });
}
//跳新页面
function jumpNewPage() {
    if (state)
        window.location.href = "/PersonalInfoManage/BasicInfo";
    else
        window.location.href = AdminJumpUrl;
}


//管理员登陆
function AdminLogin() {
    if ($("#StuAdminSubmit").html() == "登陆中")
        return;
    $("#StuAdminSubmit").html('登陆中');
    var login = new Object();
    login["UserName"] = $("#UserName").val();
    login["UserScrete"] = $("#Password").val();
    $.post("/systems/system/login", login, function (result) {
        var cache = result.split('|');
        var response = (cache.length == 1) ? JSON.parse(cache[0]) : JSON.parse(cache[1]);
        switch (response.Code) {
            case 1:
                $("#loginState").val(1);
                location.href = response.Message;
                break;
            case 2:
                $(".saveStyle").removeClass("finish");
                $("#alert").css('display', 'block');
                $(".info_sf").text(response.Message);
                $("#StuAdminSubmit").html('登陆');
                break;
            default:
                $("#StuAdminSubmit").html('登陆');
                break;
        }
    });
}
