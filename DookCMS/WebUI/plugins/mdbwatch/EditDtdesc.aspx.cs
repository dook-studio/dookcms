using System;
using System.Web.UI;
using MdbWatch;
using System.Collections;

public partial class Mdbw_EditDtdesc : BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbdesc.Value = Request["desc"];           
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();
        hs.Add("tbdesc", StringHelper.ReplaceBadChar(tbdesc.Value.Trim()));
        BLL.DataBaseHelper.instance.Update(hs, "tbnote", "tbname='" + Request["tbname"] + "'");
        
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "Finish()", true);
        
    }
 
    protected override void OnInit(EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
        //    if (Request.ServerVariables["HTTP_Referer"] == null || Request.ServerVariables["REMOTE_HOST"] == null || Request.ServerVariables["SERVER_NAME"] != Request.Url.Host)
        //    {
        //        Response.Write("非法访问!");
        //        Response.End();
        //        return;
        //    }
        //}
    }

}
