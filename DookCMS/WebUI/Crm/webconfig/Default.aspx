<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Crm_webconfig_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>网站设置</title>
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/css/dpl/??base-min.css,tables-min.css,forms-min.css"
        rel="stylesheet" />
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/??button/assets/dpl-min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://a.tbcdn.cn/s/kissy/1.3.0/seed-min.js"></script>
    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("#btnadd").click(function ()
            {
                var keytext = $.trim($("#keytext").val());
                if (keytext == "")
                {
                    alert("请填写参数名!");
                    return false;
                }
                var keyvalue = $.trim($("#keyvlaue").val());
                var type = $.trim($("#seltype").val());
                var remark = $.trim($("#remark").val());
                if (remark == "")
                {
                    alert("请填写参数说明!");
                    return false;
                }
                var px = $.trim($("#px").val());
                $.post("/ashx/postop.ashx?ac=customfrm&tbname=webconfig", { keytext: keytext, keyvalue: keyvalue, remark: remark, type: type, px: px }, function (res)
                {
                    if (res == "ok")
                    {
                        alert("添加成功");
                        location.href = location.href;
                    }
                    else
                    {
                        alert(res);
                    }
                });
            });
            $("a[data-id]").click(function ()
            {
                var obj = $(this);
                var keyid = $(obj).attr("data-id");
                var keyvalue = $($(obj).parent().parent().find(".myinput")[0]).val();
               
                var px = $($(obj).parent().parent().find(".myinput")[1]).val();
                $.post("/ashx/postop.ashx?ac=customfrm&tbname=webconfig&op=update", { keyvalue: keyvalue, px: px, where: "id=" + keyid + "" }, function (res)
                {
                    if (res == "ok")
                    {
                        alert("保存成功!");
                        //location.href = location.href;
                    } else
                    {
                        alert(res);
                    }
                });
            });

            //保存信息
            $("#btnok").click(function ()
            {

            });

            jQuery.DeleteKey = function (keytext)
            {
                $.post("/ashx/crmop.ashx?ac=singledelete", { tbname: "webconfig", strwhere: "keytext='" + keytext + "'" }, function (res)
                {
                    if (res == "ok")
                    {
                        location.href = location.href;
                    }
                    else
                    {
                        alert(res);
                    }
                });
            }


        });
    </script>
</head>
<body>
    <div class="container" style="width: auto; margin: 15px 15px 0 15px;">
        <div class="nav" style="background-color: #446B8F; padding: 5px; color: #fff;">
            当前位置：<a style="color: #fff;" href="list.aspx">网站设置</a> ＞</div>
        <div class="row">
            <div class="span21">
                <form id="form2" runat="server">
                <div style="margin: 10px 0;">
                    <div class="form-actions">
                        参数名(英文):
                        <input id="keytext" type="text" class="span2" />
                        &nbsp;&nbsp;参数值:
                        <input id="keyvalue" type="text" class="span4" />
                        &nbsp;&nbsp;描述:
                        <input id="remark" type="text" class="span4" />
                        <br />
                        <br />
                        排序值:<input id="px" type="text" value="0" class="span2" />
                        <select id="seltype">
                            <option value="0">单行</option>
                            <option value="1">多行</option>
                        </select>
                        <input id="btnadd" type="button" class="ks-button ks-button-primary" value="新增参数" />
                    </div>
                </div>
                <table class="table table-bordered table-striped">
                    <colgroup>
                        <col class="span3" />
                        <col class="span11" />
                        <col class="span2" />
                        <col class="span2" />
                        <col class="span3" />
                    </colgroup>
                    <thead>
                        <tr>
                            <th style="text-align: center;">
                                参数说明
                            </th>
                            <th>
                                参数值
                            </th>
                            <th>
                                排序值
                            </th>
                            <th>
                                标签调用
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptlist" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: right;">
                                        <%#Eval("remark") %>
                                    </td>
                                    <td>
                                        <%#Eval("type").ToString() != "1" ? "<input type='text' id='" + Eval("keytext") + "'  class=\"input-xlarge myinput\" name=\"" + Eval("keytext") + "\"   style=\"width: 90%;\" value=\"" + Eval("keyvalue") + "\" />" : "<textarea class=\"input-large myinput\" cols=\"20\" rows=\"2\" style=\"width: 90%;\">" + Eval("keyvalue") + "</textarea>"%>
                                    </td>
                                    <td>
                                        <input id="txtpx" class="span1 myinput" type="text" value="<%#Eval("px") %>" />
                                    </td>
                                    <td>
                                        {$sys.<%#Eval("keytext") %>}
                                    </td>
                                    <td><a  data-id="<%#Eval("id") %>"  class="ks-button">保存</a> &nbsp;&nbsp;<%#Eval("issys").ToString()=="True"?"":"<a href=\"javascript:$.DeleteKey('"+Eval("keytext")+"');\">删除</a>" %>                                  
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="form-actions">
                 <%--   <div id="btnok" class="ks-button ks-button-primary">
                        保存</div>--%>
                </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
