<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="list.aspx.cs"
    Inherits="Crm_Channel_List" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("a[tag-id]").click(function ()
            {
                var id = $(this).attr("tag-id");
                $.get("/ashx/crmop.ashx?ac=channelsinglecreate", { bid: id }, function (res)
                {
                    if (res == "ok")
                    {
                        alert("生成成功!");
                    }
                    else
                    {
                        alert(res);
                    }
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：<a style="color: #fff;" href="list.aspx">栏目列表</a> ＞<%=GetNavLink(fid)%>
    </div>
    <div style="margin-bottom: 5px;">
        <asp:Button ID="btnAddNew" runat="server" Text="新建栏目" OnClick="btnAddNew_Click" />
        &nbsp;&nbsp;
        <asp:Button style="float:right;" ID="btnClear" runat="server" Text="清空数据" OnClick="btnClear_Click"  OnClientClick="return confirm('警告:该功能将清空该表数据,确定吗?');"/>
    </div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="bid"
        EnableModelValidation="True" onrowcancelingedit="gvList_RowCancelingEdit" 
        onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
        onrowupdating="gvList_RowUpdating" onsorting="gvList_Sorting" OnRowCommand="gvList_RowCommand">
        <Columns>
            <asp:BoundField DataField="bid" HeaderText="编号" ReadOnly="True" SortExpression="bid"
                InsertVisible="False" />
            <asp:TemplateField HeaderText="中文名" SortExpression="cname" ControlStyle-Width="60px">
                <ItemTemplate>
                    <a href="list.aspx?fid=<%#Eval("bid") %>">
                        <%#Eval("cname").ToString() %></a>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtcname" runat="server" Text='<%# Bind("cname") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ename" HeaderText="英文名" ControlStyle-Width="60px" SortExpression="ename" />
            <asp:BoundField DataField="fid" HeaderText="父编号" SortExpression="fid" />
            <asp:TemplateField HeaderText="模型" SortExpression="patternid" ItemStyle-ForeColor="#999999"
                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                <ItemTemplate>
                    <%#GetPatternidTypeCH(Eval("patternid").ToString())%></span>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="droppatternid" runat="server" SelectedValue='<%# Bind("patternid") %>'>
                        <asp:ListItem Value="0">文章</asp:ListItem>
                        <asp:ListItem Value="1">商品</asp:ListItem>
                        <asp:ListItem Value="2">图集</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="栏目类型" SortExpression="channeltype" ItemStyle-ForeColor="#999999">
                <ItemTemplate>
                    <%#Eval("channeltype")%>-
                    <%#GetChannelTypeCH(Eval("channeltype").ToString())%></span>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="dropchanneltype" runat="server" SelectedValue='<%# Bind("channeltype") %>'>
                        <asp:ListItem Value="0">封面</asp:ListItem>
                        <asp:ListItem Value="1">列表</asp:ListItem>
                        <asp:ListItem Value="2">外链</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <%-- <asp:BoundField DataField="channeltype" HeaderText="栏目类型" SortExpression="channeltype" />--%>
            <asp:TemplateField HeaderText="链接" SortExpression="link" ItemStyle-ForeColor="#999999">
                <ItemTemplate>
                    <%#GetLinkStr(Eval("channeltype").ToString(),Eval("bid").ToString(),Eval("link").ToString(),Eval("patternid").ToString() )%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtlink" runat="server" Text='<%# Bind("link") %>' Width="250px"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="px" HeaderText="px" SortExpression="px" />
            <asp:CheckBoxField DataField="isshow" HeaderText="显示" SortExpression="isshow" />
            <asp:CheckBoxField DataField="ismain" HeaderText="主栏目" SortExpression="ismain" />
            <asp:CommandField ShowEditButton="True" ButtonType="Button" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="edit.aspx?id=<%#Eval("bid") %>&fid=<%#Eval("fid") %>">高级</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server" CommandArgument='<%#Eval("bid") %>'
                        CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="javascript:" tag-id="<%#Eval("bid") %>">生成</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>   
      <div><span style="float: left; margin-top: 15px;">共<%=pager.RecordCount %>条 当前:<%=pager.CurrentPageIndex %>/<%=pager.PageCount %>页</span>
        <webdiyer:AspNetPager ID="pager" CssClass="paginator" CurrentPageButtonClass="cpb"
            runat="server" OnPageChanged="AspNetPager1_PageChanged" PageSize="20" PageIndexBoxType="TextBox"
            ShowPageIndexBox="Always" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <div style="clear: both;">
        </div>
    </div>    
    注释:栏目类型,0=封面,1=列表,2=外链;所属模型:0=文章,1=商品
</asp:Content>
