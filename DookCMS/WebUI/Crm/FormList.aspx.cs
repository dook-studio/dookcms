using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class Crm_FormList : Page
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {       
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("form", "", 1000, "", "addtime");
        gvList.DataBind();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("editform.aspx");
        Hashtable hs=new Hashtable();
        hs.Add("cname","新表单");
        hs.Add("limitsize",0);
        hs.Add("upload_path","");
        BLL.DataBaseHelper.instance.Insert(hs, "Form");

        BindData();
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")        
        {
            string tablename = e.CommandArgument.ToString();
            new BLL.DataBaseHelper().DeleteTable(tablename);
            BindData();
        }
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
