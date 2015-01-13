<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Crm_FetchDataEdit"  ValidateRequest="false"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑节点</title>
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/css/dpl/??base-min.css,forms-min.css"
        rel="stylesheet" />
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/??button/assets/dpl-min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            
        });
    </script>
</head>
<body>
    <div class="container" style="width: auto; margin: 0 15px 0 25px;">
        <div class="form-actions" style="padding: 5px 20px;">
            <h3>
                编辑节点</h3>
            <div class="row">
                <div class="span18">
                    <form id="from1" class="form-horizontal" runat="server">
                    <div class="control-group">
                        <label class="control-label" for="selcols">
                            节点名称</label>
                        <div class="controls">
                            <input type="text" class="input-xlarge" id="cname" style="width: 200px" runat="server" />
                        </div>
                    </div>
                      <div class="control-group">
                        <label class="control-label" for="input01">
                            类型</label>
                      <div class="controls radio" style="padding-top: 5px;">               
                              <asp:RadioButtonList ID="radlType" runat="server" RepeatColumns="2" Width="150px">
                                <asp:ListItem Value="0" Selected="True">文章</asp:ListItem>
                                <asp:ListItem Value="1" >图集</asp:ListItem>                               
                            </asp:RadioButtonList>               
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="input01">
                            编码</label>
                     <div class="controls radio" style="padding-top: 5px;">                  
                              <asp:RadioButtonList ID="radlEncoding" runat="server" RepeatColumns="4" Width="300px">
                                <asp:ListItem Value="0" Selected="True">自动检测</asp:ListItem>                                
                                <asp:ListItem Value="gb2312">gb2312</asp:ListItem>
                                <asp:ListItem Value="utf-8">utf-8</asp:ListItem>
                                <asp:ListItem Value="BIG5">BIG5</asp:ListItem>
                            </asp:RadioButtonList>               
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            匹配模式</label>
                        <div class="controls radio" style="padding-top: 5px;">
                            <asp:RadioButtonList ID="radlRuleType" runat="server" RepeatColumns="2" Width="150px">
                                <asp:ListItem Value="1">正则表达式</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">字符串</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            防盗链</label>
                        <div class="controls radio" style="padding-top: 5px;">
                            <asp:RadioButtonList ID="radlDoorchain" runat="server" RepeatColumns="2" Width="150px">
                                <asp:ListItem Value="1">开启</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">关闭</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-actions">
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="select02">
                            来源属性</label>
                        <div class="controls docs-input-sizes">
                            <input type="text" class="span8" id="rule" runat="server" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            批量生成地址</label>
                     <div class="controls radio" style="padding-top: 5px;">
                               <asp:RadioButtonList ID="radllisttype" runat="server" RepeatColumns="3" Width="350px">
                                <asp:ListItem Value="0">批量生成列表网址</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">手工指定列表网址</asp:ListItem>
                                <asp:ListItem Value="2">从RSS中获取</asp:ListItem>
                            </asp:RadioButtonList>             
                            <br />
                            <br />
                            匹配网址：<input type="text" class="span8" id="Text1" value="" />
                            <input id="Submit1" type="button" class="ks-button" value="测试" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="selcols">
                            手工指定网址</label>
                        <div class="controls">
                            <textarea id="txtlisturl" style="height: 150px; width: 98%;" rows="5" cols="5" runat="server"></textarea>
                        </div>
                    </div>
                    <div class="form-actions">
                        <h3>
                            文章网址匹配规则</h3>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="selcols">
                            区域开始HTML</label>
                        <div class="controls">
                            <textarea id="txthtmls" style="height: 60px; width: 98%;" rows="5" cols="20" runat="server"></textarea>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="selcols">
                            区域结束HTML</label>
                        <div class="controls">
                            <textarea id="txthtmle" style="height: 60px; width: 98%;" rows="5" cols="20" runat="server"></textarea>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="selcols">
                            链接中的图片</label>
                        <div class="controls radio" style="padding-top: 5px;">
                            <asp:RadioButtonList ID="radlHasThumb" runat="server" RepeatColumns="2" Width="180px">
                                <asp:ListItem Value="0" Selected="True">不处理</asp:ListItem>
                                <asp:ListItem Value="1">采集为缩略图</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="selcols">
                            网址筛选(正则表达式)</label>
                        <div class="controls">
                            必须包含：<input  type="text" class="span5" id="txtinurl" runat="server" value="" placeholder="包含的规则" /><br />
                            <br />
                            不能包含：<input type="text" class="span5" id="txtouturl" runat="server" value="" placeholder="" />
                        </div>
                    </div>
                      <div class="form-actions">
                        <h3>
                            内容页获取规则</h3>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="selcols">
                            区域开始HTML</label>
                        <div class="controls">
                            <textarea id="txtbodys" style="height: 60px; width: 98%;" rows="5" cols="20" runat="server"></textarea>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="selcols">
                            区域结束HTML</label>
                        <div class="controls">
                            <textarea id="txtbodye" style="height: 60px; width: 98%;" rows="5" cols="20" runat="server"></textarea>
                        </div>
                    </div>
                     <div class="control-group">
                        <label class="control-label" for="selcols">
                            使用代理</label>
                        <div class="controls radio" style="padding-top: 5px;">
                            <asp:RadioButtonList ID="radlagent" runat="server" RepeatColumns="2" Width="140px">
                                <asp:ListItem Value="0" Selected="True">不使用</asp:ListItem>
                                <asp:ListItem Value="1">开启</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="selcols">
                            代理cookies</label>
                        <div class="controls">
                          <textarea id="txtCookies" style="height: 60px; width: 98%;" rows="5" cols="20" runat="server" placeholder="只有开启代理这里填写的才能生效"></textarea>
                        </div>
                    </div>
                    <div class="form-actions">
                        <asp:Button ID="btnSubmit" CssClass="ks-button ks-button-primary" runat="server"
                            Text="提 交" onclick="btnSubmit_Click"></asp:Button>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
