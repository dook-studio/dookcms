<%@ Page Title="" Language="C#" MasterPageFile="~/Crm/crm.master" AutoEventWireup="true" CodeFile="EditPlugin.aspx.cs" Inherits="Crm_plugins_EditPlugin"  ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>修改插件配置</title>
<style type="text/css">
        .edit { border-style: inset; padding: 5px; font-size: 14px; }
    </style>
       <script src="../edit_area/edit_area_full.js" type="text/javascript"></script>
       <script language="Javascript" type="text/javascript">
           // initialisation
           editAreaLoader.init({
               id: "<%=txtJsonStr.ClientID %>"	// id of the textarea to transform		
			, start_highlight: true	// if start with highlight
			, allow_resize: "both"
			, allow_toggle: true
			, word_wrap: false
			, language: "zh"
			, syntax: "html"
           });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" Runat="Server">
<table class="tblist" style="width: 850px; text-align: left">
        <colgroup>
            <col align="right" style="width: 100px;" class="bg1" />
        </colgroup>
        <tr class="ptitle" align="center">
            <td colspan="4" align="center">
                添加/修改插件
            </td>
        </tr>   
        <tr>
            <td style="width: 100px">
                中文名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtCname" Width="300px" MaxLength="100" CssClass="inputtxt" runat="server"></asp:TextBox>             
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除该插件" />
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                英文名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtEname" Width="300px" MaxLength="100" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                作者：
            </td>
            <td colspan="3">
               <asp:TextBox ID="txtAuthor" Width="300px" MaxLength="100" runat="server"></asp:TextBox>
            </td>
        </tr>    
         <tr>
            <td style="width: 100px">
                备注：
            </td>
            <td colspan="3">
               <asp:TextBox ID="txtRemark" Width="400px" MaxLength="100" runat="server"></asp:TextBox>
            </td>
        </tr>
          <tr>
            <td style="width: 100px">
                配置参数:
            </td>
            <td colspan="3">              
                    <textarea id="txtJsonStr" style="height: 460px; width:100%" cols="6" rows="20" runat="server"></textarea>
                    <br />
                    参数是json字符串格式.
            </td>
        </tr>     
        <tr>
            <td>
            </td>
            <td colspan="3">
                <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                &nbsp;
                <input id="Reset1" type="reset" value="清 空,重新发布" />              
                <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

