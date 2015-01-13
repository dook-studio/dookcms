<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adselector.aspx.cs" Inherits="Crm_adselector"
    MasterPageFile="~/Crm/crm.master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/plugins/easyui/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/plugins/easyui/jquery.easyui.min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav">
        当前位置：选择广告</div>
    <asp:DropDownList ID="droptype" runat="server" DataTextField="cname" DataValueField="id"
        AppendDataBoundItems="true" OnSelectedIndexChanged="droptype_SelectedIndexChanged"
        AutoPostBack="true">
        <asp:ListItem Value="">所有</asp:ListItem>
    </asp:DropDownList>
    <asp:GridView ID="gvList" runat="server" SkinID="gvcustomer" AutoGenerateColumns="False"
        DataKeyNames="id" AllowPaging="True" PageSize="25" OnRowCancelingEdit="gvList_RowCancelingEdit"
        OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" OnRowUpdating="gvList_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="序号" ItemStyle-Width="30px">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="adno" HeaderText="广告编码" SortExpression="adno" />           
             <asp:TemplateField HeaderText="所属类型">
                <ItemTemplate>
                    <%#GetTypeName(Eval("adtype").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="cname" HeaderText="广告名称" SortExpression="cname" />         
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a  href="edit.aspx?id=<%#Eval("id") %>&fromtype=<%=fromtype %>&fromid=<%=fromid %>" target="frame1">选择</a>
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
