<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditDtdesc.aspx.cs" Inherits="Mdbw_EditDtdesc"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="style/default.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="lhgdialog/lhgdialog.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function()
        {
           
        });
        function Finish()
        {
            var DG = frameElement.lhgDG;
            DG.curWin.location.href = DG.curWin.location.href;            
            DG.cancel();
        }      
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 0 0 2px;">
        <div style="padding:10px;width:340px;">  <textarea id="tbdesc" style="width:320px;height:50px;margin-bottom:5px;" runat="server"></textarea><br>
        <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
       </div>
    </div>
    </form>
</body>
</html>
