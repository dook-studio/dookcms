using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            rptlist.DataSource = BLL.DataBaseHelper.instance.GetList("form", "*", 0, "", "addtime");
            rptlist.DataBind();         
        }
    }
}