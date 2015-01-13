<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditFile.aspx.cs" Inherits="EditFile"
    ValidateRequest="false" %>

<script type="text/C#" runat="server">
    //protected override void OnInit(EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
    //        if (Request.ServerVariables["HTTP_Referer"] == null || Request.ServerVariables["REMOTE_HOST"] == null || Request.ServerVariables["SERVER_NAME"] != Request.Url.Host)
    //        {
    //            Response.Write("非法访问!");
    //            Response.End();
    //            return;
    //        }
    //    }
    //}
</script>
<!doctype html>
<html>
<head>
    <title></title>
    <link href="/crm/crm.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .edit { border-style: inset; padding: 5px; font-size: 14px; }
    </style>
    <link rel="stylesheet" href="/crm/plugins/codemirror/lib/codemirror.css">
    <link rel="stylesheet" href="/crm/plugins/codemirror/addon/fold/foldgutter.css" />
    <script src="/crm/plugins/codemirror/lib/codemirror.js"></script>
    <script src="/crm/plugins/codemirror/addon/fold/foldcode.js"></script>
    <script src="/crm/plugins/codemirror/addon/fold/foldgutter.js"></script>
    <script src="/crm/plugins/codemirror/addon/fold/brace-fold.js"></script>
    <script src="/crm/plugins/codemirror/addon/fold/xml-fold.js"></script>
    <script src="/crm/plugins/codemirror/addon/fold/markdown-fold.js"></script>
    <script src="/crm/plugins/codemirror/addon/fold/comment-fold.js"></script>
    <script src="/crm/plugins/codemirror/mode/javascript/javascript.js"></script>
    <script src="/crm/plugins/codemirror/mode/xml/xml.js"></script>
    <script src="/crm/plugins/codemirror/mode/markdown/markdown.js"></script>
    <script src="/crm/plugins/codemirror/addon/edit/matchtags.js"></script>
    <script src="/crm/plugins/codemirror/addon/selection/active-line.js"></script>
    <style type="text/css">
        .CodeMirror { border-top: 1px solid black; border-bottom: 1px solid black; font-family: 微软雅黑; font-size: 14px; height: 500px; }
        #content * { font-family: 微软雅黑; font-size: 14px; line-height: 1.3; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 5px;">
        <div class="nav">
            当前位置：编辑文件 -
            <%=path%></div>
        <div style="margin: 5px 0; font-size: 14px;">
            <div id="content">
                <textarea id="txtContents" style="height: 460px; width: 100%;" cols="6" rows="20"
                    runat="server"></textarea>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="保 存" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Text="返 回" OnClick="btnBack_Click" />
            <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </div>
    </form>
    <script>
        window.onload = function ()
        {
            var te_html = document.getElementById("txtContents");
            window.editor_html = CodeMirror.fromTextArea(te_html, {
                mode: "text/html",
                lineNumbers: true,
                lineWrapping: true,
                extraKeys: { "Ctrl-Q": function (cm) { cm.foldCode(cm.getCursor()); } },
                foldGutter: true,
                matchTags: { bothTags: true },
                styleActiveLine: true,
                gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
            });
        };
    </script>
</body>
</html>
