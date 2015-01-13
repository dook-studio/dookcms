using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using BLL;
using System.Collections;


public partial class Crm_EditTemplate : BaseCrm
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (StringHelper.isNum(Request.QueryString["id"]))//如果是更新数据,则加载该记录.
            {
                int id = int.Parse(Request.QueryString["id"]);
                DataTable dt = DataBaseHelper.instance.GetModel("template", "*", "id=" + id);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtcname.Text = dt.Rows[0]["cname"].ToString();
                    txtename.Text = dt.Rows[0]["ename"].ToString();
                    txtremark.Text = dt.Rows[0]["remark"].ToString();
                    txtfolder.Text = dt.Rows[0]["folder"].ToString();
                    lbltitle.Text = "修改";
                }
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //上传文件
        UploadFile(StringHelper.ReplaceBadChar(txtfolder.Text));
        Hashtable hs = new Hashtable();
        hs.Add("cname", StringHelper.ReplaceBadChar(txtcname.Text));
        hs.Add("ename", StringHelper.ReplaceBadChar(txtename.Text));
        hs.Add("remark", StringHelper.ReplaceBadChar(txtremark.Text));
        hs.Add("folder", StringHelper.ReplaceBadChar(txtfolder.Text));
        hs.Add("coverimg", txtCoverImg.Text);

        if (!StringHelper.isNum(Request.QueryString["id"]))//插入新纪录
        {
            DataBaseHelper.instance.Insert(hs, "Template");
            if (!Directory.Exists(Server.MapPath("~/templets/" + StringHelper.ReplaceBadChar(txtfolder.Text))))
            {
                Directory.CreateDirectory(Server.MapPath("~/templets/" + StringHelper.ReplaceBadChar(txtfolder.Text)));
            }
        }
        else//更新纪录
        {
            string id = Request.QueryString["id"];
            DataBaseHelper.instance.Update(hs, "Template", "[ID]=" + id);
        }
        Common.MessageBox.ShowAndRedirect(this, "操作成功!", "list.aspx");
    }

    #region 上传文件
    private string UploadFile(string filename)
    {

        Boolean fileOk = false;
        string path = Server.MapPath("~/upload/templets/");
        string httpPath = "~/upload/templets/";
        //判断是否已经选取文件
        if (fileUpload.HasFile)
        {
            //取得文件的扩展名,并转换成小写
            string fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
            //限定只能上传jpg和gif图片
            string[] allowExtension = { ".zip" };
            //对上传的文件的类型进行一个个匹对
            for (int i = 0; i < allowExtension.Length; i++)
            {
                if (fileExtension == allowExtension[i])
                {
                    fileOk = true;
                    break;
                }
            }
            //对上传文件的大小进行检测，限定文件最大不超过1M
            if (fileUpload.PostedFile.ContentLength > 10240000)
            {
                fileOk = false;
            }
            //最后的结果
            if (fileOk)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fileUpload.PostedFile.SaveAs(path + filename + ".zip");

                    //解压到指定目录                   
                    Common.ZipFileHelper.UnZipFile(path + filename + ".zip", Server.MapPath("~/templets/" + filename));

                    Response.Write("<script type=‘text/javascript‘>window.alert(‘上传成功‘)</script>");

                }
                catch (Exception ex)
                {
                    throw ex;

                    Response.Write("<script type=‘text/javascript‘>window.alert(‘上传失败‘)</script>");
                }
            }
            else
            {
                Response.Write("<script type=‘text/javascript‘>window.alert(‘文件类型或者文件大小超出１Ｍ‘)</script>");
            }

        }
        return filename;
    }
    #endregion



    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("editproduct.aspx");
    //}


    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
}
