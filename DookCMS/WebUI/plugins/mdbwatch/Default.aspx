<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Mdbw_DBTableList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="style/default.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="lhgdialog/lhgdialog.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function()
        {
            var selobj = null;
            var selobjcolor = null;
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
                $(this).find("td").dblclick(function()
                {
                    if ($(this).index() == 1)
                    {
                        //重命名表
                        $.poprenametb($.trim($(this).text()));
                    }
                    if ($(this).index() == 2)
                    {
                        var tbname = $.trim($(this).prev().text());
                        //修改表说明                   
                        $.popCDesc(tbname,$.trim($(this).text()));
                    }
                });
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function()
        {
            $("#btnadd").dialog({ cover: true, bgcolor: '#000', opacity: 0.3, title: '新建表', id: 'tupload', page: '/plugins/mdbwatch/EditDBTable.aspx', autoSize: false, width: 900, height: 400 });
            jQuery.renametb = function(oldname, newname)
            {
                $.post("filehandle.ashx?ac=renametb&" + Math.random(), { oldname: oldname, newname: newname }, function(res)
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
            var dg;
            jQuery.poprenametb = function(oldname)
            {
                var htmls = "<div style=\"padding:10px;width:350px;\">  <input type=\"text\" style=\"width:200px;\" id=\"tbname\" value=\"" + oldname + "\" /> <input type=\"button\" onclick=\"$.renametb('" + oldname + "',$('#tbname').val());\" value=\"提 交\"/> </div>";
                dg = new $.dialog({ cover: true, bgcolor: '#000', opacity: 0.5, id: "winrenamde", maxBtn: false, title: "重命名表" + oldname, html: htmls, width: 400, height: 150 });
                dg.ShowDialog();
            }          
            jQuery.popCDesc = function(tbname, desc)
            {
                dg = new $.dialog({ cover: true, bgcolor: '#000', opacity: 0.5, id: "winrenamde", maxBtn: false, title: "修改说明" + tbname, page: '/plugins/mdbwatch/EditDtdesc.aspx?tbname=' + tbname + "&desc=" + encodeURIComponent(desc), width: 450, height: 200 });
                dg.ShowDialog();
            }
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 0 0 2px;">
        <div style="margin-bottom: 5px;">
        </div>
        <div style="background-color: #E5E5E5; color: #CC3300; font-weight: bold; padding: 4px;
            border: solid 1px #D1D1D1; border-bottom: 0;">
            <input id="btnadd" type="button" value="添加表" /><asp:DropDownList ID="dropSort" 
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="dropSort_SelectedIndexChanged">
                <asp:ListItem Value="">全部表</asp:ListItem>                
                <asp:ListItem Value="U_">自定义表</asp:ListItem>
                <asp:ListItem Value="incms">内置表</asp:ListItem>
            </asp:DropDownList>     
            <span id="spinfo"></span>      <span>注:双击行可修改表名和表说明</span>        
        </div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
            OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="编号" ItemStyle-Width="50px">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="表名">
                    <ItemTemplate>
                        <asp:Label ID="lblname" ForeColor="#333333" runat="server" Text='<%# Bind("tbname") %>'></asp:Label>&nbsp;      
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="说明">
                    <ItemTemplate>
                        <asp:Label ID="lbldesc" ForeColor="#878787" runat="server" Text='<%# Bind("tbdesc") %>'></asp:Label>                     
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="管理数据">
                    <ItemTemplate>
                        <a href="DataList.aspx?tablename=<%#Eval("tbname") %>">数据</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="管理字段">
                    <ItemTemplate>
                        <a href="DBColumnList.aspx?tablename=<%#Eval("tbname") %>">字段</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <div <%#Eval("tbname").ToString().ToLower().StartsWith("u_") ? "" : "style=\"display:none\""%>>
                            <asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("tbname") %>' OnClientClick="return confirm('警告:删除后将不能恢复,请谨慎操作,确认删除吗？');"
                                runat="server" CausesValidation="False" CommandName="Delete" Text="删除表"></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
        <asp:Literal ID="litercopyright" runat="server"></asp:Literal>        
    </form>
</body>
</html>
