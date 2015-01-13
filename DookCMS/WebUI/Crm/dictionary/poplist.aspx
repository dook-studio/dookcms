<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="poplist.aspx.cs"
    Inherits="Crm_dictionarypoplist" %>
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
        当前位置：<a class="nav" style="color: #fff;" href="poplist.aspx">数据字典</a> ＞<%=GetNavLink(fid)%></div>
   
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
                  <asp:TemplateField HeaderText="键文本(keytext)">
                    <ItemTemplate>
                        <a href="poplist.aspx?pid=<%#Eval("id") %>&pname=<%#Eval("keytext") %>"><%#Eval("keytext") %></a>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtkeytext" Text='<%#Eval("keytext") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                <asp:BoundField DataField="keyvalue" HeaderText="键值(keyvalue)" SortExpression="keyvalue" />       
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                    <a href="javascript:">选择</a> 
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
