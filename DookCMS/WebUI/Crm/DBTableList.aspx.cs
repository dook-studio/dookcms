using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADOX;
using Dukey.DBUtility;

public partial class Crm_DBTableList : BaseCrm
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        DataTable dt = new BLL.DataBaseHelper().GetShemaTable();
        gvList.DataSource = dt;
        gvList.DataBind();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditDBTable.aspx");
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")        
        {
            string tablename = e.CommandArgument.ToString();
          //  ADOX.CatalogClass cate = new CatalogClass();
          //  ADODB.Connection conn = new ADODB.Connection();
          //  string webstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +Server.MapPath("<%$ConnectionStrings:AccessDbPath  %>") + ";Persist Security Info=True";
          //  conn.Open(webstr, null, null, 0);
          //  cate.ActiveConnection = conn;
          //var dd=  cate.Tables[tablename].Properties;
          //cate.Tables[tablename].Properties["Jet OLEDB:Table Hidden In Access"].Value = "这个是表描述";
            //for (int i = 0; i < dd.Count; i++)
            //{
            //    Response.Write(dd[i].Name+"<br/>");
               
            //}
           // conn.Close();
            new BLL.DataBaseHelper().DeleteTable(tablename);
            BindData();
        }
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    public void UpdateTables(string conn, string tablename)
    {
        ADOX.CatalogClass cate = new CatalogClass();
        cate.ActiveConnection = DbHelper.webstr;
        cate.Tables[""].Columns["fds"].Properties["fds"].Value = "fdsf";
    }
}
