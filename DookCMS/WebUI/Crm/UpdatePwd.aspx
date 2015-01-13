<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePwd.aspx.cs" Inherits="UpdatePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>  
    <link href="crm.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1 { width: 141px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div><center>
            <table class="tblist" style="width:400px;text-align:left">
            <colgroup>
            <col align="right" style="width:100px;" class="bg1"/>
            </colgroup>
                <tr class="ptitle">
                    <td colspan="2"  align="center">
                        修改密码</td>
                </tr>
                 <tr>
                    <td class="style1">
                        登录名：</td><td>
                            <asp:TextBox CssClass="inputtxt" ID="txtUserName" Enabled="false" runat="server"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td class="style1">
                        输入旧密码：</td><td><asp:TextBox CssClass="inputtxt" ID="txtPassword1" TextMode="Password" runat="server"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td class="style1">
                        输入新密码：</td><td><asp:TextBox CssClass="inputtxt" ID="txtPassword2" TextMode="Password" runat="server"></asp:TextBox></td>
                </tr>
                  <tr>
                    <td class="style1">
                        再输入一次新密码：</td><td><asp:TextBox CssClass="inputtxt" ID="txtPassword3" TextMode="Password" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="style1">
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="修改密码" OnClick="btnSubmit_Click" />
                           </td>
                </tr>
            </table></center>
        </div>
    </form>
</body>
</html>
