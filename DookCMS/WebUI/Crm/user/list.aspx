<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="list.aspx.cs"
    Inherits="Crm_userlist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .img img { width: 50px; height: 50px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：<a style="color: #fff;" href="list.aspx">用户列表</a> ＞</div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="添加用户" OnClick="btnAddNew_Click" />
        &nbsp;&nbsp;&nbsp; 用户搜索：<asp:TextBox ID="txtSearch" runat="server" Width="177px"></asp:TextBox>&nbsp;
        <asp:DropDownList ID="dropSort" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
            OnSelectedIndexChanged="dropSort_SelectedIndexChanged">
            <asp:ListItem Value="">--全部--</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="搜 索" OnClick="btnSearch_Click" />
    </div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        AllowPaging="True" AllowSorting="True">
        <Columns>
           <%-- <asp:TemplateField HeaderText="编号">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %></ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" ItemStyle-HorizontalAlign="Center" SortExpression="id"
                InsertVisible="False" Visible="true" />
               
            <%--<asp:ImageField NullDisplayText="" DataImageUrlField="picurl" ItemStyle-CssClass="img"
                ItemStyle-Width="50px" HeaderText="图片地址" />--%>
            <asp:BoundField DataField="email" HeaderText="电子邮件" SortExpression="email"  ItemStyle-HorizontalAlign="Left"/>
            <asp:BoundField DataField="qq" HeaderText="qq" SortExpression="qq"  ItemStyle-HorizontalAlign="Center"/>
         <%--   <asp:CheckBoxField DataField="islock" HeaderText="是否锁定" SortExpression="islock" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="coins" HeaderText="点数" SortExpression="coins"  ItemStyle-HorizontalAlign="Center"/>--%>
            <asp:BoundField DataField="addtime" HeaderText="注册时间" SortExpression="addtime" ItemStyle-HorizontalAlign="Center"/>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <a href="edit.aspx?id=<%#Eval("id") %>">修改</a>&nbsp;&nbsp;|&nbsp;&nbsp;
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
