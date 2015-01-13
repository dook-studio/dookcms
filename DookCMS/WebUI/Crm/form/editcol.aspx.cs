using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class editcol : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            selrule.DataSource = BLL.DataBaseHelper.instance.GetList("formrule","*",0,"","px");
            selrule.DataTextField = "cname";
            selrule.DataValueField = "value";
            selrule.DataBind();


            if (!string.IsNullOrEmpty(Request.QueryString["colid"]))
            {
                string colid = Request.QueryString["colid"];
                DataRowView dr= BLL.DataBaseHelper.instance.GetModelView("formpara", "htmlstr,jsonstr", "id=" + colid);
                if (dr != null)
                {
                    jsonstr.Value = dr["jsonstr"].ToString();
                    htmlstr.Value = dr["htmlstr"].ToString();
                }

            }
        }
    }
}