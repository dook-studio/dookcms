<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="list.aspx.cs"
    Inherits="Crm_ArticleList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/plugins/lhgdialog/lhgdialog.min.js?skin=idialog" type="text/javascript"></script>
    <style type="text/css">
        .img img
        {
            width: 50px;
            height: 50px;
        }
        .paginator{float:left;}
    </style>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            //上传文件
            $("a[data-tag]").click(function ()
            {
                var tag = $(this).attr("data-tag");
                $.dialog({
                    autoSize: false, cover: true, bgcolor: '#000', opacity: 0.3, id: "cfdg", maxBtn: false, title: "选择推送位", content: "url:/crm/ad/adselector.aspx?fromtype=article&fromid=" + tag + "&rnd=" + Math.random(), min: false, max: false, lock: true,width:600
                });
            });
        });
</script>
<script type="text/javascript">
    $(document).ready(function ()
    {
        jQuery.BindIds = function ()
        {
            $("#<%=hidselids.ClientID %>").val("");
            var ids = "";
            $("#divData [data-check]").each(function (i, item)
            {
                if ($(item).attr("checked") == true)
                {
                    ids += $(item).val() + ",";
                }
            });
            if (ids.length > 0)
            {
                ids = ids.substr(0, ids.length - 1);
            }
            $("#<%=hidselids.ClientID %>").val(ids);
        }
        $("#btnselall").click(function ()
        {

            $("#divData [data-check]").attr("checked", true);
            $.BindIds();
        });
        $("[data-check]").click(function ()
        {
            $.BindIds();
        })
        $("#btnnosel").click(function ()
        {
            $("#divData [data-check]").each(function (i, item)
            {
                if ($(item).attr("checked") == true)
                {
                    $(item).attr("checked", false);
                }
                else
                {
                    $(item).attr("checked", true);
                }
            });
            $.BindIds();
        });
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：<a style="color: #fff;" href="list.aspx">文章列表</a> ＞</div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="添加文章" OnClick="btnAddNew_Click" />
        &nbsp;&nbsp;&nbsp; 请输入标题：<asp:TextBox ID="txtSearch" runat="server" Width="177px"></asp:TextBox>&nbsp;
        <asp:DropDownList ID="dropSort" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
            OnSelectedIndexChanged="dropSort_SelectedIndexChanged">
            <asp:ListItem Value="">全部分类</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="搜 索" OnClick="btnSearch_Click" />
        &nbsp;&nbsp;
        <asp:Button style="float:right;" ID="btnClear" runat="server" Text="清空数据" OnClick="btnClear_Click"  OnClientClick="return confirm('警告:该功能将清空该表数据,确定吗?');"/>
    </div>
     <div id="divData">
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        AllowPaging="false" AllowSorting="True">
        <Columns>
            <%-- <asp:TemplateField HeaderText="编号">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %></ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" ItemStyle-HorizontalAlign="Center"
                SortExpression="id" InsertVisible="False" Visible="true" />
            <asp:TemplateField HeaderText="选择" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                      <input type="checkbox" value='<%#Eval("id") %>' data-check="myid"/>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:ImageField NullDisplayText="" DataImageUrlField="picurl" ItemStyle-CssClass="img"
                ItemStyle-Width="50px" HeaderText="图片地址" />--%>
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a href="/item.ashx?aid=<%#Eval("id") %>" target="_blank">
                        <%#Eval("title") %></a><span style="color: Red;"><%#GetFlashStr(Eval("flag").ToString())%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="typeid" HeaderText="版块编号" SortExpression="typeid" ItemStyle-HorizontalAlign="Center" />
            <asp:CheckBoxField DataField="isshow" HeaderText="是否显示" SortExpression="isshow" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="click" HeaderText="点击数" SortExpression="click" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="addtime" HeaderText="发布时间" SortExpression="addtime" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <a href="edit.aspx?id=<%#Eval("id") %>">修改</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a style="cursor:pointer;" data-tag="<%#Eval("id") %>">推送</a>
                    <%--   <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server"
                        CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>--%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <div><span style="float: left; margin: 6px 20px 6px 5px;">共<%=pager.RecordCount %>条 当前:<%=pager.CurrentPageIndex %>/<%=pager.PageCount %>页</span>
        <webdiyer:AspNetPager ID="pager" CssClass="paginator" CurrentPageButtonClass="cpb"
            runat="server" OnPageChanged="AspNetPager1_PageChanged" PageSize="20" PageIndexBoxType="TextBox"
            ShowPageIndexBox="Always" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <div style="clear: both;">
        </div>
    </div>
    <div style="background-color:#ddd;border:solid 1px #666;padding:5px;">
    <div style="border:solid 1px #ddd;float:right;width:500px;"><asp:Button ID="btnSetFlag" runat="server"  style="float:right;" Text="设 置" 
             onclick="btnSetFlag_Click" />
        <asp:CheckBoxList ID="chklFlag" runat="server" RepeatColumns="20">
                    <asp:ListItem Value="h">头条[h]</asp:ListItem>
                    <asp:ListItem Value="c">推荐[c]</asp:ListItem>
                    <asp:ListItem Value="f">幻灯[f]</asp:ListItem>
                    <asp:ListItem Value="a">特荐[a]</asp:ListItem>
                    <asp:ListItem Value="s">滚动[s]</asp:ListItem>
                    <asp:ListItem Value="b">加粗[b]</asp:ListItem>
                    <asp:ListItem Value="p">图片[p]</asp:ListItem>
                    <asp:ListItem Value="j">跳转[j]</asp:ListItem>
                </asp:CheckBoxList></div>
         <input id="hidselids" runat="server" type="hidden" />
        <input id="btnselall" type="button" value="全选" /><input id="btnnosel" type="button" value="反选" /><asp:Button ID="btnDelete" runat="server" Text="删除所选" 
            onclick="btnDelete_Click" />
        <asp:Button ID="btnMove" runat="server" Text="移动所选到" onclick="btnMove_Click" />
        <asp:DropDownList ID="dropsort2" runat="server" AppendDataBoundItems="true">    
        </asp:DropDownList>   
        <asp:Label ID="lbltips" runat="server" ForeColor="Red" Text=""></asp:Label>        
    </div>
</asp:Content>
