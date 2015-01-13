<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Crm_EditTemplate" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../crm.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../js/popup.js" type="text/javascript"></script>

    <script src="../js/popup2.js" type="text/javascript"></script>


    <script type="text/javascript">
        $(document).ready(function()
        {

            $("#btnSubmit").click(function()
            {
                if ($.trim($("#txtfolder").val()) == "")
                {
                    alert("目录名称不能为空!");
                    $("#txtfolder").focus();
                    return false;
                }
            });
            $("#txtename").keyup(function()
            {
                $("#txtfolder").val($(this).val().replace(' ', ''));
            });

        });

        function opdlg(fromid)
        {
            ShowIframe("上传图片", 'UpLoadImgFrm.aspx?fromid=' + fromid, 657, 347);
        }
        function selectdlg(fromid,filetype)
        {
            ShowIframe("从列表选择", 'FileList.aspx?fromid=' + fromid+'&filetype='+filetype, 657, 347);          
        }        
    </script>

</head>
<body>
  <div class="nav" style="padding: 5px;">
        当前位置：<a style="color: #fff;" href="list.aspx">栏目列表</a> ＞
    </div>
    <form id="form1" runat="server">
    <div>
        <table class="tblist" style="width: 800px; text-align: left">
            <colgroup>
                <col align="right" style="width: 100px;" class="bg1" />
            </colgroup>
            <tr class="ptitle" align="center">
                <td colspan="4" align="center">
                    <asp:Label ID="lbltitle" runat="server" Text="增加"></asp:Label>模板包
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    中文名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtcname" Width="200px" MaxLength="50" CssClass="inputtxt" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    英文名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtename" Width="200px" MaxLength="50" CssClass="inputtxt" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    模板描述：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtremark" Width="500px" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    模板目录名：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtfolder" Width="200px" MaxLength="50" CssClass="inputtxt" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    模板封面：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtCoverImg" Width="200px" runat="server" CssClass="inputtxt"></asp:TextBox>
                    <input type="button" value="上 传" onclick="opdlg('txtCoverImg');" /><input type="button" onclick="selectdlg('txtCoverImg','gif-jpg-jpeg-png');" value="从列表中选择" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    上传模板包：
                </td>
                <td colspan="3">
                    <asp:FileUpload ID="fileUpload" runat="server" Width="300px" />
                    只支持zip压缩包
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                    &nbsp;
                    <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
