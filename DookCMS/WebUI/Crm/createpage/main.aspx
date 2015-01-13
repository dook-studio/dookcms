<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="CreatePageMain" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>生成页面</title>
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0rc/css/dpl/??base-min.css,forms-min.css,tables-min.css"
        rel="stylesheet" />
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/??button/assets/dpl-min.css" rel="stylesheet" />
    <link href="http://a.tbcdn.cn/s/kissy/1.3.0/??calendar/assets/dpl-min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://a.tbcdn.cn/s/kissy/1.3.0/seed-min.js"></script>
    <script type="text/javascript">
        KISSY.use('calendar', function (S, Calendar)
        {
            new Calendar('#<%=txtStartTime.ClientID%>', {
                popup: true,
                triggerType: ['click'],
                closable: true,
                showTime: true
            }).on('timeSelect', function (e)
            {
                S.one('#<%=txtStartTime.ClientID%>').val(S.Date.format(e.date, 'yyyy/mm/dd HH:MM:ss'));
            });

            new Calendar('#<%=txtEndTime.ClientID%>', {
                popup: true,
                triggerType: ['click'],
                closable: true,
                showTime: true
            }).on('timeSelect', function (e)
            {
                S.one('#<%=txtEndTime.ClientID%>').val(S.Date.format(e.date, 'yyyy/mm/dd HH:MM:ss'));
            });
        });
    </script>
    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            var dd;
            jQuery.LoadProcess = function ()
            {
                $.get("/ashx/crmop.ashx?ac=createhtmlprocess", {}, function (res)
                {
                    $("#tips").html(res);
                    if (res == "生成完成!")
                    {
                        clearInterval(dd);
                    }
                });
            }

            $("#btnCreateChannel").click(function ()
            {
                var me = $(this);
                $(me).attr("disabled", true);
                $(me).attr("class", "ks-button-disabled");
                $(me).html("正在生成栏目...");
                dd = setInterval(function () { $.LoadProcess(); }, 1000);
                $.post("/ashx/crmop.ashx?ac=createhtmlchannel", {}, function (res)
                {
                    $(me).attr("disabled", false);
                    $(me).attr("class", "ks-button");
                    $(me).html("生成栏目");
                });
            })
            $("#btnCreateIndex").click(function ()
            {
                var me = $(this);
                $(me).attr("disabled", true);
                $(me).attr("class", "ks-button-disabled");
                $(me).html("正在生成首页...");
                dd = setInterval(function () { $.LoadProcess(); }, 1000);
                $.post("/ashx/crmop.ashx?ac=createhtmlindex", {}, function (res)
                {
                    $(me).attr("disabled", false);
                    $(me).attr("class", "ks-button-primary");
                    $(me).html("生成首页");
                });
            });

            $("#btncreatehtmlall").click(function ()
            {
                var channelid=$("#dropchannel").val();
                var me = $(this);
                $(me).attr("disabled", true);
                $(me).attr("class", "ks-button-disabled");
                $(me).html("正在生成文章...");
                dd = setInterval(function () { $.LoadProcess(); }, 1000);
                $.post("/ashx/crmop.ashx?ac=createhtmlarticleall", {channelid:channelid}, function (res)
                {
                    $(me).attr("disabled", false);
                    $(me).attr("class", "ks-button");
                    $(me).html("生成全部文章");
                });
            });
            $("#btncurrentarc").click(function ()//生成当天文章
            {
                var channelid=$("#dropchannel").val();
                var me = $(this);
                $(me).attr("disabled", true);
                $(me).attr("class", "ks-button-disabled");
                $(me).html("生成当天文章...");
                dd = setInterval(function () { $.LoadProcess(); }, 1000);
                $.post("/ashx/crmop.ashx?ac=createhtmlarticleall", {iscurdate:1,channelid:channelid}, function (res)
                {
                    $(me).attr("disabled", false);
                    $(me).attr("class", "ks-button");
                    $(me).html("生成当天文章");
                });
            });
        });
    </script>
</head>
<body>
    <div style="margin: 5px;">
        <form id="frm1" runat="server">       
        <section id="horizontal"> 
            <div class="row">          
                <div class="span16">
                    <div class="form-actions">                       
                             <input id="btnCreateIndex" type="button" class="ks-button-primary" value="生成首页"/>     &nbsp;&nbsp;  
                            <input id="btnCreateChannel" type="button" class="ks-button" value="生成栏目"/>
                    </div>
                      <div class="form-actions">         
                        选择栏目：<asp:DropDownList ID="dropchannel" runat="server"><asp:ListItem Value="">--全部--</asp:ListItem></asp:DropDownList>                   
                       &nbsp;&nbsp;                            
                        <input id="btncurrentarc" type="button" class="ks-button" value="生成当天文章"/>&nbsp;&nbsp;
                       <input id="btncreatehtmlall" type="button" class="ks-button" value="生成全部文章"/>
                    </div>
                      <div class="form-actions">
                      <div class="control-group">                                
                                <div class="controls docs-input-sizes">
                                选择栏目：<asp:DropDownList ID="dropchannel2" runat="server"><asp:ListItem Value="">--全部--</asp:ListItem></asp:DropDownList>                   
                       &nbsp;&nbsp;                            
                                 开始时间：<asp:TextBox ID="txtStartTime" runat="server" CssClass="inputtxt" Width="120px" Text=""></asp:TextBox>
                结束时间：<asp:TextBox ID="txtEndTime" runat="server" CssClass="inputtxt" Width="120px" Text=""></asp:TextBox>
                                  <asp:Button ID="Button2" runat="server" Text="生成文章" CssClass="ks-button"></asp:Button>
                                </div>
                            </div>                              
                    </div>
                    <div id="tips"></div>                  
                </div>
            </div>
        </section>
        </form>
    </div>
</body>
</html>
