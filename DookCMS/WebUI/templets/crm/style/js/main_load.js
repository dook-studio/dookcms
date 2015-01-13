var navTitle = "";

function initTopTab(){
    $.each(menu, function (k, v) {
        var item = $('<li><a class="link" style="cursor:pointer"  id="tab_' + k + '">' + v.text + '</a></li>');
        item.children('a').click(function () { var tabName = this.id.substr(4); if (tabName == currTab) { return; } switchTab(tabName); openItem(); });
        $('#nav').append(item);
        if (k != currTab) {
            item = $('<dl><dt>' + v.text + '</dt></dl>');
            $.each(v.children, function (k1, v1) {
                if (v1.text != 'spliter') {
                    item.append($('<dd><a style="cursor:pointer;" onclick="openItem(\'' + k1 + '\',\'' + k + '\');none_fn();">' + v1.text + '</a></dd>'));
                }
                else {
                    item.append($('<dd class="spliter"><a style="cursor:pointer"></a></dd>'));
                }
            });
            $(".back_nav_list").append(item);
        }
    });

    /* 切换到默认选项卡 */
    switchTab(currTab);
    openItem(firstOpen[1], firstOpen[0]);
    $('#iframe_refresh').click(function () {
        tipLoadding();
        $('#workspace').get(0).contentWindow.location.reload();
    });

   

    var copyitem = $('<dl style=" color:#ccc; margin:5px 0px 0px 8px;">' + pntCopyright + '</dl>');
    $('#leftMenus').append(copyitem);
}
function initHistory(){
    readHistory();
    $(window).unload(saveHistory);
}
function readHistory() {

    $('#historyText').html('操作历史');
    var h = $.getCookie('actionHistory');
    if (h != '' && h != 'undefined') {
        var error = false;
        var arr = h.split(',').reverse();
        $.each(arr, function () {
            var tmp = this.split('-');
            if (typeof (menu[tmp[0]]) != 'undefined' && typeof (menu[tmp[0]]['children'][tmp[1]]) != 'undefined') {
                addHistoryItem(tmp[0], tmp[1]);
            }
        });
        if (error == false) {
            if (arr.length) {
                firstOpen = arr[arr.length - 1].split('-');
            }
        }
    }

}
function saveHistory(){
    var h = '';
    $('#history dd').each(function(){
        h += $(this).find('a:first').attr('id') + ',';
    });
    var v = h.substr(0, (h.length - 1));
    $.setCookie('actionHistory', v);
}
function addHistoryItem(tab, item){
    var id = '#' + tab + '-' + item;
    if($(id).length == 1){
        /* 若存在提到最前 */
        var cln = $(id).parent().clone(true);
        $(id).parent().remove();
        $('#history dt').after(cln);
    }
    else {
        /* 存在，则加入 */
        try {
            if (typeof (menu[tab]['children'][item]) == 'undefined') {
                return;
            }
        } catch(e) {
            return;
        }

        if($('#history dd').length == maxHistoryLength){
            $('#history dd:last').remove();
        }
        var lnk = $('<a style="cursor:pointer" id="' + tab + '-' + item + '">' + menu[tab]['children'][item]['text'] + '</a>').css({"color":"#98a9c2"});
        var close = $('<a style="cursor:pointer" class="close"><img src="/templets/crm/style/images/close.gif" / ></a>');
        lnk.click(function(){
            openItem(item, tab);
        });
        close.click(function(){
            $(this).parent().remove();
        });
        $('<dd></dd>').append(lnk).append(close).insertAfter($('#history dt'));
    }
}
function switchTab(tabName){
    currTab = tabName;
    pickTab();
    loadSubmenu();
}
function pickTab(){
    var id = '#tab_' + currTab;
    $('#nav').find('a').each(function(){
        $(this).removeClass('actived');

        //下载多余图片bug
        //$(this).addClass('link');   
    });
    $(id).addClass('actived');
}
function loadSubmenu(){
    var m = menu[currTab];
    /* 子菜单标题 */
    $('#submenuTitle').text(m.subtext ? m.subtext : m.text);
    /* 删除所有现有子菜单 */
    $('#submenu').find('dd').remove();
    /* 将子菜单逐项添加到菜单中 */
    $.each(m.children, function (k, v) {
        var p = v.parent ? v.parent : currTab;

        if (v.text != 'spliter') {
            var item = $('<dd><a style="cursor:pointer;" url="' + v.url + '" parent="' + p + '" id="item_' + k + '">' + v.text + '</a></dd>');
            item.children('a').click(function () {
                openItem(this.id.substr(5));
            });

            ////点击框加载(二次加载bug)
            //item.click(function(){
            //    openItem($(this).children('a').attr("id").substr(5));
            //});

            $('#submenu').append(item);
        }
        else {
            var item = $('<dd class="spliter"><a  style="cursor:pointer;" url="' + v.url + '" parent="' + p + '" id="item_' + k + '"></a></dd>');
            $('#submenu').append(item);
        }
    });

    curClosed = false;
    $('#submenu dd').show();
}
function openItem(itemIndex, tab, params){
    if(typeof(itemIndex) == 'undefined')
    {
        var itemIndex = menu[currTab]['default'];
    }
    

    var id = '#item_' + itemIndex;
    if(tab){
        var parent = tab;
    }else{
        var parent  = $(id).attr('parent');
    }
    /* 若不在当前选项卡内 */
    if(parent != currTab){
        /* 切换到指定选项卡 */
        switchTab(parent);
    }
    /* 高亮当前项 */
    $('#submenu').find('a').each(function(){
        $(this).removeClass('selected');
    });
    $(id).addClass('selected');

    navTitle = menu[currTab]['text'] + '&nbsp;&gt;&nbsp;' + menu[currTab]['children'][itemIndex]['text'];

	/* 更新iframe的内容 */
    $('#workspace').show();
    var rndurl = $(id).attr('url');
    if (rndurl.indexOf('?') >= 0) {
        rndurl = rndurl + "&rnd=" + Math.random();
    } else {
        rndurl = rndurl + "?rnd=" + Math.random();
    }

    tipLoadding();

    if(typeof(params) == 'undefined')
    {
        $('#workspace').attr('src', rndurl);
    }else{
        $('#workspace').attr('src', rndurl + '&' + params.toString().replace('?', ''));
	}

    /* 将该操作加入到历史访问当中 */
    addHistoryItem(currTab, itemIndex);
}

/* 加载中 agui-2013-06-29 */
function tipLoadding() {
    var tipWidth = parseInt($('#workspace').width() / 2) - parseInt(200 / 2);
    var tipHeight = parseInt($('#workspace').height() / 2) - parseInt(38 / 2);
    var contenthtml = '<div style="position:absolute; left:' + tipWidth + 'px; top:' + tipHeight + 'px; width:200px; height:38px; border:2px solid #b0d3ee; overflow-x:hidden; background-color:#ffffff;">'
        + '    <div id="keygroup_list" style=" height:38x;  font-size:16px; padding-left:20px; padding-top:10px; font-weight:bold; color:#075198;"> '
        + '       <img src="/templets/crm/style/images/tip_loadding.gif" width="20" height="12" /><span>&nbsp;&nbsp;正在载入中...</span>'
        + '    </div>'
        + '  </div>';
    $('#workspace').contents().find("body").append($(contenthtml));
}

/* 导航提示 */
function getNavTitle() {
    if (navTitle != "") {
        return navTitle;
    } else {
        return "后台导航";
    }
}

/* 设置工作区 */
function setWorkspace(e){
    var wWidth = $(window).width();
    var wHeight = $(window).height();
	/*兼容性*/
	if(navigator.appVersion.split("MSIE") && navigator.userAgent.indexOf('Opera')===-1 && parseFloat(navigator.appVersion.split("MSIE")[1])<7)wWidth = wWidth -1; if(window.innerHeight)wHeight = window.innerHeight -1;
    //$('#workspace').width(wWidth - $('#left').width() - parseInt($('#left').css('margin-right')));
    $('#workspace').height(wHeight - $('#head').height());
}

function sReplace(tStrText, AFindText, ARepText) {
    var raRegExp = new RegExp(AFindText.replace(/([\(\)\[\]\{\}\^\$\+\-\*\?\.\"\'\|\/\\])/g, "\\$1"), "ig");
    return tStrText.replace(raRegExp, ARepText);
}

function display_fn() {
    if (div == null) {
        div = document.getElementsByTagName('div');
    }
    for (i = 0; i < div.length; i++) {
        if (div[i].className == 'back_nav') {
            num = i;
        }
    }
    div[num].style.display = 'block';
}

function none_fn() {
    if (div == null) {
        div = document.getElementsByTagName('div');
    }
    for (i = 0; i < div.length; i++) {
        if (div[i].className == 'back_nav') {
            num = i;
        }
    }
    div[num].style.display = 'none';
}

function display_fm() {
    if (div == null) {
        div = document.getElementsByTagName('div');
    }
    for (i = 0; i < div.length; i++) {
        if (div[i].className == 'back_nav') {
            div[i].style.display = 'none';
        }
    }
    div[num].style.display = 'block';
}




/* 当前所在选项卡 */
var firstOpen = [];
var maxHistoryLength = 5;
/* 后台导航 */
var num = 0;
var close_num = 0;
var div = null;
var curHref = "";
var curClosed = false;

$(function () {

    /* 设置工作区 */
    setWorkspace();

    /* 初始化标签页 */
    initHistory();
    initTopTab();

    /* resize时重新设置工作区 */
    $(window).resize(setWorkspace);

  

    var img = document.getElementById('back_btn');
    img.onclick = display_fn;
    var closes = document.getElementsByTagName('div');
    for (i = 0; i < closes.length; i++) {
        if (closes[i].className == 'close_float') {
            close_num2 = i;
        }
    }
    closes[close_num2].onclick = none_fn;

    curHref = window.location.toString().toLowerCase();
    curHref = curHref.substring(0, curHref.indexOf('/default.aspx'));


    $("#submenuTitle").click(function () {
        if (!curClosed) {
            $('#submenu dd').slideUp(50);
            curClosed = true;
        } else {
            $('#submenu dd').slideDown(50); 	
            curClosed = false;
        }
    });

});
