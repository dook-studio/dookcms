<%@ Page Language="C#" AutoEventWireup="true" CodeFile="formcol.aspx.cs" Inherits="form_formcol" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>表单字段管理</title>
    <link rel="stylesheet" type="text/css" href="/plugins/easyui/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="/plugins/easyui/themes/icon.css">
    <script type="text/javascript" src="/plugins/easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="/plugins/easyui/jquery.easyui.min.js"></script>
    <script src="/plugins/easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/ecmascript">
        var products = [
		    { productid: 'FI-SW-01', name: '文本框' },
		    { productid: 'K9-DL-01', name: '下拉框' },
		    { productid: 'RP-SN-01', name: '单选框' },
		    { productid: 'RP-SN-01', name: '复选框' },
		    { productid: 'RP-LI-02', name: '多行文本框' },
		    { productid: 'FL-DSH-01', name: '编辑器' }
        ];
        var coltype = ["分给大家", "gfd"];
        function productFormatter(value)
        {
            for (var i = 0; i < products.length; i++)
            {
                if (products[i].productid == value) return products[i].name;
            }
            return value;
        }
        $(function ()
        {
            var lastIndex;
            $('#tt').datagrid({
                toolbar: [{
                    text: '删除列',
                    iconCls: 'icon-remove',
                    handler: function ()
                    {
                        var row = $('#tt').datagrid('getSelected');
                        if (row)
                        {
                            var index = $('#tt').datagrid('getRowIndex', row);
                            $('#tt').datagrid('deleteRow', index);

                            //ajax请求删除列
                        }
                    }
                }, '-', {
                    text: '保存',
                    iconCls: 'icon-save',
                    handler: function ()
                    {
                        $('#tt').datagrid('acceptChanges');
                    }
                }, '-', {
                    text: '撤销',
                    iconCls: 'icon-undo',
                    handler: function ()
                    {
                        $('#tt').datagrid('rejectChanges');
                    }
                }, '-', {
                    text: 'GetChanges',
                    iconCls: 'icon-search',
                    handler: function ()
                    {
                        var rows = $('#tt').datagrid('getChanges');
                        alert('changed rows: ' + rows.length + ' lines');
                    }
                }],
                onBeforeLoad: function ()
                {
                    $(this).datagrid('rejectChanges');
                },
                onClickRow: function (rowIndex)
                {
                    if (lastIndex != rowIndex)
                    {
                        //$('#tt').datagrid('endEdit', lastIndex);
                        $('#tt').datagrid('beginEdit', rowIndex);
                    }
                    lastIndex = rowIndex;
                }
            });

            //添加编号
            $("#divcol a").each(function ()
            {
                $(this).click(function ()
                {
                    var len = $('#tt').datagrid('getRows').length;
                    if (len > 0)
                    {
                        lastIndex = len;
                    } else
                    {
                        lastIndex = 0;
                    }

                    $('#tt').datagrid('appendRow', {
                        colname: $(this).attr("data-id"),
                        cname: $(this).attr("data-title")

                    });
                    lastIndex = $('#tt').datagrid('getRows').length - 1;
                    $('#tt').datagrid('beginEdit', lastIndex);
                    //$('#tt').datagrid('beginEdit', lastIndex);
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divcol" style="margin: 10px 0; line-height: 1.9;">
        <asp:Repeater ID="rptlist" runat="server">
            <ItemTemplate>
                <a data-id="<%#Eval("column_name") %>" data-title="<%#Eval("description")%>" class="easyui-linkbutton">
                    <%#Eval("column_name")%><%#Eval("description")%></a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div>
        <table id="tt" style="width: auto; height: auto" data-options="iconCls:'icon-edit',singleSelect:true,idField:'itemid',url:'datagrid_data2.json'"
            title="表单字段管理">
            <thead>
                <tr>
                    <th data-options="field:'colname',width:100">
                        字段名称
                    </th>
                    <th data-options="field:'cname',width:80,editor:{type:'text',options:{precision:1}}">
                        中文名称
                    </th>
                    <th data-options="field:'datatype',width:80,editor:{type:'combobox',
                            options:{
								valueField:'name',
								textField:'name',
								data:products
                            }
                            }">
                        字段类型
                    </th>
                    <th data-options="field:'maxlength',width:80,editor:'numberbox'">
                        maxlength
                    </th>
                    <th data-options="field:'width',width:60,editor:'numberbox'">
                        宽度
                    </th>
                    <th data-options="field:'height',width:60,editor:'numberbox'">
                        高度
                    </th>
                    <th data-options="field:'htmlstr',width:250,align:'center',editor:{type:'textarea',options:{on:'P',off:''}}">
                        html代码
                    </th>
                    <th data-options="field:'defaultvalue',width:80,editor:{type:'text',options:{precision:1}}">
                        默认值
                    </th>
                    <th data-options="field:'rule',width:60,align:'center',editor:{type:'text',options:{on:'P',off:''}}">
                        规则
                    </th>
                    <th data-options="field:'listwidth',width:80,editor:'numberbox'">
                        列宽
                    </th>
                    <th data-options="field:'isshow',width:60,align:'center',editor:{type:'checkbox',options:{on:'P',off:''}}">
                        列显示
                    </th>
                    <th data-options="field:'px',width:80,align:'right',editor:'numberbox'">
                        排序值
                    </th>
                    <th data-options="field:'listpx',width:80,align:'right',editor:'numberbox'">
                        列排序
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    </form>
</body>
</html>
