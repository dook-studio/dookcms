<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataBaseHelper.aspx.cs" Inherits="Crm_DataBaseHelper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="crm.css" rel="stylesheet" type="text/css" />
    <%--<style type="text/css">
.nav{color:#ffffff;}
</style>--%>

    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>

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


            jQuery.Delete = function(path, filetype)
            {
                if (filetype == "folder")
                {
                    if (confirm("确认删除该文件夹以及该文件夹下所有文件吗?"))
                    {
                        $("#<%=hidfilepath.ClientID %>").val(path);
                        $("#<%=btnDelete.ClientID %>").click();
                    }
                }
                else
                {
                    if (confirm("确认删除该文件吗?"))
                    {
                        $("#<%=hidfilepath.ClientID %>").val(path);
                        $("#<%=btnDelete.ClientID %>").click();
                    }
                }

            }
            jQuery.Restore = function(path, filetype)
            {
               
                    if (confirm("警告:请在还原之前备份好当前数据库,确认还原该文件吗?"))
                    {
                        $("#<%=hidfilepath.ClientID %>").val(path);
                        $("#<%=btnRestore.ClientID %>").click();
                    }
             
            }

        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 5px;">
        <div class="nav" style="padding: 5px;">
            当前位置：数据库备份与还原 ＞</div>
        <div style="margin-bottom: 5px;">
            <asp:Button ID="btnAddNew" runat="server" Text="立即备份数据库" OnClick="btnAddNew_Click" /> <asp:Button ID="btnCompress" runat="server" Text="压缩Access数据库" OnClick="btnCompress_Click" /></div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            AllowSorting="true">
            <Columns>
               <asp:TemplateField HeaderText="序号" SortExpression="filename">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称" SortExpression="filename">
                    <ItemTemplate>
                        <%#Eval("filename") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="updatetime" HeaderText="修改时间" />
                <asp:BoundField DataField="filesize" ControlStyle-ForeColor="#6D6D6D" ItemStyle-ForeColor="#6D6D6D"
                    HeaderText="大小">
                    <ControlStyle ForeColor="#6D6D6D"></ControlStyle>
                    <ItemStyle ForeColor="#6D6D6D"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="javascript:" onclick="$.Restore('<%#Eval("filename") %>','<%#Eval("filetype") %>')">立即还原</a> | &nbsp;&nbsp;<a href="javascript:" onclick="$.Delete('<%#Eval("filename") %>','<%#Eval("filetype") %>')">删除</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:Button ID="btnRestore" runat="server" Text="" Style="display: none;" OnClick="btnRestore_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="" Style="display: none;" OnClick="btnDelete_Click" />
    <asp:HiddenField ID="hidfilepath" runat="server" />
    </form>
    <asp:Label ID="lblTips" runat="server"></asp:Label>
</body>
</html>
