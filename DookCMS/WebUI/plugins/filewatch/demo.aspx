<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="lhgdialog/lhgdialog.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function()
        {
            $("#btnBrowser").dialog(
            {
                title: '选择文件', id: 'test9', page: 'filelist.aspx?root=~/upload&fromid=file1&' + Math.random(), autoSize: false, width: 800, height: 500
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="file1" type="text" style="width: 300px;" /><input type="button" id="btnBrowser"
            value="浏览服务器" />
    </div>
    </form>
</body>
</html>
