<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="list.aspx.cs"
    Inherits="Crm_webconfig" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .img img { width: 50px; height: 50px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：<a style="color: #fff;" href="list.aspx">网站设置</a> ＞</div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="添加参数" OnClick="btnAddNew_Click" />
    </div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        AllowPaging="True" AllowSorting="True" onrowdeleting="gvList_RowDeleting" 
        onrowcancelingedit="gvList_RowCancelingEdit" onrowediting="gvList_RowEditing"
        OnRowUpdating="gvList_RowUpdating" OnRowCommand="gvList_RowCommand" OnSorting="gvList_Sorting"
       >
        <Columns>
            <asp:TemplateField HeaderText="编号" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="编号"  ReadOnly="True" ItemStyle-HorizontalAlign="Center" SortExpression="id" 
                InsertVisible="False" Visible="false" />             
            <asp:BoundField DataField="keytext" HeaderText="名称" SortExpression="keytext"  ItemStyle-HorizontalAlign="Left" ControlStyle-Width="150px" ItemStyle-Width="150px"/>
            <asp:BoundField DataField="keyvalue" HeaderText="值" SortExpression="keyvalue"  ItemStyle-HorizontalAlign="Left" ControlStyle-Width="200px" ItemStyle-Width="200px" ItemStyle-Wrap="false"/>
            <asp:BoundField DataField="remark" HeaderText="备注" SortExpression="remark" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="px" HeaderText="排序" ItemStyle-Width="50px" ControlStyle-Width="50px"   SortExpression="px"  ItemStyle-HorizontalAlign="Center"/>   
            <asp:CommandField ShowEditButton="True" />  
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>                  
                       <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server"
                        CausesValidation="False" CommandName="Delete" Text="删除" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
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
