<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Crm/crm.master" CodeFile="TemplateFileList.aspx.cs"
    Inherits="Crm_TemplateFileList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="js/popup.js" type="text/javascript"></script>

    <script src="js/popup2.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function()
        {          
            jQuery.Delete = function(path, filetype)
            {
                if (filetype == "folder")
                {
                    if (confirm("确认删除该文件夹以及该文件夹下所有文件吗?"))
                    {
                        $("#<%=hidfilepath.ClientID %>").val(path);
                        $("#<%=btnDelete.ClientID %>").click();
                    }
                }
                else
                {
                    if (confirm("确认删除该文件吗?"))
                    {
                        $("#<%=hidfilepath.ClientID %>").val(path);
                        $("#<%=btnDelete.ClientID %>").click();
                    }
                }

            }
            var localpath="~/template<%=Request.QueryString["path"] %>";
            var filetype="";
            var oldname="";
            
            jQuery.PreReplaceFile=function(sourcename,type)//替换文件
            {
                sourcename="/template/"+sourcename;
                filetype=type;      
                //显示重命名框             
               var title="替换文件"+sourcename;
                ShowIframe(title, "ReplaceFile.aspx?fn="+sourcename+"&filetype="+type, 500, 100);
            }
            
            jQuery.PreRename=function(sourcename,type)
            {
                oldname=sourcename;                
                filetype=type;      
                //显示重命名框
                var htmls="<div style=\"margin:10px 0 10px 10px;\"><input id=\"txtnewname\" value=\""+sourcename+"\" type=\"text\" /> <input type=\"button\" onclick=\"$.Rename();\" value=\"确 定\" /> <input type=\"button\" onclick=\"pop.close();\" value=\"取 消\" /></div>";
                ShowHtmlString("重命名:"+sourcename,htmls,280,60);
                
            }
            
            
            
            jQuery.Rename = function()
            {
               
                var newname1=$.trim($("#txtnewname").val());
                if(newname1=="")
                {
                    alert("请填写新的名称!");
                    $("#txtnewname").focus();
                    return;
                }      
                             
                    $.get("ashx/crmop.ashx?<%=DateTime.Now %>",{ac:"filerename",type:filetype,localpath:localpath,oldname:oldname,newname:newname1},                    function(res)
                    {
                        if(res=="ok")
                        {
                            location.href=location.href;
                        }                      
                    });
           }

            jQuery.getparas = function(item)
            {
                var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i"));
                return svalue ? svalue[1] : svalue;
            }          
            $(".tipsimg").hover(function()
            {           
                var top1=$(this).offset().top;
                var left1=$(this).offset().left+$(this).width();
                                
                $("#viewimg").html("<img src='"+$(this).attr('href')+"' style=\"width:200px;\" />");               
                $("#viewimg").css({ "position": "absolute", "display": "block" }).offset({ top: top1, left: left1 });
               
            },function()
            {
                 $("#viewimg").hide();
            });
          

        });
      

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cbody" runat="Server">
    <div id="viewimg" style="position: absolute; display: none;">
    </div>
    <div class="nav">
        当前位置：模板文件列表 > ~/template<%=Request.QueryString["path"] %></div>
    <div style="margin-bottom: 5px;">
        <span style="float: right;">输入文件名称:(如:article.htm)<asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>
            <asp:Button ID="btnAddFile" runat="server" Text="新建文件" OnClick="btnAddFile_Click" /><span
                id="spalert" runat="server" style="color: Red;"></span></span>
        <asp:ImageButton type="image" name="ImageButton1" runat="server" ID="upbtn" title="向上"
            ImageUrl="images/folder_back.gif" Style="border-width: 0px;" OnClick="upbtn_Click" />
        <input type="image" name="ImageButton2" id="ImageButton2" title="刷新" src="images/refresh.gif"
            onclick="location.href=location.href;" style="border-width: 0px;" />
        文件路径:~/template<%=Request.QueryString["path"] %>
    </div>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="false"
        AllowSorting="false">
        <Columns>
            <asp:ImageField HeaderText="类型" DataImageUrlField="filetype" ItemStyle-Width="40px"
                SortExpression="filetype" DataImageUrlFormatString="images/filetype/{0}.gif">
            </asp:ImageField>
            <asp:TemplateField HeaderText="名称" SortExpression="filename">
                <ItemTemplate>
                    <%#GetFileNameStr(Eval("filetype").ToString(),Eval("filename").ToString())%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="updatetime" HeaderText="修改时间" />
            <asp:BoundField DataField="filesize" ControlStyle-ForeColor="#6D6D6D" ItemStyle-ForeColor="#6D6D6D"
                HeaderText="大小">
                <ControlStyle ForeColor="#6D6D6D"></ControlStyle>
                <ItemStyle ForeColor="#6D6D6D"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <%--  <%# Eval("filetype").ToString() == "folder" ? "<a href=\"?path=" + Request.QueryString["path"] + "/" + Eval("filename") + "&fromid=" + Request.QueryString["fromid"] + "&filetype=" + Request.QueryString["filetype"] + "\">打开</a> |" : ""
                        %>
                        &nbsp;&nbsp; <a href="javascript:" onclick="$.PreRename('<%#Eval("filename") %>','<%#Eval("filetype") %>')">重命名</a> | &nbsp;&nbsp; <a href="editfile.aspx?path=<%#Request.QueryString["path"]+"/"+Eval("filename") %>">编辑</a>&nbsp;&nbsp; |&nbsp;&nbsp; <a href="javascript:" onclick="$.Delete('<%#Eval("filename") %>','<%#Eval("filetype") %>')">删除</a>--%>
                    <%#GetOpStr(Eval("filetype").ToString(),Eval("filename").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Literal ID="literFolder" runat="server" Visible="false">
        <a href="?path={0}&fromid={1}&filetype={2}">打开</a> | &nbsp;&nbsp; <a href="javascript:" onclick="$.PreRename('{3}','{4}')">重命名</a>&nbsp;&nbsp; |&nbsp;&nbsp;  <a href="javascript:" onclick="$.Delete('{3}','{4}')">删除</a>
    </asp:Literal>
    <asp:Literal ID="literImg" runat="server" Visible="false">
        <a href="javascript:" onclick="$.PreRename('{0}','{1}')">重命名</a>&nbsp;&nbsp;  | &nbsp;&nbsp; <a href="javascript:" onclick="$.Delete('{0}','{1}')">删除</a>&nbsp;&nbsp; |&nbsp;&nbsp;  <a href="javascript:" onclick="$.PreReplaceFile('{2}','{1}')">替换图片</a>
    </asp:Literal>
    <asp:Literal ID="literOther" runat="server" Visible="false">
        <a href="javascript:" onclick="$.PreRename('{0}','{1}')">重命名</a> | &nbsp;&nbsp; <a href="/plugins/edit_area/editfile.aspx?path=~/template{2}">编辑</a>&nbsp;&nbsp; |&nbsp;&nbsp;  <a href="javascript:" onclick="$.Delete('{0}','{1}')">删除</a>&nbsp;&nbsp; |&nbsp;&nbsp; <a href="javascript:" onclick="$.PreReplaceFile('{2}','{1}')">替换文件</a>
    </asp:Literal>
    <asp:Button ID="btnDelete" runat="server" Text="" Style="display: none;" OnClick="btnDelete_Click" />
    <asp:HiddenField ID="hidfilepath" runat="server" />
</asp:Content>
