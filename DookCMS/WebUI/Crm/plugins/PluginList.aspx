<%@ Page Title="" Language="C#" MasterPageFile="~/Crm/crm.master" AutoEventWireup="true"
    CodeFile="PluginList.aspx.cs" Inherits="Crm_plugins_PluginList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>插件管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：<a style="color: #fff;" href="PluginList.aspx">插件管理</a> ＞</div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="添加插件" OnClick="btnAddNew_Click" /></div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        AllowPaging="True" AllowSorting="True">
        <Columns>
            <asp:TemplateField HeaderText="编号">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" SortExpression="id"
                InsertVisible="False" Visible="false" />
            <asp:BoundField DataField="cname" HeaderText="中文名称" SortExpression="cname" />
            <asp:BoundField DataField="ename" HeaderText="英文名称" SortExpression="ename" />
            <asp:BoundField DataField="author" HeaderText="作者" SortExpression="author" />
            <asp:BoundField DataField="remark" HeaderText="备注" SortExpression="remark" />
            <asp:BoundField DataField="addtime" HeaderText="发布时间" SortExpression="addtime" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <a href="EditPlugin.aspx?id=<%#Eval("id") %>">修改</a>&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server"
                        CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>
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
</asp:Content>
