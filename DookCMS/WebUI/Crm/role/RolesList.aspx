<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RolesList.aspx.cs" Inherits="Crm_RolesList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/default.css" rel="stylesheet" type="text/css" />
 
    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>

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
            当前位置：商品列表</div>      
        <div id="divData">
            <asp:Button ID="Button1" runat="server" Text="添加角色" onclick="Button1_Click" />
            <br />
            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False"    DataKeyNames="id" AllowSorting="True"   onrowdeleting="gvList_RowDeleting" onsorting="gvList_Sorting" OnRowCommand="gvList_RowCommand">
                <Columns>                   
                    <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" SortExpression="id" InsertVisible="False" />
                    <asp:BoundField DataField="RoleName" HeaderText="角色名称" SortExpression="RoleName" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="EditRole.aspx?id=<%#Eval("id") %>" >修改</a> &nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('系统角色请谨慎操作,确认要删除吗？');"  CommandArgument='<%#Eval("id") %>'  runat="server" CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
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
