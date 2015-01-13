<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpLoadImg.aspx.cs" Inherits="Crm_UpLoadImg" ValidateRequest="false" ResponseEncoding="gb2312" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="crm.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="swfupload/js/swfupload.js" type="text/javascript" charset="gb2312"></script>

    <script src="swfupload/js/swfupload.queue.js" type="text/javascript" charset="gb2312"></script>

    <script src="swfupload/js/fileprogress.js" type="text/javascript" charset="gb2312"></script>

    <script src="swfupload/js/handlers.js" type="text/javascript" charset="gb2312"></script>

    <style type="text/css">
        div.fieldset { border: 1px solid #afe14c; margin: 10px 0; padding: 20px 10px; }
        div.fieldset span.legend { position: relative; background-color: #FFF; padding: 3px; top: -30px; font: 700 14px Arial, Helvetica, sans-serif; color: #73b304; }
        div.flash { width: 375px; margin: 10px 5px; border-color: #D9E4FF; -moz-border-radius-topleft: 5px; -webkit-border-top-left-radius: 5px; -moz-border-radius-topright: 5px; -webkit-border-top-right-radius: 5px; -moz-border-radius-bottomleft: 5px; -webkit-border-bottom-left-radius: 5px; -moz-border-radius-bottomright: 5px; -webkit-border-bottom-right-radius: 5px; }
        td { font-size: 12px; font-family: 宋体; vertical-align: top; }
        .progressWrapper { width: 500px; overflow: hidden; }
        .progressContainer { margin: 2px; padding: 4px; border: solid 1px #E8E8E8; background-color: #fff; overflow: hidden; }
        /* Message */.message { margin: 1em 0; padding: 10px 20px; border: solid 1px #FFDD99; background-color: #FFFFCC; overflow: hidden; }
        /* Error */.red { border: solid 1px #B50000; background-color: #FFEBEB; }
        /* Current */.green { border: solid 1px #DDF0DD; background-color: #EBFFEB; }
        /* Complete */.blue { border: solid 1px #CEE2F2; background-color: #F0F5FF; }
        .progressName { font-size: 8pt; font-weight: 700; color: #555; width: 323px; height: 14px; text-align: left; white-space: nowrap; overflow: hidden; }
        .progressBarInProgress, .progressBarComplete, .progressBarError { font-size: 0; width: 0%; height: 2px; background-color: blue; margin-top: 2px; }
        .progressBarComplete { width: 100%; background-color: green; visibility: hidden; }
        .progressBarError { width: 100%; background-color: red; visibility: hidden; }
        .progressBarStatus { margin-top: 2px; width: 337px; font-size: 12px; font-family: Arial; text-align: left; white-space: nowrap; color: #999; }
        a.progressCancel { font-size: 0; display: block; height: 14px; width: 14px; background-image: url(images/cancelbutton.gif); background-repeat: no-repeat; background-position: -14px 0px; float: right; }
        a.progressCancel:hover { background-position: 0px 0px; }
        /* -- SWFUpload Object Styles ------------------------------- */.swfupload { vertical-align: top; }
    </style>

    <script type="text/javascript">
        var swfu;

        window.onload = function()
        {
            var settings = {
                flash_url: "swfupload/swfupload.swf",
                upload_url: "upload.aspx", // Relative to the SWF file
                file_post_name: "Filedata", // 是POST过去的$_FILES的数组名
                post_params: {
                    "ASPSESSID": "<%=Session.SessionID %>"

                },
                //post_params: {"PHPSESSID" : "<?php echo session_id(); ?>"},
                file_size_limit: "100 MB",
                file_types: "*.jpg;*.gif;*.jpeg;*.png",
                file_types_description: "所有图片",
                file_upload_limit: 500,
                file_queue_limit: 0,
                custom_settings: {
                    progressTarget: "fsUploadProgress"
                    //cancelButtonId: "btnCancel"
                },
                debug: false,
                // Button settings
                button_image_url: "images/upload.png", // Relative to the Flash file
                button_width: "70",
                button_height: "22",
                button_placeholder_id: "spanButtonPlaceHolder",
                button_text: '选择图片',
                //button_text_style: ".theFont { font-size:12; }",
                button_text_left_padding: 12,
                button_text_top_padding: 3,

                // The event handler functions are defined in handlers.js
                file_queued_handler: fileQueued,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_start_handler: uploadStart,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: uploadSuccess,
                //upload_complete_handler: uploadComplete,
                queue_complete_handler: queueComplete
            };

            swfu = new SWFUpload(settings);
        };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 5px;">
        <div class="nav" style="padding: 5px;">
            当前位置：上传照片 ＞</div>
        <div style="width: 100%; clear: both;">
            <div style="float: left;">
                <div style="border: solid 1px #DBDBDB; background-color: #F2F2F2; width: 590px; padding: 10px;">
                    <p id="pSelAlbum">
                        <span style="color: #005FA9; font-weight: bold;">选择版块：</span>
                        <asp:DropDownList ID="dropSort" runat="server" DataTextField="cname" AppendDataBoundItems="true" DataValueField="bid">
                            <asp:ListItem Value="0">不选择</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <div id="fsUploadProgress" class="flash">
                        <span style="color: #999;"></span>
                    </div>
                    <div id="divStatus" style="color: #666; text-indent: 8px;">
                    </div>
                </div>
                <div style="float: none; margin: 10px;">
                    <div id="spanButtonPlaceHolder">
                    </div>
                    <input id="btnupload" type="button" value="开始上传" onclick="goStep();" style="float: right;" /><input type="button" style="visibility: hidden;" value="取消" />
                </div>
            </div>
            <div style="float: right; color: #999;">
                <p class="fb">
                    友情提示：</p>
                <p style="line-height: 1.7">
                    支持.jpg,.jpeg,.gif,.png格式<br />
                    相片尺寸大于500*500的系统将自动压缩</p>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function setCookie(name, value)
    {
        var Days = 30;     //此     cookie     将被保存     30     天   
        var exp = new Date();           //new     Date("December     31,     9998");   
        exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    }
    function getCookie(name)
    {
        var arr = document.cookie.match(new RegExp("(^|     )" + name + "=([^;]*)(;|$)"));
        if (arr != null) return unescape(arr[2]); return null;
    }
    function delCookie(name)
    {
        var exp = new Date();
        exp.setTime(exp.getTime() - 1);
        var cval = getCookie(name);
        if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    }

    function goStep()
    {
        $("#btnupload").attr("disabled", true);
        $("#pSelAlbum").hide();
        setCookie("photobid", $("#dropSort").val());
        swfu.startUpload();
    }
    
</script>

