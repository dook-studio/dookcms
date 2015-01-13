<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomFormList.aspx.cs" Inherits="CustomFormList" %>

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
        function load() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }
 
        function EndRequestHandler() 
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
            });
        }
    </script>

    <%--<script type="text/javascript">
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
            });
        });
    </script>--%>

</head>
<body onload="load();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="padding: 0 5px;">
        <div class="nav" style="padding: 5px;">
            当前位置：<a style="color: #fff;" href="CustomFormList.aspx">表单列表操作</a> ＞</div>
        <div style="margin-bottom: 5px;">
           <a href="#" onclick="javascript:$('#tdrightarea').toggle();" style="float: right;">切换功能区</a> <a href="#" onclick="javascript:$('#divhelp').toggle();" style="float: right;">帮助?</a> 
            <asp:Button ID="btnAddNew" runat="server" Text="添加文章" OnClick="btnAddNew_Click" />
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width:100%;">
                        <tr>
                            <td valign="top">
                                <div>
                                    <asp:GridView ID="gvList" SkinID="gvcustomer" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound" OnRowEditing="gvList_RowEditing" OnRowDeleting="gvList_RowDeleting" onrowcancelingedit="gvList_RowCancelingEdit" onrowupdating="gvList_RowUpdating">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td id="tdrightarea"  valign="top">
                       
                                <div id="divtable" style="width:400px;border:solid 1px #97C193;padding:5px;margin:0 2px;" >
                                    <asp:DropDownList ID="dropTable" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropTable_SelectedIndexChanged">
                                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                                    </asp:DropDownList>
                                    <div style="width: 400px;">
                                        <asp:GridView ID="rptColumnList" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" DataKeyNames="ID" DataSourceID="AccessDataSource1" OnRowUpdated="rptColumnList_RowUpdated">
                                            <Columns>
                                                <asp:CheckBoxField DataField="isshow" HeaderText="显示" SortExpression="isshow" ItemStyle-Width="40px" ControlStyle-Width="20px" />
                                                <asp:BoundField HeaderText="ID" DataField="ID" InsertVisible="False" ReadOnly="True" Visible="false" SortExpression="ID" />
                                                <asp:BoundField DataField="cname" HeaderText="名称" ItemStyle-Width="100px" ControlStyle-Width="100px" SortExpression="cname" />
                                                <asp:BoundField DataField="listwidth" HeaderText="列宽" SortExpression="listwidth" ItemStyle-Width="50px" ControlStyle-Width="20px" />
                                                <asp:BoundField DataField="listpx" HeaderText="排序" SortExpression="listpx" ItemStyle-Width="40px" ControlStyle-Width="20px" />
                                                <asp:CommandField ShowEditButton="true" ItemStyle-Width="60px" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="<%$ConnectionStrings:AccessDbPath  %>" SelectCommand="SELECT [ID], [cname], [listwidth], [isshow], [listpx] FROM [FormParas] WHERE ([formID] = ?)" UpdateCommand="UPDATE [FormParas] SET [cname] = ?, [listwidth] = ?, [isshow] = ?, [listpx] = ? WHERE [ID] = ?">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="dropTable" DefaultValue="0" Name="formID" PropertyName="SelectedValue" Type="Int32" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="cname" Type="String" />
                                                <asp:Parameter Name="listwidth" Type="Int32" />
                                                <asp:Parameter Name="isshow" Type="Boolean" />
                                                <asp:Parameter Name="listpx" Type="Int32" />
                                                <asp:Parameter Name="ID" Type="Int32" />
                                            </UpdateParameters>
                                        </asp:AccessDataSource>
                                    </div>
                                    <div style="padding: 5px;">
                                        分页:<asp:TextBox ID="txtPageSize" Text="0" runat="server"></asp:TextBox>
                                        0表示不分页<br />
                                        <asp:CheckBox ID="chkAutoColumn" Text="添加自增列" runat="server" />
                                        <asp:CheckBox ID="chkUpdate" Text="添加行修改列" runat="server" />
                                        <asp:CheckBox ID="chkEdit" Text="添加行编辑列" runat="server" />
                                        <asp:CheckBox ID="chkDelete" Text="添加删除列" runat="server" />
                                        <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click" />
                                        <asp:Label ID="lbltip" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="divhelp" style="border: solid 1px #333; display: none; background-color: #fff; position: absolute; top: 30px; left: 600px; width: 200px; height: 300px;">
        <a href="javascript:">帮助</a>
    </div>
    </form>
</body>
</html>
