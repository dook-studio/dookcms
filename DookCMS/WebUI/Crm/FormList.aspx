<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="FormList.aspx.cs"
    Inherits="Crm_FormList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：表单 ＞</div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="添加表单" OnClick="btnAddNew_Click" /></div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="false"
        AllowSorting="True" OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="编号">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="cname" HeaderText="表单名称" />
            <asp:BoundField DataField="upload_path" HeaderText="上传文件目录" />
            <asp:BoundField DataField="limitsize" HeaderText="限定上传大小" />
            <asp:BoundField DataField="remark" HeaderText="备注" />
            <asp:BoundField DataField="addtime" HeaderText="添加时间" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="editform.aspx?formid=<%#Eval("formId") %>">编辑</a> &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a
                        href="formColumnList.aspx?formid=<%#Eval("formId") %>">字段列表</a> &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a
                            href="formColumnList.aspx?formid=<%#Eval("formId") %>">生成表单</a> &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("formId") %>' runat="server"
                        CausesValidation="False" CommandName="Delete" Text="删除表"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
