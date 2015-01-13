<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Crm_ADList"
    MasterPageFile="~/Crm/crm.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav">
        当前位置：内容和广告管理</div>
    <asp:Button ID="btnAdd" runat="server" Text="新增一行" OnClick="btnAdd_Click" />
    <asp:DropDownList ID="droptype" runat="server" DataTextField="cname" DataValueField="id"
        AppendDataBoundItems="true" OnSelectedIndexChanged="droptype_SelectedIndexChanged"
        AutoPostBack="true">
        <asp:ListItem Value="">所有</asp:ListItem>
    </asp:DropDownList>
          <a href="sort.aspx">广告分类管理</a>
    <asp:GridView ID="gvList" runat="server" SkinID="gvcustomer" AutoGenerateColumns="False"
        DataKeyNames="id" AllowPaging="True" PageSize="25" OnRowCancelingEdit="gvList_RowCancelingEdit"
        OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" OnRowUpdating="gvList_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="序号" ItemStyle-Width="30px">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="adno" HeaderText="广告编码" SortExpression="adno" />
            <asp:BoundField DataField="adtype" HeaderText="所属类型" SortExpression="adtype" />
             <asp:TemplateField HeaderText="所属类型">
                <ItemTemplate>
                    <%#GetTypeName(Eval("adtype").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="cname" HeaderText="广告名称" SortExpression="cname" />

            <asp:BoundField DataField="title" HeaderText="标题" SortExpression="title" />
           <%-- <asp:ImageField HeaderText="缩略图" DataImageUrlField="litpic" NullDisplayText="" />--%>
            <asp:BoundField DataField="remark" HeaderText="备注" SortExpression="remark" />
            <asp:BoundField DataField="px" HeaderText="排序值" SortExpression="px" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="edit.aspx?id=<%#Eval("id") %>">修改</a>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server"
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
        <span style="float: left; margin-top: 15px;">共<%=total %>条 当前:<%=pager.CurrentPageIndex %>
            /
            <%=pagecount %>页</span>
        <div style="clear: both;">
        </div>
    </div>
</asp:Content>
