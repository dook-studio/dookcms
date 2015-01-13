//插件名称:都客无限级分类
//功能:实现无限级下拉框select级联选择.
//data,设置的json格式数据
//returnobj,文本框对象,用来接收返回的选定值
//txtobj,文本框对象,用来接收返回的选定值字符串.
//作者:钟健 2011年3月31日(http://www.mou18.com)
(function($){
    jQuery.fn.dukeyselect=function(topid,data,returnobj,txtobj)
    {
        var colObj;
//               var colObj = {"Items":[
//				{"keytext":"北京市","keyvalue":"1","pid":"0","id":"1"},
//				    {"keytext":"通州区","keyvalue":"2","pid":"1","id":"2"},
//				        {"keytext":"马驹桥","keyvalue":"3","pid":"2","id":"3"},
//				            {"keytext":"卢沟起","keyvalue":"9","pid":"3","id":"9"},
//				                {"keytext":"aaa","keyvalue":"11","pid":"9","id":"11"},
//				    {"keytext":"大兴区","keyvalue":"7","pid":"1","id":"7"},
//				   
//				{"keytext":"北京市","keyvalue":"4","pid":"2","id":"4"},
//				{"keytext":"北a京市","keyvalue":"6","pid":"0","id":"6"},
//				    {"keytext":"通a州区","keyvalue":"2","pid":"6","id":"8"},			
//				    {"keytext":"g州区","keyvalue":"2","pid":"6","id":"10"}
//			    ]};
			
colObj=$.extend(colObj,data);

var obj=$(this);
var d1=document.createElement("select");
$(d1).change(function()
{   
    $(this).nextAll().remove();
    $(returnobj).val("");
    $(txtobj).val("");
//    alert($(obj).attr("id"));
    $.changed(obj,$(this).val(),2,colObj);	       
});	
 $(d1).append("<option value=''>请选择</option>");	    
$(colObj.Items).each(function(i,n)
{
    if(n.pid==topid)
    $(d1).append("<option value='"+n.id+"'>"+n.keytext+"</option>");	       
});
$(obj).append($(d1));
//加载根目录结束	    

$.changed=function(sender,pid,n,curdata)
{	        
   
    var str="";
    $(curdata.Items).each(function(i,n)
        {	       
            if(n.pid==pid)
            str+="<option value='"+n.id+"'>"+n.keytext+"</option>";  
            
         });	 
     if(str=="")
     {	            
       $(returnobj).val(pid);
       var txtobjstr="";
       $(sender).find("select").each(function()
       {
            txtobjstr+=$(this).val()+":"+$(this).find("option:selected").text()+"|";
            
       });
       if(txtobjstr.length>0)
       txtobjstr=txtobjstr.substring(0,txtobjstr.length-1);	        
       $(txtobj).val(txtobjstr);
        return false;
      }
    var tmps;	    
    if($(sender).find("#select"+n).length>0)
    {
       $(sender).find("#select"+n).remove();
    }	           
 	    tmps=document.createElement("select");	     	
 	    tmps.setAttribute("id", "select"+n);
 	    $(tmps).append("<option value=''>请选择</option>");	
 	    $(tmps).change(function()
        {
            $(returnobj).val("");
            $(txtobj).val("");                
            $(this).nextAll().remove();                    
            $.changed(sender,$(this).val(),n+1,curdata);
        });
        $(tmps).append(str);
        $(sender).append(tmps);
    }
    }
})(jQuery);  