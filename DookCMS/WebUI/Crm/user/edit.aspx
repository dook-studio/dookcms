<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Crm_User_Edit"
    MasterPageFile="~/Crm/crm.master" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">   
   
    <script type="text/javascript">

        $(document).ready(function ()
        {
            $("#btnSubmit").click(function ()
            {
                if ($.trim($("#txtemail").val()) == "")
                {
                    alert("请输入电子邮件!");
                    $("#txtemail").focus();
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
                添加/修改用户
            </td>
        </tr>
      
        <tr>
            <td style="width: 100px">
                email：
            </td>
            <td>
                <asp:TextBox ID="txtemail" Width="380px" MaxLength="100" CssClass="inputtxt" runat="server"></asp:TextBox>              
            </td>
            <td>
                qq：
            </td>
            <td>
                <asp:TextBox ID="txtqq" Width="150px" MaxLength="100" CssClass="inputtxt" runat="server"></asp:TextBox>
            </td>
        </tr>
      
        <tr>
            <td style="width: 100px">
                密 码：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtpwd" Width="150px" CssClass="inputtxt" MaxLength="20" runat="server"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                金币：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtcoins" Width="150px" CssClass="inputtxt" MaxLength="20" runat="server"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" Width="100%" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
        </tr>
     
        <tr>
            <td style="width: 100px">
                是否锁定：
            </td>
            <td colspan="3">
                <asp:CheckBox ID="chkIslock" Checked="false" runat="server" /><asp:Label ID="lblTip" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>        
        <tr>
            <td style="width: 100px">
                发布时间：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtPublishTime" Width="300px" CssClass="inputtxt" MaxLength="50"
                    runat="server"></asp:TextBox>
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
