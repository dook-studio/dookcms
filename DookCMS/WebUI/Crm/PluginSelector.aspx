<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PluginSelector.aspx.cs" Inherits="Crm_PluginSelector" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>插件选择器</title>
    <link href="css/default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>

    <script src="/Plugins/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function getparas(item)
        {
            var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i"));
            return svalue ? svalue[1] : svalue;
        }
        $(document).ready(function()
        {
            $(".seletor").click(function()
            {
                var originstr = getparas("edit");
                var fn = getparas("fn");
                var type = $(this).attr("tag");
                $.ajax({ url: "PluginSelector.aspx/SelectPlugins", data: "{\"originstr\":\"" + originstr + "\",\"type\":\"" + type + "\",\"fn\":\"" + fn + "\"}", dataType: "json", type: "POST", contentType: "application/json; charset=utf-8", success: function(res)
                {                    
                    if (res.d == "ok")
                    {
                        var DG = frameElement.lhgDG;
                        if (DG != null)
                        {
                            top.ReLoad();
                            DG.cancel();
                        }
                    }
                    else
                    {
                        alert(res.d);
                    }
                }
                });
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="dropList" runat="server" AppendDataBoundItems="true">
            <asp:ListItem Value="">--请选择分类--</asp:ListItem>
        </asp:DropDownList>
        请输入插件名称：<asp:TextBox ID="txtSearch" runat="server" Width="161px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="搜 索" OnClick="btnSearch_Click" />
    </div>
    <div style="margin: 8px 0;">
        <asp:GridView ID="gvList" runat="server" SkinID="gvcustomer" AutoGenerateColumns="False"
            DataKeyNames="id" AllowPaging="True" PageSize="25">
            <Columns>
                <asp:BoundField DataField="ename" HeaderText="英文名称" />
                <asp:BoundField DataField="cname" HeaderText="中文名称" />
                <asp:BoundField DataField="remark" HeaderText="介 绍" />
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <a class="seletor" href="javascript:" tag='<%#Eval("ename") %>'>选择</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div>
            <webdiyer:AspNetPager ID="pager" CssClass="paginator" CurrentPageButtonClass="cpb"
                runat="server" OnPageChanged="AspNetPager1_PageChanged" PageSize="20" PageIndexBoxType="TextBox"
                ShowPageIndexBox="Always" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
                NextPageText="下页" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
            </webdiyer:AspNetPager>
            <span style="float: left; margin-top: 15px;">共<%=pager.RecordCount %>条 当前:<%=pager.CurrentPageIndex %>
                /
                <%=pager.PageCount %>页</span>
            <div style="clear: both;">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
