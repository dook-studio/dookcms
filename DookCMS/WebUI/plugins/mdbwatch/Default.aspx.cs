using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADOX;
using MdbWatch;

public partial class Mdbw_DBTableList : BaseCrm
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
    protected void Page_Load(object sender, EventArgs e)
    {
        litercopyright.Text = "版权所有,试用版请勿用于商业用途!38809972@qq.com";
        BindData();
    }

    private void BindData()
    {
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("tbnote");
        gvList.DataBind();
    }


    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string tablename = e.CommandArgument.ToString();
            try
            {
                BLL.DataBaseHelper.instance.Delete("tbnote", "tbname='" + tablename + "'");
                BLL.DataBaseHelper.instance.DeleteTable(tablename);
                BindData();
            }
            catch { }
        }
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
   
    protected void dropSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (dropSort.SelectedValue)
        {
            case "incms":
                {
                    DataTable dt = BLL.DataBaseHelper.instance.GetList("tbnote").Tables[0];
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "tbname not like  'U_%' and tbname not like  'sqlite_%'";
                    gvList.DataSource = dv;
                    gvList.DataBind();
                    break;
                }
            case "U_":
                {
                    DataTable dt = BLL.DataBaseHelper.instance.GetList("tbnote").Tables[0];
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "tbname like '%" + dropSort.SelectedValue + "%'";
                    gvList.DataSource = dv;
                    gvList.DataBind();
                    break;
                }
            default:
                {
                    BindData();
                    break;
                }
        }
    }
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            ////添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
            //if (Request.ServerVariables["HTTP_Referer"] == null || Request.ServerVariables["REMOTE_HOST"] == null || Request.ServerVariables["SERVER_NAME"] != Request.Url.Host)
            //{
            //    Response.Write("非法访问!");
            //    Response.End();
            //    return;
            //}
        }
    }
}

