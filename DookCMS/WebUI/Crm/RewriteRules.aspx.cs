using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Crm_RewriteRules :BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtContents.Value = Common.FileHelper.ReadFile(Server.MapPath("~/xml/rule.xml"), "utf-8");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Common.FileHelper.WriteFile(Server.MapPath("~/xml/rule.xml"),txtContents.Value.Trim(),"utf-8");
    }
}
