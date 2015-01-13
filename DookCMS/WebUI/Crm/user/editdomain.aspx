<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editdomain.aspx.cs" Inherits="Crm_User_Editdomain"
    MasterPageFile="~/Crm/crm.master" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">   
   
    <script type="text/javascript">

        $(document).ready(function ()
        {
            $("#btnSubmit").click(function ()
            {
                if ($.trim($("#txtdomain").val()) == "")
                {
                    alert("请输入电子邮件!");
                    $("#txtdomain").focus();
                    return false;
                }
            });
       
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <table class="tblist" style="width: 850px; text-align: left">
        <colgroup>
            <col align="right" style="width: 100px;" class="bg1" />
        </colgroup>
        <tr class="ptitle" align="center">
            <td colspan="4" align="center">
                添加/修改域名
            </td>
        </tr>
      
        <tr>
            <td style="width: 100px">
                域名：
            </td>
            <td>
                <asp:TextBox ID="txtdomain" Width="380px" MaxLength="100" CssClass="inputtxt" runat="server"></asp:TextBox>              
            </td>
            <td>
                用户编号：
            </td>
            <td>
                <asp:TextBox ID="txtUserid" Width="150px" MaxLength="100" CssClass="inputtxt" runat="server"></asp:TextBox>
            </td>
        </tr>
      
    
        <tr>
            <td style="width: 100px">
                是否使用：
            </td>
            <td colspan="3">
                <asp:CheckBox ID="chkIsuse" Checked="false" runat="server" /><asp:Label ID="lblTip" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>        
     
        <tr>
            <td>
            </td>
            <td colspan="3">
                <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                &nbsp;
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="清 空 " />
                <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
