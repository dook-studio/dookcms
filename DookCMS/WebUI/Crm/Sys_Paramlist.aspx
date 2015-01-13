<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sys_Paramlist.aspx.cs" Inherits="Modules_Sys_Paramlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="crm.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-1.2.6.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function()
        {
            var selobj = null;
            var selobjcolor = null;
            //d = this.style.backgroundColor; this.style.backgroundColor = '#D1F880';
            $(".trnei,.lupai").each(function()
            {

                $(this).click(function()
                {
                    try
                    {
                        selobj.css("background-color", selobjcolor);
                    }
                    catch (ex)
                    {
                    }
                    selobjcolor = $(this).css("background-color");
                    $(this).css("background-color", "#D1F880");
                    selobj = $(this);
                });
            });

        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 5px;">
        <div class="nav">
            当前位置：参数管理</div>
        <div style="margin-bottom: 5px;">
            <asp:DropDownList ID="dropSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropSort_SelectedIndexChanged">
                <asp:ListItem Value="">请选择</asp:ListItem>              
                <asp:ListItem Value="showtime">上市时间</asp:ListItem>
                <asp:ListItem Value="howold">年龄段</asp:ListItem>
                <asp:ListItem Value="sex">性别</asp:ListItem>
                <asp:ListItem Value="size">尺码</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnAddNew" runat="server" Text="新建参数" OnClick="btnAddNew_Click" />
            <span style="color:Red">注意：系统参数请慎重操作,请在发布产品之前设定好.</span>
            </div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="编号" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="code" HeaderText="分类代码" ReadOnly="true" SortExpression="code" />
                <asp:BoundField DataField="codevalue" HeaderText="参数值" SortExpression="codevalue" />
                <asp:BoundField DataField="codetxt" HeaderText="参数文本" SortExpression="codetxt" />
                <asp:CommandField HeaderText="操作" ShowEditButton="True" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server" CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:AccessDataSource ID="SqlDataSource1" runat="server" DataFile="~/App_Data/flashcrm.mdb" SelectCommand="SELECT * FROM [sys_Params]" DeleteCommand="DELETE FROM [sys_Params] WHERE [ID] = ?" InsertCommand="INSERT INTO [sys_Params] ([ID], [code], [codevalue], [codetxt]) VALUES (?, ?, ?, ?)" UpdateCommand="UPDATE [sys_Params] SET  [codevalue] = ?, [codetxt] = ? WHERE [ID] = ?">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="codevalue" Type="String" />
            <asp:Parameter Name="codetxt" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="code" Type="String" />
            <asp:Parameter Name="codevalue" Type="String" />
            <asp:Parameter Name="codetxt" Type="String" />
        </InsertParameters>
    </asp:AccessDataSource>
    </form>
</body>
</html>
