using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Common;

public partial class config : BaseCrm
{
    public string title = "新增表单";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Request.QueryString["id"].IsEmpty())
            {
                title = "编辑表单";
            }
            DataTable dt= BLL.DataBaseHelper.instance.GetShemaTable();
            seltable.DataSource = dt;
            seltable.DataTextField = "name";
            seltable.DataValueField = "name";
            seltable.DataBind();
        }
    }
}