<%@ Page Language="C#" AutoEventWireup="true" CodeFile="colslist.aspx.cs" Inherits="colslist" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0rc/css/dpl/??base-min.css,forms-min.css,tables-min.css"
        rel="stylesheet" />
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/??button/assets/dpl-min.css" rel="stylesheet" />
    <style type="text/css">
    
    </style>
</head>
<body>
 <div class="nav" style="padding: 5px;">
            当前位置：<a href="formlist.aspx" style="color:blue;">智能表单</a> ＞ <%=cname %>> 编辑字段</div>
    <div class="container" style="width: auto; margin: 0 15px 0 25px;">
        <section id="horizontal">
            <h2>编辑字段</h2>
            <div class="row">
                <div class="span16">
                    <div class="form-actions">
                        <a id="btnadd" href="editcol.aspx?id=<%=Request["id"] %>" class="ks-button ks-button-primary">新增字段</a>
                    </div>
                    <form id="frm1" runat="server">
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>序号</th>
                                <th>中文名称</th>
                                <th>列名</th>
                                <th>类型</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody>   
                            <asp:Repeater ID="rptlist" runat="server" onitemcommand="rptlist_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Container.ItemIndex+1 %></td>
                                        <td><%#Eval("cname") %></td>
                                        <td><%#Eval("colname") %></td>
                                        <td><%#Eval("datatype") %></td>
                                        <td><a href="editcol.aspx?colid=<%#Eval("ID") %>&id=<%=Request["id"] %>">编辑</a></td>
                                        <td><asp:LinkButton Runat="server" OnClientClick="return confirm('确认要删除吗?');" CommandName ="del" CommandArgument= '<%# Eval("id") %>' ID="Linkbutton1">删除</asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table></form>
                </div>
            </div>
        </section>
    </div>
</body>
</html>
