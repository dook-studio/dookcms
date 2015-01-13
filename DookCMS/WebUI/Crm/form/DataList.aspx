<%@ Page Title="" Language="C#" MasterPageFile="~/Crm/crm.master" AutoEventWireup="true"
    CodeFile="DataList.aspx.cs" Inherits="Crm_form_DataList"  %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div style="padding: 0 5px;">
        <div class="nav" style="padding: 5px;">
            当前位置：<a href="formlist.aspx" style="color:#fff;">智能表单</a> ＞ <%=cname %></div>
        <div style="margin-bottom: 5px;">
            <input type="button" name="btnAddNew" value="录入数据" onclick="location.href='/crm.ashx?fn=form/smartform&formid=<%=Request["formid"] %>&from=crm';" id="btnAddNew" /></div>
        <div>
            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" 
                AllowPaging="false" AllowSorting="True" OnSorting="gvList_Sorting" OnRowDeleting="gvList_RowDeleting">
                <Columns> 
                <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                <a href="/crm.ashx?fn=form/smartform&formid=<%=Request["formid"] %>&pkid=<%#Eval("id") %>">修改</a>
                </ItemTemplate>
                </asp:TemplateField>            
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div>
        <span style="float: left; margin: 7px 10px 0 5px;">共<%=pager.RecordCount %>条 当前:<%=pager.CurrentPageIndex %>/<%=pager.PageCount %>页</span>
        <webdiyer:AspNetPager ID="pager" CssClass="paginator" CurrentPageButtonClass="cpb"
            runat="server" OnPageChanged="AspNetPager1_PageChanged" PageSize="20" PageIndexBoxType="TextBox"
            ShowPageIndexBox="Always" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <div style="clear: both;">
        </div>
    </div>
    <asp:HiddenField ID="hidcols" runat="server" />
    <asp:HiddenField ID="hidformid" runat="server" />
</asp:Content>
