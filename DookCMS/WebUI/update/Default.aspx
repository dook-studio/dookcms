<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //升级前的备份
        
        
        //下载文件
        string localpath=Server.MapPath("~/update/update.zip");
        Common.FileHelper.DownLoadFile("http://www.mou18.com/soft/dukeycms/update.zip", localpath);
        //解压文件
        Common.ZipFileHelper.UnZipFile(localpath, Server.MapPath("~/"));        
        //删除文件
        Common.FileHelper.FileDel(localpath);        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>更新网站</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" Text="点此更新网站程序" OnClick="btnAddNew_Click" />
    </div>
    </form>
</body>
</html>
