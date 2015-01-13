<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="form_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>表单列表</title>
    <link rel="stylesheet" type="text/css" href="/easyui/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="/easyui/themes/icon.css">
    <script type="text/javascript" src="/easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="/easyui/jquery.easyui.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvList" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" EnableModelValidation="True">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                    <asp:BoundField DataField="cname" HeaderText="中文名称" SortExpression="cname" />
                    <asp:BoundField DataField="tbname" HeaderText="表名称" SortExpression="tbname" />
                    <asp:BoundField DataField="paras" HeaderText="参数" SortExpression="paras" />
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:TemplateField HeaderText="编辑字段">
                        <ItemTemplate>
                            <a href="formcol.aspx?id=<%#Eval("ID") %>" ></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connstr %>" ProviderName="<%$ ConnectionStrings:connstr.ProviderName %>" SelectCommand="SELECT * FROM [form]" DeleteCommand="DELETE FROM [form] WHERE [ID] = @ID" InsertCommand="INSERT INTO [form] ([ID], [cname], [tbname], [paras]) VALUES (@ID, @cname, @tbname, @paras)" UpdateCommand="UPDATE [form] SET [cname] = @cname, [tbname] = @tbname, [paras] = @paras WHERE [ID] = @ID">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="cname" Type="String" />
                    <asp:Parameter Name="tbname" Type="String" />
                    <asp:Parameter Name="paras" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="cname" Type="String" />
                    <asp:Parameter Name="tbname" Type="String" />
                    <asp:Parameter Name="paras" Type="String" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
