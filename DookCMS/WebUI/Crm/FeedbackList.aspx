<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeedbackList.aspx.cs" Inherits="Crm_FeedbackList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="crm.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function()
        {
            var selobj = null;
            var selobjcolor = null;
            //d = this.style.backgroundColor; this.style.backgroundColor = '#D1F880';
            $(".trnei,.lupai").each(function()
            {

                $(this).click(function()
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
        <div class="nav">
            当前位置：留言反馈管理</div>
        <div style="margin-bottom: 5px;">
            </div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataSourceID="AccessDataSource1"
            DataKeyNames="ID" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                    SortExpression="ID" />
                <asp:BoundField DataField="email" HeaderText="电子邮件" SortExpression="email" />
                <asp:BoundField DataField="nickname" HeaderText="昵称" 
                    SortExpression="nickname" />
                <asp:BoundField DataField="contents" HeaderText="内容" 
                    SortExpression="contents" />
                <asp:BoundField DataField="addtime" HeaderText="留言时间" 
                    SortExpression="addtime" />        
                       <asp:TemplateField  HeaderText="操作" >
                <ItemTemplate>          
                    <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('确认要删除吗？');" runat="server"
                        CausesValidation="False" CommandName="Delete" Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="<%$ConnectionStrings:AccessDbPath  %>"
        
        SelectCommand="SELECT [ID], [email], [nickname], [contents], [addtime] FROM [FeedBack] order by id desc" DeleteCommand="DELETE FROM [FeedBack] WHERE [ID] = ?"
        InsertCommand="INSERT INTO [FeedBack] ([ID], [email], [nickname], [contents], [addtime]) VALUES (?, ?, ?, ?, ?)"
        
        UpdateCommand="UPDATE [FeedBack] SET [email] = ?, [nickname] = ?, [contents] = ?, [addtime] = ? WHERE [ID] = ?">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="nickname" Type="String" />
            <asp:Parameter Name="contents" Type="String" />
            <asp:Parameter Name="addtime" Type="DateTime" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="nickname" Type="String" />
            <asp:Parameter Name="contents" Type="String" />
            <asp:Parameter Name="addtime" Type="DateTime" />
        </InsertParameters>
    </asp:AccessDataSource>
    </form>
</body>
</html>
