using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Collections;

public partial class Plugins_mdbwatch_EditData : BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string tbname = Request.QueryString["tbname"];
            string tbcol = Request.QueryString["colname"];
            string pk = Request.QueryString["pk"];
            string pkvalue = Request.QueryString["pkvalue"];
            try
            {
                
                txtData.Value = BLL.DataBaseHelper.instance.GetColumnValue(tbname, tbcol, pk + "=" + pkvalue).ToString();

                alink.HRef = "EditBigData.aspx?tbname=" + tbname + "&colname=" + tbcol + "&pk=" + pk + "&pkvalue=" + pkvalue;
            }
            catch { }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string tbname = Request.QueryString["tbname"];
            string tbcol = Request.QueryString["colname"];
            string pk = Request.QueryString["pk"];
            string pkvalue = Request.QueryString["pkvalue"];
       
            Hashtable hs = new Hashtable();
            hs.Add("colvalue", txtData.Value.Trim());

            string sql = string.Format("update {0} set {1}=@colvalue where {2}='{3}'", tbname, tbcol, pk,pkvalue);
            BLL.DataBaseHelper.instance.ExecuteSql(sql, hs);
            //MdbWatch.DbHelper.ExecuteSql(sql, paras);
            lblResult.Text = "操作成功!";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "", "Finish()", true);
        }
        catch (Exception ex)
        {
            lblResult.Text = ex.Message.ToString();
        }
    }
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
            if (Request.ServerVariables["HTTP_Referer"] == null || Request.ServerVariables["REMOTE_HOST"] == null || Request.ServerVariables["SERVER_NAME"] != Request.Url.Host)
            {
                Response.Write("非法访问!");
                Response.End();
                return;
            }
        }
    }
}
