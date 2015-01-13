<%@ Page Title="" Language="C#" MasterPageFile="~/Crm/crm.master" AutoEventWireup="true" CodeFile="sort.aspx.cs" Inherits="Crm_ad_sort" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" Runat="Server">
   <div class="nav" style="padding: 5px;">
        当前位置：广告和推送 ＞</div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="添加分类" OnClick="btnAddNew_Click" /> <a href="list.aspx">返回广告管理</a></div>
    <asp:GridView ID="gvList" runat="server" SkinID="gvcustomer" AutoGenerateColumns="False"
            DataKeyNames="id" AllowPaging="false" PageSize="25"  OnRowCancelingEdit="gvList_RowCancelingEdit"
                            OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" OnRowUpdating="gvList_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cname" HeaderText="分类名称" SortExpression="cname" />
                <asp:BoundField DataField="px" HeaderText="排序值" SortExpression="px" />
                <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" HeaderText="操作" ButtonType="Button" />                
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

