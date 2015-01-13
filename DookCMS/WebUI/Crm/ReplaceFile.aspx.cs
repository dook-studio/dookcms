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

public partial class Crm_ReplaceFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["fn"]))
        {
            return;
        }
        string path=Request.QueryString["fn"];
        try
        {
            string filetype=Request.QueryString["filetype"];
            if (FileUpload1.FileName.EndsWith(filetype))
            {
                if (chkbackup.Checked)
                {
                    Common.FileHelper.FileMove(Server.MapPath(path), Server.MapPath(path + ".bak"));
                }
                else
                {
                    Common.FileHelper.FileDel(Server.MapPath(path));
                }
                FileUpload1.SaveAs(Server.MapPath(path));
                ClientScript.RegisterClientScriptBlock(this.GetType(), "closef", "<script>window.parent.pop.close();</script>");
            }
            else
            {
                Common.MessageBox.Show(this, "文件类型不正确");
            }
        }
        catch(Exception ex)
        {
            Common.MessageBox.Show(this, ex.Message);
        }
    }
}
