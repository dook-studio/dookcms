<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="list.aspx.cs"
    Inherits="Crm_TemplateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav">
        当前位置：模板管理</div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="上传模板" OnClick="btnAddNew_Click" />&nbsp;&nbsp;&nbsp; <a href="visualedit.aspx" target="_blank">可视化编辑模板</a>
    </div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
        <Columns>
            <%--  <asp:TemplateField HeaderText="模板图片">
                    <ItemTemplate>
                        <img style="width:100px;" src="<%#Eval("coverimg")%>" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="模板名称">
                <ItemTemplate>
                    <%#Eval("cname") %>
                    <span style="color: Red;">
                        <%#(currentTemplate == Eval("ename").ToString()) ? "正在使用" : ""%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="英文名称" DataField="ename" />        
            <asp:BoundField HeaderText="模板目录" DataField="folder" />
            <asp:TemplateField HeaderText="编辑">
                <ItemTemplate>
                    <a href="/plugins/filewatch/filelist.aspx?root=~/templets/<%#Eval("folder") %>">文件列表</a> | <a href="edit.aspx?id=<%#Eval("ID") %>">
                        上传模板包</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" OnClientClick="return confirm('确认使用该模板？');" runat="server"
                        CausesValidation="False" CommandName="UseIt" CommandArgument='<%#Eval("ename").ToString()%>'
                        Text="使用该模板"></asp:LinkButton>&nbsp;&nbsp; |&nbsp;&nbsp; 
                    <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server"
                        CausesValidation="False" CommandName="Delete" CommandArgument='<%#Eval("ID").ToString()+"|"+Eval("folder").ToString() %>'
                        Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="数据包">
                <ItemTemplate>
                    <a href="inout.aspx?id=<%#Eval("ID") %>&cname=<%#Eval("cname") %>">导入或导出</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
