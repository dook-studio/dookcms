<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileList.aspx.cs" Inherits="FW_FileList" %>
<script type="text/C#" runat="server">
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
            if (Request.ServerVariables["HTTP_Referer"] == null || Request.ServerVariables["REMOTE_HOST"] == null || Request.ServerVariables["SERVER_NAME"]!=Request.Url.Host)
            {
                Response.Write("非法访问!");
                Response.End();
                return;
            }
        }
    }
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="crm.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html { overflow: auto; }
        #splitter { background-color: white; height: 500px; border: 1px solid #ccc; width: 100%; }
        #filetree { overflow: auto; padding: 0px 10px 10px 10px; height: 500px; min-width: 100px; }
        #filetree > ul { padding: 4px; }
        #fileinfo {min-width: 100px; height:500px; overflow: auto; /* No margin or border allowed */ }
        .switch-bar { background: #E5E5E5 url(images/toggle.png) no-repeat scroll 0px center; width: 10px; cursor: pointer; }
      
    </style>

    <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="lhgdialog/lhgdialog.min.js?t=self" type="text/javascript"></script>

    <script src="js/vtip-min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function()
        {
            var selobj = null;
            var selobjcolor = null;
            $(".trnei,.lupai").each(function()
            {
                $(this).click(function()
                {
                    if (selobj != null)
                    {
                        selobj.css("background-color", selobjcolor);
                    }
                    selobjcolor = $(this).css("background-color");
                    $(this).css("background-color", "#D1F880");
                    selobj = $(this);
                });
                $(this).dblclick(function()
                {
                    var filetype = $(this).find(".filetype").val();
                    if (filetype != "folder")
                    {
                        var url = $(this).find(".url").val();
                        $.SelectFile(url);
                    }
                });
            });

            jQuery.CheckALL = function()
            {
                $(":checkbox").not("#chkall").attr("checked", $("#chkall").attr("checked"));
            }
            //上传文件
            $("#btnUpload").dialog({ cover: true, bgcolor: '#000', opacity: 0.3, title: '上传文件', id: 'tupload', page: 'upload.aspx?path=' + encodeURIComponent($("#<%=hidfilepath.ClientID %>").val()) + '&' + Math.random(), autoSize: false, width: 500, height: 200 });


            //重命名文件        
            var filetype = "";
            var oldname = "";
            var dg;
            jQuery.PreRename = function(sourcename, type)
            {
                oldname = sourcename;
                filetype = type;
                //显示重命名框
                var htmls = "<div style=\"margin:10px 0 10px 10px;\"><input id=\"txtnewname\" style=\"width:250px;\" value=\"" + sourcename + "\" type=\"text\" /> <input type=\"button\" onclick=\"$.Rename();\" value=\"确 定\" /></div>";
                dg = new $.dialog({ cover: true, bgcolor: '#000', opacity: 0.3, id: "winrenamde", maxBtn: false, title: "重命名文件" + sourcename, html: htmls, width: 400, height: 150 });
                dg.ShowDialog();
            }
            jQuery.Rename = function()
            {
                var newname1 = $.trim($("#txtnewname").val());
                if (newname1 == "")
                {
                    alert("请填写新的名称!");
                    $("#txtnewname").focus();
                    return;
                }
                $.post("FileHandle.ashx?ac=filerename&" + Math.random(), { type: filetype, localpath: encodeURIComponent($("#<%=hidfilepath.ClientID %>").val()), oldname: oldname, newname: newname1 }, function(res)
                {
                    if (res == "ok")
                    {
                        $("#btnRefresh").click();
                        dg.cancel();
                    }
                });
            }

            jQuery.getparas = function(item)
            {
                var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i"));
                return svalue ? svalue[1] : svalue;
            }
            jQuery.SelectFile = function(path)
            {
                var fromid = $.getparas("fromid");
                if (frameElement != null)
                {
                    try
                    {
                        var DG = frameElement.lhgDG;
                        $('#' + fromid + '', DG.curDoc).val(path);
                        DG.cancel();
                    }
                    catch (ex) { return false; }
                }
            }

        });
        $(document).ready(function()
        {
            reload();
        });
        function EndRequestHandler()
        {
            var selobj = null;
            var selobjcolor = null;
            $(".trnei,.lupai").each(function()
            {
                $(this).click(function()
                {
                    if (selobj != null)
                    {
                        selobj.css("background-color", selobjcolor);
                    }
                    selobjcolor = $(this).css("background-color");
                    $(this).css("background-color", "#D1F880");
                    selobj = $(this);
                });
                $(this).dblclick(function()
                {
                    var filetype = $(this).find(".filetype").val();
                    if (filetype != "folder")
                    {
                        var url = $(this).find(".url").val();
                        $.SelectFile(url);
                    }
                });

            });
            //上传文件
            $("#btnUpload").dialog({ cover: true, bgcolor: '#000', opacity: 0.3, title: '上传文件', id: 'tupload', page: 'upload.aspx?path=' + encodeURIComponent($("#<%=hidfilepath.ClientID %>").val()) + '&' + Math.random(), autoSize: false, width: 500, height: 200 });

            this.vtip = function() { this.xOffset = -10; this.yOffset = 10; $(".vtip").unbind().hover(function(a) { this.t = this.title; this.title = ""; this.top = (a.pageY + yOffset); this.left = (a.pageX + xOffset); $("body").append('<p id="vtip"><img id="vtipArrow" />' + this.t + "</p>"); $("p#vtip #vtipArrow").attr("src", "images/vtip_arrow.png"); $("p#vtip").css("top", this.top + "px").css("left", this.left + "px").fadeIn("slow") }, function() { this.title = this.t; $("p#vtip").fadeOut("slow").remove() }).mousemove(function(a) { this.top = (a.pageY + yOffset); this.left = (a.pageX + xOffset); $("p#vtip").css("top", this.top + "px").css("left", this.left + "px") }) }; jQuery(document).ready(function(a) { vtip() });
        }
        function reload()
        {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }

    </script>

</head>
<body>
    <div id="viewimg" style="position: absolute; display: none;">
    </div>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="padding: 0 5px;">
                <div style="background-color: #E5E5E5; color: #CC3300; font-weight: bold; padding: 4px;
                    border: solid 1px #D1D1D1; border-bottom: 0;">
                    当前位置：文件列表 >&gt;
                    <%=hidfilepath.Value.Replace("~","") %>/
                </div>
                <div>
                    <div style="float: right; width: 690px;">
                        <asp:Button ID="btnCopy" runat="server" Text="复制" OnClick="btnCopy_Click" />
                        <asp:Button ID="btnCut" runat="server"  Text="剪切" OnClick="btnCut_Click" />
                        <asp:Button ID="btnPaste" runat="server" Text="粘贴" Enabled="false" OnClick="btnPaste_Click" />
                        <asp:Button ID="btnBatDel" runat="server" OnClick="btnBatDel_Click" Text="批量删除" />
                        <input id="btnUpload" type="button" value="上传文件" />
                        输入文件名称:(如:article.htm)<asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>
                        <asp:Button ID="btnAddFile" runat="server" Text="新建文件夹" OnClick="btnAddFile_Click" /><span
                            id="Span1" runat="server" style="color: Red;"></span></div>
                    <asp:ImageButton type="image" name="ImageButton1" runat="server" ID="upbtn" title="向上"
                        ImageUrl="images/folder_back.gif" Style="border-width: 0px;" OnClick="upbtn_Click" />
                    <asp:ImageButton ID="btnRefreshit" ImageUrl="images/refresh.gif" runat="server" OnClick="btnRefreshit_Click" />
                    <span id="spalert" runat="server" style="color: Red;"></span>
                    <div class="nofloat">
                    </div>
                </div>
                <table id="splitter">
                    <tbody>
                        <tr valign="top">
                            <td id="tdleft" style="width: 200px; overflow: auto;">
                                <div id="filetree">
                                    <asp:TreeView ID="treeMenu" runat="server" ExpandDepth="0" BorderStyle="None" AutoGenerateDataBindings="False"
                                        align="left" Font-Names="宋体" Font-Size="12px" NoExpandImageUrl="images/item.gif"
                                        ExpandImageUrl="images/expand.png" CollapseImageUrl="images/collapse.png" OnSelectedNodeChanged="treeMenu_SelectedNodeChanged">
                                        <NodeStyle HorizontalPadding="4px" NodeSpacing="0px" VerticalPadding="2px" />
                                        <RootNodeStyle />
                                        <ParentNodeStyle Font-Bold="False" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" />
                                    </asp:TreeView>
                                </div>
                            </td>
                            <td class="switch-bar" onclick="$('#tdleft').toggle();">
                            </td>
                            <td>
                                <div id="fileinfo">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <img src="images/loading.gif" style="text-align: center;" alt="正在加载中..." align="middle" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <div style="margin: 6px;">
                                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                            AllowSorting="true" OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting"
                                            OnSorting="gvList_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="类型" ItemStyle-Width="55px" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <input id="chkall" type="checkbox" onclick="$.CheckALL()" value="全选" /><asp:ImageButton
                                                            ID="btnImageUp" ImageUrl="images/folder.up.gif" OnClick="upbtn_Click" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <input id="chkFile" type="checkbox" value='<%#Eval("url") %>' runat="server" />
                                                        <img src="images/filetype/thumb/<%#Eval("filetype") %>.gif" alt="" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="名称" SortExpression="filename">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" CommandName="open" CommandArgument='<%#Eval("url") %>'
                                                            Visible='<%#(Eval("filetype").ToString()!="folder"?false:true )%>' runat="server"><%#Eval("filename") %></asp:LinkButton>
                                                        <%#GetFileNameStr(Eval("filetype").ToString(),Eval("filename").ToString())%>
                                                        <input class="filetype" type="hidden" value='<%#Eval("filetype").ToString()%>' />
                                                        <input class="url" type="hidden" value='<%#Eval("url").ToString().Replace("~","") %>'/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="updatetime" HeaderText="修改时间" SortExpression="updatetime" />
                                                <asp:BoundField DataField="filesize" ControlStyle-ForeColor="#6D6D6D" ItemStyle-ForeColor="#6D6D6D"
                                                    HeaderText="大小" SortExpression="filesize" DataFormatString="{0} K">
                                                    <ControlStyle ForeColor="#6D6D6D"></ControlStyle>
                                                    <ItemStyle ForeColor="#6D6D6D"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="操作" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnOpen" runat="server" CommandName="open" Visible='<%#(Eval("filetype").ToString()!="folder"?false:true )%>'
                                                            CommandArgument='<%#Eval("url") %>'>打开</asp:LinkButton>
                                                        <%# Eval("filetype").ToString() == "folder" ? "" : "<a href=\"javascript:\" onclick=\"$.SelectFile('"+Eval("url").ToString().Replace("~","")+"');\">选择</a>"
                                                        %>&nbsp;&nbsp; <a href="/plugins/edit_area/editfile.aspx?path=<%#Eval("url") %>">
                                                            编辑</a> |
                                                        &nbsp;&nbsp; <a href="javascript:" onclick="$.PreRename('<%#Eval("filename") %>','<%#Eval("filetype") %>')">
                                                            重命名</a> |
                                                        <asp:LinkButton ID="btnDelete" OnClientClick="return confirm('确认要删除吗?'); " CommandName="delete"
                                                            CommandArgument='<%#Eval("url") %>' runat="server">删除</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <table class="gv1" cellspacing="0" cellpadding="0" rules="all" border="0" id="gvList"
                                                    style="border-width: 0px; width: 100%; border-collapse: collapse;">
                                                    <tr class="lup">
                                                        <th align="left" scope="col" style="width:100px;">
                                                            <asp:ImageButton type="image" name="ImageButton1" runat="server" ID="upbtn" title="向上"
                        ImageUrl="images/folder_back.gif" Style="border-width: 0px;" OnClick="upbtn_Click" />
                                                        </th>
                                                        <th scope="col">
                                                            <a href="javascript:__doPostBack('gvList','Sort$filename')">名称</a>
                                                        </th>
                                                        <th scope="col">
                                                            <a href="javascript:__doPostBack('gvList','Sort$updatetime')">修改时间</a>
                                                        </th>
                                                        <th scope="col">
                                                            <a href="javascript:__doPostBack('gvList','Sort$filesize')">大小</a>
                                                        </th>
                                                        <th scope="col">
                                                            操作
                                                        </th>
                                                    </tr>
                                                    <tr class="trnei">
                                                        <td colspan="5">
                                                            <div style="padding: 5px;">
                                                                <center>
                                                                    没有任何文件!
                                                                    </center>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="nofloat">
                </div>
            </div>
            <asp:Literal ID="litercopyright" runat="server"></asp:Literal>
            </div>
            <asp:Button ID="btnRename" runat="server" Text="" Style="display: none;" />
            <asp:Button Style="display: none;" ID="btnRefresh" runat="server" Text="刷新列表" OnClick="btnRefresh_Click" />
            <input type="hidden" id="hidfilepath" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
