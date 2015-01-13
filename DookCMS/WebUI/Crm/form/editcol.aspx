<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editcol.aspx.cs" Inherits="editcol" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>配置参数</title>
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/css/dpl/??base-min.css,forms-min.css"
        rel="stylesheet" />
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/??button/assets/dpl-min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://a.tbcdn.cn/s/kissy/1.3.0/seed-min.js"></script>
    <script type="text/javascript">
        KISSY.use("gallery/validation/1.0/", function (S, Validation)
        {
            var form = new Validation('#from1', {
                //attrname: "data-check",       
                isok: false,
                style: 'under'
            });
            S.Event.on("#btnok", "click", function ()
            {
                if (form.isValid())
                {
                    return true;
                }
                return false;
            });
        });

    </script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="../js/json2.js" type="text/javascript"></script>
    <script src="/crm/js/comm.js" type="text/javascript"></script>
    <script src="/plugins/lhgdialog/lhgdialog.min.js?skin=discuz" type="text/javascript"></script>
    <script type="text/javascript">
        ///获取信息
        jQuery.SetUI = function (datatype)//设置ui
        {
            switch (datatype)
            {
                case "text":
                case "password":
                    {
                        $("#inheight").hide();
                        $("#inselectvalue").hide();
                        $("#indatasource").hide();
                        break;
                    }
                case "textarea":
                case "editor":
                    {
                        $("#inheight").show();
                        $("#inselectvalue").hide();
                        $("#indatasource").hide();
                        break;
                    }
                case "checkbox":
                    {
                        $("#inplaceholder").hide();
                        break;
                    }
                case "select": //下拉框
                    {

                        break;
                    }
            }
        }
        $(document).ready(function ()
        {
            var jsonstr = $.trim($("#jsonstr").val());

            if (jsonstr != "")//如果是编辑字段
            {
                //{"htype":"text","colname":"mybaby","cname":"姓名","ismust":"0","ruleid":"","rule":"","width":"300px","maxlength":"100","defaultvalue":"张三","placeholder":"请填写真实姓名!","islist":"1","issearch":"1","ispx":"1"}
                var item = JSON.parse(jsonstr);
                $("#datatype").val(item.htype);
                $("#colname").val(item.colname);
                $("#cname").val(item.cname);
                if (item.ismust == "1")
                {
                    $($("input:radio[name='ismust']")[0]).attr("checked", true);
                } else
                {
                    $($("input:radio[name='ismust']")[1]).attr("checked", true);
                }
                $("#ismust").val(item.ismust);
                $("#ruleid").val(item.ruleid);
                $("#rule").val(item.rule);
                $("#width").val(item.width);
                $("#maxlength").val(item.maxlength);
                $("#defaultvalue").val(item.defaultvalue);
                $("#placeholder").val(item.placeholder);
                $("#islist").attr("checked", item.islist == "0" ? false : true);
                $("#issearch").attr("checked", item.issearch == "0" ? false : true);
                $("#ispx").attr("checked", item.ispx == "0" ? false : true);
                $.SetUI(item.htype);

            }
            else//添加字段
            {
                $.SetUI("text");
            }


            $("#btnChannel").click(function ()
            {
                $.dialog({
                    title: '选择栏目', id: "addnew", content: 'url:/crm/channel/poplist.aspx', autoSize: false, width: 500, max: false, lock: true
                });
            });
            $("#btnDir").click(function ()
            {
                $.dialog({
                    title: '选择字典项', id: "addnew", content: 'url:/crm/dictionary/poplist.aspx', autoSize: false, width: 500, max: false, lock: true
                });
            });

            jQuery.autocheck = function ()
            {
                var htmlstr = "";
                //var jsonstr =null;
                var datatype = $("#datatype").val();
                var name = $.trim($("#colname").val());
                var cname = $.trim($("#cname").val());
                var defaultvalue = $.trim($("#defaultvalue").val());
                var placeholder = $.trim($("#placeholder").val());
                var ph = placeholder;
                var ruleid = $("#selrule").val();
                var rule = $.trim($("#rule").val());
                if (placeholder.length > 0)
                {
                    placeholder = "placeholder=\"" + placeholder + "\"";
                }
                var maxlength = $.trim($("#maxlength").val());
                var width = $.trim($("#width").val());
                var height = $.trim($("#height").val());
                var datavalid = "{";
                var ismust = $("input:radio[name='ismust']:checked").val();
                var islist = $("#islist").is(':checked') == true ? "1" : "0";
                var issearch = $("#issearch").is(':checked') == true ? "1" : "0";
                var ispx = $("#ispx").is(':checked') == true ? "1" : "0";
                if (ismust == "0")
                {
                    datavalid += "required:false";
                    datavalid += "," + $.trim($("#rule").val());
                }
                else
                {
                    datavalid += $.trim($("#rule").val());
                }
                datavalid += "}";

                switch (datatype)
                {

                    //必须添加data-col="true"属性.用于前台进行判断 
                    case "text":
                    case "password":
                        {
                            htmlstr = '<input type="' + datatype + '" id="' + name + '" name="' + name + '" value="' + defaultvalue + '" style="width:' + width + '" maxlength="' + maxlength + '" ' + placeholder + ' data-valid="' + datavalid + '" data-col="' + datatype + '"/>';
                            jsonstr = { htype: datatype, colname: name, cname: cname, ismust: ismust, ruleid: ruleid, rule: rule, width: width, maxlength: maxlength, defaultvalue: defaultvalue, placeholder: ph, islist: islist, issearch: issearch, ispx: ispx };
                            $("#jsonstr").val(JSON.stringify(jsonstr));
                            break;
                        }
                    case "textarea": //多行文本框
                        {
                            htmlstr += '<textarea  id="' + name + '" name="' + name + '"  style="width:' + width + ';height:' + height + '" maxlength="' + maxlength + '" ' + placeholder + ' data-valid="' + datavalid + '"  data-col="' + datatype + '">' + defaultvalue + '</textarea>';
                            jsonstr = { htype: datatype, colname: name, cname: cname, ismust: ismust, ruleid: ruleid, rule: rule, width: width, maxlength: maxlength, defaultvalue: defaultvalue, placeholder: ph, islist: islist, issearch: issearch, ispx: ispx };
                            $("#jsonstr").val(JSON.stringify(jsonstr));
                            break;
                        }
                    case "editor":
                        {
                            htmlstr += '<textarea  id="' + name + '" name="' + name + '"  style="width:' + width + ';height:' + height + '" maxlength="' + maxlength + '" ' + placeholder + ' data-valid="' + datavalid + '" data-col="' + datatype + '">' + defaultvalue + '</textarea>';
                            htmlstr += "<script type=\"text/javascript\">var editor = new baidu.editor.ui.Editor();editor.render('" + name + "');</scr" + "ipt>";
                            jsonstr = { htype: datatype, colname: name, cname: cname, ismust: ismust, ruleid: ruleid, rule: rule, width: width, maxlength: maxlength, defaultvalue: defaultvalue, placeholder: ph, islist: islist, issearch: issearch, ispx: ispx };
                            $("#jsonstr").val(JSON.stringify(jsonstr));
                            break;
                        }
                    case "select":
                        {
                            htmlstr = '<select  id="' + name + '" name="' + name + '" data-col="' + datatype + '" data-valid="' + datavalid + '">';
                            var ops = defaultvalue.split(',');
                            $(ops).each(function (i, op)
                            {
                                var opitems = op.split('|');
                                if (opitems.length > 2 && opitems[2] == "1")
                                {
                                    htmlstr += '<option value="' + opitems[0] + '" selected="selected">' + opitems[1] + '</option>';
                                }
                                else
                                {
                                    htmlstr += '<option value="' + opitems[0] + '">' + opitems[1] + '</option>';
                                }
                            });
                            htmlstr += '</select>';
                            jsonstr = { htype: datatype, colname: name, cname: cname, ismust: ismust, ruleid: ruleid, rule: rule, width: width, maxlength: maxlength, defaultvalue: defaultvalue, placeholder: ph, islist: islist, issearch: issearch, ispx: ispx };
                            $("#jsonstr").val(JSON.stringify(jsonstr));
                            break;
                        }
                    case "checkbox": //多选框
                        {

                            htmlstr = '<div id="' + name + '" data-col="' + datatype + '">';
                            var ops = defaultvalue.split(',');
                            $(ops).each(function (i, op)
                            {
                                var opitems = op.split('|');
                                if (opitems.length > 2 && opitems[2] == "1")
                                {
                                    htmlstr += '<label><input value="' + opitems[0] + '" type="checkbox"  checked="checked" />' + opitems[1] + '</label>';
                                }
                                else
                                {
                                    htmlstr += '<label><input value="' + opitems[0] + '" type="checkbox"  />' + opitems[1] + '</label>';
                                }
                            });
                            htmlstr += '</div>';
                            jsonstr = { htype: datatype, colname: name, cname: cname, ismust: ismust, ruleid: ruleid, rule: rule, width: width, maxlength: maxlength, defaultvalue: defaultvalue, placeholder: ph, islist: islist, issearch: issearch, ispx: ispx };
                            $("#jsonstr").val(JSON.stringify(jsonstr));
                            break;
                        }

                    case "radio": //单选框
                        {
                            htmlstr = '<div>';
                            var ops = defaultvalue.split(',');
                            $(ops).each(function (i, op)
                            {
                                var opitems = op.split('|');
                                if (opitems.length > 2 && opitems[2] == "1")
                                {
                                    htmlstr += '<label><input value="' + opitems[0] + '" name="' + name + '" type="radio"  checked="checked" data-col="' + datatype + '"/>' + opitems[1] + '</label>';
                                }
                                else
                                {
                                    htmlstr += '<label><input value="' + opitems[0] + '" name="' + name + '" type="radio" data-col="' + datatype + '" />' + opitems[1] + '</label>';
                                }
                            });
                            htmlstr += '</div>';
                            jsonstr = { htype: datatype, colname: name, cname: cname, ismust: ismust, ruleid: ruleid, rule: rule, width: width, maxlength: maxlength, defaultvalue: defaultvalue, placeholder: ph, islist: islist, issearch: issearch, ispx: ispx };
                            $("#jsonstr").val(JSON.stringify(jsonstr));
                            break;
                        }
                    case "picurl": //选择单张图片
                        {
                            htmlstr = '<input id="' + name + '" name="'+name+'" type="text" style="width:' + width + '" maxlength="100" ' + placeholder + ' data-valid="' + datavalid + '" data-type="upload"  data-col="' + datatype + '"/>';
                            htmlstr += '<input type="button" id="btn' + name + '" onloadstart="$.loadUpload(\'' + name + '\');" onclick="$.SingleUpload(\'' + name + '\');"  value="上传图片"/>';
                            jsonstr = { htype: datatype, colname: name, cname: cname, ismust: ismust, ruleid: ruleid, rule: rule, width: width, maxlength: maxlength, defaultvalue: defaultvalue, placeholder: ph, islist: islist, issearch: issearch, ispx: ispx };
                            $("#jsonstr").val(JSON.stringify(jsonstr));             
                            break;
                        }
                    case "bit": //是否选项
                        {
                            if (defaultvalue == "1")
                            {
                                htmlstr = '<label><input  id="' + name + '" name="' + name + '" type="checkbox"  checked="checked"  data-col="' + datatype + '"/></label>';
                            } else
                            {
                                htmlstr = '<label><input  id="' + name + '" name="' + name + '" type="checkbox" data-col="' + datatype + '"/></label>';
                            }
                            jsonstr = { htype: datatype, colname: name, cname: cname, ismust: ismust, ruleid: ruleid, rule: rule, width: width, maxlength: maxlength, defaultvalue: defaultvalue, placeholder: ph, islist: islist, issearch: issearch, ispx: ispx };
                            $("#jsonstr").val(JSON.stringify(jsonstr));
                            break;
                        }
                    case "url":
                    case "hidden": //隐藏区域,地址栏参数
                        {
                            htmlstr = '<input type="hidden" id="' + name + '" name="' + name + '" value="' + defaultvalue + '" data-col="' + datatype + '"/>';
                            jsonstr = { htype: datatype, colname: name, cname: cname, ismust: ismust, ruleid: ruleid, rule: rule, width: width, maxlength: maxlength, defaultvalue: defaultvalue, placeholder: ph, islist: islist, issearch: issearch, ispx: ispx };
                            $("#jsonstr").val(JSON.stringify(jsonstr));
                            break;
                        }


                }
                $("#htmlstr").val(htmlstr);
            }


            $("#datatype").change(function ()
            {
                var datatype = $(this).val();
                $.SetUI(datatype);
                $.autocheck();
            });
            $("#selrule").change(function ()//选择规则
            {
                $("#rule").val($(this).val());
            });
        });
    </script>
</head>
<body>
    <div class="container" style="width: auto; margin: 0 15px 0 25px;">
        <section id="horizontal">
            <div class="form-actions" style="padding: 5px 20px;">
                <h3>
                    编辑字段</h3>
                <div class="row">
                    <div class="span18">
                        <form id="from1" class="form-horizontal" action="/ashx/crmop.ashx?ac=updatecol" method="post"
                        runat="server">
                        <div class="control-group">
                            <label class="control-label" for="selcols">
                                对应字段</label>
                            <div class="controls">
                                <input type="text" class="input-xlarge" id="colname" name="colname" style="width: 200px"
                                    data-valid="{}" onchange="$.autocheck();" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="input01">
                                中文名称</label>
                            <div class="controls">
                                <input type="text" class="input-xlarge" id="cname" name="cname" style="width: 200px"
                                    data-valid="{}" onchange="$.autocheck();" />
                                <span class="help-inline">请填写字段中文名称</span>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                是否必填</label>
                            <div class="controls">
                                <label class="radio control-inline">
                                    <input type="radio" class="span1" value="1" name="ismust" onchange="$.autocheck();" />
                                    是
                                </label>
                                <label class="radio control-inline">
                                    <input type="radio" class="span1" value="0" name="ismust" checked="checked" onchange="$.autocheck();" />
                                    否
                                </label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="select02">
                                验证规则</label>
                            <div class="controls docs-input-sizes">
                                <input type="text" class="span8" id="rule" name="rule" />
                                <asp:DropDownList ID="selrule" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Value="">不验证</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                字段类型</label>
                            <div class="controls docs-input-sizes">
                                <select id="datatype" name="datatype" onchange="$.autocheck();">
                                    <option value="text">文本框</option>
                                    <option value="password">密码框</option>
                                    <option value="textarea">多行文本框</option>
                                    <option value="editor">编辑器</option>
                                    <option value="select">下拉框</option>
                                    <option value="checkbox">多选项</option>
                                    <option value="radio">单选框</option>
                                    <option value="picurl">选择图片</option>
                                    <option value="multipicurl">选择多张图片</option>
                                    <option value="datetime">日期时间</option>
                                    <option value="datasource">关联表</option>
                                    <option value="url">地址参数</option>
                                    <option value="bit">是/否</option>
                                    <option value="hidden">隐藏域</option>
                                </select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="selcols">
                                宽度</label>
                            <div class="controls">
                                <input type="text" class="span2" id="width" value="200px" onchange="$.autocheck();" />
                            </div>
                        </div>
                        <div class="control-group" id="inheight">
                            <label class="control-label" for="selcols">
                                高度</label>
                            <div class="controls">
                                <input type="text" class="span2" id="height" value="200px" onchange="$.autocheck();" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="selcols">
                                maxlength</label>
                            <div class="controls">
                                <input type="text" class="input-small" id="maxlength" name="maxlength" data-valid="{}"
                                    value="50" onchange="$.autocheck();" />
                                <span class="help-inline">允许输入的最大字符数</span>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="selcols">
                                默认值</label>
                            <div class="controls">
                                <input type="text" class="span5" id="defaultvalue" name="defaultvalue" value="" onchange="$.autocheck();" />
                                <span id="spdefault" class="help-inline">下拉框这里填写候选项,格式:值|文本|选中,如0|男|0,1|女|1,默认选中女</span>
                            </div>
                        </div>
                        <div class="control-group" id="inplaceholder">
                            <label class="control-label" for="selcols">
                                默认提示</label>
                            <div class="controls">
                                <input type="text" class="span5" id="placeholder" value="" placeholder="比如这个文本框里效果提示"
                                    onchange="$.autocheck();" />
                            </div>
                        </div>
                        <div class="control-group" id="inselectvalue">
                            <label class="control-label" for="selcols">
                                选项值</label>
                            <div class="controls">
                                <input type="text" class="input-small" id="Text5" value="" />
                                <input id="btnChannel" type="button" class="ks-button" value="从栏目中选择" />
                                <input id="btnDir" type="button" class="ks-button" value="从数据字典中选择" />
                            </div>
                        </div>
                        <div class="control-group" id="indatasource">
                            <label class="control-label" for="selcols">
                                数据源</label>
                            <div class="controls">
                                <input type="text" class="span10" id="Text6" value="" placeholder="这里选择表的sql语句:如 select id,cname from ad" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                显示</label>
                            <div class="controls">
                                <label class="checkbox">
                                    <input type="checkbox" id="islist" name="isshow" onchange="$.autocheck();" />
                                    列表显示
                                </label>
                                <label class="checkbox">
                                    <input type="checkbox" id="issearch" name="issearch" onchange="$.autocheck();" />
                                    允许搜索
                                </label>
                                <label class="checkbox">
                                    <input type="checkbox" id="ispx" name="ispx" onchange="$.autocheck();" />
                                    允许排序
                                </label>
                            </div>
                        </div>
                        <div class="form-actions">
                            <input type="hidden" name="formid" value="<%=Request["id"] %>" />
                            <input type="hidden" name="colid" value="<%=Request["colid"] %>" />
                            <input id="btnok" type="submit" class="ks-button ks-button-primary" value="保存" />
                            <div class="ks-button">
                                取消</div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="textarea">
                                配置信息</label>
                            <div class="controls">
                                <textarea class="input-xlarge" id="jsonstr" runat="server" name="jsonstr" rows="3"
                                    style="width: 98%;" cols="20"></textarea>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="textarea">
                                html片段</label>
                            <div class="controls">
                                <textarea class="input-xlarge" id="htmlstr" runat="server" name="htmlstr" rows="2"
                                    style="width: 98%;" cols="20"></textarea>
                            </div>
                        </div>
                       <%-- <div id="hideditor" style="display: none;">
                            <script src="/ueditor/ueditor.config.js" type="text/javascript"></script>
                            <script src="/ueditor/ueditor.all.min.js" type="text/javascript"></script>
                        </div>--%>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    </div>
</body>
</html>
