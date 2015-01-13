<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysUserList.aspx.cs" Inherits="Crm_SysUserList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/default.css" rel="stylesheet" type="text/css" />
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
            当前位置：后台管理员列表</div>
        <div style="margin-bottom: 5px;">
            <asp:Button ID="btnAddNew" runat="server" Text="添加管理员" OnClick="btnAddNew_Click" /></div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ID" AllowPaging="True" AllowSorting="True" 
        onrowdeleting="gvList_RowDeleting" onsorting="gvList_Sorting" OnRowCommand="gvList_RowCommand">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" SortExpression="ID"
                    InsertVisible="False" />
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="用户名" SortExpression="UserName" />             
                <asp:TemplateField HeaderText="角色">
                    <ItemTemplate>
                        <%#GetRoles(Eval("roleids").ToString()) %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="EditSysUser.aspx?id=<%#Eval("ID") %>">修改</a> &nbsp;&nbsp;<asp:LinkButton
                            ID="LinkButton1" OnClientClick="return confirm('请谨慎操作,确认要删除吗？');" runat="server"
                            CausesValidation="False" CommandName="Delete" Text="删除" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>    
    </form>
      <div><span style="float: left; margin-top: 15px;">共<%=pager.RecordCount %>条 当前:<%=pager.CurrentPageIndex %>/<%=pager.PageCount %>页</span>
        <webdiyer:AspNetPager ID="pager" CssClass="paginator" CurrentPageButtonClass="cpb"
            runat="server" OnPageChanged="AspNetPager1_PageChanged" PageSize="20" PageIndexBoxType="TextBox"
            ShowPageIndexBox="Always" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <div style="clear: both;">
        </div>
    </div>    
</body>
</html>
