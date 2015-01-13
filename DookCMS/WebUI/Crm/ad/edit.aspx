<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Crm_Ad_Edit"
    MasterPageFile="~/Crm/crm.master" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/ueditor/ueditor.config.js" type="text/javascript"></script>
    <script src="/ueditor/ueditor.all.min.js" type="text/javascript"></script>
    <script src="/plugins/filewatch/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("#btnBrowser").dialog(
            {
                title: '选择文件', id: 'test9', page: '/plugins/filewatch/filelist.aspx?root=~/upload&fromid=<%=file1.ClientID %>&' + Math.random(), autoSize: false, width: 800, height: 500
            });
            var editor = new baidu.editor.ui.Editor();
            editor.render('<%=txtContent.ClientID %>');
            var imageobj = new baidu.editor.ui.Editor();
            imageobj.render('<%=file1.ClientID %>');
            imageobj.addListener('beforeInsertImage', function (t, arg)
            {
                if ($.isArray(arg) == true)
                {
                    $('#<%=file1.ClientID %>').val(arg[0].src);
                }
                else
                {
                    $('#<%=file1.ClientID %>').val(arg.src);
                }
                return false;
            });
            $("#<%=btnSubmit.ClientID %>").click(function ()
            {
                if ($.trim($("#<%=adno.ClientID%>").val()) == "")
                {
                    alert("请输入标题!");
                    $("#<%=adno.ClientID%>").focus();
                    return false;
                }
            });

            //上传单个文件
            jQuery.SingleUpload = function (inputobj)//接收的文本框控件
            {
                d = imageobj.getDialog("insertimage");
                d.render();
                d.open();
            }
            $("#btnUpload").click(function ()
            {
                $.SingleUpload($("#<%=file1.ClientID %>"));
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <table class="tblist" style="width: 900px; text-align: left">
        <colgroup>
            <col align="right" style="width: 100px;" class="bg1" />
        </colgroup>
        <tr class="ptitle" align="center">
            <td colspan="4" align="center">
                添加/修改广告
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                广告编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="adno" Width="100px" MaxLength="100" CssClass="inputtxt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                广告分类：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="dropadtype" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                中文名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="cname" Width="200px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                标题：
            </td>
            <td colspan="3">
                <asp:TextBox ID="title" Width="250px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                链接地址：
            </td>
            <td colspan="3">
                <asp:TextBox ID="link" Width="300px" MaxLength="200" runat="server"></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td style="width: 100px">
                摘要：
            </td>
            <td colspan="3">
                <asp:TextBox ID="brief" Width="100%" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td style="width: 100px">
                配置参数：
            </td>
            <td colspan="3">
                <textarea id="config" rows="20" cols="7" runat="server" style="height: 100px;width:100%;"></textarea>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                封面图片：
            </td>
            <td colspan="3">
                <input id="file1" type="text" runat="server" style="width: 300px;" class="inputtxt" />
                <input type="button" id="btnUpload"  value="上传图片"/>
                <input type="button" id="btnBrowser" value="浏览服务器" />
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 100px">
                内容：
            </td>
            <td colspan="3">               
                <textarea id="txtContent" rows="20" cols="7" runat="server" style="width:800px;height:250px;"></textarea>
            </td>
        </tr>
       
        <tr>
            <td style="width: 100px">
                是否显示：
            </td>
            <td colspan="3">
                <asp:CheckBox ID="chkIsShow" Checked="true" runat="server" /><input id="hidTypes"
                    runat="server" type="hidden" /><asp:Label ID="lblTip" runat="server" ForeColor="Red"></asp:Label>
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
            <td style="width: 100px">
                排序值：
            </td>
            <td colspan="3">
                <asp:TextBox ID="px" Text="0" Width="20px" runat="server"></asp:TextBox>
            </td>
        </tr>      
        <tr>
            <td>
            </td>
            <td colspan="3">
                <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                &nbsp;
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="清 空,重新发布" />
                <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
