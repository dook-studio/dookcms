<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Crm_Image_Edit"
    MasterPageFile="~/Crm/crm.master" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/js/Color.js" type="text/javascript"></script>
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
        });
    </script>
    <script type="text/javascript">
        function getparas(item)
        {
            var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i"));
            return svalue ? svalue[1] : svalue;
        }
        $(document).ready(function ()
        {
            //获取图片集列表.
            if (getparas("id"))
            {
                var jsonstr = $("#<%=hidjson.ClientID %>").val();
                var json = eval(jsonstr);
                var str = "<ul>";
                $(json).each(function (i, item)
                {
                    var title = (item.title == null ? "" : item.title);
                    str += "<li><img src=\"" + item.src + "\" alt=\"" + title + "\" /><div><input  type=\"text\" value=\"" + title + "\" /><br/><a style=\"cursor:pointer;\" onclick=\"$.Delnode(this);\">删除</a></div></li>";
                });
                str += "</ul>";
                $("#divpreview").html(str);
            }

            var editor = new baidu.editor.ui.Editor();
            editor.render('<%=txtBody.ClientID %>');

            $("#<%=btnSubmit.ClientID %>").click(function ()
            {
                $.GetImglistData();
                if ($.trim($("#<%=txtTitle.ClientID %>").val()) == "")
                {
                    alert("请输入标题!");
                    $("#<%=txtTitle.ClientID %>").focus();
                    return false;
                }
            });


            //上传单个文件 
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



            $("#btnUpload").click(function ()
            {
                d = imageobj.getDialog("insertimage");
                d.open();
            });


            var imageobjm = new UE.ui.Editor();
            imageobjm.render('btnUploadMutil');
            imageobjm.addListener('beforeInsertImage', function (t, arg)
            {
                var str;
                if ($.isArray(arg) == true)
                {
                    str = "";
                    $(arg).each(function (i, item)
                    {
                        var title = (item.title == null ? "" : item.title);
                        str += "<li><img src=\"" + item.src + "\" alt=\"" + title + "\" /><div><input  type=\"text\" value=\"" + title + "\" /><br/><a style=\"cursor:pointer;\" onclick=\"$.Delnode(this);\">删除</a></div></li>";
                    });
                }
                $(str).appendTo($("#divpreview"));
            });

            $("#btnUploadMutil").click(function ()
            {
                d = imageobjm.getDialog("insertimage");
                d.open();
            });
            jQuery.Delnode = function (obj)
            {
                $(obj).parents("li").remove();
            }
            jQuery.GetImglistData = function ()
            {
                var str = "[";
                $("#divpreview img").each(function (i, item)
                {
                    var title = $.trim($(this).parent().find("input").val());
                    str += "{\"title\":\"" + title + "\",\"src\":\"" + $(item).attr("src") + "\"},";
                });
                if (str.length > 1)
                {
                    str = str.substr(0, str.length - 1);
                }
                str += "]";
                $("#<%=hidjson.ClientID %>").val(str);
            }
        });
    </script>
    <style type="text/css">
        .sub_main { width: 670px; height: auto; }
        .tagPlate { height: auto; border: solid 1px #ddd; margin-top: 20px; }
        .mt0 { margin-top: 0 !important; }
        .tagPlate { height: auto; border: solid 1px #ddd; margin-top: 20px; }
        .tagPlate .plateName { display: inline-block; width: 20px; padding: 20px 10px; border: solid 1px #ddd; border-right: 0; position: absolute; margin: -1px 0 0 -41px; background: #fff; font-size: 20px; color: #b10000; font-family: "微软雅黑"; }
        .tagPlate .spe { background: #f4f4f4; }
        .tagPlate .first { border-top: 0; }
        .tagPlate .plateList { height: auto; padding: 10px; border-top: solid 1px #ddd; }
        
        #divpreview li { float: left; width: 105px; height: 155px; }
        #divpreview li input { margin: 2px; width: 87px; }
        #divpreview li img { width: 100px; height: 100px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <table class="tblist" style="width: 900px; text-align: left">
        <colgroup>
            <col align="right" style="width: 100px;" class="bg1" />
        </colgroup>
        <tr class="ptitle" align="center">
            <td colspan="4" align="center">
                添加/修改图集
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
                标题：
            </td>
            <td>
                <asp:TextBox ID="txtTitle" Width="380px" MaxLength="100" CssClass="inputtxt" runat="server"></asp:TextBox>
                <input name="DianCMS_Color" type="text" value="#000000" id="txtcolor" class="in1"
                    runat="server" onclick="rcolor(this,true)" title="点击选取颜色" style="background-color: #000000;
                    width: 60px; color: white;" />
            </td>
            <td>
                简短标题：
            </td>
            <td>
                <asp:TextBox ID="txtStitle" Width="150px" MaxLength="100" CssClass="inputtxt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                自定义属性：
            </td>
            <td colspan="3" class="non-border">
                <asp:CheckBoxList ID="chklFlag" runat="server" RepeatColumns="20">
                    <asp:ListItem Value="h">头条[h]</asp:ListItem>
                    <asp:ListItem Value="c">推荐[c]</asp:ListItem>
                    <asp:ListItem Value="f">幻灯[f]</asp:ListItem>
                    <asp:ListItem Value="a">特荐[a]</asp:ListItem>
                    <asp:ListItem Value="s">滚动[s]</asp:ListItem>
                    <asp:ListItem Value="b">加粗[b]</asp:ListItem>
                    <asp:ListItem Value="p">图片[p]</asp:ListItem>
                    <asp:ListItem Value="j">跳转[j]</asp:ListItem>
                </asp:CheckBoxList>
                <div>
                    <asp:TextBox ID="txtLink" Width="400px" MaxLength="255" runat="server"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                封面图片：
            </td>
            <td colspan="3">
                <input id="file1" type="text" runat="server" style="width: 300px;" class="inputtxt" />
                <input type="button" id="btnUpload" value="上传图片" />
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                Tags：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtTag" Width="300px" CssClass="inputtxt" runat="server"></asp:TextBox>
                标签之间用","号分隔.每个标签不要超过6个汉字
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 100px">
                图集：
            </td>
            <td colspan="3">
                <input id="btnUploadMutil" type="button" value="上传图片(可多张)" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div>
                    <ul id="divpreview">
                    </ul>
                </div>
                <input id="hidjson" type="hidden" runat="server" />
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 100px">
                内容：
            </td>
            <td colspan="3">
                <textarea id="txtBody" rows="20" cols="7" runat="server" style="width: 800px; height: 250px;"></textarea>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                是否显示：
            </td>
            <td colspan="3">
                <asp:CheckBox ID="chkIsshow" Checked="true" runat="server" /><asp:Label ID="lblTip"
                    runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr style="display: none;">
            <td style="width: 100px">
                点击率：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDots" Text="0" Width="20px" MaxLength="5" runat="server"></asp:TextBox>
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
                <asp:TextBox ID="txtOrders" Text="0" Width="20px" runat="server"></asp:TextBox>
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
