using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class Crm_DataBaseHelper : BaseCrm
{
    public int fid = 0;
    private static string dal = ConfigurationManager.AppSettings["DAL"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string filetype = "bak";
            if (!string.IsNullOrEmpty(Request.QueryString["filetype"]))
            {
                filetype = Request.QueryString["filetype"];
            }

            string root = "~/backup";
            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                root += Request.QueryString["path"];

            }
            string path = Server.MapPath(root);

            DirectoryInfo dir = new DirectoryInfo(path);
            List<Dukey.Model.Fileinfo> list = new List<Dukey.Model.Fileinfo>();

            foreach (var item in dir.GetDirectories())
            {
                Dukey.Model.Fileinfo model = new Dukey.Model.Fileinfo();
                model.filetype = "folder";
                model.filename = item.Name;
                model.filesize = (getSize(item.FullName) / 1024).ToString() + " K";
                model.updatetime = item.LastWriteTime.ToString();

                list.Add(model);

            }

            foreach (var item in dir.GetFiles())
            {
                Dukey.Model.Fileinfo model = new Dukey.Model.Fileinfo();
                model.filetype = item.Extension.Replace(".", "");
                model.filename = item.Name;
                model.filesize = (item.Length / 1024).ToString() + " K";
                model.updatetime = item.LastWriteTime.ToString();

                if (filetype.Contains(model.filetype.ToLower()))
                {
                    list.Add(model);
                }
            }

            gvList.DataSource = list;
            gvList.DataBind();

            if (dal.ToLower() == "SQLServerDAL".ToLower())//sqlserver数据库
            {
                btnCompress.Visible = false;
            }
        }
    }

    public static long getSize(string path)
    {
        if (!System.IO.Directory.Exists(path))
        {
            return 0;
        }
        else
        {
            long size = 0;
            System.IO.DirectoryInfo DI = new System.IO.DirectoryInfo(path);
            foreach (System.IO.FileInfo fi in DI.GetFiles())
            {
                size += fi.Length;
            }
            foreach (System.IO.DirectoryInfo di in DI.GetDirectories())
            {
                size += getSize(di.FullName);
            }
            return size;
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (dal.ToLower() == "SQLServerDAL".ToLower())
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["strWeb"].ConnectionString);
                Common.DataBaseBackUpHelper.SQLBACK(conn.DataSource, "landswift", "1234", conn.Database, Server.MapPath("~/backup/db" + DateTime.Now.ToString("yy年MM月dd日HH时mm分ssfff") + ".bak"));
            }
            else
            {
                Common.DataBaseBackUpHelper.Backup(Server.MapPath("<%$ConnectionStrings:AccessDbPath  %>"), Server.MapPath("~/backup/db" + DateTime.Now.ToString("yy年MM月dd日HH时mm分ssfff") + ".bak"));
            }
            Common.MessageBox.ShowAndRedirect(this, "操作成功!", Request.Url.ToString());
        }
        catch (Exception ex)
        {
            Common.MessageBox.Show(this, "备份错误!");
            lblTips.Text = ex.Message;
        }
    }

    #region 删除备份文件
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (hidfilepath.Value != "")
        {
            try
            {
                string root = "~/backup";
                if (!string.IsNullOrEmpty(Request.QueryString["path"]))
                {
                    root += Request.QueryString["path"];
                }
                root += "/" + hidfilepath.Value;
                string path = Server.MapPath(root);
                File.Delete(path);
                Response.Redirect(Request.Url.ToString());
            }
            catch
            {

            }
        }
    }
    #endregion

    #region 压缩数据库
    protected void btnCompress_Click(object sender, EventArgs e)
    {
        try
        {
            Common.DataBaseBackUpHelper.CompactAccess(Server.MapPath("<%$ConnectionStrings:AccessDbPath  %>"));
            Common.MessageBox.ShowAndRedirect(this, "操作成功!", Request.Url.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


    protected void btnRestore_Click(object sender, EventArgs e)
    {
        if (hidfilepath.Value != "")
        {
            try
            {
                string root = "~/backup";
                if (!string.IsNullOrEmpty(Request.QueryString["path"]))
                {
                    root += Request.QueryString["path"];
                }
                root += "/" + hidfilepath.Value;
                string path = Server.MapPath(root);
                Common.DataBaseBackUpHelper.RecoverAccess(path, Server.MapPath("<%$ConnectionStrings:AccessDbPath  %>"));
                Common.MessageBox.ShowAndRedirect(this, "操作成功!", Request.Url.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
