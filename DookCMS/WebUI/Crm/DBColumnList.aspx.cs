using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class Crm_DBColumnList : BaseCrm
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
    private Hashtable hs = BLL.DataBaseHelper.instance.GetPrimaryKeys();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StringHelper.IsBadChar(Request.QueryString["tablename"]))
        {
            string tablename = Request.QueryString["tablename"];
            DataTable dt = new BLL.DataBaseHelper().GetShemaColumnName(tablename);
            DataView dtv = dt.DefaultView;
            dtv.Sort = "ordinal_position";
            gvList.DataSource = dtv;
            gvList.DataBind();
        }
    }

    public bool IsPrimaryKey(string column)
    {
        string tablename = Request.QueryString["tablename"];
        if (hs.Contains(tablename.ToLower()))
        {
            if (column.ToLower() == hs[tablename.ToLower()].ToString())
                return true;          
        }
        return false;
    }

  
}
