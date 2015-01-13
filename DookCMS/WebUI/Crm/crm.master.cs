using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class Crm_crm : MasterPage
{
    protected override void OnInit(EventArgs e)
    {

        //如果session超时或者不存在
        if (Session["MyCrmUserName"] == null)
        {
            Response.Write("<script>window.top.location.replace( '/crm/login.aspx');</script>");
            Response.End();
            return;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["crmskin"] != null && Request.Cookies["crmskin"].Value!="")
        {
            mycss.Href = "~/crm/css/" + Request.Cookies["crmskin"].Value + ".css";
        }
    }
}
