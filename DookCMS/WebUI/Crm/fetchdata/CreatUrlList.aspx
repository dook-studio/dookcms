<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="CreatUrlList.aspx.cs"
    Inherits="Crm_FetchCreatUrlList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="http://a.tbcdn.cn/s/kissy/1.3.0/??button/assets/dpl-min.css" rel="stylesheet" />
<script type="text/javascript">
    $(document).ready(function ()
    {
        //上传文件
        $("a[data-tag]").click(function ()
        {
            var tag = $(this).attr("data-tag");
            $.dialog({
                autoSize: false, cover: true, bgcolor: '#000', opacity: 0.3, id: "cfdg", maxBtn: false, title: "选择推送位", content: "url:/crm/ad/adselector.aspx?fromtype=article&fromid=" + tag + "&rnd=" + Math.random(), min: false, max: false, lock: true, width: 600
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
    <style type="text/css">
        .img img
        {
            width: 50px;
            height: 50px;
        }
        .paginator{float:left;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：<a style="color: #fff;" href="list.aspx">数据采集</a> ＞<%=cname %></div>
    <div style="margin: 5px;">      
        <asp:Button ID="btnGetUrls" runat="server" Text="获取采集网址" CssClass="ks-button" OnClick="btnGetUrls_Click" />&nbsp;&nbsp;  <asp:Button ID="btnCollect" runat="server" Text="开始采集"  CssClass="ks-button"
        onclick="btnCollect_Click" />
    </div>
     <div id="divData">
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        AllowPaging="false" AllowSorting="True">
        <Columns>       
            <asp:TemplateField HeaderText="选择" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                      <input type="checkbox" value='<%#Eval("id") %>' data-check="myid"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" ItemStyle-HorizontalAlign="Center"
                SortExpression="id" InsertVisible="False" Visible="true" />            
            <asp:BoundField DataField="title" HeaderText="标题" SortExpression="title" ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField DataTextField="surl" DataNavigateUrlFields="surl" Target="_blank" HeaderText="网址" SortExpression="url" ItemStyle-HorizontalAlign="Left"  />
            <asp:CheckBoxField DataField="iscollect" HeaderText="是否采集" SortExpression="iscollect" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="lasttime" HeaderText="采集时间" SortExpression="lasttime" ItemStyle-HorizontalAlign="Center" /> 
        </Columns>
        <EmptyDataTemplate>没有找到任何数据!</EmptyDataTemplate>
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
      <div>
         <input id="hidselids" runat="server" type="hidden" />
        <input id="btnselall" type="button" value="全选" class="ks-button" /> <input id="btnnosel" type="button" value="反选"  class="ks-button"/> <asp:Button ID="btnDelete" runat="server" Text="删除所选"  CssClass="ks-button"
            onclick="btnDelete_Click" />
        <asp:Button ID="btnMove" CssClass="ks-button" runat="server" Text="移动所选到" onclick="btnMove_Click" />
        <asp:DropDownList ID="dropsort2" runat="server" AppendDataBoundItems="true">    
        </asp:DropDownList>   
        <asp:Label ID="lbltips" runat="server" ForeColor="Red" Text="Label"></asp:Label>
    </div>
 
</asp:Content>
