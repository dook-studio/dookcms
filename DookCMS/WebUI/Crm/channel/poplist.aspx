<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="poplist.aspx.cs"
    Inherits="Crm_Channel_PopList" %>

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
        当前位置：<a style="color: #fff;" href="poplist.aspx">栏目列表</a> ＞<%=GetNavLink(fid)%>
    </div>  
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="bid"
        DataSourceID="accessdata" EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="bid" HeaderText="编号" ReadOnly="True" SortExpression="bid"
                InsertVisible="False" />
            <asp:TemplateField HeaderText="中文名" SortExpression="cname" ItemStyle-Width="200px">
                <ItemTemplate>
                    <a href="poplist.aspx?fid=<%#Eval("bid") %>">
                        <%#Eval("cname").ToString() %></a>
                </ItemTemplate>               
            </asp:TemplateField>
            <asp:BoundField DataField="ename" HeaderText="英文名" ControlStyle-Width="60px" SortExpression="ename" />
            <asp:BoundField DataField="fid" HeaderText="父编号" SortExpression="fid" />         

            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="javascript:">选择</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:AccessDataSource ID="accessdata" runat="server" DataFile="<%$ConnectionStrings:AccessDbPath  %>" 
        DeleteCommand="DELETE FROM [Channel] WHERE [bid] = ?" SelectCommand="SELECT [bid],[cname], [ename], [fid], xpath,link,[channeltype],[px], [isshow], [ismain],patternid  FROM [Channel] where fid=@fid order by px"
        UpdateCommand="UPDATE [Channel] SET [cname] = ?, [ename] = ?, [fid] = ?, [link] = ?,[channeltype] = ?, [px] = ?,  [isshow] = ?, [ismain] = ? ,patternid=? WHERE [bid] = ?">
        <SelectParameters>
            <asp:QueryStringParameter Name="fid" DefaultValue="0" QueryStringField="fid" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="bid" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="cname" Type="String" />
            <asp:Parameter Name="ename" Type="String" />
            <asp:Parameter Name="fid" Type="Int32" />
            <asp:Parameter Name="link" Type="String" />
            <asp:Parameter Name="channeltype" Type="Int32" />
            <asp:Parameter Name="px" Type="Int32" />
            <asp:Parameter Name="isshow" Type="Boolean" />
            <asp:Parameter Name="ismain" Type="Boolean" />
            <asp:Parameter Name="bid" Type="Int32" />
            <asp:Parameter Name="patternid" Type="Int32" />
        </UpdateParameters>
    </asp:AccessDataSource>
    <asp:SqlDataSource ID="sqlserverdata" runat="server" ConnectionString="<%$ ConnectionStrings:strWeb %>"
        DeleteCommand="DELETE FROM [Channel] WHERE [bid] = @bid" SelectCommand="SELECT [bid],[cname], [ename], [fid], xpath,link,[channeltype],[px], [isshow], [ismain],patternid FROM [Channel] where fid=@fid order by px"
        UpdateCommand="UPDATE [Channel] SET [cname] = @cname, [ename] = @ename, [fid] = @fid, [link] = @link,[channeltype] = @channeltype, [px] = @px,  [isshow] = @isshow, [ismain] =@ismain,patternid=@patternid WHERE [bid] = @bid">
        <SelectParameters>
            <asp:QueryStringParameter Name="fid" DefaultValue="0" QueryStringField="fid" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="bid" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="cname" Type="String" />
            <asp:Parameter Name="ename" Type="String" />
            <asp:Parameter Name="fid" Type="Int32" />
            <asp:Parameter Name="link" Type="String" />
            <asp:Parameter Name="channeltype" Type="Int32" />
            <asp:Parameter Name="px" Type="Int32" />
            <asp:Parameter Name="isshow" Type="Boolean" />
            <asp:Parameter Name="ismain" Type="Boolean" />
            <asp:Parameter Name="bid" Type="Int32" />
            <asp:Parameter Name="patternid" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>   
</asp:Content>
