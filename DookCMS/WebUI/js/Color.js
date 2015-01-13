var div_rcolor; //颜色选择框DIV
var rtext_color; //调用颜色选择框的文本域
var roldcolor; //旧颜色DIV
var rnewcolor; //新颜色DIV
var rset_background = false; //是否改背景色
function rcolor(text, set_back)
{   
    rtext_color = text;
    rset_background = set_back;

    if (div_rcolor == null)
    { //如果颜色选择框不存在，则创建它
        //设置颜色选择框的样式 BEGIN
        var css = "";
        if (document.compatMode == "BackCompat" && navigator.userAgent.indexOf("MSIE") != -1)
        {
            css += "#rcolor {width:191px; height:147px; border:1px #000000 solid; background:#FFFFFF; border-bottom:0px; border-right:0px; overflow:hidden;}";
            css += "#rcolor_top {width:190px; height:11px; border-right:1px #000000 solid; border-bottom:1px #000000 solid; overflow:hidden; background:#000000;}";
            css += "#rcolor_copyright {width:100px; height:10px; float:left; font-size:9px; font-family:Arial; overflow:hidden; color:#CCCCCC;}";
            css += "#rcolor_close {width:89px; height:10px; float:right; font-size:11px; font-family:宋体; overflow:hidden; text-align:right; color:#CCCCCC;}";
            css += ".rcolor_1 {width:60px; height:60px; float:left; overflow:hidden;}";
            css += ".rcolor_2 {float:left; width:10px; height:10px; border:1px #000000 solid; border-top:0px; border-left:0px; overflow:hidden;}";
            css += "#roldcolor {float:left; height:15px; width:60px; border-bottom:1px #000000 solid; font-size:11px; font-family:Arial; text-align:center; cursor:default; overflow:hidden;}";
            css += "#rold2color {float:left; height:15px; width:70px; border-bottom:1px #000000 solid; font-size:11px; font-family:Arial; text-align:center; cursor:default; overflow:hidden;}";
            css += "#rnewcolor {float:left; height:15px; width:60px; border:1px #000000 solid; border-top:0px; border-left:0px; font-size:11px; font-family:Arial; text-align:center; cursor:default; overflow:hidden;}";
        }
        else
        {
            css += "#rcolor {width:190px; height:146px; border:1px #000000 solid; background:#FFFFFF; border-bottom:0px; border-right:0px; overflow:hidden;}";
            css += "#rcolor_top {width:189px; height:10px; border-right:1px #000000 solid; border-bottom:1px #000000 solid; overflow:hidden; background:#000000;}";
            css += "#rcolor_copyright {width:100px; height:10px; float:left; font-size:9px; font-family:Arial; overflow:hidden; color:#CCCCCC;}";
            css += "#rcolor_close {width:89px; height:10px; float:right; font-size:11px; font-family:宋体; overflow:hidden; text-align:right; color:#CCCCCC;}";
            css += ".rcolor_1 {width:60px; height:60px; float:left; overflow:hidden;}";
            css += ".rcolor_2 {float:left; width:9px; height:9px; border:1px #000000 solid; border-top:0px; border-left:0px; overflow:hidden;}";
            css += "#roldcolor {float:left; height:14px; width:60px; border-bottom:1px #000000 solid; font-size:11px; font-family:Arial; text-align:center; cursor:default; overflow:hidden;}";
            css += "#rold2color {float:left; height:14px; width:69px; border-bottom:1px #000000 solid; font-size:11px; font-family:Arial; text-align:center; cursor:default; overflow:hidden;}";
            css += "#rnewcolor {float:left; height:14px; width:60px; border:1px #000000 solid; border-top:0px; border-left:0px; font-size:11px; font-family:Arial; text-align:center; cursor:default; overflow:hidden;}";
        }
        try
        { //IE下可行
            var style = document.createStyleSheet();
            style.cssText = css;
        }
        catch (e)
        { //Firefox,Opera,Safari,Chrome下可行
            var style = document.createElement("style");
            style.type = "text/css";
            style.textContent = css;
            document.getElementsByTagName("HEAD").item(0).appendChild(style);
        }
        //设置颜色选择框的样式 END

        div_rcolor = document.createElement("div");
        div_rcolor.setAttribute("id", "rcolor");
        div_rcolor.style.position = "absolute";
        div_rcolor.style.zIndex = 2008;
        div_rcolor.style.display = "none";

        var str = "";
        str += '<div id="rcolor_top">';
        str += '  <div id="rcolor_close"><span style="cursor:pointer;" onclick="div_rcolor.style.display=\'none\';">×</span></div>';
        str += '</div>';

        str += '<div style="width:10px; height:120px; float:left; overflow:hidden;">'; //最左边的那列常用色
        str += get_color_div("#000000");
        str += get_color_div("#333333");
        str += get_color_div("#666666");
        str += get_color_div("#999999");
        str += get_color_div("#CCCCCC");
        str += get_color_div("#FFFFFF");
        str += get_color_div("#FF0000");
        str += get_color_div("#00FF00");
        str += get_color_div("#0000FF");
        str += get_color_div("#FFFF00");
        str += get_color_div("#00FFFF");
        str += get_color_div("#FF00FF");
        str += '</div>';

        for (var i = 0x000000; i <= 0xFFFFFF; i += 0x330000)
        {
            str += '<div class="rcolor_1">';
            for (var j = i; j <= i + 0x00FF00; j += 0x003300)
            {
                for (var k = j; k <= j + 0x0000FF; k += 0x000033)
                {
                    var c = k.toString(16).toUpperCase();
                    while (c.length < 6)
                        c = '0' + c;
                    str += get_color_div("#" + c);
                }
            }
            str += '</div>';
        }
        str += '<div id="rold2color" onclick="rset_color_2();">Clear Color</div>';
        str += '<div id="roldcolor" onclick="div_rcolor.style.display=\'none\';"></div>';
        str += '<div id="rnewcolor" onclick="rset_color(this.innerHTML);"></div>';

        div_rcolor.innerHTML = str;
        document.body.appendChild(div_rcolor);
        roldcolor = document.getElementById("roldcolor");
        rnewcolor = document.getElementById("rnewcolor");
    }
    //颜色选择框定位
    var l = 0, t = 0;
    var obj_tmp = text;
    do
    {
        l += obj_tmp.offsetLeft;
        t += obj_tmp.offsetTop;
    } while (obj_tmp = obj_tmp.offsetParent);
    div_rcolor.style.left = l + "px";
    div_rcolor.style.top = (t + text.offsetHeight + 1) + "px";
    //给旧颜色Div(roldcolor)赋值
    if (text.value == "")
    {
        roldcolor.innerHTML = "No Color";
        roldcolor.style.backgroundColor = "";
        roldcolor.style.color = "#000000";
    }
    else
    {
        try
        {
            roldcolor.innerHTML = roldcolor.style.backgroundColor = text.value;
            roldcolor.style.color = rworkout_fontcolor(text.value);
        }
        catch (e)
        {
            roldcolor.innerHTML = "No Color";
            roldcolor.style.backgroundColor = "";
            roldcolor.style.color = "#000000";
        }
    }
    //把颜色选择框显示出来
    div_rcolor.style.display = "";
    
}
function rpreview_color(c)
{
    rnewcolor.style.color = rworkout_fontcolor(c);
    rnewcolor.innerHTML = rnewcolor.style.backgroundColor = c;
}
function rset_color(c)
{
    rtext_color.value = c;
    if (rset_background)
    {
        rtext_color.style.color = rworkout_fontcolor(c);
        rtext_color.style.backgroundColor = c;
    }
    div_rcolor.style.display = "none";
    
    if (document.getElementById("btnview"))
    {
        document.getElementById("btnview").click();
    }
//    if (callback != null)
//        callback;
}
function rset_color_2()
{
    rtext_color.value = "";
    if (rset_background)
    {
        rtext_color.style.color = rworkout_fontcolor("#FFFFFF");
        rtext_color.style.backgroundColor = "#FFFFFF";
    }
    div_rcolor.style.display = "none";
}
function rworkout_fontcolor(c)
{
    var r = "0x" + c.substr(1, 2);
    var g = "0x" + c.substr(3, 2);
    var b = "0x" + c.substr(5, 2);
    flag = 0;
    if (parseInt(r) <= 0x33)
        flag += 2;
    else if (parseInt(r) <= 0x66)
        flag += 1;
    if (parseInt(g) <= 0x33)
        flag += 2;
    else if (parseInt(g) <= 0x66)
        flag += 1;
    else if (parseInt(g) >= 0xCC)
        flag -= 2;
    if (parseInt(b) <= 0x33)
        flag += 2;
    else if (parseInt(b) <= 0x66)
        flag += 1;

    return flag >= 3 ? "#FFFFFF" : "#000000";
}
function get_color_div(c)
{
    return '<div class="rcolor_2" style="background-color:' + c + ';" onmouseover="rpreview_color(\'' + c + '\')" onclick="rset_color(\'' + c + '\')"></div>'
}