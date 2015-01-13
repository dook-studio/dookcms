<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditDBTable.aspx.cs"  MasterPageFile="~/Crm/crm.master"  Inherits="Crm_EditDBTable" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript">
      $(document).ready(function()
      {
          $("#btnSubmit").click(function()
          {
              if ($.trim($("#txtTableName").val()) == "")
              {
                  alert("请输入表名!");
                  $("#txtTableName").focus();
                  return false;
              }
          });
      });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" Runat="Server">
<table class="tblist" style="width: 800px; text-align: left">
            <colgroup>
                <col align="right" style="width: 100px;" class="bg1" />
            </colgroup>
            <tr class="ptitle" align="center">
                <td colspan="4" align="center">
                    添加表
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    表名称：
                </td>
                <td colspan="3">
                    U_<asp:TextBox ID="txtTableName" Width="200px" MaxLength="50" CssClass="inputtxt" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    初始字段：
                </td>
                <td colspan="3">
                <div>
                    <asp:CheckBoxList ID="chklColumn" runat="server" RepeatColumns="8" Width="650px">
                    <asp:ListItem Value="ID" Selected="True">自动编号[ID]</asp:ListItem>
                    <asp:ListItem Value="UserID" Selected="True">用户编号[UserID]</asp:ListItem>
                    <asp:ListItem Value="AdminID" Selected="True">管理员编号[AdminID]</asp:ListItem>
                    <asp:ListItem Value="addtime" Selected="True">发布时间[addtime]</asp:ListItem>
                    <asp:ListItem Value="uptime" Selected="True">更新时间[uptime]</asp:ListItem>
                    </asp:CheckBoxList></div>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    描述：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtBrief" Width="528px" MaxLength="500" TextMode="MultiLine" CssClass="inputtxt" runat="server" Height="61px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                    &nbsp;
                    <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
</asp:Content>
