$(function () {
    //tooltip  全局
    $(document).on("mouseover", "[data-toggle='tooltip']", function () {
        $(this).tooltip('show')
    })

    //#region 顶部搜索
    $("#btn_global_search").click(function () {
        var keyword = $("#ipt_global_search").val().trim();
        if (keyword == "") {
            return;
        }
        window.open("/?kw=" + encodeURIComponent(keyword));
    })

    $("#ipt_global_search").keyup(function () {
        if (event.keyCode == 13) {
            $("#btn_global_search").click();
        }
    })

    var kw = getUrlParam("kw");
    if (kw) {
        $("#ipt_global_search").val(kw);
        search(kw);
    }
    //#endregion

    //#region  登录注册页

    //登录
    $("#panel-login .btn-login").click(function (e) {
        e.preventDefault();
        var $form = $("#panel-login form");
        if (!$form.valid())
            return;
        var data = $form.serialize();
        $.ajax({
            type: "post",
            url: "/Member/Login",
            dataType: "json",
            data: data,
            success: function (result) {
                if (result.code == 10000) {
                    $("#panel-login .close").click();
                    layer.msg("登录成功");
                    var returnUrl = getUrlParam("returnUrl");
                    if (returnUrl != null) {
                        location.href = returnUrl;
                    } else {
                        $(".nav-login").html("<p>{0}<br />{1} <a href='/Member/Logout'>注销</a></p><a href='/account/index'><img class='avatar' src='/{2}' height='42' width='42' /></a>".format(
                            result.content.nickName, result.content.email, result.content.avatar));
                    }   
                } else {
                    layer.msg(result.message, function () { });             
                }
            },
            error: function (ex) {
                layer.msg("异常", function () { });
            }
        })
    })
    
    //获取验证码
    $("#panel-register .btn-verifyCode").click(function () {
        var $ct = $(this);
        var disabled_css = "dis_vc";
        var $inputEmail = $("#RegisterInput_Email");
        if ($(this).hasClass(disabled_css) || !$inputEmail.valid()) {
            return;
        }

        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Member/SendVerifyCode",
            data: { email: $inputEmail.val() },
            success: function (result) {
                if (result.code == 10000) {
                    layer.msg("验证码已发送");
                    countDown(60, $ct, $ct.val(), disabled_css);
                } else {
                    layer.msg(result.message, function () { });
                }
            },
            error: function (ex) {
                layer.msg("异常", function () { });
            }
        })
    })

    //提交注册表单
    $("#panel-register .btn-register").click(function (e) {
        e.preventDefault();
        var $form = $("#panel-register form");
        if (!$form.valid())
            return;
        var data = $form.serialize();

        $.ajax({
            type: "post",
            url: "/Member/Register",
            dataType: "json",
            data: data,
            success: function (result) {
                if (result.code == 10000) {
                    location.href = "/account/index";
                } else {
                    layer.msg(result.message, function () { });
                }
            },
            error: function (ex) {
                layer.msg("异常", function () { });
            }
        })
    })

    //#endregion

    //#region 搜索块
    $(".condition .label").click(function () {
        if ($(this).hasClass("label-primary") || $(this).hasClass("lb-more"))
            return;
        $parent = $(this).parent();
        $parent = $parent.hasClass("more") ? $parent.parent() : $parent;
        $parent.find(".label-primary").addClass("label-default").removeClass("label-primary")
        $(this).addClass("label-primary").removeClass("label-default");
        //自定义时间
        if ($(this).hasClass("cdt-time-custom")) {
            $("#datetimeStart,#datetimeEnd,.btn-year-confirm").prop("disabled", false);
        } else {
            if ($parent.hasClass("cdt-time")) {
                $("#datetimeStart,#datetimeEnd,.btn-year-confirm").prop("disabled", true);
            }
            search(null,1);
        }
    })

    //自定义时间 确定
    $(".btn-year-confirm").click(function () {
        var datetimeStart = $("#datetimeStart").val();
        var datetimeEnd = $("#datetimeEnd").val();
        $(".cdt-time-custom").attr({ "data-start": datetimeStart, "data-end": datetimeEnd })
        search(null,1);
    })

    //#endregion
    
    //评分
    $(".rating").on("change", function () {
        var movieId = $("#movieId").val();
        var score = $(this).val();
        $(this).rating('refresh', {
            disabled: true
        });

        var data = {
            movieId:movieId,
            score:score
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Movie/MovieRating",
            contentType:"application/json",
            data: JSON.stringify(data),
            success: function (result) {
                if (result.code == 10000) {
                    layer.msg("感谢支持！");
                } else if (result.code == 11005) {
                    $('#panel-login').modal();
                }  else {
                    layer.msg(result.message, function () { });
                }
            },
            error: function (ex) {
                layer.msg("异常", function () { });
            }
        })
    })

    //#region  留言

    //回复(引用)
    //$(".comment-result").on("click","a.reply",function () {
    //    $("#parentId").val($(this).attr("data-id"));
    //    var m = $(this).parent().next().html();
    //    m = m.length < 10 ? m.substr(0, m.length) : m.substr(0, 10);
    //    $("#txt-comment").val("@" + $(this).attr("data-l") + "[" + m + "...]：").focus();
    //})

    //提交留言
    $("#btn-comment").click(function () {
        var movieId = $("#movieId").val();
        var content = $("#txt-comment").val().trim();
        if (content.length < 2) {
            layer.msg("能再短一点？", function () { });
            return;
        }

        var data = {
            movieId: movieId,
            //parentId: $("#parentId").val(),
            content: content
        };
        $.ajax({
            type:"post",
            dataType: "json",
            url: "/Movie/AddMovieComment",
            headers:{
                "RequestVerificationToken": $("input[name='__RequestVerificationToken']").val()
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (result) {
                if (result.code == 10000) {
                    location.href = location.href;
                } else if (result.code == 11005) {
                    $('#panel-login').modal();
                } else {
                    layer.msg(result.message, function () { });
                }
            },
            error: function (ex) {
                layer.msg("异常", function () { });
            }
        })
    })

    //获取留言列表
    $(".comment .pagination").on("click", "li:not('.disabled,.active') a", function () {
        var pageIndex = $(this).attr("data-page");
        var movieId = $("#movieId").val();
        var pageSize = 10;
        GetCommentList(pageIndex, pageSize, movieId);
    })
    function GetCommentList(pageIndex, pageSize,movieId) {
        var load = layer.load(3, { shade: [0.1, '#000'] });
        $.ajax({
            type: "get",
            dataType: "json",
            url: "/Movie/GetMovieComments",
            data: {
                condition: movieId,
                pageIndex: pageIndex,
                pageSize: pageSize
            },
            success: function (result) {
                if (result.code == 10000) {
                    var html = "";
                    var commentList = result.content.data;
                    $.each(commentList, function (i, comment) {
                        html += "<div><img class='avatar pull-left' src='/" + comment.userAvatar + "' height='38' width='38'>\n\
                            <p class='u-info'>" + comment.userNickName + " &nbsp;&nbsp; <span class=\"pull-right\">" + comment.addTime + "</span></p>\n\
                            <p class='conment-cnt'>"+ comment.content + "</p>\n";
                        html += "</div>\n<hr />\n";
                    })
                    $(".comment-result").html(html);
                    $(".comment .pagination").html(paging(pageIndex, result.content.totalCount, pageSize, 3));
                    $(document).scrollTop(0);
                }
                else {
                    layer.msg(result.message, function () { });
                }
                layer.close(load);
            }
        })
    }

    //#endregion

    //#region  提问
    
    //提交问题
    $("#btn-question").click(function () {
        var movieId = $("#movieId").val();
        var title = $("#txt-question-title").val().trim();
        var content = $("#txt-question-content").val().trim();
        if (title.length < 5) {
            layer.msg("标题过短", function () { });
            return;
        }

        var data = {
            movieId: movieId,
            title: title,
            content: content
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Movie/AddQuestion",
            headers: {
                "RequestVerificationToken": $("input[name='__RequestVerificationToken']").val()
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (result) {
                if (result.code == 10000) {
                    location.href = location.href;
                } else if (result.code == 11005) {
                    $('#panel-login').modal();
                } else {
                    layer.msg(result.message, function () { });
                }
            },
            error: function (ex) {
                layer.msg("异常", function () { });
            }
        })
    })

    //获取问题列表
    $(".question .pagination").on("click", "li:not('.disabled,.active') a", function () {
        var pageIndex = $(this).attr("data-page");
        var movieId = $("#movieId").val();
        var pageSize = 20;
        GetQuestionList(pageIndex, pageSize, movieId);
    })
    function GetQuestionList(pageIndex, pageSize, movieId) {
        var load = layer.load(3, { shade: [0.1, '#000'] });
        $.ajax({
            type: "get",
            dataType: "json",
            url: "/Movie/GetQuestions",
            data: {
                condition: movieId,
                pageIndex: pageIndex,
                pageSize: pageSize
            },
            success: function (result) {
                if (result.code == 10000) {
                    var html = "";
                    var list = result.content.data;
                    $.each(list, function (i, item) {
                        html += "<p><span class='pull-right'>回答(" + item.answerCount + ")</span><a target='_blank' href='/movie/question/" + item.id + "'>" + item.title + "</a></p>\n";
                    })
                    $(".question-result").html(html);
                    $(".question .badge").html(result.content.totalCount);
                    $(".question .pagination").html(paging(pageIndex, result.content.totalCount, pageSize, 3));
                    $(document).scrollTop(0);
                } else {
                    layer.msg(result.message, function () { });
                }
                layer.close(load);
            }
        })
    }

    //#endregion

    //#region 问题详情页

    $(".question-detail .pagination").on("click", "li:not('.disabled,.active') a", function () {
        var pageIndex = $(this).attr("data-page");
        var questionId = $("#questionId").val();
        var pageSize = 10;
        GetReplyList(pageIndex, pageSize, questionId);
    })    

    //回答
    $(".question-detail #btn-reply").click(function () {
        var replyContent = $("#txt-question-reply").val().trim();
        if (replyContent.length < 2) {
            layer.msg("能再短一点？", function () { });
            return;
        }
        var data = {
            movieQuestionId: $("#questionId").val(),
            content: replyContent
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Movie/AddQuestionAnswer",
            headers: {
                "RequestVerificationToken": $("input[name='__RequestVerificationToken']").val()
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (result) {
                if (result.code == 10000) {
                    location.href = location.href;
                } else if (result.code == 11005) {
                    $('#panel-login').modal();
                } else {
                    layer.msg(result.message, function () { });
                }
            },
            error: function (ex) {
                layer.msg("异常", function () { });
            }
        })
    })

    //赞
    $(".question-detail").on("click", "span.glyphicon-thumbs-up", function () {
        var $support = $(this);
        if ($support.css("cursor") == "default") {
            return;
        }
        var data = {
            answerId:$support.attr("data-id")
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/Movie/SupportAnswer",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (result) {
                if (result.code == 10000) {
                    if (result.content) {
                        layer.msg("赞 +1");
                        var count = parseInt($support.find("font").html()) + 1;
                        $support.find("font").html(count);
                    }
                } else if (result.code == 11005) {
                    $('#panel-login').modal();
                } else {
                    layer.msg(result.message, function () { });
                }
            },
            error: function (ex) {
                layer.msg("异常", function () { });
            }
        })
    })

    //#endregion

    //修改密码
    $(".upd-password .btn").click(function () {
        var oldPwd = $(".upd-password .oldPwd").val()
        var newPwd = $(".upd-password .newPwd").val()
        var newPwdAg = $(".upd-password .newPwdAg").val()
        if (oldPwd =="" || newPwd=="" || newPwdAg == "")
        {
            layer.msg("请填写完整",function(){});
            return;
        }
        if (newPwd.length < 6) {
            layer.msg("密码太短", function () { });
            return;
        }
        if (newPwd != newPwdAg) {
            layer.msg("两次输入的密码不一致", function () { });
            return;
        }
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/account/UpdPassword",
            data: {
                oldPwd: oldPwd,
                newPwd: newPwd
            },
            success: function (result) {
                if (result.Code == 1) {
                    layer.msg("修改成功");
                    $(".upd-password input[type='password']").val("");
                } else {
                    layer.msg("密码错误！", function () { });
                }
            }
        })

    })

    //修改昵称
    $(".upd-nickname").click(function () {
        var open= layer.open({
            type: 1,
            title: "修改昵称",
            btn:["确定"],
            area: ['300px', '170px'],
            content: $(".lay-nickname"),
            yes: function () {
                var newNickname = $(".new-nickname").val().trim();
                if (newNickname != "") {
                    $.ajax({
                        type: "post",
                        dataType: "json",
                        url: "/account/UpdNickname",
                        data: {
                            newNickname: newNickname
                        },
                        success: function (result) {
                            if (result.Code == 1) {
                                layer.msg("修改成功");
                                $("#nickname").html(newNickname);
                                layer.close(open);
                            } else {
                                layer.msg("该昵称已存在...", function(){});
                            }
                        }
                    })
                }
                //
            }
        });
    })

    //数字输入框
    $(document).on("keyup", ".only-num", function (event) {
        //先把非数字的都替换掉,保证第一个为数字而不是0
        //if (event.keyCode < 45 || event.keyCode > 57)
        //    return false;
        $(this).val($(this).val().replace(/[^\d]|^0/g, ""));
    })
})

function GetReplyList(pageIndex, pageSize, questionId) {
    var load = layer.load(3, { shade: [0.1, '#000'] });
    $.ajax({
        type: "get",
        dataType: "json",
        url: "/Movie/GetQuestionAnswers",
        data: {
            questionId: questionId,
            pageIndex: pageIndex,
            pageSize: pageSize
        },
        success: function (result) {
            if (result.code == 10000) {
                var html = "";
                var list = result.content.data;
                $.each(list, function (i, item) {
                    html += "<div class='reply-box'>\n\
                                <img class='avatar pull-left' src='/" + item.userAvatar + "' height='38' width='38'>\n\
                                <p class='u-info'><span class='pull-right glyphicon glyphicon-thumbs-up' data-id='"+ item.id + "' title='赞'><font>" + item.support + "</font></span>" + item.userNickName + " &nbsp;&nbsp; " + item.addTime + "</p>\n\
                                <pre>" + item.content + "</pre>\n\
                            </div><hr />";
                })
                $(".question-result").html(html);
                $(".question-detail .rowsCount").html(result.content.totalCount);
                $(".question-detail .pagination").html(paging(pageIndex, result.content.totalCount, pageSize, 3));
            }

            if (result.Code > 0) {
                
            }
            layer.close(load);
        }
    })
}

//搜索
function search(keyword,pageIndex) {
    var load = layer.load(3, { shade: [0.1, '#000'] });
    var area = $(".cdt-area .label-primary").attr("data-id");
    var type = $(".cdt-type .label-primary").attr("data-id");
    var datetimeStart = $(".cdt-time .label-primary").attr("data-start");
    var datetimeEnd = $(".cdt-time .label-primary").attr("data-end");
    var sort = $(".cdt-sort .label-primary").attr("data-id");
    var pageSize = 1;
    if (!pageIndex) {
        pageIndex = $(".pagination li.active a").attr("data-page") - 0;
        pageIndex = !pageIndex ? 1 : pageIndex;
    }

    var data = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        condition: {
            movieAreaId: area,
            movieTypeId: type,
            beginDate: datetimeStart,
            endDate: datetimeEnd,
            sort: sort,
            keyword: keyword
        }
    };
    $.ajax({
        type: "post",
        url: "/Movie/GetPageList",
        dataType: "json",
        contentType:"application/json",
        data: JSON.stringify(data),
        success: function (result) {
            if (result.code == 10000) {
                var html = "";
                $.each(result.content.data, function (idx, obj) {
                    html += "<div class='col-xs-6 col-md-2'>"
                        + "<a href='/movie/" + obj.id + "' target='_blank' class='thumbnail'><img alt='" + obj.shortName + "' src='" + obj.imgUrl + "'></a>"
                        + "<div class='intro'><p title='" + obj.shortName + "'><span>" + Number(obj.score).toFixed(1) + "</span>" + obj.shortName + "</p></div>"
                        + "</div>";
                })
                if (html == "")
                    html = "<p style='padding-left:14px;'><span class='glyphicon glyphicon-exclamation-sign'></span> 没有找到结果。</p>";
                $(".search-result .list-movies").html(html);
                $(".search-result .pagination").html(paging(pageIndex, result.content.totalCount, pageSize, 3));
            } else {
                layer.msg(result.message, function () { });
            }
            layer.close(load);
        },
        error: function (ex) {
            layer.close(load);
            layer.msg("系统异常，请稍后再试！");
        }
    })
}

//获取验证码倒计时
function countDown(time, btn, btn_txt, disabled_css) {
    btn.addClass(disabled_css);
    downTimer(time);

    function downTimer(t) {
        timer1 = setInterval(function () {
            btn.val(btn_txt + "[" + --t + "]");
            if (t < 1) {
                btn.removeClass(disabled_css);
                btn.val(btn_txt);
                clearInterval(timer1);
            }
        }, 1000)
    }
}

//String.format字符串拼接
String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
}

//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return decodeURIComponent(r[2]); return null; //返回参数值
}

//分页  当前第几页,总条数,每页显示条数,连续显示分页数,是否显示总记录数0 or 1
function paging(pageIndex, total, pageSize, groups) {
    var result = "";
    //当前页
    pageIndex = parseInt(pageIndex);
    //总条数
    total = parseInt(total);
    //每页显示条数
    pageSize = parseInt(pageSize);
    //连续显示分页数
    groups = parseInt(groups);
    //总页数
    var pages = Math.ceil(total / pageSize);

    if (total == 0)
        return result;

    //1    上一页 首页
    if (pageIndex == 1)
        result += "<li class='disabled'><a href='javascript:void(0)'>&laquo;</a></li>";
    else
        result += "<li><a data-toggle='tooltip' data-placement='top' title='第" + (pageIndex - 1) + "页' href='javascript:void(0)' data-page='" + (pageIndex - 1) + "'>&laquo;</a></li><li><a data-toggle='tooltip' data-placement='top' title='第1页' href='javascript:void(0)' data-page='1'>1</a></li>";
    //2    ...
    if (pageIndex > groups + 2)
        result += "<li><a data-toggle='tooltip' data-placement='top' title='第" + (pageIndex - groups - 1) + "页' href='javascript:void(0)' data-page='" + (pageIndex - groups - 1) + "'>...</a></li>";

    //3    本页前页码
    var frontPage = "";
    for (var i = pageIndex - 1; i > pageIndex - (groups + 1) && i > 1; i--) {
        frontPage = "<li><a data-toggle='tooltip' data-placement='top' title='第"+i+"页' href='javascript:void(0)' data-page='"+i+"'>" + i + "</a></li>" + frontPage;
    }
    result += frontPage;

    //4    本页页码
    result += "<li class='active'><a data-toggle='tooltip' data-placement='top' title='第"+pageIndex+"页' href='javascript:void(0)'  data-page='"+pageIndex+"'>"+pageIndex+"</a></li>";

    //5    页后页码
    for (var i = pageIndex + 1; i < pageIndex + (groups + 1) && i < pages; i++)
        result += "<li><a data-toggle='tooltip' data-placement='top' title='第"+i+"页' href='javascript:void(0)' data-page='"+i+"'>"+i+"</a></li>";

    //6    ...
    if (pageIndex + groups + 1 < pages)
        result += "<li><a data-toggle='tooltip' data-placement='top' title='第"+(pageIndex + groups + 1)+"页' href='javascript:void(0)' data-page='"+(pageIndex + groups + 1)+"'>...</a></li>";

    //7    尾页 下一页
    if (pageIndex == pages)
        result += "<li class='disabled'><a href='javascript:void(0)'>&raquo;</a></li>";
    else
        result += "<li><a data-toggle='tooltip' data-placement='top' title='第" + pages + "页' href='javascript:void(0)' data-page='" + pages + "'>" + pages + "</a></li><li><a data-toggle='tooltip' data-placement='top' title='第" + (pageIndex + 1) + "页' href='javascript:void(0)'  data-page='" + (pageIndex + 1) + "'>&raquo;</a></li>";

    return result;
}
