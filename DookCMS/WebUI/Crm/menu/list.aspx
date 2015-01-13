<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Crm_MenuList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/crm/crm.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            var selobj = null;
            var selobjcolor = null;
            //d = this.style.backgroundColor; this.style.backgroundColor = '#D1F880';
            $(".trnei,.lupai").each(function ()
            {

                $(this).click(function ()
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
            当前位置：菜单管理</div>
        <div style="margin-bottom: 5px;">
            <asp:Button ID="btnAddNew" runat="server" Text="新建菜单" OnClick="btnAddNew_Click" /></div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        EnableModelValidation="True" onrowcancelingedit="gvList_RowCancelingEdit" 
        onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
        onrowupdating="gvList_RowUpdating" onsorting="gvList_Sorting" OnRowCommand="gvList_RowCommand">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="id" InsertVisible="False" ReadOnly="True"
                    SortExpression="id" Visible="false" />
                <asp:BoundField DataField="MenuId" HeaderText="菜单编码" SortExpression="MenuId" />
                <asp:BoundField DataField="ParentId" HeaderText="父级编码" SortExpression="ParentId" />
                <asp:BoundField DataField="MenuName" HeaderText="菜单名称" SortExpression="MenuName" />
                <asp:BoundField DataField="Path" HeaderText="页面路径" SortExpression="Path" />
                <asp:BoundField DataField="target" HeaderText="打开窗口" SortExpression="target" />
                <asp:BoundField DataField="MenuType" HeaderText="菜单类型(0=父级/1=子级)" Visible="false"
                    SortExpression="MenuType" />             
            <asp:CommandField ShowEditButton="True" ButtonType="Button" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>                   
                    <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server" CommandArgument='<%#Eval("id") %>'
                        CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>   
      <div><span style="float: left; margin-top: 15px;">共<%=pager.RecordCount %>条 当前:<%=pager.CurrentPageIndex %>/<%=pager.PageCount %>页</span>
        <webdiyer:AspNetPager ID="pager" CssClass="paginator" CurrentPageButtonClass="cpb"
            runat="server" OnPageChanged="AspNetPager1_PageChanged" PageSize="50" PageIndexBoxType="TextBox"
            ShowPageIndexBox="Always" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <div style="clear: both;">
        </div>
    </div>
    </div>   
    </form>
</body>
</html>
