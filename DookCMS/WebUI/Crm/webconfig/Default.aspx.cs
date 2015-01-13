using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Crm_webconfig_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rptlist.DataSource = BLL.DataBaseHelper.instance.GetList("webconfig","id,keytext,keyvalue,px,remark,issys,type",0,"","px");
            rptlist.DataBind();
        }
    }
}