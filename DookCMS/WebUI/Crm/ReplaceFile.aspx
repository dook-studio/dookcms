<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReplaceFile.aspx.cs" Inherits="Crm_ReplaceFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
    function close()
    {
        pop.close();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" Width="400px" runat="server" />
        <br />
        <asp:CheckBox ID="chkbackup" Text="备份原文件" Checked="true" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="确 定" onclick="Button1_Click" />
    </div>
    </form>
</body>
</html>
