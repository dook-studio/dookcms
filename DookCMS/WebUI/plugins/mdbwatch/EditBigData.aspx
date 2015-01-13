<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditBigData.aspx.cs" Inherits="Plugins_mdbwatch_EditBigData" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    *{margin:0;padding:0;}
    </style>
     <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>
 <script src="/xheditor/xheditor-1.1.10-zh-cn.min.js" type="text/javascript"></script>
    <script src="lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
     <script type="text/javascript">        
         function pageInit()
         {
             $('#<%=txtData.ClientID %>').xheditor({ remoteImgSaveUrl: '/xheditor/saveremoteimg.aspx', upLinkUrl: "/xheditor/upload.aspx", upLinkExt: "zip,rar,txt", upImgUrl: "/xheditor/upload.aspx", upImgExt: "jpg,jpeg,gif,png", upFlashUrl: "/xheditor/upload.aspx", upFlashExt: "swf", upMediaUrl: "/xheditor/upload.aspx", upMediaExt: "wmv,avi,wma,mp3,mid", shortcuts: { 'ctrl+enter': submitForm} });
         }
         function submitForm() { }
        $(document).ready(function()
        { 
            $(pageInit);
            $("#btnSubmit").click(function()
            {
                 
            });
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
    <div>
         <textarea id="txtData" rows="20" cols="7" runat="server" style="height: 340px;
                    width: 100%;"></textarea>  
      <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                    &nbsp;
                    <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <div><a id="alink"  style="font-size:12px;"  runat="server" href="EditData.aspx?">转到文本框</a></div>
    </div>
    </form>
</body>
</html>
