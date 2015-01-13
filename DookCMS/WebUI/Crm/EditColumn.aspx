<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditColumn.aspx.cs" Inherits="Crm_EditColumn"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加修改字段</title>
    <link href="crm.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function()
        {
            $("#btnSubmit").click(function()
            {
                if ($.trim($("#txtColumnName").val()) == "")
                {
                    alert("请输入字段名称!");
                    $("#txtColumnName").focus();
                    return false;
                }
            });
        });
        function OK()
        {
            window.parent.location.href = window.parent.location.href;
            window.parent.pop.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tblist" style="width: 500px; text-align: left">
            <colgroup>
                <col align="right" style="width: 100px;" class="bg1" />
            </colgroup>
            <tr class="ptitle" align="center">
                <td colspan="4" align="center">
                    添加/编辑字段
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    字段名：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtColumnName" Width="150px" CssClass="inputtxt" MaxLength="100"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    类型：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="dropType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropType_SelectedIndexChanged">
                        <asp:ListItem Value="TEXT">文本</asp:ListItem>
                        <asp:ListItem Value="COUNTER">自动编号</asp:ListItem>
                        <asp:ListItem Value="INTEGER">数字</asp:ListItem>
                        <asp:ListItem Value="MEMO">备注</asp:ListItem>
                        <asp:ListItem Value="DATETIME">日期</asp:ListItem>
                        <asp:ListItem Value="YESNO">是/否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trLength" runat="server">
                <td style="width: 100px">
                    长度：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtLength" Width="100px" CssClass="inputtxt" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    允许空：
                </td>
                <td colspan="3">
                    <div>
                        <asp:CheckBox ID="chkIsNull" Checked="true" runat="server" />
                    </div>
                </td>
            </tr>
            <tr id="trDefaultValue" runat="server">
                <td style="width: 100px">
                    默认值：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDefaultValue" Width="100px" CssClass="inputtxt" MaxLength="100"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <%-- <tr>
                <td style="width: 100px">
                    是否主键：
                </td>
                <td colspan="3">
                    <div>
                        <asp:CheckBox ID="chkIsPrimaryKey" runat="server" />
                    </div>
                </td>
            </tr>--%>
            <tr>
                <td style="width: 100px">
                    描述：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDesc" Width="300px" CssClass="inputtxt" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                    &nbsp;<input type="button" value="取 消" onclick="window.parent.pop.close();" />
                    <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
