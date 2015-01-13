using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Crm_dbs_edit : BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCompress_Click(object sender, EventArgs e)
    {
        try
        {
            Common.DataBaseBackUpHelper.CompactAccess(Server.MapPath(ConfigurationManager.ConnectionStrings["AccessDbPath"].ConnectionString));
            lbltip.Text = "压缩数据库成功!";
        }
        catch(Exception ex)
        {
            lbltip.Text = ex.Message;
        }
    }
}