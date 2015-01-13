<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotoList.aspx.cs" Inherits="Crm_PhotoList" %>

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

        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 5px;">
        <div class="nav" style="padding: 5px;">
            当前位置：相册列表 ＞</div>
        <div style="margin-bottom: 5px;">
            <asp:Button ID="btnAddNew" runat="server" Text="上传图片" OnClick="btnAddNew_Click" /></div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="false" AllowSorting="True" onrowcommand="gvList_RowCommand" onrowdeleting="gvList_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" SortExpression="id" InsertVisible="False" Visible="false" />
                <asp:BoundField DataField="title" HeaderText="标题" SortExpression="title" />
                <asp:TemplateField HeaderText="图片地址" SortExpression="imgurl">
                    <ItemTemplate>
                        <a href="<%#Eval("imgurl") %>" target="_blank"><img src="<%#Eval("imgurl") %>"  width="50px" height="50px"/></a>
                        [<a href="javascript:">复制地址</a>]
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="bid" HeaderText="版块编号" SortExpression="bid" />
                <asp:CheckBoxField DataField="isshow" HeaderText="是否显示" SortExpression="isshow" />
                <asp:BoundField DataField="dots" HeaderText="点击数" SortExpression="dots" />
                <asp:BoundField DataField="addtime" HeaderText="添加时间" SortExpression="addtime" />
                <asp:BoundField DataField="uptime" HeaderText="更新时间" SortExpression="uptime" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <a href="editPhoto.aspx?id=<%#Eval("id") %>">修改</a>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server" CausesValidation="False" CommandName="Delete"  CommandArgument='<%#Eval("ID").ToString()+"|"+Eval("imgurl").ToString() %>' Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
