<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditContent.aspx.cs" Inherits="Crm_EditContent"
    MasterPageFile="~/Crm/crm.master" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../xheditor/xheditor-1.1.10-zh-cn.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(pageInit);
        function pageInit()
        {
            $('#<%=txtContent.ClientID %>').xheditor({ remoteImgSaveUrl: '/xheditor/saveremoteimg.aspx', upLinkUrl: "/xheditor/upload.aspx", upLinkExt: "zip,rar,txt", upImgUrl: "/xheditor/upload.aspx", upImgExt: "jpg,jpeg,gif,png", upFlashUrl: "/xheditor/upload.aspx", upFlashExt: "swf", upMediaUrl: "/xheditor/upload.aspx", upMediaExt: "wmv,avi,wma,mp3,mid", shortcuts: { 'ctrl+enter': submitForm} });
        }

        function submitForm() { }

        $(document).ready(function()
        {
            $("#btnSubmit").click(function()
            {
                if ($.trim($("#txtTitle").val()) == "")
                {
                    alert("请输入标题!");
                    $("#txtTitle").focus();
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
                添加/修改内容
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                所属分类：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="dropSort" runat="server" DataTextField="cname" DataValueField="bid">
                </asp:DropDownList>
            </td>
        </tr>     
        <tr>
            <td style="width: 100px">
                内容：
            </td>
            <td colspan="3">
                <textarea id="txtContent" rows="20" cols="7" runat="server" style="height: 500px;
                    width: 100%;"></textarea>
            </td>
        </tr>     
        <tr>
            <td>
            </td>
            <td colspan="3">
                <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                &nbsp;
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="还原默认值" />
                <asp:Label ID="lblTip" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table> 
</asp:Content>
