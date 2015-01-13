var scripts = [
"/js/jquery-1.4.4.min.js",
"/Plugins/lhgdialog/lhgdialog.min.js"
];
seriesLoadScripts(scripts, function()
{
    function getparas(item)
    {
        var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i"));
        return svalue ? svalue[1] : svalue;
    }
    $(document).ready(function()
    {
        $("div[edit]").mouseenter(function()
        {
            var me = $(this);
            var top = $(this).offset().top;
            var left = $(this).offset().left;
            var height = $(this).height() > 20 ? $(this).height() : 20;
            var edit = $(this).attr("edit");
            $("#drag_operate_box").stop().show();
            $("#drag_operate_box").animate({ top: top, left: left, width: $(this).width(), height: height }, 200);
            $("#drag_operate_box").find(".visual_title").text("  " + edit);
            if (edit.indexOf("placeholder") == 0)
            {

                $("#drag_operate_box").find(".visual_edit a").text("添加插件");
                $("#drag_operate_box").find(".visual_edit a").attr("tag", edit);
                $("#drag_operate_box").find(".op_delete a").hide();
            }
            else
            {
                $("#drag_operate_box").find(".visual_edit a").text("编辑");
                $("#drag_operate_box").find(".visual_edit a").attr("tag", edit);
                $("#drag_operate_box").find(".op_delete a").show();
                $("#drag_operate_box").find(".op_delete a").attr("tag", edit);
            }

        });
        $(".visual_edit a").click(function()
        {
            var dg;
            if ($(this).attr("tag") != "" && $(this).attr("tag") != null && $(this).attr("tag").indexOf("placeholder") == -1)
            {
                var paras = $(this).attr("tag").split('.');
                dg = new $.dialog({ autoSize: true, cover: true, bgcolor: '#000', opacity: 0.3, id: "showedit", maxBtn: false, title: "配置参数", page: "/plugins/" + paras[0] + "/config.html?type=" + paras[0] + "&id=" + paras[1] + "&fn=" + getparas("fn") + "&" + Math.random(), width: 600, height: 350, lockScroll: true });
            }
            else
            {//空白区域,选择插件
                dg = new $.dialog({ autoSize: true, cover: true, bgcolor: '#000', opacity: 0.3, id: "showedit2", maxBtn: false, title: "选择插件", page: "/crm/PluginSelector.aspx?edit=" + $(this).attr("tag") + "&fn=" + getparas("fn") + "&" + Math.random(), width: 600, height: 350, lockScroll: true });
            }
            dg.ShowDialog();
        });

        //删除插件
        $(".op_delete a").click(function()
        {
            if ($(this).attr("tag") != "" && $(this).attr("tag") != null && $(this).attr("tag") != "placeholder")
            {
                if (confirm("确认要删除该插件吗?"))
                {
                    var paras = $(this).attr("tag").split('.');
                    $.post("/ashx/postop.ashx?ac=plugindelete", { type: paras[0], id: paras[1], fn: getparas("fn") }, function(res)
                    {
                        if (res == "ok")
                        {
                            alert("插件已经删除");
                            top.ReLoad();
                        }
                        else
                        {
                            alert(res);
                        }
                    });
                }
            }
        });
        $("div[edit^=placeholder]").each(function()
        {
            $(this).html("<div style=\"padding:4px;border:dotted 1px #999;background-color: #ffffff; color: #999;overflow:hidden;\">该位置可以添加插件 宽:" + $(this).width() + "</div>");
        });

        //绑定高度
        $("div[edit]").each(function()
        {
            if ($.trim($(this).html()) == "")
            {
                $(this).html("<div style=\"padding:4px;border:dotted 1px #999;background-color: #ffffff; color: #999;overflow:hidden;\">" + $(this).attr("edit") + " 宽:" + $(this).width() + "</div>");
                $(this).height(30);
            }
        });
//        $("div").each(function()
//        {
//            $(this).append("<div style=\"padding:4px;border:dotted 1px #999;background-color: #ffffff; color: #999;overflow:hidden;\">" + $(this).attr("edit") + " 宽:" + $(this).width() + "</div>");
//        });
    });
});

/** 
* 串联加载指定的脚本
* 串联加载[异步]逐个加载，每个加载完成后加载下一个
* 全部加载完成后执行回调
* @param array|string 指定的脚本们
* @param function 成功后回调的函数
* @return array 所有生成的脚本元素对象数组
*/

function seriesLoadScripts(scripts, callback)
{
    if (typeof (scripts) != "object") var scripts = [scripts];
    var HEAD = document.getElementsByTagName("head").item(0) || document.documentElement;
    var s = new Array(), last = scripts.length - 1, recursiveLoad = function(i)
    { //递归
        s[i] = document.createElement("script");
        s[i].setAttribute("type", "text/javascript");
        s[i].onload = s[i].onreadystatechange = function()
        { //Attach handlers for all browsers
            if (!/*@cc_on!@*/0 || this.readyState == "loaded" || this.readyState == "complete")
            {
                this.onload = this.onreadystatechange = null; this.parentNode.removeChild(this);

                if (i != last) recursiveLoad(i + 1); else if (typeof (callback) == "function") callback();
            }
        }
        s[i].setAttribute("src", scripts[i]);
        HEAD.appendChild(s[i]);
    };
    recursiveLoad(0);
}

/**
* 并联加载指定的脚本
* 并联加载[同步]同时加载，不管上个是否加载完成，直接加载全部
* 全部加载完成后执行回调
* @param array|string 指定的脚本们
* @param function 成功后回调的函数
* @return array 所有生成的脚本元素对象数组
*/
function parallelLoadScripts(scripts, callback)
{
    if (typeof (scripts) != "object") var scripts = [scripts];
    var HEAD = document.getElementsByTagName("head").item(0) || document.documentElement, s = new Array(), loaded = 0;
    for (var i = 0; i < scripts.length; i++)
    {
        s[i] = document.createElement("script");
        s[i].setAttribute("type", "text/javascript");
        s[i].onload = s[i].onreadystatechange = function()
        { //Attach handlers for all browsers
            if (!/*@cc_on!@*/0 || this.readyState == "loaded" || this.readyState == "complete")
            {
                loaded++;
                this.onload = this.onreadystatechange = null; this.parentNode.removeChild(this);

                if (loaded == scripts.length && typeof (callback) == "function") callback();
            }
        };
        s[i].setAttribute("src", scripts[i]);
        HEAD.appendChild(s[i]);
    }
}