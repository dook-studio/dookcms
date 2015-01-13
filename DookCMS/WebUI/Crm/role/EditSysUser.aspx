<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditSysUser.aspx.cs" Inherits="crm_EditSysUser"
    ValidateRequest="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/default.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
 
    <div>
        <table class="tblist" style="width: 800px; text-align: left">
            <colgroup>
                <col align="right" style="width: 100px;" class="bg1" />
            </colgroup>
            <tr class="ptitle" align="center">
                <td colspan="2" align="center">
                    <asp:Label ID="lbltitle" runat="server" Text="添加"></asp:Label>管理员
                </td>
            </tr>           
            <tr>
                <td>
                   用户名：
                </td>
                <td>            
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="40" Width="198px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                   密码：
                </td>
                <td>            
                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="30" Width="198px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    角色分配：
                </td>
                <td>
                    <div>   
                    <asp:CheckBoxList ID="chklRoles" runat="server"  DataTextField="RoleName" DataValueField="ID" RepeatColumns="5">
                                        
                    </asp:CheckBoxList>
                       
                    </div>
                </td>
            </tr>
                       
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" /> 
               
                &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="返 回" onclick="btnBack_Click" />
               
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td><asp:Label ID="lblTip" runat="server" ForeColor="Red"></asp:Label>
                   
                </td>
            </tr>
        </table>
    </div>

 
    </form>
</body>
</html>
