<%@ Page Language="C#" AutoEventWireup="true" CodeFile="formlist.aspx.cs" Inherits="formlist" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>自定义表单管理</title>
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0rc/css/dpl/??base-min.css,forms-min.css,tables-min.css" rel="stylesheet" />
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/??button/assets/dpl-min.css" rel="stylesheet" />
</head>
<body>
    <div class="container" style="width:auto;margin:0 15px 0 25px;">
        <section id="horizontal">            
            <div class="row">
                <div >
                    <div class="form-actions">
                        <div id="btnok" onclick="location.href='config.aspx';" class="ks-button ks-button-primary">新增表单</div>
                    </div>
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>序号</th>
                                <th>中文名称</th>
                                <th>对应表</th>
                                <th>备注</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptlist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Container.ItemIndex+1 %></td>
                                        <td><a href="colslist.aspx?id=<%#Eval("id") %>"><%#Eval("cname")%></a></td>
                                        <td><%#Eval("tableName") %></td>
                                        <td><%#Eval("remark")%></td>
                                        <td><a href="config.aspx?id=<%#Eval("id") %>">修改</a> | <a href="colslist.aspx?id=<%#Eval("id") %>">编辑字段</a> | <a href="config.aspx?id=<%#Eval("id") %>">专家模式</a> | <a href="DataList.aspx?formid=<%#Eval("id") %>">管理数据</a> | <a href="/crm.ashx?fn=form/smartform&formid=<%#Eval("id") %>&from=crm">录入数据</a> | <a href="config.aspx?id=<%#Eval("id") %>">删除</a></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>                   
                </div>
            </div>
        </section>
    </div>
</body>
</html>
