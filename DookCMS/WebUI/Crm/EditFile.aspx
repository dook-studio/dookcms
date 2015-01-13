<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditFile.aspx.cs" Inherits="Crm_EditFile" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="crm.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .edit { border-style: inset; padding: 5px; font-size: 14px; }
    </style>

    <script src="edit_area/edit_area_full.js" type="text/javascript"></script>

    <script language="Javascript" type="text/javascript">
        // initialisation
        editAreaLoader.init({
        id: "txtContents"	// id of the textarea to transform		
			, start_highlight: true	// if start with highlight
			, allow_resize: "both"
			, allow_toggle: true
			, word_wrap: false
			, language: "zh"
			, syntax: "<%=fileExt %>"
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0 5px;">
        <div class="nav">
            当前位置：编辑文件 -
            <%=path%></div>
        <div style="margin: 5px 0;">
        <textarea id="txtContents" style="height: 460px; width:100%" cols="6" rows="20" runat="server"></textarea>
          <%--  <asp:TextBox ID="txtContents" Width="800px" CssClass="edit" TextMode="MultiLine" runat="server" Height="457px"></asp:TextBox>
            <br />
            <br />--%>
            <asp:Button ID="btnSubmit" runat="server" Text="保 存" OnClick="btnSubmit_Click" />
            <a href="javascript:" onclick="history.go(-1);">返 回</a>            
            <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
