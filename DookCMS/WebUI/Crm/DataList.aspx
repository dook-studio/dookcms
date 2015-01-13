<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="DataList.aspx.cs" Inherits="Crm_DataList" %>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="cbody" runat="Server">
   <div class="nav" style="padding: 5px;">
            当前位置：<a style="color:White;" href="DBTableList.aspx">数据表</a> ＞<%=Request.QueryString["tablename"] %> </div>
        <div style="margin-bottom: 5px;">
            <asp:Button ID="btnAddNew" runat="server" Text="添加列" OnClick="btnAddNew_Click" /></div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="true"  DataKeyNames="id"
            AllowPaging="true" SkinID="gvcustomer" PageSize="20"
            AllowSorting="True" ShowHeader="true" 
            OnPageIndexChanged="gvList_PageIndexChanged" 
            onpageindexchanging="gvList_PageIndexChanging" onrowcommand="gvList_RowCommand" 
            onrowdeleting="gvList_RowDeleting">
          <Columns>
          <asp:CommandField ShowDeleteButton="true" />
          </Columns>
        </asp:GridView>
</asp:content>
