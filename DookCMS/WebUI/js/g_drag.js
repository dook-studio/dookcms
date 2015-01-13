var g_false   = false,     //通用为假变量
    g_true    = true,      //通用为真变量
    ig_       = null,      //通用为空变量
	_old_html = g_true,    //兼容table页面结构
	ig_t      = g_false,
	ig_u      = [],        //存放未被执行的方法
    can_over  = g_false;


//__防止多次引入JS
if (!window.FLAG_isInSplitJS) window.FLAG_isInSplitJS = g_false;


/**
 * 通过ID获取元素
 * a string 元素ID
 */
function _gel(a) {
    return document.getElementById(a)
}


/**
 * 通过TagName获取元素
 * a string 元素名称
 * b object 获取元素的范围
 */ 
function _gelstn(a, b) {
    var c = b || document;
    if (a == "*" && c.all) return c.all;
    return c.getElementsByTagName ? c.getElementsByTagName(a) : []
}


/**
 * 插件入口
 * a string 调用的方法
 */ 
function _IG_callPostLoad(a) {
	//ig_t 默认为false
	//进入js后 window.FLAG_isInSplitJS 为 false
	//push() 方法可向数组的末尾添加一个或多个元素，并返回新的长度。
    ig_t || !window.FLAG_isInSplitJS ? a() : ig_u.push([a, ig_, []])
}


var _pl,ig_B = g_false,_table = ig_,_tabs = ig_;


/**
 * 初始化
 */ 
function _init_drag_drop() {
	//_pl   是否可以拖动 在外部可以设置
	//_cbp  为true 不晓得的有什么用处
	//_uli  不晓得的有什么用处
	//ig_B  为true 是否初始化
    if (_pl) if (!ig_B) {
		//用户自定义触发事件
        _IG_TriggerCustomEvent("initdrag", _table, _tabs);
        ig_B = g_true;
		can_over = g_true;
    }
}



var ig_sa = {
    domload: 1,
    xsetpdone: 1,
    moduledragstart: 1,
    moduledragend: 1,
    moduletitleclick: 1,
    load: 1,
    unload: 1,
    resize: 1
},
ig_I = {},
ig_ta = {
    dragstart: 1,
    dragend: 1,
    titleclick: 1
};

function ig_ua(a) {
    if (a.hasOwnProperty && a.hasOwnProperty("ig_event_hashcode_")) return a.ra;
    a.ra || (a.ra = ++ig_J.qb);
    return a.ra
}

function ig_va(a) {
    return "builtin_" + a
}
function ig_wa(a, b) {
    if (a.indexOf && a.indexOf("m_") == 0) a = a.substring(2);
    return "builtin_m" + a + "_" + b
}
function ig_xa(a, b) {
    return "builtin_" + ig_J.mb(a) + "_" + b
}

/**
 * 加入自定义前缀
 * a string 方法名
 */ 
function ig_ya(a) {
    return "custom_" + a
}
/**
 * 系统退出 回收资源
 */ 
function ig_za() {
    for (var a in ig_J.c) {
        for (var b = 0; b < ig_J.c[a].length; b++) ig_J.c[a][b] = ig_;
        if (a in ig_J.Q) {
            var c = ig_J.Q[a],
            d = c[0],
            e = c[1],
            f = c[2];
            if (d.removeEventListener) d.removeEventListener(e, f, g_false);
            else d.detachEvent && d.detachEvent("on" + e, f);
            ig_J.Q[a] = ig_
        }
    }
}

/**
 * 复制数组并返回
 * a string 新数组
 * b string 源数组
 * c 数组的起始位置 通常为1
 */ 
function ig_Aa(a, b, c) {
    for (var d = c; d < b.length; d++) a[a.length] = b[d];
    return a
}
function ig_Ba(a, b) {
    if (a in ig_J.c) for (var c = 0; c < ig_J.c[a].length; c++) if (ig_J.c[a][c] === b) {
        ig_J.c[a][c] = ig_;
        return g_true
    }
    return g_false
}

/**
*__执行内部初始化方法(没完全看明白)
return function(a, b) {_IG_initDrag(a, b)}
*/
function ig_Ca(a) {
    if (a in ig_J.c) for (var b = 0; b < ig_J.c[a].length; b++) if (ig_J.c[a][b]) {
        for (var c = [], d = 1; d < arguments.length; d++) c[c.length] = arguments[d];
        ig_J.c[a][b].apply(this, c)
    }
}
//xb
function ig_Da(a, b) {
    if (! (a in ig_J.xb)) throw Error("Unsupported event type: " + a);
    var c = ig_J.ca(a);
    c in ig_J.c || (ig_J.c[c] = []);
    ig_J.c[c][ig_J.c[c].length] = b;
    return ig_J.c[c].length
}
//ig_ta
function ig_Ea(a, b, c) {
    if (! (b in ig_J.yb)) throw Error("Unsupported module event type: " + b);
    var d = ig_J.da(a, b);
    d in ig_J.c || (ig_J.c[d] = []);
    ig_J.c[d][ig_J.c[d].length] = c
}

/**
* 添加DOM事件处理器
* a DOM对象
* b 事件
* c 处理方法
*/
function ig_Fa(a, b, c) {
    var d = ig_J.Na(a, b);
    if (! (d in ig_J.c)) {
        ig_J.c[d] = [];
        var e = function(g) {
            if (!g) g = window.event;
            ig_J.Y.apply(a, [d, g])
        };
        if (a.addEventListener) a.addEventListener(b, e, g_false);
        else if (a.attachEvent) a.attachEvent("on" + b, e);
        else throw Error("Object {" + a + "} does not support DOM events.");
        ig_J.Q[d] = [a, b, e]
    }
    var f = ig_J.c[d].length;
    if (a === window && b == "unload" && f > 0) {
        ig_J.c[d][f] = ig_J.c[d][f - 1];
        ig_J.c[d][f - 1] = c
    } else if (ig_I[b]) setTimeout(c, 10);
    else ig_J.c[d][f] = c
}
function ig_Ga(a, b) {
    var c = ig_J.ba(a);
    c in ig_J.c || (ig_J.c[c] = []);
    ig_J.c[c][ig_J.c[c].length] = b
}
function ig_Ha(a, b) {
    var c = ig_J.ca(a);
    return ig_J.W(c, b)
}
function ig_Ia(a, b, c) {
    var d = ig_J.da(a, b);
    return ig_J.W(d, c)
}
function ig_Ja(a, b, c) {
    var d = ig_J.Na(a, b);
    return ig_J.W(d, c)
}
function ig_Ka(a, b) {
    var c = ig_J.ba(a);
    return ig_J.W(c, b)
}
function ig_La(a) {
    if (a == "load" || a == "domload") ig_I[a] = g_true;
    var b = ig_J.J([ig_J.ca(a)], arguments, 1);
    ig_J.Y.apply(window, b)
}
function ig_Ma() {
    var a = ig_J.J([], arguments, 0);
    setTimeout(function() {
        ig_J.triggerEvent.apply(window, a)
    },
    0)
}
function ig_Na(a, b) {
    var c = ig_J.J([ig_J.da(a, b)], arguments, 2);
    ig_J.Y.apply(window, c)
}
function ig_Oa() {
    var a = ig_J.J([], arguments, 0);
    setTimeout(function() {
        ig_J.triggerModuleEvent.apply(window, a)
    },
    0)
}
/**
 * 用户自定义事件触发器
 * a string 调用的方法
 */ 
function ig_Pa(a) {
	//ig_J.J = ig_Aa
	//ig_J.ba = ya 加入自定义前缀 custom_
    var b = ig_J.J([ig_J.ba(a)], arguments, 1);
	// b custom_initdrag,[object],
	// 调用一个对象的一个方法，以另一个对象替换当前对象。
	// 可以理解为在window下执行了ig_J.Y
	// b 是参数数组
	// ig_J.Y ig_Ca
    ig_J.Y.apply(window, b)
}
function ig_Qa() {
    var a = ig_J.J([], arguments, 0);
    setTimeout(function() {
        ig_J.triggerCustomEvent.apply(window, a)
    },
    0)
}

var ig_J = {
    qb: 0,
    c: {},
    Q: {},
    xb: ig_sa,
    yb: ig_ta,
    mb: ig_ua,
    ca: ig_va,
    da: ig_wa,
    Na: ig_xa,
    ba: ig_ya,
    hb: ig_za,
    J: ig_Aa,
    W: ig_Ba,
    Y: ig_Ca,
    addEventHandler: ig_Da,
    addModuleEventHandler: ig_Ea,
    addDOMEventHandler: ig_Fa,
    addCustomEventHandler: ig_Ga,
    removeEventHandler: ig_Ha,
    removeModuleEventHandler: ig_Ia,
    removeDOMEventHandler: ig_Ja,
    removeCustomEventHandler: ig_Ka,
    triggerEvent: ig_La,
    triggerModuleEvent: ig_Na,
    triggerCustomEvent: ig_Pa,//用户自定义事件
    triggerDelayedEvent: ig_Ma,
    triggerDelayedModuleEvent: ig_Oa,
    triggerDelayedCustomEvent: ig_Qa
},
_IG_AddEventHandler = ig_J.addEventHandler,
_IG_AddModuleEventHandler = ig_J.addModuleEventHandler,
_IG_AddGadgetEventHandler = ig_J.addModuleEventHandler,
_IG_AddDOMEventHandler = ig_J.addDOMEventHandler,//添加DOM事件处理器
_IG_AddCustomEventHandler = ig_J.addCustomEventHandler,//添加自定事件处理器
_IG_RemoveEventHandler = ig_J.removeEventHandler,
_IG_RemoveModuleEventHandler = ig_J.removeModuleEventHandler,
_IG_RemoveGadgetEventHandler = ig_J.removeModuleEventHandler,
_IG_RemoveDOMEventHandler = ig_J.removeDOMEventHandler,
_IG_RemoveCustomEventHandler = ig_J.removeCustomEventHandler,
_IG_TriggerEvent = ig_J.triggerEvent,
_IG_TriggerModuleEvent = ig_J.triggerModuleEvent,
_IG_TriggerGadgetEvent = ig_J.triggerModuleEvent,
_IG_TriggerCustomEvent = ig_J.triggerCustomEvent,//用户自定义事件触发器
_IG_TriggerDelayedEvent = ig_J.triggerDelayedEvent,
_IG_TriggerDelayedModuleEvent = ig_J.triggerDelayedModuleEvent,
_IG_TriggerDelayedGadgetEvent = ig_J.triggerDelayedModuleEvent,
_IG_TriggerDelayedCustomEvent = ig_J.triggerDelayedCustomEvent;

_IG_AddDOMEventHandler(window, "unload", ig_J.hb);

_IG_AddDOMEventHandler(window, "load",
function() {
    _IG_TriggerEvent("load")
});

_IG_AddDOMEventHandler(window, "unload",
function() {
    _IG_TriggerEvent("unload")
});

_IG_AddDOMEventHandler(window, "resize",
function() {
    _IG_TriggerEvent("resize")
});



// 可拖拽Element的原形，用来将event绑定到各个钩子，这部分市比较通用的，netvibes也是基本完全相同的实现 
// 这部分推荐看dindin的这个，也会帮助理解，http://www.jroller.com/page/dindin/?anchor=pro_javascript_12 
var dragHandler = {
	// 对这个element的引用，一次只能拖拽一个Element 
    obj: ig_,
	// b 是被拖拽的对象的引用 , a就是鼠标可以拖拽的区域 
    init: function(a, b) {
	
		// 将start绑定到onmousedown事件，按下鼠标触发start 
        a.onmousedown = dragHandler.start;
		//a.onmouseover = dragHandler.over;
        a.obj = b;
		// 初始化绝对坐标，因为不是position=absolute所以不会起什么作用，但是防止后面onDrag的时候parse出错
        if (isNaN(parseInt(b.style.left, 10))) b.style.left = "0px";
        if (isNaN(parseInt(b.style.top, 10))) b.style.top = "0px";
		 // 挂上空Function，初始化这几个成员，在dragHandler.init被调用后才帮定到实际的函数，可以参照draggable里面的内容 
        b.onDragStart = new Function;
        b.onDragEnd = new Function;
        b.onDrag = new Function
    },
	//注销
    uninit: function(a, b) {
        window.clearInterval(b.Va);
        a.onmousedown = ig_;
        a.obj = ig_;
        b.onDragStart = ig_;
        b.onDragEnd = ig_;
        b.onDrag = ig_
    },
	 start: function(a) {
        var b = dragHandler.obj = this.obj;
		// 解决不同浏览器的event模型不同的问题 
        a = dragHandler.fixE(a);
        if (a.which != 1) return g_true;
        b.onDragStart(a);
        var c = a.clientY;
        ig_6.Ra || (c += document.body.scrollTop);
        b.lastMouseX = a.clientX;
        b.lastMouseY = c;
        b.offsetMouseX = parseInt(b.style.left, 10) - a.clientX;
        b.offsetMouseY = parseInt(b.style.top, 10) - c;

        //b.Va = window.setInterval(ig_pb(b, document.body.scrollHeight > document.documentElement.clientHeight ? document.body.scrollHeight: document.documentElement.clientHeight), 10);
        // 将Global的event绑定到被拖动的element上面来 
		document.onmouseup = dragHandler.end;
        document.onmousemove = dragHandler.drag;
        return g_false
    },
    drag: function(a) {
        a = dragHandler.fixE(a);
        if (a.which == 0) return dragHandler.end();
        var b = dragHandler.obj,
        c = a.clientY;
        ig_6.Ra || (c += document.body.scrollTop);
        var d = a.clientX;
		// 如果鼠标没动就什么都不作 
        if (b.lastMouseX == d && b.lastMouseY == c) return g_false;
        var e = d + b.offsetMouseX,
        f = c + b.offsetMouseY;
		// 修改移动块坐标 
        b.style.left = e + "px";
        b.style.top = f + "px";
		
		this.eBox = _gel("drag_operate_box");
		this.eBox.style.left = e + "px";
		this.eBox.style.top = f + "px";


		// 记录element现在的坐标供下一次移动使用 
        b.lastMouseX = d;
        b.lastMouseY = c;
		// 参照这个函数的解释，挂接上Drag时的钩子 
        b.onDrag(e, f);
        return g_false
    },
	// Element正在被释放的函数，停止拖拽 
    end: function(a) {
		// 解决不同浏览器的event模型不同的问题 
        a = dragHandler.fixE(a);
		// 解除对Global的event的绑定 
        document.onmousemove = ig_;
        document.onmouseup = ig_;
        window.clearInterval(dragHandler.obj.Va);
		// 先记录下onDragEnd的钩子，好移除obj 
        var b = dragHandler.obj.onDragEnd();
		// 拖拽完毕，obj清空 
        dragHandler.obj = ig_;
        return b
    },
	// 解决不同浏览器的event模型不同的问题
    fixE: function(a) {
        if (typeof a == "undefined") a = window.event;
        if (typeof a.layerX == "undefined") a.layerX = a.offsetX;
        if (typeof a.layerY == "undefined") a.layerY = a.offsetY;
        if (typeof a.which == "undefined") a.which = a.button;
        return a
    }
};


/**
* 获取指定元素按样式筛选后的个数
* a 区域对象 
* b 元素类型
* c 样式名称
*/
function ig_qb(a, b, c) {
    if (!a) return [];
    var d = b == "*" && a.all ? a.all: _gelstn(b, a),
    e = [];
    c = c.replace(/\-/g, "\\-");
    for (var f = new RegExp("(^|\\s)" + c + "(\\s|$)"), g, h = 0; h < d.length; h++) {
        g = d[h];
        f.test(g.className) && e.push(g)
    }
    return e
}
/**
*初始化拖动区域
* a 可拖动整体区域对象 t_1 
* b null
*/
var _IG_initDrag = function(a, b) {
    ig_6.K = a;
    ig_6.X = b;
	//获取列数
    ig_6.D = ig_qb(ig_6.K, "div", "cc");
	//兼容table版本
    if (_old_html) {
        ig_6.Ua = ig_6.K.tBodies[0].rows[0];
        ig_6.D = ig_6.Ua.cells
    }
    ig_6.l = [];
    for (var c = 0, d = 0; d < ig_6.D.length; d++) {
        var e = ig_6.D[d];
        if (e.style.display != "none") 
			for (var f = 0; f < e.childNodes.length; f++) {
            var g = e.childNodes[f];
            if (g.tagName == "DIV") {
				//如果样式为dm 屏蔽
				//intDragBox 初始化单个拖动块
                ig_6.l[c] = g.className != "dm" ? new intDragBox(g) : new ig_sb(g);
                c++
            }
        }
    }
    _IG_AddEventHandler("unload", ig_tb)
};

//关闭浏览器 注销
function ig_tb() {
    if (ig_6.l) for (var a = 0; a < ig_6.l.length; a++) {
        ig_6.l[a].P();
        ig_6.l[a] = ig_
    }
    ig_6.l = ig_;
    ig_6.D = ig_;
    if (_old_html) ig_6.Ua = ig_;
    ig_6.K = ig_
}

var ig_6 = {};
ig_6.Da = navigator.userAgent;
ig_6.sa = ig_6.Da.indexOf("Gecko") != -1;
ig_6.ta = ig_6.Da.indexOf("Opera") != -1;
ig_6.Ra = ig_6.Da.indexOf("Safari") != -1;
ig_6.Ia = "IG_pageDivMaskId";
ig_6.Ha = "IG_moduleDivMaskId";
//隐藏
ig_6.ib = function() {
    ig_6.K.style.display = "none"
};
//显示
ig_6.kb = function() {
    ig_6.K.style.display = ""
};

ig_6.B = ig_;

// 获取这个虚线框，通过dom动态生成 
ig_6.posBox = function() {
    if (!ig_6.B) {
        ig_6.B = document.createElement("DIV");
        ig_6.B.className = "modbox";
        ig_6.B.backgroundColor = "";
        ig_6.B.style.border = "2px dashed #aaa";
        ig_6.B.innerHTML = "&nbsp;"
    }
    return ig_6.B
};
ig_6.p = function(a, b) {
    return function() {
        return a[b].apply(a, arguments)
    }
};
ig_6.Qa = function() {
    var a = [];
    if (ig_6.X) {
        var b;
        b = _old_html ? ig_6.X.tBodies[0].rows[0].cells: _gelstn("LI", ig_6.X);
        for (var c = 0; c < b.length; c++) {
            var d = b[c];
            (" " + d.className + " ").indexOf(" unselectedtab ") < 0 || d.style.display == "none" || a.push(d)
        }
        b = _gelstn("DIV", ig_6.X);
        for (c = 0; c < b.length; c++) {
            d = b[c];
            (" " + d.className + " ").indexOf(" leftunselectedtab ") < 0 || d.style.display == "none" || a.push(d)
        }
    }
    return a
};
ig_6.db = function() {
    for (var a = ig_6.Qa(), b = 0; b < a.length; b++) {
        var c = a[b];
        c.isDraggableTo = g_true;
        c.pagePosLeft = ig_6.k(c, g_true);
        c.pagePosRight = c.pagePosLeft + c.offsetWidth;
        c.pagePosTop = ig_6.k(c, g_false);
        c.pagePosBottom = c.pagePosTop + c.offsetHeight
    }
};

/**
* 重新计算所有的可以拖拽的element的坐标
* 对同一个column下面的可拖拽图层重新计算它们的高度而得出新的坐标，防止遮叠 
* 计算出来的坐标记录在pagePosLeft和pagePosTop两个属性里面
*/
ig_6.cb = function(a) {
    for (var b = 0; b < ig_6.l.length; b++) {
        var c = ig_6.l[b];
        c.b.pagePosLeft = ig_6.k(c.b, g_true);
        c.b.pagePosTop = ig_6.k(c.b, g_false);
    }
    for (var d = a.b.nextSibling; d;) {
        d.pagePosTop -= a.b.offsetHeight;
        d = d.nextSibling
    }
};

/**
* 获取一个element的offset信息，其实就是相对于Body的padding以内的绝对坐标 
* 后面一个参数如果是true则获取offsetLeft，false则是offsetTop
* 关于offset、style、client等坐标的定义参考dindin的这个帖子：
* http://www.jroller.com/page/dindin/?anchor=pro_javascript_12 
*/

ig_6.k = function(a, b) {
    var c = 0,
    d = 0;
	//offsetParent  基于上级节点的偏移
    if (a && a.offsetParent && a.offsetParent.id) if (!b) {
        var e = _gelstn(a.tagName, a.offsetParent);
        if (e && e[0]) d = e[0].offsetTop
    }
    for (; a;) {
        c += a["offset" + (b ? "Left": "Top")];
        a = a.offsetParent
    }
    return c - d
};

/**
* __创建遮罩层
* 暂时没有明白有什么用处 
* 也许是为了解决跨浏览器 跨窗口
*/
ig_6.CreateMask = function(a) {
    ig_6.RemovemMask();
    var b = document.createElement("DIV");
    b.id = ig_6.Ia;
    b.innerHTML = "&nbsp;";
    b.style.position = "absolute";
    b.style.width = "100%";
    b.style.height = document.body.offsetHeight + "px";
    b.style.left = "0px";
    b.style.top = "0px";
    //b.style.backgroundImage = "url(http://www.google.com/c.gif)";
    b.style.zIndex = "9999";
    document.body.appendChild(b);
    if (a.pb) {
        b = b.cloneNode(g_true);
        b.id = ig_6.Ha;
        b.style.height = a.b.offsetHeight - a.j.offsetHeight + "px";
        b.style.top = a.j.offsetHeight + "px";
        a.b.appendChild(b)
    }
};

/**
* 移出遮罩层
*/
ig_6.RemovemMask = function() {
    for (var a = [ig_6.Ha, ig_6.Ia], b = 0; b < a.length; b++) {
        var c = _gel(a[b]);
        c && c.parentNode.removeChild(c)
    }
};
ig_6.ub = function() {
    for (var a = "", b = 0; b < ig_6.D.length; b++) for (var c = ig_6.D[b], d = 0; d < c.childNodes.length - 1; d++) {
        var e = c.childNodes[d];
        if (e.tagName == "DIV") {
            a += a != "" ? ":": "";
            a += e.id.substring(2) + "_" + c.id.substring(2)
        }
    }
  //  _xsetp("mp=" + _esc(a))
};
function ig_sb(a) {
    this.P = ig_ub;
    this.b = a;
    this.b.innerHTML = "<br />"
}
function ig_ub() {
    this.b = ig_
}

/**
* 初始化单个拖动块
* a 拖动对象
*/
function intDragBox(a) {
    this._urlMouseUp = ig_vb;
    this._urlMouseDown = ig_wb;
    this._urlClick = ig_xb;
    this._dragStart = ig_yb;
    this._onMouseOver = ig_over;
    this._drag = ig_zb;
    this._dragEnd = ig_Ab;
	this._move = BufferMove;
    this.ia = ig_Bb;
    this.Z = ig_Cb;
    this.P = ig_Db;
    this.o = g_false;
    this.b = a;
    this.j    = _gel(a.id + "_h"); //可拖动区域
    this.e    = _gel(a.id + "_url");
	this.eBox = _gel("drag_operate_box");
	this.eBox.lastLeft   = 0;
	this.eBox.lastTop    = 0;
	this.eBox.lastWidth  = 0;
	this.eBox.lastHeight = 0;

    this.pb = _gelstn("IFRAME", this.b).length > 0;
    this.b.DND_Module = this;
    if (this.j) {
        this.j.style.cursor = "move";
        dragHandler.init(this.j, this.b);
		this.b.onmouseover = ig_6.p(this, "_onMouseOver");
        
		//this.b.onDragStart = ig_6.p(this, "_dragStart");
        //this.b.onDrag = ig_6.p(this, "_drag");
        //this.b.onDragEnd = ig_6.p(this, "_dragEnd");
        //if (this.e) {
        //   this.e.style.cursor = "pointer";
        //    if (ig_6.sa) {
        //        this.e.onmousedown = ig_6.p(this, "_urlMouseDown");
        //        this.e.onclick = ig_6.p(this, "_urlClick")
        //    } else this.e.onmouseup = ig_6.p(this, "_urlMouseUp")
        //}
    }
}
//P
function ig_Db() {
    if (this.j) {
        if (this.e) {
            this.e.onclick = ig_;
            this.e = this.e.onmouseup = ig_
        }
        dragHandler.uninit(this.j, this.b);
        this.b.onDragStart = ig_;
        this.b.onDrag = ig_;
        this.j = this.b.onDragEnd = ig_
    }
    this.b = ig_
}

/**
* 点击移动块链接
*/
function ig_vb(a) {
    a = dragHandler.fixE(a);
    if (this.o || !this.e || !this.e.href || a.which != 1) return g_true;
    this.Z("titleclick");
    if (this.e.target || a.shiftKey) window.open(this.e.href, this.e.target);
    else document.location = this.e.href;
    return g_false
}

//_urlMouseDown
function ig_wb(a) {
    a = dragHandler.fixE(a);
    a.stopPropagation();
    return g_true
}
//_urlClick
function ig_xb() {
    if (!this.o && this.e && this.e.href) {
        this.Z("titleclick");
        return g_true
    }
    return g_false
}

//sProperty:所需设置的属性
//nStart:属性值的起始值
//nEnd:属性值的结束值
//nInterval:总的运动时间
//nStep:步进时间
//fnCallback:缓动完成后的回调函数
var interval;
function BufferMove(obj,tStart,tEnd,lStart,lEnd,wStart,wEnd,hStart,hEnd){
	var nInterval=10;
	var nStep=1;
	var rank = 5;
	var curStep = 5;
	

	var GetArg = function(Start,End){
		var arg = ((End-Start)/Math.abs(End-Start)) * Math.pow(Math.abs(End - Start),rank) / nInterval;
		return arg;
	}
	var GetStep = function(arg,Start){
		var step =((arg/Math.abs(arg)) * Math.pow((Math.abs(arg)*curStep),1/parseFloat(rank.toString())) + Start);
		return step;
	}
	var targ =GetArg(tStart,tEnd);
	var larg =GetArg(lStart,lEnd);
	var warg =GetArg(wStart,wEnd);
	var harg =GetArg(hStart,hEnd);
	
	
	if(targ || larg){
	clearInterval(interval);

	var SetValue = function(){
		if(curStep + nStep >= nInterval){
			clearInterval(interval);
			obj.style.top=tEnd+"px";
			
			obj.style.left=lEnd+"px";
			obj.style.width=wEnd+"px";
			obj.style.height=hEnd+"px";


		obj.lastLeft   = tEnd;
		obj.lastTop    = lEnd;
		obj.lastWidth  = wEnd;
		obj.lastHeight = hEnd;

		}else{
				curStep += nStep;
				if(targ){
					obj.style.top=GetStep(targ,tStart);
				}
				if(larg){
					obj.style.left=GetStep(larg,lStart);

				}
				if(warg){
					obj.style.width=GetStep(warg,wStart);

				}
				if(harg) obj.style.height=GetStep(harg,hStart);
		}
	}
    interval = setInterval(SetValue,nStep);
	}
}


function BufferMove3(obj,tStart,tEnd,lStart,lEnd,wStart,wEnd,hStart,hEnd){
	if(wStart!=wEnd){
var obj_w=wStart;
//alert(obj_w+" "+wEnd);
var s=((wEnd-wStart)/Math.abs(wEnd-wStart));
 var changeW=function(){ 
  if(Math.abs(obj_w-wEnd)>=1){
	obj.style.width=s * (obj_w+Math.ceil((wEnd-obj_w)/15))+"px";
  }
  else{
  clearInterval(bw);
  }
 } 
 var bw=window.setInterval(changeW,1);
	 }
}


var timer;
function BufferMove3(obj,tStart,tEnd,lStart,lEnd,wStart,wEnd,hStart,hEnd){
	
	if(tEnd!=tStart ||lEnd!=lStart){
	var b=new Array();
    b[0]=tEnd;
	b[1]=lEnd;
	b[2]=wEnd;
	b[3]=hEnd;
	
	var as=new Array();
    as[0]=tStart;
	as[1]=lStart;
	as[2]=wStart;
	as[3]=hStart;

	var SetValue2 = function(){
	

	//var h=parseInt(b[3])-parseInt(as[3]);
	//var w=parseInt(b[2])-parseInt(as[2]);
	//var l=parseInt(b[1])-parseInt(as[1]);
	var t=parseInt(b[0])-parseInt(as[0]);
	
	//var hc=(h>0)?(h/3):(-h/3);
	//var wc=(w>0)?(w/3):(-w/3);
	//var lc=(l>0)?(l/3):(-l/3);
	var tc=(t>0)?(t/3):(-t/3);
	
	//var h1=(h>hc)?hc:((h<-hc)?-hc:h);
	//var w1=(w>wc)?wc:((w<-wc)?-wc:w);
	//var l1=(l>lc)?lc:((l<-lc)?-lc:l);
	var t1=(t>tc)?tc:((t<-tc)?-tc:t);
	
	//obj.style.height=b[3]-h1+"px";
	//obj.style.width=parseInt(b[2])-w1+"px";
	//obj.style.left=parseInt(b[1])-l1+"px";
	obj.style.top=parseInt(b[0])-t1+"px";
	
	//if(h1==0&&w1==0&&l1==0&&t1==0)
	if(t1==0)
	{
		window.clearInterval(timer);
	}
	} 
	timer = setInterval(SetValue2,10);
	}
}

/**
* 鼠标移上浮动拖动块
*/
function ig_over() {
	if(can_over){
		this.eBox.style.display = "block";
		this.eBox.style.cursor = "move";
		dragHandler.init(this.eBox, this.b);
		this.b.onmouseover = ig_6.p(this, "_onMouseOver");
		this.b.onDragStart = ig_6.p(this, "_dragStart");
		this.b.onDrag = ig_6.p(this, "_drag");
		this.b.onDragEnd = ig_6.p(this, "_dragEnd");

		//if (this.e) {
        //   this.e.style.cursor = "pointer";
        //    if (ig_6.sa) {
        //        this.e.onmousedown = ig_6.p(this, "_urlMouseDown");
        //        this.e.onclick = ig_6.p(this, "_urlClick")
        //    } else this.e.onmouseup = ig_6.p(this, "_urlMouseUp")
        //}
		
		
		d = ig_6.k(this.b, g_true),e = ig_6.k(this.b, g_false);


		if (ig_6.sa) {
			this._move(this.eBox,parseInt(this.eBox.offsetTop),parseInt(e),parseInt(this.eBox.offsetLeft),parseInt(d),parseInt(this.eBox.lastWidth),parseInt(this.b.offsetWidth-2),parseInt(this.eBox.lastHeight),parseInt(this.b.offsetHeight-2));
		}else{
			this._move(this.eBox,parseInt(this.eBox.offsetTop),parseInt(e),parseInt(this.eBox.offsetLeft),parseInt(d),parseInt(this.eBox.lastWidth),parseInt(this.b.offsetWidth),parseInt(this.eBox.lastHeight),parseInt(this.b.offsetHeight));
		}
		
		//this.eBox.lastLeft   = d;
		//this.eBox.lastTop    = e;
		//this.eBox.lastWidth  = this.b.offsetWidth;
		//this.eBox.lastHeight = this.b.offsetHeight;

	 }
}
//_dragStart
function ig_yb() {
	can_over=g_false;
    ig_6.cb(this);
    ig_6.db();
	//nextSibling 直接返回一个节点之后的节点
    this.origNextSibling = this.b.nextSibling;
    var a = ig_6.posBox(),
    b = this.b.offsetHeight;
    b -= parseInt(a.style.borderTopWidth, 10) * 2;



    var c = this.b.offsetWidth,
    d = ig_6.k(this.b, g_true),
    e = ig_6.k(this.b, g_false);
    this.b.style.width = c + "px";
    a.style.height = b + "px";

    this.b.parentNode.insertBefore(a, this.b.nextSibling);
    this.b.style.position = "absolute";
    this.b.style.zIndex = 10000;
    this.b.style.left = d + "px";
    this.b.style.top = e + "px";

    this.eBox.style.position = "absolute";
	this.eBox.style.left = d + "px";
    this.eBox.style.top = e + "px";
	this.eBox.style.width = this.b.offsetWidth + "px";
	this.eBox.style.height = this.b.offsetHeight + "px";
	if(ig_6.sa){
		this.eBox.style.width = this.b.offsetWidth-2 + "px";
		this.eBox.style.height = this.b.offsetHeight-2 + "px";
	}

    ig_6.CreateMask(this);
    return this.o = g_false
}
//_drag
function ig_zb(a, b) {
	can_over=g_false;

    if (!this.o) {
        this.b.style.filter = "alpha(opacity=70)";
        this.b.style.opacity = 0.5;
        this.o = g_true;
        this.Z("dragstart")
    }
    for (var c = ig_, d = 100000000, e = 0; e < ig_6.l.length; e++) {
        var f = ig_6.l[e],
        g = a;
        if (document.body.dir == "rtl" && f.b.offsetWidth == 1) g += this.b.offsetWidth;
        var h = Math.sqrt(Math.pow(g - f.b.pagePosLeft, 2) + Math.pow(b - f.b.pagePosTop, 2));
        if (! (f == this)) if (!isNaN(h)) if (h < d) {
            d = h;
            c = f
        }
    }
    this.z = ig_;
    var j = ig_6.Qa();
    for (e = 0; e < j.length; e++) {
        f = j[e];
        if (f.isDraggableTo) {
            var i = ig_Eb();
            if (this.b.lastMouseX >= f.pagePosLeft && this.b.lastMouseX <= f.pagePosRight && this.b.lastMouseY + i[1] >= f.pagePosTop && this.b.lastMouseY + i[1] <= f.pagePosBottom) {
                this.z = f;
                var k = ig_6.posBox();
                k.parentNode != ig_ && k.parentNode.removeChild(k);
                break
            }
        }
    }

    for (e = 0; e < j.length; e++) {
        f = j[e];
        if (f.id.indexOf("view") >= 0 || f.id.indexOf("contents") >= 0) if (f == this.z) {
            if (f.className.indexOf(" tab_hover") < 0) f.className += " tab_hover"
        } else f.className = f.className.replace(/ tab_hover/g, "")
    }
    k = ig_6.posBox();
    if (this.z == ig_ && c != ig_ && k.nextSibling != c.b) {
        c.b.parentNode.insertBefore(k, c.b);
        if (ig_6.ta) {
            document.body.style.display = "none";
            document.body.style.display = ""
        }
    }
}
//_dragEnd
function ig_Ab() {
    ig_6.RemovemMask();
    this.ia() && ig_6.ub();
    //if (this.z) {
    //    var a = this.z.id.match(/.*(\d+).*/)[1],
    //    b = this.b.id.match(/m_(\d+)/)[1];
    //    _xsetp("mt=" + b + ":" + a);
    //    this.b.style.display = "none";
    //    this.z.className = this.z.className.replace(/ tab_hover/g, "");
    //    var c = _gel("left_nav_m_" + b + "_title"),
    //    d = _gel("gadget_set" + a);
    //    if (c && d) {
    //        c.parentNode.removeChild(c);
    //        d.appendChild(c);
    //        _gelstn("a", c)[0].onclick = function() {
    //            _select_gadget_and_log(a, b)
    //        }
    //    }
    //}
	
	//alert("保存");
    this.o && this.Z("dragend");
	can_over=g_true;
    return g_true
}

//推动后释放鼠标
function ig_Bb() {
    var a = g_false;
	// 把拖拽时的position=absolute和相关的那些style都消除 
    this.b.style.position = "";
    this.b.style.width = "";
    this.b.style.zIndex = "";
    this.b.style.filter = "";
    this.b.style.opacity = "";
    window.scrollBy(0, 0);
	// 获取灰框 
    var b = ig_6.posBox();

    if (b.parentNode != ig_) {
		// 如果现在的邻居不是原来的邻居了 
        if (b.nextSibling != this.origNextSibling) {
			// 把被拖拽的这个节点插到灰框的前面 
            b.parentNode.insertBefore(this.b, b.nextSibling);
			
			// 标明被拖拽了新的地方 
            a = g_true
        }
		// 移除灰框，这是这个灰框的生命周期应该就结束了 
        b.parentNode.removeChild(b);

		d = ig_6.k(this.b, g_true),
		e = ig_6.k(this.b, g_false);
		//alert("d:"+d+" e:"+e);

		this.eBox.style.left   = d;
		this.eBox.style.top    = e;
		this.eBox.style.width  = this.b.offsetWidth;
		this.eBox.style.height = this.b.offsetHeight;
		if(ig_6.sa){
			this.eBox.style.width  = this.b.offsetWidth-2;
			this.eBox.style.height = this.b.offsetHeight-2;
		}
		this.eBox.lastLeft     = d;
		this.eBox.lastTop      = e;
		this.eBox.lastWidth    = this.b.offsetWidth;
		this.eBox.lastHeight   = this.b.offsetHeight;
		
    }
    if (ig_6.ta) {
		// Opera的现实问题，要隐藏/显示后才能刷新出变化 
        document.body.style.display = "none";
        document.body.style.display = ""
    }
    return a
}
//Z
function ig_Cb(a) {
    switch (a) {
    case "titleclick":
		alert("ig_Cb titleclick");
        _IG_TriggerModuleEvent(this.b.id, a, this.e.href);
        _IG_TriggerEvent("module" + a, this.b.id, this.e.href);
        break;
    case "dragstart":
    case "dragend":
        _IG_TriggerDelayedModuleEvent(this.b.id, a);
        _IG_TriggerDelayedEvent("module" + a, this.b.id);
        break
    }
}

_IG_AddCustomEventHandler("initdrag",function(a, b) {_IG_initDrag(a, b)});