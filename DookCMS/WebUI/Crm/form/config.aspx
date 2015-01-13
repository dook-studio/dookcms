<%@ Page Language="C#" AutoEventWireup="true" CodeFile="config.aspx.cs" Inherits="config" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>配置参数</title>
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/css/dpl/??base-min.css,forms-min.css"
        rel="stylesheet" />
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/??button/assets/dpl-min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://a.tbcdn.cn/s/kissy/1.3.0/seed-min.js"></script>
    <script src="/js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            if("<%=Request.QueryString["id"] %>"!="")
            {
            $.get("/ashx/crmop.ashx?ac=getsingledata", {tbname:"Form",strwhere:"id=<%=Request.QueryString["id"] %>"}, function (res)
            {                
                var re=eval(res);
                $(re).each(function(i,item)
                {
                    $("input").each(function(j,citem)
                    {
                        var value=eval("item."+$(citem).attr("name"));                                  
                        $(citem).val(value);                        
                        if($(citem).attr("type")=="checkbox")
                        {                            
                            $(citem).attr("checked",value=="True"?true:false);
                        }                    
                    });

                    $("input[name='showtype']").eq(item.showtype).attr("checked",true);
             
                });
            });
            }
        });
    </script>
    <script type="text/javascript">
        KISSY.use("gallery/validation/1.0/", function (S, Validation)
        {
            var form = new Validation('#form1', {
                //attrname: "data-check",       
                isok: false,
                style: 'under'
            });
            $("#btnok").click(function ()
            {
                var op="add";
                  if("<%=Request.QueryString["id"] %>"!="")
            {
                op="update";
            }
                var where="id=<%=Request.QueryString["id"] %>";
                form.isValid();
                var cname=$("input[name='cname']").val();
                var remark=$("input[name='remark']").val();
                var tablename=$("input[name='tablename']").val();
                var pk=$("input[name='pk']").val();
                var showtype=$("input[name='showtype']:checked").attr("data-value");             
                var ischeckcode=$("input[name='ischeckcode']").attr("checked")==false?0:1;
                var isshowtitle=$("input[name='isshowtitle']").attr("checked")==false?0:1;
                var isopen=$("input[name='isopen']").attr("checked")==false?0:1;
                var gototips=$("input[name='gototips']").val();
                var gotourl=$("input[name='gotourl']").val();
                $.post("/ashx/postop.ashx?ac=customfrm&op="+op+"&tbname=form",{cname:cname,remark:remark,tablename:tablename,pk:pk,showtype:showtype,ischeckcode:ischeckcode,isshowtitle:isshowtitle,isopen:isopen,gototips:gototips,gotourl:gotourl,where:where},function(res)
                {
                    if(res=="ok")
                    {
                        alert("发布成功!");
                        location.href="formlist.aspx";
                    }
                    else
                    {
                    alert(res);
                    }
                });              
            });
        });
    </script>
</head>
<body>
    <div class="container" style="width: auto; margin: 0 15px 0 25px;">
        <section id="horizontal">
            <div class="form-actions" style="padding: 0px 20px;">
                <h3>
                    <%=title %></h3>
            </div>
            <div class="row">
                <div class="span18">
                    <form id="form1" class="form-horizontal" runat="server">
                    <fieldset>
                        <legend>表单全局设置</legend>
                        <div class="control-group">
                            <label class="control-label" for="input01">
                                中文名称</label>
                            <div class="controls">
                                <input type="text" id="cname" class="input-xlarge" name="cname" style="width: 200px"
                                    data-valid="{}" placeholder="给表单取个中文名称" />
                                <span class="help-inline">请填写表单中文名称</span>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="input01">
                                副标题</label>
                            <div class="controls">
                                <input type="text" class="span10" id="remark" name="remark" />
                                <div class="help-block">
                                    请填写表单描述副标题</div>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="tablename">
                                对应表</label>
                            <div class="controls">
                                <input type="text" id="tablename" class="input-xlarge" name="tablename" style="width: 100px"
                                    data-valid="{}" placeholder="填写英文名称" />
                                <asp:DropDownList ID="seltable" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Value="">选择已有表</asp:ListItem>
                                </asp:DropDownList>
                                <span class="help-inline">只能是英文数字或者下划线组合</span>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="tablename">
                                主键</label>
                            <div class="controls">
                                <input type="text" id="pk" class="input-xlarge" name="pk" style="width: 100px" data-valid="{}"
                                    placeholder="填写英文名称" value="id" />
                                <span class="help-inline">数据库主键字段</span>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                显示</label>
                            <div class="controls docs-input-sizes">
                                <label class="radio control-inline">
                                    <input name="showtype" data-value="0" checked="checked" class="span1" type="radio" />
                                    水平表单
                                </label>
                                <label class="radio control-inline">
                                    <input name="showtype" data-value="1" class="span1" type="radio" />
                                    垂直表单
                                </label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                显示</label>
                            <div class="controls">
                                <label class="checkbox">
                                    <input type="checkbox" name="ischeckcode" />
                                    验证码
                                </label>
                                <label class="checkbox">
                                    <input type="checkbox" name="isshowtitle" />
                                    显示标题
                                </label>
                                <label class="checkbox">
                                    <input type="checkbox" name="isopen" />
                                    允许访问
                                </label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="input01">
                                操作提示</label>
                            <div class="controls">
                                <input type="text" id="gototips" class="input-xlarge" value="提交成功!" name="gototips"
                                    style="width: 400px" placeholder="操作成功后显示的提示信息!" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="input01">
                                Url</label>
                            <div class="controls">
                                <input type="text" id="gotourl" class="input-xlarge" name="gotourl" style="width: 400px"
                                    placeholder="操作后跳转的页面地址" />
                            </div>
                        </div>
                        <div class="form-actions">
                            <div id="btnok" class="ks-button ks-button-primary">
                                保存</div>
                            <a class="ks-button" href="formlist.aspx">取消</a>
                        </div>
                    </fieldset>
                    </form>
                </div>
            </div>
        </section>
    </div>
</body>
</html>
