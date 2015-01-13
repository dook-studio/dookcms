<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="list.aspx.cs"
    Inherits="Crm_ProductCatList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .img img { width: 50px; height: 50px; }    
        a.nav { color: #fff; }
        .none { display: none; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：<a class="nav" style="color: #fff;" href="list.aspx">商品分类管理</a> ＞<%=GetNavLink(fid)%></div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="新 建" OnClick="btnAddNew_Click" />
    </div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
         AllowSorting="True" onrowdeleting="gvList_RowDeleting" 
        onrowcancelingedit="gvList_RowCancelingEdit" onrowediting="gvList_RowEditing"
        OnRowUpdating="gvList_RowUpdating" OnRowCommand="gvList_RowCommand" OnSorting="gvList_Sorting"
       >
        <Columns>
           <asp:TemplateField HeaderText="编号" >
                    <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                    <span style="color:#999;">-<%#Eval("id") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" Visible="false" SortExpression="id" InsertVisible="False" />
                <asp:BoundField DataField="pid" HeaderText="父级编号" SortExpression="pid" />               
                  <asp:TemplateField HeaderText="分类名称">
                    <ItemTemplate>
                        <a href="list.aspx?pid=<%#Eval("id") %>&pname=<%#Eval("cname") %>"><%#Eval("cname") %></a>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtkeytext" Text='<%#Eval("cname") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>    
                <asp:BoundField DataField="px" HeaderText="排序"  SortExpression="px" />

                <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('注意,请谨慎操作商品分类,确认要删除吗？');" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%#Eval("id") %>' Text="删除"></asp:LinkButton>
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
