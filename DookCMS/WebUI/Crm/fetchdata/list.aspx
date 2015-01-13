<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="list.aspx.cs"
    Inherits="Crm_FetchDataList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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
        当前位置：<a style="color: #fff;" href="list.aspx">数据采集</a> ＞</div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="添加节点" OnClick="btnAddNew_Click" />
        &nbsp;&nbsp;&nbsp; 请输入标题：<asp:TextBox ID="txtSearch" runat="server" Width="177px"></asp:TextBox>       
        &nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="搜 索" OnClick="btnSearch_Click" />
    </div>
     <div id="divData">
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        AllowPaging="false" AllowSorting="True">
        <Columns>       
            <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" ItemStyle-HorizontalAlign="Center"
                SortExpression="id" InsertVisible="False" Visible="true" /> 
            <asp:TemplateField HeaderText="节点名称">
                <ItemTemplate>
                    <a href="creaturllist.aspx?id=<%#Eval("id") %>">
                        <%#Eval("cname") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="type" HeaderText="类型" SortExpression="type" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="encoding" HeaderText="编码" SortExpression="encoding" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="addtime" HeaderText="添加时间" SortExpression="addtime" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="lasttime" HeaderText="最后采集时间" SortExpression="lasttime" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="links"  HeaderText="网址数" SortExpression="links" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <a href="edit.aspx?id=<%#Eval("id") %>">修改</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="xml.aspx" style="cursor:pointer;">高级</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="CreatUrlList.aspx?id=<%#Eval("id") %>" >获取地址</a>
                       <asp:LinkButton ID="LinkButton1"  OnClientClick="return confirm('确认要删除吗？');" runat="server"
                        CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <div><span style="float: left; margin-top: 15px;">共<%=pager.RecordCount %>条 当前:<%=pager.CurrentPageIndex %>/<%=pager.PageCount %>页</span>
        <webdiyer:AspNetPager ID="pager" CssClass="paginator" CurrentPageButtonClass="cpb"
            runat="server" OnPageChanged="AspNetPager1_PageChanged" PageSize="20" PageIndexBoxType="TextBox"
            ShowPageIndexBox="Always" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <div style="clear: both;">
        </div>
    </div>   
</asp:Content>
