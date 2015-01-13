<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RewriteRules.aspx.cs" Inherits="Crm_RewriteRules"  ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="crm.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 5px;">
        <div class="nav">
            当前位置：重写规则</div>
        <div style="margin: 5px 0;">
            <textarea id="txtContents" style="height: 460px; width:100%" cols="6" runat="server"></textarea>
            
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
        </div>
        
    </div>
    </form>
</body>
</html>
