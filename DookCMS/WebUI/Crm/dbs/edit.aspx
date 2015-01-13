<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Crm_dbs_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server"> 
       <asp:label runat="server" ID="lbltip" ForeColor="Red"></asp:label>
    <div>
    <asp:Button ID="btnCompress" runat="server" Text="压缩数据库" 
            onclick="btnCompress_Click" />
    </div>
    </form>
</body>

</html>
