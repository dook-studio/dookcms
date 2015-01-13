<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inout.aspx.cs" MasterPageFile="~/Crm/crm.master" ValidateRequest="false"
    Inherits="template_data_inout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>配置参数</title>
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/css/dpl/??base-min.css,forms-min.css"
        rel="stylesheet" />
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/??button/assets/dpl-min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://a.tbcdn.cn/s/kissy/1.3.0/seed-min.js"></script>
    <script src="/js/jquery-1.4.4.min.js" type="text/javascript"></script>
    
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
                $.post("/ashx/postop.ashx?ac=customfrm&op=update&tbname=form",{cname:cname,remark:remark,tablename:tablename,pk:pk,showtype:showtype,ischeckcode:ischeckcode,isshowtitle:isshowtitle,isopen:isopen,gototips:gototips,gotourl:gotourl,where:where},function(res)
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div style="padding: 0 5px;">
        <div class="nav" style="padding: 5px;">
            当前位置：<a href="list.aspx" style="color: #fff;">模板列表</a> &gt; <%=Request.QueryString["cname"] %> - 模板数据导入导出</div>
    </div>
    <div class="container" style="width: auto; margin: 0 15px 0 25px;">        
            <div class="row">
                <div>
                    <div class="control-group">
                        <label class="control-label" for="input01">
                            请在以下文本框中输入数据</label>
                        <div class="controls">
                            <textarea id="txtData" runat="server" cols="200" rows="20" style="width:98%;"></textarea>
                            <span class="help-block">请填写模板数据</span>
                        </div>
                    </div>
                    <div class="form-actions">
                        <asp:Button ID="btnLoadxml" runat="server"  CssClass="ks-button" Text="加载本模板包下的data.xml" 
                            onclick="btnLoadxml_Click" />
                        <asp:Button ID="btnCreateXML" CssClass="ks-button ks-button-primary" runat="server" Text="当前数据库生成data.xml" 
                            onclick="btnCreateXML_Click" />
                       &nbsp;&nbsp;
                        <asp:Button ID="btnImportData" runat="server" 
                            CssClass="ks-button" Text="导入到当前数据库" 
                            onclick="btnImportData_Click" />               &nbsp;&nbsp;
                        <asp:Button ID="btnExport" CssClass="ks-button  ks-button-primary" runat="server" 
                            Text="导出到该模板目录的data.xml" onclick="btnExport_Click" />                       
                    </div>
                </div>
            </div>
    </div>
</asp:Content>
