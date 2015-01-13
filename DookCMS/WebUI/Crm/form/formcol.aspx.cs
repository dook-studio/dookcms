using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class form_formcol : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                var id=Request.QueryString["id"];
                var model = BLL.DataBaseHelper.instance.GetModelView("form", "tablename", "id=" + id);
                if (model != null)
                {
                    BindData(model["tablename"].ToString());
                }
            }
        }

    }
    private void BindData(string tbname)
    {
        DataTable dt = BLL.DataBaseHelper.instance.GetShemaColumnName(tbname);
        DataView dtv = dt.DefaultView;
        dtv.Sort = "ordinal_position";
        rptlist.DataSource = dtv;
        rptlist.DataBind();
    }
}