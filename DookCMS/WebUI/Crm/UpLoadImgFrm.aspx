<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpLoadImgFrm.aspx.cs" Inherits="Crm_UpLoadImgFrm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传图片</title>
    <link rel="stylesheet" type="text/css" href="crm.css" />
    <style type="text/css">
        fieldset { margin-top: 3px; border: 1px solid #666666; }
    </style>
    <script type="text/javascript">
        function getPath(obj)
        {
            if (obj)
            {
                if (window.navigator.userAgent.indexOf("MSIE") >= 1)
                {
                    obj.select(); return document.selection.createRange().text;
                }
                else if (window.navigator.userAgent.indexOf("Firefox") >= 1)
                {
                    if (obj.files)
                    {
                        return obj.files.item(0).getAsDataURL();
                    }
                    return obj.value;
                }
                return obj.value;
            }
        }  

        function Preview(objtxt)
        {
            document.getElementById("imgview").src =getPath(objtxt);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="2" height="100%" cellspacing="0" border="0" width="600" align="center">
            <tr>
                <td>
                    <table width="590" align="center" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td valign="top" style="line-height: 2.0;">
                                <fieldset>
                                    <legend>图片来源</legend>
                                    <table cellpadding="4" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <input id="upload1" type="radio" name="UpLoad" value="upload1" checked="checked" onclick="UpLoadType(this.id);" /><label for="upload1">上传：</label>
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="fileupload" onchange="Preview(this);" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="upload3" type="radio" name="UpLoad" value="upload3" onclick="UpLoadType(this.id);" /><label for="upload3">网络：</label>
                                            </td>
                                            <td>
                                                <input name="txtWebUrl" type="text" value="http://" size="35" id="txtWebUrl" class="in1" onchange="Preview(this);" /><span><input id="DownLoadPic" type="checkbox" name="DownLoadPic" checked="checked" /><label for="DownLoadPic">下载到本地</label></span>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset>
                                    <legend>设置图片</legend>
                                    <table cellpadding="4" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <div style="float: left">
                                                    <input id="WaterMark" type="checkbox" name="WaterMark" /><label for="WaterMark">水印</label></div>
                                                <div id="ajax" style="float: left">
                                                    <input id="NewSize" type="checkbox" name="NewSize" onclick="chooseOne(this,'ajax');" /><label for="NewSize">约束比例</label><input id="Fastness" type="checkbox" name="Fastness" onclick="chooseOne(this,'ajax');" /><label for="Fastness">固定大小</label></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="newsize1" type="radio" name="NewSizeType" value="newsize1" checked="checked" /><label for="newsize1">按比例缩放：</label><input name="txtBiLi" type="text" value="50" size="4" id="txtBiLi" class="in1" onclick="SelectTextBox(this)" />%
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="newsize2" type="radio" name="NewSizeType" value="newsize2" /><label for="newsize2">按设置缩放：</label>
                                                宽<input name="txtWidth" type="text" value="200" size="4" id="txtWidth" class="in1" onclick="SelectTextBox(this)" />
                                                高<input name="txtHeight" type="text" value="200" size="4" id="txtHeight" class="in1" onclick="SelectTextBox(this)" />
                                                单位：px
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset>
                                    <legend>确认提交</legend>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td align="center" height="47">
                                                <input type="text" id="txturl" />
                                                <input type="button" name="Button1" value=" 确 定 " id="Button1" />
                                                <input id="Button3" type="button" value=" 取消 " onclick="window.parent.pop.close();" style="margin-left: 10px;" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="padding-left: 6px; line-height: 2.0;">
                                <fieldset>
                                    <legend>预览图像</legend>                                    
                                    <table cellpadding="2" cellspacing="0" border="0" width="200px" style="height: 201px;">
                                        <tr valign="top">
                                            <td align="center" style="padding: 10px;">
                                                <div style="width: 200px; height: 201px; overflow: hidden; border: solid 1px #333;">
                                                    <%--<img id="imgview" src="" />--%>
                                                    <img id="imgview" alt=""  />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
