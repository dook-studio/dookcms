<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>网站后台登录--DukeyCMS</title>
    <style type="text/css">
    /* reset */
body{background:#fff;line-height:166.6%;font-size:12px}
body,input,select,button{font-family:verdana,sans-serif}
h1,h2,h3,h4,h5,h6,select,input,textarea,button,table{font-size:100%}
body,h1,h2,h3,h4,h5,h6,ul,ol,li,form,p,dl,dt,dd,table,th,td,img,blockquote{margin:0;padding:0;border:0}
input,button,textarea,img{line-height:normal}
abbr,acronym,address,cite,q,em,code,var,dfn,ins{font-style:normal;text-decoration:none;border:0}
q:before,q:after{content:""}
ul,ol{list-style:none}
table{border-collapse:collapse;border-spacing:0}
select,input,button,button img{vertical-align:middle}
.ipt-r,
.ipt-c{width:16px;height:16px;+width:15px;+height:15px;_height:14px;padding:0;margin:2px 3px 2px 0;overflow:hidden;-ms-box-sizing:border-box}
.page,.header,.content,.footer{margin:auto}

/* 全局 */
a{color:#1F4E8B}

/* 清除浮动和垂直边距重叠 */

.login .fi-btns,
.header,
.fn-clear{+zoom:1;}

.login .fi-btns:before,
.login .fi-btns:after,
.header:before,
.header:after,
.fn-clear:before,
.fn-clear:after{clear:both;content:".";font-size:0;display:block;height:0;overflow:hidden;visibility:hidden;}

/* 内联块元素 */
.ftinfo .split,
.btn,
.fn-ib{display:-moz-inline-box;-moz-box-align:center;display:inline-block;+display:inline;+zoom:1;vertical-align:middle;}


/* 公用背景图 */
.clogo,
.main .intro li,
.ftlink h3,
.ftinfo,
.ftinfo .case,
.ftinfo .info,
.ftinfo h2,
.logo em,
.ico,
.btn,
.bg{background-image:url(images/bg.gif);background-color:transparent;background-repeat:no-repeat}

/* 整体 */
html,body,.page{height:100%}
.page{position:relative}
.inner{position:absolute;top:50%;margin-top:-312px;width:100%;height:624px;background:url(images/bgx.jpg) 0 58px repeat-x}

/* 页头 */
.header{width:892px;position:relative;z-index:2}
.logo{float:left}
.logo h1,
.logo em{float:left}
.logo em{width:186px;height:30px;margin-left:20px;text-indent:-999em;overflow:hidden}
.logo h1{position:relative}
.logo .beta{position:absolute;right:0;top:-16px}
.toplink{float:right;display:inline;text-align:right}
.toplink,
.toplink a{color:#848585;text-decoration:none}
.toplink a{margin:0 5px}

/* 主体 */
.content{width:892px;margin-top:27px}
.main{height:468px;background:url(images/theme_dft.jpg) no-repeat;position:relative}

.intro{width:360px;position:absolute;top:330px;left:70px}
.intro li{float:left;width:165px;background-position:0 -32px;padding-left:15px;color:#626262}

.login{width:356px;padding:80px 0 0 40px;position:absolute;top:15px;left:456px}
.login h2{display:none}
.login .form{zoom:1}
.login .fi{margin:22px 0;position:relative}
.login .fi .lb{font-size:14px}
.login .fi-btns,
.login .fi-nolb{padding-left:50px}
.login .at{font-size:16px;font-family:arial,sans-serif}
.login .ipt-t{width:200px;height:20px;margin-right:3px;border:1px solid #7A8395;padding:4px;font-size:14px;line-height:18px;font-weight:bold;ime-mode:disabled;background-color:#EFF6FA}
.login .ipt-t-focus,
.login .ipt-t:focus{background-color:#FFFFE1}
.login .ipt-t-fullid{width:255px}
.login .ipt-t-domain{width:100px;margin-left:3px}
.login .cookie,
.login .ssl{color:#707070;margin-right:35px}
.login .btn-login{float:left;padding:0;border:0;width:95px;height:35px;_height:36px;margin-right:20px;_margin-right:17px;background-position:0 -144px;cursor:pointer}
.login .btn-login span{visibility:hidden}
.login .btn-login-hover,
.login .btn-login:hover{background-position:-144px -144px}
.login .domain{width:125px;position:absolute;left:230px;top:5px;+left:235px;+top:8px;line-height:normal;word-break:break-all;word-wrap:break-word}
.login .msg-err{color:#d90000}
.login .link{margin:0 20px 0 -20px;padding-top:12px;border-top:1px solid #CDD8E6;text-align:center}
.login .link a{color:#768AA4;text-decoration:none}

.create{position:absolute;left:269px;top:170px}
.create .btn-create{width:132px;height:46px;line-height:999em;overflow:hidden;background-position:0 -72px}
.create .btn-create-hover,
.create .btn-create:hover{background-position:-144px -72px}

.ftinfo{height:73px;margin-top:2px;background-position:100% -180px}
.ftinfo h2{float:left;display:inline;width:58px;height:17px;margin:28px 0 0 16px;text-indent:-999px;overflow:hidden}
.ftinfo .case{float:left;height:73px;background-position:0 -253px}
.ftinfo .info{float:right;width:300px;height:73px;background-position:0 -253px}
.ftinfo .case h2{background-position:-288px 0}
.ftinfo .case ul{float:left;padding:12px 0 0 12px}
.ftinfo .case li{float:left;margin-right:10px;height:47px;line-height:47px;padding:0 10px 0 0;border:1px solid #DDE5E8;background:#fff;color:#707070}
.ftinfo .case li a{float:left;height:100%;padding-right:10px;margin-right:-10px;text-decoration:none;color:#707070}
.ftinfo .clogo{float:left;margin:5px 6px 0;_margin-right:3px;width:36px;height:36px}
.ftinfo .clogo-1{background-position:0 -360px}
.ftinfo .clogo-2{background-position:-72px -360px}
.ftinfo .clogo-3{background-position:-144px -360px}
.ftinfo .info h2{background-position:-288px -36px}
.ftinfo .info ul{float:left;margin:6px 0 0 10px}
.ftinfo .info li{width:210px;height:20px;overflow:hidden}
.ftinfo .info a{color:#898989;text-decoration:none}
.ftinfo .info span{color:#3A6F9E;font-family:simsun,serif}
</style>
    <script src="/js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            jQuery.changeyzm = function ()
            {
                $("#imgcode").hide();
                $("#imgcode").attr("src", "/ashx/postop.ashx?ac=yzm&rnd=" + Math.random());
                $("#imgcode").show();
            }
        });
    </script>
</head>
<body>    
    <div class="page" id="divPage">
        <div class="inner">
            <div class="content">
                <div class="main">
                    <div class="login">
    
                        <h2>
                            登录网站后台系统</h2>
                        <form class="form" runat="server" id="frm1">
                        <div class="fi">
                            <label class="lb">
                                帐 号：</label>
                            <asp:TextBox CssClass="ipt-t" ID="txtUserName" Text="admin" runat="server"></asp:TextBox>
                        </div>
                        <div class="fi">
                            <label class="lb">
                                密 码：</label>
                            <asp:TextBox CssClass="ipt-t ipt-t-pwd" ID="txtPassword" TextMode="Password" Text="admin"
                                runat="server"></asp:TextBox>
                        </div>
                    <%--    <div class="fi">
                            <label class="lb">
                                验 证：</label>
                            <input type="text" id="validecode" class="ipt-t" maxlength="6" runat="server" style="width: 60px;" onfocus="$.changeyzm();" /><img
                                style="vertical-align: middle; display: none;" onclick="$.changeyzm();" width="60"
                                height="27" border="0" title="点击换一张" id="imgcode"><a href="javascript:void(0);" onclick="$.changeyzm();">看不清换一张</a><span
                                    class="success" style="display: none;"></span>
                            <input type="hidden" id="mycode" value="345" />
                        </div>--%>
                        <div class="fi fi-btns">
                        <asp:Button ID="btnsubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-login" onmouseover="this.className+=' btn-login-hover'"
                                onmouseout="this.className='btn btn-login'"  Text="" />
                         <%--   <button type="button" class="btn btn-login" onmouseover="this.className+=' btn-login-hover'"
                                onmouseout="this.className='btn btn-login'" runat="server" onclick="">
                                <span>登 录</span></button>--%>
                        </div>
                        </form>
                        <div class="link">
                            <a style="color: red;" href="http://www.dukeycms.com/" target="_blank">都客科技,专注于企业网站建设!</a> <a style="color: blue;" href="/">返回网站首页</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
