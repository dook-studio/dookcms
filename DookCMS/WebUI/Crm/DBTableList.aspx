<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="DBTableList.aspx.cs"
    Inherits="Crm_DBTableList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        $(document).ready(function()
        {
            jQuery.renametb = function(oldname, newname)
            {
                $.post("/crm/ashx/crmop.ashx?ac=renametb&" + Math.random(), { oldname: oldname, newname: newname }, function(res)
                {
                    if (res == "ok")
                    {
                        location.href = location.href;
                    } 
                    else
                    {
                        alert("重命名失败!可能名称已经存在!");
                    }
                });
            }
            jQuery.poprenametb = function(oldname)
            {
                ShowHtmlString("重命名" + oldname, "<div style=\"padding:10px;width:350px;\">  <input type=\"text\" style=\"width:200px;\" id=\"tbname\" value=\"" + oldname + "\" /> <input type=\"button\" onclick=\"$.renametb('" + oldname + "',$('#tbname').val());\" value=\"提 交\"/> <input type=\"button\" onclick=\"pop.close();\" value=\"取 消\"/></div>", 350, 100);
            }
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">

    <div class="nav" style="padding: 5px;">
        当前位置：数据表 ＞</div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="添加表" OnClick="btnAddNew_Click" /></div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
        OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="编号">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="表名">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("table_name") %>'></asp:Label>  <a style="margin-left:30px;"
                        href="javascript:$.poprenametb('<%#Eval("table_name") %>')">重命名</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="description" HeaderText="说明" />
            <asp:TemplateField HeaderText="管理数据">
                <ItemTemplate>
                    <a href="DataList.aspx?tablename=<%#Eval("table_name") %>">数据</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="管理字段">
                <ItemTemplate>
                    <a href="DBColumnList.aspx?tablename=<%#Eval("table_name") %>">字段</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <div <%#Eval("table_name").ToString().StartsWith("U_") ? "" : "style=\"display:none\""%>>
                        <asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("table_name") %>' OnClientClick="return confirm('警告:删除后将不能恢复,请谨慎操作,确认删除吗？');"
                            runat="server" CausesValidation="False" CommandName="Delete" Text="删除表"></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
