using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class exit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseWeb.instance.UserId = 0;
        Response.Redirect("/login");
    }
}