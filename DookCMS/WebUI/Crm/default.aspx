<%@ Page Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MyCrmUserName"] == null)
        {
            Response.Write("<scr"+"ipt>window.top.location.replace( '/crm/login.aspx');</scr"+"ipt>");
            Response.End();
            return;
        }
        Response.Redirect("/crm.ashx?fn=index");
    }
</script>
