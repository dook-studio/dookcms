<%@ Page Language="C#" %>
<script type="text/C#" runat="server">
protected override void OnInit(EventArgs e)
{        
    //如果session超时或者不存在
    if (Session["MyCrmUserName"] == null)
    {
        Response.Write("<script>window.top.location.replace( 'default.aspx');</sc"+"ript>");
        Response.End();
        return;
    }
}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="crm.css" rel="stylesheet" type="text/css" />
    <%--<style type="text/css">
.nav{color:#ffffff;}
</style>--%>

    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function ()
        {
            var selobj = null;
            var selobjcolor = null;
            //d = this.style.backgroundColor; this.style.backgroundColor = '#D1F880';
            $(".trnei,.lupai").each(function ()
            {
                $(this).click(function ()
                {
                    try
                    {
                        selobj.css("background-color", selobjcolor);
                    }
                    catch (ex)
                    {
                    }
                    selobjcolor = $(this).css("background-color");
                    $(this).css("background-color", "#D1F880");
                    selobj = $(this);
                });
            });

        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 5px;">
        <div class="nav" style="padding: 5px;">
            当前位置：<a style="color: #fff;" href="ArticleList.aspx">订单列表</a> ＞</div>
        <div style="margin-bottom: 5px;">
            </div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="id" AllowPaging="True" AllowSorting="True"  
            DataSourceID="AccessDataSource1" EnableModelValidation="True">
            <Columns>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" SortExpression="id" InsertVisible="False" Visible="false" />                
                <asp:BoundField DataField="order_no" HeaderText="订单号" SortExpression="order_no" />
                <asp:BoundField DataField="trade_no" HeaderText="交易号" SortExpression="trade_no" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />        
                <asp:BoundField DataField="cash" HeaderText="cash" ItemStyle-Width="30px" ControlStyle-Width="30px" SortExpression="cash" />                       
                <asp:BoundField DataField="signkey" HeaderText="机器码" SortExpression="signkey" />
                <asp:BoundField DataField="regcode" HeaderText="注册码"  ItemStyle-Wrap="false" ControlStyle-Width="250px" SortExpression="regcode" /> 
                <asp:BoundField DataField="addtime" HeaderText="注册时间" SortExpression="addtime" Visible="false"  />  
                <asp:BoundField DataField="updatetime" HeaderText="到期时间" SortExpression="updatetime" />                <asp:CommandField ShowEditButton="True" />                
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>                    
                        <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server" CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
        DataFile="<%$ConnectionStrings:AccessDbPath  %>" 
        DeleteCommand="DELETE FROM [U_Orders] WHERE [id] = ?" 
        SelectCommand="SELECT id,order_no,trade_no,email,signkey,regcode,addtime,updatetime,[cash] FROM [U_Orders] order by id desc" 
       UpdateCommand="UPDATE [U_Orders] SET [order_no] = ?, [trade_no] = ?, [email] = ?, [cash] = ?, [signkey] = ?, [regcode] = ?, [addtime] = ?, [updatetime] = ? WHERE [id] = ?">
         <UpdateParameters>
            <asp:Parameter Name="order_no" Type="String" />
            <asp:Parameter Name="trade_no" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="cash" Type="String" />
            <asp:Parameter Name="signkey" Type="String" />
            <asp:Parameter Name="regcode" Type="String" />
            <asp:Parameter Name="addtime" Type="DateTime" />
            <asp:Parameter Name="updatetime" Type="DateTime" />
            <asp:Parameter Name="id" Type="Int32" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>      
    </asp:AccessDataSource>
    </form>
</body>
</html>