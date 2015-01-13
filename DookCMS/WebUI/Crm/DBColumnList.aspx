<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBColumnList.aspx.cs" MasterPageFile="~/Crm/crm.master"
    Inherits="Crm_DBColumnList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">   
        $(document).ready(function()
        {         
            $("#btnAdd").click(function()
            {
                ShowIframe("添加字段", "editcolumn.aspx?tablename=<%=Request.QueryString["tablename"] %>", 530, 380);
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：<a style="color: White;" href="DBTableList.aspx">数据表</a> ＞<%=Request.QueryString["tablename"] %>
        的字段</div>
    <div style="margin-bottom: 5px;">
        <input id="btnAdd" value="添加字段" type="button" />
    </div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" AllowPaging="false"
        AllowSorting="True">
        <Columns>
            <asp:TemplateField HeaderText="编号">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="字段名称">
                <ItemTemplate>
                    <%#Eval("column_name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="数据类型">
                <ItemTemplate>
                    <%#StringHelper.AccessTypeEn(Eval("data_type").ToString())%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="character_maximum_length" HeaderText="长度" />
            <%--    <asp:BoundField DataField="tablename" HeaderText="小数" />--%>
            <%-- <asp:BoundField DataField="column_name" HeaderText="主键" />--%>
            <asp:TemplateField HeaderText="允许空">
                <ItemTemplate>
                    <%#IsPrimaryKey(Eval("column_name").ToString())?"<span style=\"color:#878787\">主键</span>":""%>
                    <%#Eval("is_nullable").ToString()=="True"?"√":""%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="column_default" HeaderText="默认值" />
            <asp:BoundField DataField="description" HeaderText="描述" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <a href="javascript:" onclick="ShowIframe('添加字段', 'editcolumn.aspx?tablename=<%=Request.QueryString["tablename"] %>&column_name=<%#Eval("column_name") %>', 530, 380);">
                        修改</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server"
                        CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
