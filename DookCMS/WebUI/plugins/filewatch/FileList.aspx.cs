using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;


/// <summary>
/// 总共有两个参数:root,filetype,
/// </summary>
public partial class FW_FileList : Page
{
    private string root = "~/upload";
    private string filetype = "jpg,gif,js,html,htm,aspx,png,css,swf,zip,rar";
    private static string orderstr = "";
    private string curdir = "";
    private bool canedit, candelete, canrename, canselect;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["root"]))
        {
            root = Request.QueryString["root"];
        }
        if (!string.IsNullOrEmpty(Request.QueryString["filetype"]))
        {
            filetype = Request.QueryString["filetype"];
        }
        if (!IsPostBack)
        {
            litercopyright.Text = "版权所有,试用版请勿用于商业用途!38809972@qq.com";
            //绑定目录

            hidfilepath.Value = root;
            if (!string.IsNullOrEmpty(Request.QueryString["curdir"]))
            {
                hidfilepath.Value = Request.QueryString["curdir"];
            }

            BindData(hidfilepath.Value);
            BindTree();
        }
    }

    private void BindTree()
    {
        treeMenu.Nodes.Clear();
        int i = hidfilepath.Value.LastIndexOf("/");
        string foldername = hidfilepath.Value.Remove(0, i + 1);
        TreeNode topnode = new TreeNode(foldername);
        topnode.Value = hidfilepath.Value;
        treeMenu.Nodes.Add(topnode);
        topnode.Expand();
        if (!Directory.Exists(Server.MapPath(hidfilepath.Value)))
        {
            Directory.CreateDirectory(Server.MapPath(hidfilepath.Value));
        }
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath(hidfilepath.Value));
        foreach (var item in dir.GetDirectories())
        {
            if (!item.Attributes.ToString().Contains("Hidden"))
            {
                TreeNode myNode = new TreeNode(item.Name);
                myNode.Value = hidfilepath.Value + "/" + item.Name;
                myNode.ToolTip = myNode.Value;
                myNode.PopulateOnDemand = true;
                topnode.ChildNodes.Add(myNode);

                //PopulateTreeView(myNode, item);
            }
        }
    }
    //private void PopulateTreeView(TreeNode parentNode, DirectoryInfo parentFolder)
    //{
    //    foreach (var item in parentFolder.GetDirectories())
    //    {
    //        if (!item.Attributes.ToString().Contains("Hidden"))
    //        {
    //            TreeNode myNode = new TreeNode(item.Name);
    //            myNode.Value = parentNode.Value + "/" + item.Name;
    //            myNode.ToolTip = myNode.Value;
    //            parentNode.ChildNodes.Add(myNode);
    //            PopulateTreeView(myNode, item);
    //        }
    //    }
    //}


    private void BindData(string path)
    {
        path = Server.MapPath(path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        DirectoryInfo dir = new DirectoryInfo(path);
        List<Fileinfo> list = new List<Fileinfo>();

        foreach (var item in dir.GetDirectories())
        {
            if (!item.Attributes.ToString().Contains("Hidden"))
            {
                Fileinfo model = new Fileinfo();
                model.filetype = "folder";
                model.filename = item.Name;
                model.filesize = 0;
                model.updatetime = item.LastWriteTime.ToString();
                model.url = hidfilepath.Value + "/" + item.Name;
                list.Add(model);
            }
        }

        foreach (var item in dir.GetFiles())
        {
            if (!item.Attributes.ToString().Contains("Hidden"))
            {
                Fileinfo model = new Fileinfo();
                model.filetype = item.Extension.Replace(".", "");
                model.filename = item.Name;
                model.filesize = (item.Length / 1024);
                model.updatetime = item.LastWriteTime.ToString();
                model.url = hidfilepath.Value + "/" + item.Name;

                if (filetype.Contains(model.filetype.ToLower()))
                {
                    list.Add(model);
                }
            }
        }
        DataTable dt = List2DataTable(list);
        dt.DefaultView.Sort = orderstr;

        gvList.DataSource = dt.DefaultView;
        gvList.DataBind();

        spalert.InnerText = "";
    }
    private DataTable List2DataTable(List<Fileinfo> list)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("filetype");
        dt.Columns.Add("filename");
        dt.Columns.Add("filesize");
        dt.Columns.Add("updatetime");
        dt.Columns.Add("url");
        dt.Columns[2].DataType = typeof(long);
        foreach (var item in list)
        {
            DataRow dr = dt.NewRow();
            dr["filetype"] = item.filetype;
            dr["filename"] = item.filename;
            dr["filesize"] = item.filesize;
            dr["updatetime"] = item.updatetime;
            dr["url"] = item.url;
            dt.Rows.Add(dr);
        }

        return dt;
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
        if (string.Equals(hidfilepath.Value.ToUpper(), root.ToUpper()) == false)
        {
            hidfilepath.Value = hidfilepath.Value.Remove(hidfilepath.Value.LastIndexOf('/'), hidfilepath.Value.Length - hidfilepath.Value.LastIndexOf('/'));
            BindData(hidfilepath.Value);
            BindTree();
            BindSelectNode();
        }
        else
        {
            spalert.InnerText = "已经到达顶端!";
        }
    }

    public string GetFileNameStr(string filetype, string filename)
    {
        //主要处理图片可以提示效果    
        string str = "";
        switch (filetype)
        {
            case "folder":
                str = "";
                break;
            case "png":
            case "jpg":
            case "gif":
            case "jpeg":
            case "bmp":
                str = string.Format("<a class='vtip' title=\"<img src='" + (hidfilepath.Value + "/" + filename).Replace("~", "") + "' />\" target=\"_blank\" href=\"{0}\">{1}</a>", (hidfilepath.Value + "/" + filename).Replace("~", ""), filename);
                break;
            default:
                str = filename;
                break;
        }
        return str;
    }
    protected void treeMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        //Common.MessageBox.Show(this, treeMenu.SelectedNode.Value);
        hidfilepath.Value = treeMenu.SelectedNode.Value;
        BindData(treeMenu.SelectedNode.Value);
        treeMenu.CollapseAll();
        FindTopNode(treeMenu.SelectedNode);
        treeMenu.SelectedNode.Expand();
    }

    #region 获取根节点
    private TreeNode FindTopNode(TreeNode n)
    {
        if (n.Parent != null)
        {
            n.Parent.Expand();
            return FindTopNode(n.Parent);
        }
        else
        {
            n.Expand();
            return n;
        }
    }
    #endregion


    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "open"://打开文件夹
                {
                    if (!e.CommandArgument.ToString().Contains("."))
                    {
                        hidfilepath.Value = e.CommandArgument.ToString();
                        BindData(e.CommandArgument.ToString());
                        BindTree();
                        //BindSelectNode();
                    }
                    break;
                }

            case "delete":
                {
                    try
                    {
                        if (!e.CommandArgument.ToString().Contains("."))//删除文件夹
                        {
                            Common.FileHelper.DeleteFolder(Server.MapPath(e.CommandArgument.ToString()));
                        }
                        else//删除文件
                        {
                            Common.FileHelper.FileDel(Server.MapPath(e.CommandArgument.ToString()));
                        }
                        BindData(hidfilepath.Value);
                        BindTree();
                        BindSelectNode();
                    }
                    catch (Exception ex)
                    {
                        spalert.InnerText = "错误! " + ex.Message;
                    }
                    break;
                }
        }
    }

    private void BindSelectNode()
    {
        TreeNode selnode = null;
        foreach (TreeNode item in treeMenu.Nodes)
        {
            selnode = FindNodeByValue(item, hidfilepath.Value);
            if (selnode != null)
                break;
        }
        if (selnode != null)
        {
            treeMenu.CollapseAll();
            FindTopNode(selnode);
            selnode.Selected = true;
            selnode.Expand();
        }
    }

    #region 递归查找节点
    private TreeNode FindNodeByValue(TreeNode parent, string value)
    {
        if (parent == null) return null;
        if (parent.Value == value) return parent;
        TreeNode result = null;
        foreach (TreeNode tn in parent.ChildNodes)
        {
            result = FindNodeByValue(tn, value);
            if (result != null) break;
        }
        return result;

    }
    #endregion

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //UploadFile(hidfilepath.Value);
    }

    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    #region 新建文件和文件夹
    protected void btnAddFile_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath(hidfilepath.Value);
        if (txtFileName.Text.Trim() == "")
        {
            spalert.InnerText = "文件名不能为空!";
            return;
        }
        string filename = txtFileName.Text.Trim();
        if (filename.IndexOf('.') <= 0)
        {

            if (!Directory.Exists(path + "/" + filename))
            {
                Directory.CreateDirectory(path + "/" + filename);
            }
            spalert.InnerText = "文件夹已创建";
            BindData(hidfilepath.Value);
            BindTree();
            BindSelectNode();
            return;
        }
        if (filetype.Contains(filename.Substring(filename.IndexOf(".") + 1)) == false)
        {
            spalert.InnerText = "不允许创建该类型文件!";
            return;
        }
        if (!File.Exists(path + "/" + filename))
        {
            using (StreamWriter sw = File.CreateText(path + "/" + filename))
            {
                sw.WriteLine("/*新文件建立于 " + DateTime.Now + "*/");
            }
            spalert.InnerText = "文件创建成功!";
            BindData(hidfilepath.Value);
        }
        else
        {
            spalert.InnerText = "文件已存在";
        }
    }
    #endregion

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData(hidfilepath.Value);
        BindTree();
        BindSelectNode();
    }
    private List<string> GetSelFiles()//选择文件列表
    {
        List<string> list = new List<string>();
        foreach (GridViewRow item in gvList.Rows)
        {
            HtmlInputCheckBox chk = (HtmlInputCheckBox)item.FindControl("chkFile");
            if (chk.Checked)
            {
                list.Add(chk.Value);
            }
        }
        return list;
    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        List<string> list = GetSelFiles();
        if (list.Count > 0)
        {
            Session["fw_cut_clipbook_files"] = null;
            Session["fw_copy_clipbook_files"] = list;
            spalert.InnerText = "选择了" + list.Count + "个对象";
            btnPaste.Enabled = true;
        }
        else
        {
            spalert.InnerText = "你没有选择任何文件";
        }
    }
    protected void btnCut_Click(object sender, EventArgs e)
    {
        List<string> list = GetSelFiles();
        if (list.Count > 0)
        {
            Session["fw_copy_clipbook_files"] = null;
            Session["fw_cut_clipbook_files"] = list;
            btnPaste.Enabled = true;
        }
        else
        {
            spalert.InnerText = "你没有选择任何文件";
        }
    }
    protected void btnPaste_Click(object sender, EventArgs e)
    {
        try
        {
            //复制
            if (Session["fw_copy_clipbook_files"] != null)
            {
                spalert.InnerText = "正在粘贴复制的文件,请稍候...";
                List<string> list = Session["fw_copy_clipbook_files"] as List<string>;
                foreach (var item in list)
                {
                    if (item.Contains("."))
                    {
                        //文件
                        int i = item.LastIndexOf("/");
                        string dir = item.Remove(i, item.Length - i);
                        string filename = item.Remove(0, i + 1);
                        if (dir == hidfilepath.Value)//如果是当前文件夹
                        {
                            filename = "复件_" + filename;
                        }
                        File.Copy(Server.MapPath(item), Server.MapPath(hidfilepath.Value + "/" + filename), true);
                    }
                    else //文件夹
                    {
                        int i = item.LastIndexOf("/");
                        string dir = item.Remove(i, item.Length - i);
                        string foldername = item.Remove(0, i + 1);
                        if (dir == hidfilepath.Value)
                        {
                            foldername = "复件_" + foldername;
                        }
                        if (hidfilepath.Value.Contains(item))
                        {
                            spalert.InnerText = "复制的文件夹不能包含在本身的子文件夹中!";
                            return;
                        }
                        Common.FileHelper.CopyDir(Server.MapPath(item), Server.MapPath(hidfilepath.Value + "/" + foldername));

                    }
                }
                spalert.InnerText = "复制完毕!";
                Session["fw_copy_clipbook_files"] = null;

                //更新列表
                btnPaste.Enabled = false;
                BindData(hidfilepath.Value);
                BindTree();
                BindSelectNode();
            }
            else if (Session["fw_cut_clipbook_files"] != null)
            {
                spalert.InnerText = "正在粘贴剪切的文件,请稍候...";
                List<string> list = Session["fw_cut_clipbook_files"] as List<string>;
                foreach (var item in list)
                {
                    if (item.Contains("."))
                    {
                        //文件
                        int i = item.LastIndexOf("/");
                        string dir = item.Remove(i, item.Length - i);
                        string filename = item.Remove(0, i + 1);
                        if (dir != hidfilepath.Value)//如果是当前文件夹
                        {
                            File.Move(Server.MapPath(item), Server.MapPath(hidfilepath.Value + "/" + filename));
                        }
                    }
                    else //文件夹
                    {
                        int i = item.LastIndexOf("/");
                        string dir = item.Remove(i, item.Length - i);
                        string foldername = item.Remove(0, i + 1);
                        if (dir == hidfilepath.Value)
                        {
                            foldername = "复件_" + foldername;
                        }
                        if (hidfilepath.Value.Contains(item))
                        {
                            spalert.InnerText = "剪切的文件夹不能包含在本身的子文件夹中!";
                            return;
                        }
                        Common.FileHelper.CopyDir(Server.MapPath(item), Server.MapPath(hidfilepath.Value + "/" + foldername));
                        Common.FileHelper.DeleteFolder(Server.MapPath(item));
                    }
                }
                spalert.InnerText = "操作完毕!";
                Session["fw_cut_clipbook_files"] = null;
                btnPaste.Enabled = false;
                BindData(hidfilepath.Value);
                BindTree();
                BindSelectNode();
            }
        }
        catch (Exception ex)
        {
            spalert.InnerText = "操作失败,可能有文件正在使用!";
            return;
        }
    }
    protected void btnBatDel_Click(object sender, EventArgs e)
    {
        List<string> list = GetSelFiles();
        if (list.Count > 0)
        {
            spalert.InnerText = "正在删除文件,请稍候...";
            try
            {
                foreach (var item in list)
                {
                    if (item.Contains("."))
                    {
                        File.Delete(Server.MapPath(item));
                    }
                    else
                    {
                        Common.FileHelper.DeleteFolder(Server.MapPath(item));
                    }
                }
                spalert.InnerText = "删除成功!";
                //更新列表
                BindData(hidfilepath.Value);
                BindTree();
                BindSelectNode();

            }
            catch (Exception ex)
            {
                spalert.InnerText = "错误, " + ex.Message;
            }
        }
        else
        {
            spalert.InnerText = "你没有选择任何文件";
        }
    }
    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            orderstr = sortExpression + " ASC";
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            orderstr = sortExpression + " DESC";
        }
        BindData(hidfilepath.Value);
    }
    /// <summary>
    /// 排序方向属性
    /// </summary>
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;
            return (SortDirection)ViewState["sortDirection"];
        }
        set
        {
            ViewState["sortDirection"] = value;
        }
    }
    protected void btnRefreshit_Click(object sender, ImageClickEventArgs e)
    {
        BindData(hidfilepath.Value);
    }
}

[Serializable]
public class Fileinfo
{
    public string filetype { get; set; }
    public string filename { get; set; }
    public long filesize { get; set; }
    public string updatetime { get; set; }
    public string url { get; set; }
}
