using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class FW_Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string descfolder = Server.HtmlDecode(Request.QueryString["path"]);
        UploadFile(descfolder);
    }

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
            if (Request.ServerVariables["HTTP_Referer"] == null || Request.ServerVariables["REMOTE_HOST"] == null || Request.ServerVariables["SERVER_NAME"]!=Request.Url.Host)
            {
                Response.Write("非法访问!");
                Response.End();
                return;
            }    
        }
    }

    #region 上传文件
    private string UploadFile(string descfolder)
    {
        if (descfolder == "")
        {
            lbltips.Text = "参数错误!";
            return "";
        }
        string filename = "";
        Boolean fileOk = false;
        string path = Server.MapPath(descfolder);
        //判断是否已经选取文件
        if (fileUpload.HasFile)
        {
            //取得文件的扩展名,并转换成小写
            string fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
            //限定只能上传jpg和gif图片
            filename = fileUpload.FileName;
            string[] allowExtension = { ".zip", ".jpg", ".gif", ".js", ".html", ".htm", ".aspx", ".png", ".css", ".swf", ".zip", ".rar",".txt",".doc",".docx",".xsl",".xslx" };
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
                    fileUpload.PostedFile.SaveAs(path + "/" + filename);
                    lbltips.Text = "上传成功!";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "Finish()", true);

                }
                catch (Exception ex)
                {                   
                    lbltips.Text = "上传失败! "+ex.Message;

                }
            }
            else
            {
                lbltips.Text = "文件类型或者文件大小超出10Ｍ!";
            }

        }
        else
        {
            lbltips.Text = "请选择文件!";
        }
        return filename;
    }
    #endregion
}
