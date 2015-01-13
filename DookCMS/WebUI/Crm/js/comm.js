// <!CDATA[
function gel(a){return document.getElementById?document.getElementById(a):null;}
//function $(a){return document.getElementById?document.getElementById(a):null;}

String.prototype.len = function() { return this.replace(/[^\x00-\xff]/g, "rr").length; }

function getparas(item){
    var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)","i"));
    return svalue ? svalue[1] : svalue;
}
//获取真正字节数目
String.prototype.getRealLength = function() { return this.replace(/[^\x00-\xff]/g, "aa").length; }
String.prototype.sub = function(n) {     
    var r = /[^\x00-\xff]/g;     
    if(this.replace(r, "mm").length <= n) return this;  
    var m = Math.floor(n/2);     
    for(var i=m; i<this.length; i++) {     
    if(this.substr(0, i).replace(r, "mm").length>=n) {     
    return this.substr(0, i) +"..."; }     
    } return this;    
};   

String.prototype.trim = function()
{
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

String.prototype.sub2 = function(n)
{
    var r = /[^\x00-\xff]/g;
    if (this.replace(r, "mm").length <= n) return this;
    var m = Math.floor(n / 2);
    for (var i = m; i < this.length; i++)
    {
        if (this.substr(0, i).replace(r, "mm").length >= n)
        {
            return this.substr(0, i);
        }
    } return this;
};

function maxlength(node, maxcount)
{    
    if (node.value.len() > maxcount)
    {
        node.value = node.value.sub2(maxcount);
    }
}

function HTMLEncode(html)
{
    var temp = document.createElement("div");
    (temp.textContent != null) ? (temp.textContent = html) : (temp.innerText = html);
    var output = temp.innerHTML;
    temp = null;
    return output;
}
function HTMLDecode(text)
{
    var temp = document.createElement("div");
    temp.innerHTML = text;
    var output = temp.innerText || temp.textContent;
    temp = null;
    return output;
}

// ]]>