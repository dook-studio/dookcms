<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBColumnList.aspx.cs" Inherits="Mdbw_DBColumnList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="style/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html { overflow: auto; }
        #splitter { background-color: white; border: 1px solid #ccc; width: 100%; }
        #filetree { overflow: auto; padding: 0px 10px 10px 0px; min-width: 100px; }
        #fileinfo { min-width: 100px; overflow: auto;  /* No margin or border allowed */ }
        .switch-bar { background: #E5E5E5 url(images/toggle.png) no-repeat scroll 0px center; width: 10px; cursor: pointer; }
    </style>

    <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="lhgdialog/lhgdialog.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function()
        {
            reload();
            var selobj = null;
            var selobjcolor = null;
            $(".trnei,.lupai").each(function()
            {
                $(this).click(function()
                {
                    if (selobj != null)
                    {
                        selobj.css("background-color", selobjcolor);
                    }
                    selobjcolor = $(this).css("background-color");
                    $(this).css("background-color", "#D1F880");
                    selobj = $(this);
                });
            });
            var dg = null;
            jQuery.ShowPopColEdit = function(tbname, colname)
            {
                dg = new $.dialog({ cover: true, bgcolor: '#000', opacity: 0.3, id: "editcol", maxBtn: false, title: "修改字段" + colname, page: "/plugins/mdbwatch/editcolumn.aspx?tbname=" + tbname + "&colname=" + colname + "&" + Math.random(), width: 580, height: 380 });
                dg.ShowDialog();
            }
        });    
        function EndRequestHandler()
        {
            var selobj = null;
            var selobjcolor = null;
            $(".trnei,.lupai").each(function()
            {
                $(this).click(function()
                {
                    if (selobj != null)
                    {
                        selobj.css("background-color", selobjcolor);
                    }      
                    selobjcolor = $(this).css("background-color");
                    $(this).css("background-color", "#D1F880");
                    selobj = $(this);
                });

            });
            $("#btnAdd").dialog({ cover: true, bgcolor: '#000', opacity: 0.3, title: '添加字段', id: 'tupload', page: '/plugins/mdbwatch/editcolumn.aspx?tbname=' + $("#<%=hidtbname.ClientID %>").val(), autoSize: false, width: 600, height: 380 }); 
        }
        function reload()
        {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function()
        {
            $("#btnAdd").dialog({ cover: true, bgcolor: '#000', opacity: 0.3, title: '添加字段', id: 'tupload', page: '/plugins/mdbwatch/editcolumn.aspx?tbname=' + $("#<%=hidtbname.ClientID %>").val(), autoSize: false, width: 600, height: 380 });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding: 0 5px;">
                <div style="background-color: #E5E5E5; color: #CC3300; padding: 4px; border: solid 1px #D1D1D1;
                    border-bottom: 0;">
                    <div>
                    <input id="btnAdd" style="float: right;" value="添加字段" type="button" />
                    当前位置：<a href="Default.aspx">数据表</a> ＞<%=hidtbname.Value%>的字段</div>
                    <div class="nofloat">
                    </div>
                </div>
                <div>
                    <table id="splitter">
                        <tbody>
                            <tr valign="top">
                                <td id="tdleft" style="width: 200px; overflow: auto;">
                                    <div id="filetree">
                                        <asp:ListBox ID="listTable" runat="server" Width="100%" Height="600px" 
                                            style="padding:5px;border:solid 1px #f8f8f8;color:#333;line-height:2.0" 
                                            AutoPostBack="True" onselectedindexchanged="listTable_SelectedIndexChanged"></asp:ListBox>
                                    </div>
                                </td>
                                <td class="switch-bar" onclick="$('#tdleft').toggle();">
                                </td>
                                <td>
                                    <div id="fileinfo">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <img src="images/loading.gif" style="text-align: center;" alt="正在加载中..." align="middle" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <div style="margin: 6px;">
                                            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                AllowSorting="True" onrowcommand="gvList_RowCommand" 
                                                onrowdeleting="gvList_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="编号">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="字段名称">
                                                        <ItemTemplate>
                                                            <%#Eval("name")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="数据类型">
                                                        <ItemTemplate>
                                                            <%#Eval("type")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <%--    <asp:BoundField DataField="tablename" HeaderText="小数" />--%>                                                   
                                                    <asp:TemplateField HeaderText="允许空">
                                                        <ItemTemplate>
                                                            <%#Eval("pk").ToString()=="1"?"<span style=\"color:#878787\">主键</span>":""%>
                                                            <%#Eval("notnull").ToString()=="0"?"√":""%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="dflt_value" HeaderText="默认值" />
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <a href="javascript:" onclick="$.ShowPopColEdit('<%=hidtbname.Value %>','<%#Eval("name") %>');">
                                                                修改</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                                                            <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server"
                                                                CausesValidation="False" CommandName="Delete" CommandArgument='<%#Eval("name") %>' Text="删除"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="nofloat">
                </div>
            </div>
            <input type="hidden" id="hidtbname" runat="server" />
            <asp:Literal ID="litercopyright" runat="server"></asp:Literal>
            <asp:Button Style="display: none;" ID="btnRefresh" runat="server" Text="刷新列表" OnClick="btnRefresh_Click" />   
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
