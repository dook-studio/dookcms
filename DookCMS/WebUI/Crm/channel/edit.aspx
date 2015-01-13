<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Crm_Channel_Edit"
    MasterPageFile="~/Crm/crm.master" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <script src="../../plugins/easyui/jquery.easyui.min.js" type="text/javascript"></script>
   <script src="/ueditor/ueditor.config.js" type="text/javascript"></script>
    <script src="/ueditor/ueditor.all.min.js" type="text/javascript"></script>
    <script type="text/javascript">   
        $(document).ready(function () {
            var editor = new baidu.editor.ui.Editor();
            editor.render('<%=txtBody.ClientID %>');        
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div class="nav" style="padding: 5px;">
        当前位置：<a style="color: #fff;" href="list.aspx">栏目管理</a> ＞</div>
    <div id="tt" class="easyui-tabs" data-options="tools:'#tab-tools'">
        <div title="高级设置" data-options="tools:'#p-tools'" style="padding: 20px;">
            <table class="tblist" style="width: 850px; text-align: left">
                <colgroup>
                    <col align="right" style="width: 100px;" class="bg1" />
                </colgroup>
                <tr class="ptitle" align="center">
                    <td colspan="4" align="center">
                        添加/编辑栏目
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        封面模板：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="diyfileurl" Width="250px" MaxLength="100" CssClass="inputtxt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        列表模板：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="listfileurl" Width="250px" MaxLength="100" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        内容模板：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="itemfileurl" Width="250px" MaxLength="100" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        列表生成目录：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="listrule" Width="250px" MaxLength="100" runat="server"></asp:TextBox>
                        <div style="color:#999;">这里是静态页面存放目录,必须以"/"开头和结尾.默认为:"/html/"</div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        内容页生成目录：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="itemrule" Width="250px" MaxLength="100" runat="server"></asp:TextBox>
                           <div style="color:#999;">这里是静态页面存放目录,必须以"/"开头和结尾.默认为:"/html/{yyyy}/{m}/{d}/"</div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        seo标题：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="seotitle" Width="250px" MaxLength="100" runat="server"></asp:TextBox>
                        {seotitle}
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        关键字：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="seokeywords" Width="400px" MaxLength="100" runat="server"></asp:TextBox>{seokeywords}
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        栏目描述：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="seodesc" Width="100%" MaxLength="200" TextMode="MultiLine" runat="server" Rows="3"></asp:TextBox>{seodesc}
                    </td>
                </tr>            
            </table>
        </div>
        <div title="栏目内容" style="padding: 20px;">
            <textarea id="txtBody" rows="20" cols="7" runat="server" style="width: 900px;height:200px;" width="100%"></textarea>
        </div>
    </div>
    <div style="padding: 10px 0 6px 20px;">
        <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
        <input type="button" value="返回" onclick="location.href='list.aspx?fid=<%=Request.QueryString["fid"] %>';" />
        &nbsp;
        <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
