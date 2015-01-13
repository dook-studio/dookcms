using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Crm_PhotoList : BaseCrm
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        gvList.DataSource = new BLL.Photos().GetAllList();
        gvList.DataBind();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpLoadImg.aspx");
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                string str = e.CommandArgument.ToString();

                string photoid = str.Split('|')[0];
                string picurl = str.Split('|')[1];

                if (File.Exists(Server.MapPath(picurl)))
                {
                    File.Delete(Server.MapPath(picurl));
                }
                new BLL.Photos().Delete(int.Parse(photoid));
                BindData();
            }
            catch (Exception ex)
            {
                Common.MessageBox.Show(this, ex.Message);
            }
        }
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
