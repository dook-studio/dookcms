using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Crm_TemplateFileList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string root = "~/template";
            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                root += Request.QueryString["path"];

            }
            string path = Server.MapPath(root);

            DirectoryInfo dir = new DirectoryInfo(path);
            List<Dukey.Model.Fileinfo> list = new List<Dukey.Model.Fileinfo>();

            foreach (var item in dir.GetDirectories())
            {
                if (!item.Attributes.ToString().Contains("Hidden"))
                {
                    Dukey.Model.Fileinfo model = new Dukey.Model.Fileinfo();
                    model.filetype = "folder";
                    model.filename = item.Name;
                    model.filesize = (getSize(item.FullName) / 1024).ToString() + " K";
                    model.updatetime = item.LastWriteTime.ToString();
                    list.Add(model);
                }
            }

            foreach (var item in dir.GetFiles())
            {
                if (!item.Attributes.ToString().Contains("Hidden"))
                {
                    Dukey.Model.Fileinfo model = new Dukey.Model.Fileinfo();
                    model.filetype = item.Extension.Replace(".", "");
                    model.filename = item.Name;
                    model.filesize = (item.Length / 1024).ToString() + " K";
                    model.updatetime = item.LastWriteTime.ToString();
                    list.Add(model);
                }
            }

            gvList.DataSource = list;
            gvList.DataBind();

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



    protected void upbtn_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["path"]))
        {
            string path = Request.QueryString["path"];
            path = path.Remove(path.LastIndexOf("/"));
            Response.Redirect("templatefilelist.aspx?path=" + path);
        }

    }
    protected void btnAddFile_Click(object sender, EventArgs e)
    {
        if (txtFileName.Text.Trim() == "")
        {
            spalert.InnerText = "文件名不能为空!";
            return;
        }
        string root = "~/template";
        if (!string.IsNullOrEmpty(Request.QueryString["path"]))
        {
            root += Request.QueryString["path"];
        }
        string path = Server.MapPath(root);
        string filename = txtFileName.Text.Trim();
        if (filename.IndexOf('.') <= 0)
        {

            if (!Directory.Exists(path + "/" + filename))
            {
                Directory.CreateDirectory(path + "/" + filename);
            }
            spalert.InnerText = "文件夹已创建";
            Response.Redirect(Request.Url.ToString());
            return;
        }
        //Response.Write(path + "\\" + txtFileName.Text.Trim());
        if (!File.Exists(path + "/" + filename))
        {
            using (StreamWriter sw = File.CreateText(path + "/" + filename))
            {
                sw.WriteLine("/*新文件建立于 " + DateTime.Now + "*/");
            }
            Response.Redirect(Request.Url.ToString());
        }
        else
        {
            spalert.InnerText = "文件已存在";
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (hidfilepath.Value != "")
        {
            try
            {
                string root = "~/template";
                if (!string.IsNullOrEmpty(Request.QueryString["path"]))
                {
                    root += Request.QueryString["path"];
                }
                root += "/" + hidfilepath.Value;
                string path = Server.MapPath(root);
                if (path.IndexOf('.') <= 0)
                {
                    Common.FileHelper.DeleteFolder(path);
                }
                else
                {
                    File.Delete(path);
                }
                Response.Redirect(Request.Url.ToString());
                //Common.MessageBox.ShowAndRedirect(this, "操作成功!", Request.Url.ToString());              
            }
            catch
            {

            }
        }
    }

    public string GetFileNameStr(string filetype, string filename)
    {
        //主要处理图片可以提示效果
        string path = Request.QueryString["path"];
        string fromid = Request.QueryString["fromid"];
        string type = Request.QueryString["filetype"];
        string str = "";
        switch (filetype)
        {
            case "folder":
                str = "<a href=\"?path=" + path + "/" + filename + "&fromid=" + fromid + "&filetype=" + type + "\">" + filename + "</a>";
                break;
            case "png":
            case "jpg":
            case "gif":
            case "jpeg":
            case "bmp":
                str = "<a class='tipsimg' target=_blank  href=\"/template" + path + "/" + filename + "?"+DateTime.Now.Ticks.ToString()+"\">" + filename + "</a>";
                break;
            default:
                str = filename;
                break;

        }
        return str;
    }
    public string GetOpStr(string filetype, string filename)
    {
        string path = Request.QueryString["path"];
        string fromid = Request.QueryString["fromid"];
        string type = Request.QueryString["filetype"];
        string str = "";
        switch (filetype)
        {
            case "folder":
                str = string.Format(literFolder.Text, path+"/"+filename, fromid, type, filename,filetype);
                break;
            case "png":
            case "jpg":
            case "gif":
            case "jpeg":
            case "bmp":
                str = string.Format(literImg.Text, filename, filetype, path + "/" + filename);
                break;
            default:
                str = string.Format(literOther.Text, filename, filetype, path + "/" + filename);
                break;

        }
        return str;
    }
}
