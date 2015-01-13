<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormColumnList.aspx.cs" Inherits="Crm_FormColumnList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑表单字段</title>
    <link href="crm.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="js/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function()
        {
            var selobj = null;
            var selobjcolor = null;
            $(".trnei,.lupai").each(function()
            {
                var id = $(this).find("#txtID").val();
                $(this).click(function()
                {
                    if (selobj != null)
                        selobj.css("background-color", selobjcolor);
                    selobjcolor = $(this).css("background-color");
                    $(this).css("background-color", "#D1F880");
                    selobj = $(this);
                });
                $(this).dblclick(function()
                {
                    var dg = new $.dialog({ cover: true, bgcolor: '#000', opacity: 0.3, id: "editcol", maxBtn: false, title: "修改表单", page: "editformcolumn.aspx?id=" + id + "&" + Math.random(), width: 600, height: 500 });
                    dg.ShowDialog();                  
                });
            });

        });
        function selectdlg(root, fromid, filetype)
        {
            ShowIframe("从列表选择", 'FileList.aspx?root=' + root + '&fromid=' + fromid + '&filetype=' + filetype, 657, 347);
        }
    </script>

</head>
<body>
    <div id="images">
    </div>
    <form id="form1" runat="server">
    <div style="padding: 0 5px;">
        <div class="nav" style="padding: 5px;">
            当前位置：<a href="FormList.aspx" style="color: #fff;">表单管理</a> ＞<%=cname %></div>
        <div style="margin-bottom: 5px;">
            <table class="tblist" style="width: 800px; text-align: left">
                <colgroup>
                    <col align="right" style="width: 100px;" class="bg1" />
                </colgroup>
                <tr class="ptitle" align="center">
                    <td colspan="4" align="center">
                        生成表单
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        文件名(英文):
                    </td>
                    <td colspan="3">
                        <span id="spCurTemplatePath" runat="server"></span>
                        <input id="txtFileName" class="inputtxt" style="width: 100px;" value="xxx.html" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        选择模板:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtTemplate" Width="300px" runat="server" CssClass="inputtxt"></asp:TextBox><input
                            type="button" onclick="selectdlg('/template','txtTemplate','html');" value="从列表中选择" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                    </td>
                    <td colspan="3">
                        <asp:Button ID="btnCreatePage" Text="生成页面" runat="server" OnClick="btnCreatePage_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                    </td>
                    <td colspan="3">
                        <a href="#" onclick="javascript:$('#trAddColumns').toggle();">添加字段↓</a>
                    </td>
                </tr>
                <tr style="display: none;" id="trAddColumns">
                    <td style="width: 100px">
                        选择关联表：
                    </td>
                    <td colspan="3">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:ListBox ID="listTables" runat="server" Height="200px" Style="vertical-align: middle;"
                                    AutoPostBack="true" DataTextField="table_name" DataValueField="table_name" OnSelectedIndexChanged="listTables_SelectedIndexChanged">
                                </asp:ListBox>
                                <asp:ListBox ID="ListColumns" Height="200px" SelectionMode="Multiple" Style="vertical-align: middle;"
                                    runat="server"></asp:ListBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="btnAddCoulmns" Style="vertical-align: middle;" runat="server" Text="添加所选字段"
                            OnClick="btnAddCoulmns_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-bottom: 5px; width: 800px;">
            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                AllowSorting="True" OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="编号">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                            <input id="txtID" type="hidden" value="<%#Eval("ID") %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cname" HeaderText="中文名称" />
                    <asp:BoundField DataField="para_name" HeaderText="名称" />
                    <asp:BoundField DataField="tablename" HeaderText="对应表" />
                    <asp:BoundField DataField="datatype" HeaderText="类型" />
                    <asp:BoundField DataField="maxlength" HeaderText="最大长度" />
                    <asp:BoundField DataField="width" HeaderText="宽度" />
                    <asp:BoundField DataField="px" HeaderText="排序值" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('警告:删除后将不能恢复,请谨慎操作,确认删除吗？');"
                                runat="server" CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
