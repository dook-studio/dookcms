<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="FW_Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传文件</title>

    <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Finish()
        {
            var DG = frameElement.lhgDG;
            $('#btnRefresh', DG.curDoc).click();
            DG.cancel();
        }
      
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="line-height: 1.8px;">
        <asp:FileUpload ID="fileUpload" Width="400px" runat="server" />
        <div style="height: 5px;">
        </div>
        <asp:CheckBox ID="chkbackup" Text="备份原文件" Visible="false" Checked="true" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="确 定" OnClick="Button1_Click" />
        <asp:Label ID="lbltips" runat="server" ForeColor="Red" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
