using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using BLL;

public partial class Crm_TemplateList : Page
{
    public static string currentTemplate;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentTemplate = BLL.DataBaseHelper.instance.GetColumnValue("webconfig", "keyvalue", "keytext='template'").ToString();
        BindData();
    }

    private void BindData()
    {
        int total = 0;
        int pagecount = 1;
        DataSet ds = BLL.DataBaseHelper.instance.GetList("template", "[ID]", "*", 200, 1, "", "", out total,out pagecount);
        gvList.DataSource = ds;
        gvList.DataBind();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("edit.aspx");
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                string templatestr = e.CommandArgument.ToString();

                string templateid = templatestr.Split('|')[0];
                string folder = templatestr.Split('|')[1];
                if (DataBaseHelper.instance.GetColumnValue("webconfig", "keyvalue", "keytext='template'").ToString() != templateid)
                {
                    //Common.FileHelper.DeleteFolder(Server.MapPath("~/templets/" + folder));不建议删除文件夹.手动删除
                    DataBaseHelper.instance.Delete("template", "[ID]=" + templateid);
                    Common.MessageBox.ShowAndRedirect(this, "删除成功!", Request.Url.ToString());
                }
                else
                {
                    Common.MessageBox.ShowAndRedirect(this, "该模板正在使用,不能删除!", Request.Url.ToString());
                }
               
            }
            catch (Exception ex)
            {
                Common.MessageBox.Show(this, ex.Message);
            }
        }
        else if (e.CommandName == "UseIt")
        {
            string temlateid = e.CommandArgument.ToString();
            Hashtable hs = new Hashtable();
            hs.Add("keyvalue", temlateid);
            DataBaseHelper.instance.Update(hs, "webconfig", "keytext='template'");
            Common.MessageBox.ShowAndRedirect(this, "设置成功!", Request.Url.ToString());
        }
    }
}
