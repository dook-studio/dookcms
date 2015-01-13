using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;


public partial class Crm_UpLoadImg : BaseCrm
{
    //List<Dukey.Model.Channel> listboard = BLL..instance.GetList("");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //dropSort.DataSource = listboard;
            //dropSort.DataBind();
        }
    }
}
